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
    using System.IO;
    using System.Linq;

    /// <summary>
    /// Newtonsoft json serializer
    /// </summary>
    public class NewtonSoftJsonSerializer : IJsonSerializer {

        /// <summary>
        /// Json serializer settings
        /// </summary>
        public JsonSerializerSettings Settings { get; }

        /// <summary>
        /// Create serializer
        /// </summary>
        /// <param name="config"></param>
        public NewtonSoftJsonSerializer(IJsonSerializerSettingsProvider config = null) {
            var settings = config?.GetSettings() ?? new JsonSerializerSettings();
            settings.Converters.Add(new JsonVariantConverter(this));
            settings.FloatFormatHandling = FloatFormatHandling.String;
            settings.FloatParseHandling = FloatParseHandling.Double;
            settings.DateParseHandling = DateParseHandling.DateTime;
            settings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
            settings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
            if (settings.MaxDepth > 64) {
                settings.MaxDepth = 64;
            }
            Settings = settings;
        }

        /// <inheritdoc/>
        public object Deserialize(TextReader reader, Type type) {
            try {
                var jsonSerializer = JsonSerializer.CreateDefault(Settings);
                return jsonSerializer.Deserialize(reader, type);
            }
            catch (JsonReaderException ex) {
                throw new SerializerException(ex.Message, ex);
            }
        }

        /// <inheritdoc/>
        public void Serialize(TextWriter writer, object o, Formatting format) {
            try {
                var jsonSerializer = JsonSerializer.CreateDefault(Settings);
                jsonSerializer.Formatting = format == Formatting.Indented ?
                    Newtonsoft.Json.Formatting.Indented :
                    Newtonsoft.Json.Formatting.None;
                jsonSerializer.Serialize(writer, o);
            }
            catch (JsonReaderException ex) {
                throw new SerializerException(ex.Message, ex);
            }
        }

        /// <inheritdoc/>
        public VariantValue FromObject(object o) {
            try {
                return new JsonVariantValue(o == null ? JValue.CreateNull() :
                    JToken.FromObject(o, JsonSerializer.CreateDefault(Settings)), this);
            }
            catch (JsonReaderException ex) {
                throw new SerializerException(ex.Message, ex);
            }
        }

        /// <inheritdoc/>
        public VariantValue Parse(TextReader reader) {
            try {
                using (var jsonReader = new JsonTextReader(reader)) {

                    jsonReader.FloatParseHandling = Settings.FloatParseHandling;
                    jsonReader.DateParseHandling = Settings.DateParseHandling;
                    jsonReader.DateTimeZoneHandling = Settings.DateTimeZoneHandling;
                    jsonReader.MaxDepth = Settings.MaxDepth;

                    var token = JToken.Load(jsonReader);
                    return new JsonVariantValue(token, this);
                }
            }
            catch (JsonReaderException ex) {
                throw new SerializerException(ex.Message, ex);
            }
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
                        case JTokenType.Bytes:
                            return VariantValueType.Bytes;
                        case JTokenType.Guid:
                        case JTokenType.Raw:
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
            public override IEnumerable<VariantValue> Values {
                get {
                    if (Token is JArray array) {
                        return array.Select(i => new JsonVariantValue(i, _serializer));
                    }
                    throw new NotSupportedException("Variant is not an array.");
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
                return Token.ToObject(type,
                    JsonSerializer.CreateDefault(_serializer.Settings));
            }

            /// <inheritdoc/>
            public override VariantValue SelectToken(string path) {
                var selected = Token.SelectToken(path);
                return new JsonVariantValue(selected, _serializer);
            }

            /// <inheritdoc/>
            public override void Set(object value) {
                Token = JToken.FromObject(value,
                    JsonSerializer.CreateDefault(_serializer.Settings));
            }

            /// <inheritdoc/>
            public override string ToString(Formatting format) {
                return Token.ToString(format == Formatting.Indented ?
                    Newtonsoft.Json.Formatting.Indented :
                    Newtonsoft.Json.Formatting.None,
                    _serializer.Settings.Converters.ToArray());
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
            protected override bool ValueEquals(object o) {
                // Compare tokens
                if (!(o is JToken t)) {
                    if (FastCompare(o)) {
                        return true;
                    }
                    t = JToken.FromObject(o,
                        JsonSerializer.CreateDefault(_serializer.Settings));
                }
                else {
                    if (ReferenceEquals(t, Token)) {
                        return true;
                    }
                }
                if (!DeepEquals(Token, t)) {
                    return false;
                }
                return true;
            }

            /// <inheritdoc/>
            protected override bool DeepEquals(VariantValue v) {
                return ValueEquals(v.Value);
            }

            /// <summary>
            /// Compare tokens in more consistent fashion
            /// </summary>
            /// <param name="t1"></param>
            /// <param name="t2"></param>
            /// <returns></returns>
            internal bool DeepEquals(JToken t1, JToken t2) {
                if (t1 is null || t2 is null) {
                    return t1 == t2;
                }

                if (t1 is JContainer c1 && t2 is JContainer c2) {
                    // Compare all items - they are per json.net ordered.
                    return c1.Children().SequenceEqual(c2.Children(),
                        Compare.Using<JToken>((x, y) => DeepEquals(x, y)));
                }

                if (t1 is JValue && t2 is JValue) {
                    if (t1.Equals(t2)) {
                        return true;
                    }
                    var s1 = t1.ToString(Newtonsoft.Json.Formatting.None,
                        _serializer.Settings.Converters.ToArray());
                    var s2 = t2.ToString(Newtonsoft.Json.Formatting.None,
                        _serializer.Settings.Converters.ToArray());
                    if (s1 == s2) {
                        return true;
                    }
                }
                return false;
            }

            /// <summary>
            /// Quick compare to object
            /// </summary>
            /// <param name="o"></param>
            /// <returns></returns>
            private bool FastCompare(object o) {
                // Handle special cases
                switch (o) {
                    case byte[] b:
                        return Convert.ToBase64String(b) == Token.ToString();
                }
                return false;
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
                switch (value) {
                    case JsonVariantValue json:
                        json.Token.WriteTo(writer, serializer.Converters.ToArray());
                        break;
                    case VariantValue variant:
                        switch (variant.Type) {
                            case VariantValueType.Null:
                                writer.WriteNull();
                                break;
                            case VariantValueType.Array:
                                writer.WriteStartArray();
                                foreach (var item in variant.Values) {
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
                            default:
                                serializer.Serialize(writer, variant.Value);
                                break;
                        }
                        break;
                    default:
                        throw new NotSupportedException("Unexpected type passed");
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
    }
}