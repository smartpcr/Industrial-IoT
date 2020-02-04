// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------

namespace Microsoft.Azure.IIoT.Serializers {
    using Microsoft.Azure.IIoT.Exceptions;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Collections.Generic;
    using System.Dynamic;
    using System.IO;
    using System.Linq;
    using System.Linq.Expressions;

    /// <summary>
    /// Newtonsoft json serializer
    /// </summary>
    public class NewtonSoftJsonSerializer : IJsonSerializer {

        /// <summary>
        /// Create serializer
        /// </summary>
        /// <param name="config"></param>
        public NewtonSoftJsonSerializer(IJsonSerializerSettingsProvider config = null) {
            _config = config ?? new NewtonSoftJsonConverters();
        }

        /// <inheritdoc/>
        public object Deserialize(TextReader reader, Type type) {
            try {
                var jsonSerializer = JsonSerializer.CreateDefault(_config.GetSettings());
                return jsonSerializer.Deserialize(reader, type);
            }
            catch (JsonReaderException ex) {
                throw new SerializerException(ex.Message, ex);
            }
        }

        /// <inheritdoc/>
        public void Serialize(TextWriter writer, object o, Formatting format) {
            try {
                var jsonSerializer = CreateSerializer();
                jsonSerializer.Formatting = format == Formatting.Indented ?
                    Newtonsoft.Json.Formatting.Indented : Newtonsoft.Json.Formatting.None;
                jsonSerializer.Serialize(writer, o);
            }
            catch (JsonReaderException ex) {
                throw new SerializerException(ex.Message, ex);
            }
        }

        /// <inheritdoc/>
        public object ToObject(VariantValue token, Type type) {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public VariantValue FromObject(object o) {
            return new JsonVariantValue(JToken.FromObject(o), this);
        }

        /// <inheritdoc/>
        public VariantValue Parse(TextReader reader) {
            using (var jsonReader = new JsonTextReader(reader)) {
                var token = JToken.Load(jsonReader);
                return new JsonVariantValue(token, this);
            }
        }

        /// <summary>
        /// Create serializer
        /// </summary>
        /// <returns></returns>
        private JsonSerializer CreateSerializer() {
            var settings = _config.GetSettings();
            settings.Converters.Add(new JsonVariantConverter(this));
            return JsonSerializer.CreateDefault(settings);
        }

        /// <summary>
        /// Token wrapper
        /// </summary>
        private class JsonVariantValue : VariantValue {

            /// <summary>
            /// The wrapped token
            /// </summary>
            internal JToken Token { get; set; }

            /// <summary>
            /// Create value
            /// </summary>
            /// <param name="token"></param>
            /// <param name="serializer"></param>
            public JsonVariantValue(JToken token, NewtonSoftJsonSerializer serializer) {
                Token = token ?? JValue.CreateNull();
                _serializer = serializer;
            }

            /// <inheritdoc/>
            public override VariantValueType Type {
                get {
                    switch (Token.Type) {
                        case JTokenType.Object:
                            return VariantValueType.Object;
                        case JTokenType.Array:
                            return VariantValueType.Array;
                        case JTokenType.Integer:
                            return VariantValueType.Integer;
                        case JTokenType.Float:
                            return VariantValueType.Float;
                        case JTokenType.Date:
                            return VariantValueType.Date;
                        case JTokenType.Raw:
                        case JTokenType.Bytes:
                        case JTokenType.Guid:
                        case JTokenType.Uri:
                        case JTokenType.TimeSpan:
                        case JTokenType.String:
                            return VariantValueType.String;
                        case JTokenType.Boolean:
                            return VariantValueType.Boolean;
                        case JTokenType.Null:
                            return VariantValueType.Null;
                        default:
                            return VariantValueType.Undefined;
                    }
                }
            }

            /// <inheritdoc/>
            public override object Value => Token;

            /// <inheritdoc/>
            public override IEnumerable<string> Keys {
                get {
                    if (Token is JObject o) {
                        return o.Properties().Select(p => p.Name);
                    }
                    throw new NotSupportedException(
                        "Variant is not an object.");
                }
            }

            /// <inheritdoc/>
            public override int Count {
                get {
                    if (Token is JArray array) {
                        return array.Count;
                    }
                    throw new NotSupportedException(
                        "Variant is not an array.");
                }
            }

            /// <inheritdoc/>
            public override VariantValue Copy(bool shallow) {
                return new JsonVariantValue(shallow ? Token :
                    Token.DeepClone(), _serializer);
            }

            /// <inheritdoc/>
            public override object ToType(Type type, IFormatProvider provider) {
                return Token.ToObject(type);
            }

            /// <inheritdoc/>
            public override IEnumerator<VariantValue> GetEnumerator() {
                if (Token is JArray array) {
                    return array.Select(i => new JsonVariantValue(i, _serializer)).GetEnumerator();
                }
                throw new NotSupportedException("Variant is not an array.");
            }

            /// <inheritdoc/>
            public override VariantValue SelectToken(string path) {
                var selected = Token.SelectToken(path);
                return new JsonVariantValue(selected, _serializer);
            }

            /// <inheritdoc/>
            public override DynamicMetaObject GetMetaObject(Expression parameter) {
                return ((IDynamicMetaObjectProvider)Token).GetMetaObject(parameter);
            }

            /// <inheritdoc/>
            public override void Set(object value) {
                Token = JToken.FromObject(value);
            }

            /// <inheritdoc/>
            public override string ToString(Formatting format) {
                return Token.ToString(format == Formatting.Indented ?
                    Newtonsoft.Json.Formatting.Indented : Newtonsoft.Json.Formatting.None);
            }

            /// <inheritdoc/>
            public override bool TryGetValue(string key, out VariantValue value,
                StringComparison compare) {
                if (Token is JObject o) {
                    var success = o.TryGetValue(key, compare, out var token);
                    if (success) {
                        value = new JsonVariantValue(token, _serializer);
                        return true;
                    }
                }
                value = new JsonVariantValue(null, _serializer);
                return false;
            }

            /// <inheritdoc/>
            public override bool TryGetValue(int index, out VariantValue value) {
                if (index >= 0 && Token is JArray o && index < o.Count) {
                    value = new JsonVariantValue(o[index], _serializer);
                    return true;
                }
                value = new JsonVariantValue(null, _serializer);
                return false;
            }

            /// <inheritdoc/>
            protected override int GetDeepHashCode() {
                return JToken.EqualityComparer.GetHashCode(Token);
            }

            /// <inheritdoc/>
            protected override VariantValue Null() {
                return new JsonVariantValue(null, _serializer);
            }

            /// <inheritdoc/>
            protected override bool DeepEquals(object o) {
                return JToken.DeepEquals(Token, o is JToken t ? t :
                    JToken.FromObject(o));
            }

            private readonly NewtonSoftJsonSerializer _serializer;
        }

        /// <summary>
        /// Json veriant converter
        /// </summary>
        private sealed class JsonVariantConverter : JsonConverter {

            /// <summary>
            /// Converter
            /// </summary>
            /// <param name="serializer"></param>
            public JsonVariantConverter(NewtonSoftJsonSerializer serializer) {
                _serializer = serializer;
            }

            /// <inheritdoc/>
            public override void WriteJson(JsonWriter writer, object value,
                JsonSerializer serializer) {
                if (value is JsonVariantValue json) {
                    json.Token.WriteTo(writer, serializer.Converters.ToArray());
                    return;
                }
                if (!(value is global::Microsoft.Azure.IIoT.Serializers.VariantValue variant)) {
                    throw new NotSupportedException("Unexpected type passed");
                }
                switch (variant.Type) {
                    case VariantValueType.Null:
                        writer.WriteNull();
                        break;
                    case VariantValueType.Array:
                        writer.WriteStartArray();
                        foreach (var item in variant) {
                            WriteJson(writer, item, serializer);
                        }
                        writer.WriteEndArray();
                        break;
                    case VariantValueType.Object:
                        writer.WriteStartObject();
                        foreach (var key in variant.Keys) {
                            writer.WritePropertyName(key);
                            // Write value
                            WriteJson(writer, variant[key], serializer);
                        }
                        writer.WriteEndObject();
                        break;
                    case VariantValueType.Undefined:
                    case VariantValueType.String:
                        writer.WriteValue(variant.ToString(null));
                        break;
                    case VariantValueType.Integer:
                        writer.WriteRawValue(variant.ToString(null));
                        break;
                    case VariantValueType.Boolean:
                        writer.WriteValue(variant.ToBoolean(null));
                        break;
                    case VariantValueType.Float:
                        writer.WriteValue(variant.ToDecimal(null));
                        break;
                    case VariantValueType.Date:
                        writer.WriteValue(variant.ToDateTime(null));
                        break;
                }
            }

            /// <inheritdoc/>
            public override object ReadJson(JsonReader reader, Type objectType,
                object existingValue, JsonSerializer serializer) {
                // Read variant from json
                return new JsonVariantValue(JToken.Load(reader), _serializer);
            }

            /// <inheritdoc/>
            public override bool CanConvert(Type objectType) {
                return typeof(VariantValue).IsAssignableFrom(objectType);
            }

            private readonly NewtonSoftJsonSerializer _serializer;
        }

        private readonly IJsonSerializerSettingsProvider _config;
    }
}