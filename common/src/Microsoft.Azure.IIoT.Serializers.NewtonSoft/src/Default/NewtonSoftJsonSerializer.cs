// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------

namespace Microsoft.Azure.IIoT.Serializers.NewtonSoft {
    using Microsoft.Azure.IIoT.Serializers;
    using Microsoft.Azure.IIoT.Exceptions;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Buffers;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Newtonsoft json serializer
    /// </summary>
    public class NewtonSoftJsonSerializer : IJsonSerializerSettingsProvider, ISerializer {

        /// <inheritdoc/>
        public string MimeType => ContentMimeType.Json;

        /// <inheritdoc/>
        public Encoding ContentEncoding => Encoding.UTF8;

        /// <summary>
        /// Json serializer settings
        /// </summary>
        public JsonSerializerSettings Settings { get; }

        /// <summary>
        /// Create serializer
        /// </summary>
        /// <param name="providers"></param>
        public NewtonSoftJsonSerializer(
            IEnumerable<IJsonSerializerConverterProvider> providers = null) {
            var settings = new JsonSerializerSettings();
            if (providers != null) {
                foreach (var provider in providers) {
                    settings.Converters.AddRange(provider.GetConverters());
                }
            }
            settings.Converters.Add(new JsonVariantConverter(this));
            settings.FloatFormatHandling = FloatFormatHandling.String;
            settings.FloatParseHandling = FloatParseHandling.Double;
            settings.DateParseHandling = DateParseHandling.DateTime;
            settings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
            settings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
            settings.ReferenceLoopHandling = ReferenceLoopHandling.Error;
            if (settings.MaxDepth > 64) {
                settings.MaxDepth = 64;
            }
            Settings = settings;
        }

        /// <inheritdoc/>
        public object Deserialize(ReadOnlyMemory<byte> buffer, Type type) {
            try {
                // TODO move to .net 3 to use readonly span as stream source
                var jsonSerializer = JsonSerializer.CreateDefault(Settings);
                using (var stream = new MemoryStream(buffer.ToArray()))
                using (var reader = new StreamReader(stream, ContentEncoding)) {
                    return jsonSerializer.Deserialize(reader, type);
                }
            }
            catch (JsonReaderException ex) {
                throw new SerializerException(ex.Message, ex);
            }
        }

        /// <inheritdoc/>
        public void Serialize(IBufferWriter<byte> buffer, object o, SerializeOption format) {
            try {
                var jsonSerializer = JsonSerializer.CreateDefault(Settings);
                jsonSerializer.Formatting = format == SerializeOption.Indented ?
                    Formatting.Indented :
                    Formatting.None;
                // TODO move to .net 3 to use buffer writer as stream sink
                using (var stream = new MemoryStream()) {
                    using (var writer = new StreamWriter(stream)) {
                        jsonSerializer.Serialize(writer, o);
                    }
                    var written = stream.ToArray();
                    buffer.Write(written);
                }
            }
            catch (JsonReaderException ex) {
                throw new SerializerException(ex.Message, ex);
            }
        }

        /// <inheritdoc/>
        public VariantValue Parse(ReadOnlyMemory<byte> buffer) {
            try {
                // TODO move to .net 3 to use readonly span as stream source
                using (var stream = new MemoryStream(buffer.ToArray()))
                using (var reader = new StreamReader(stream, ContentEncoding))
                using (var jsonReader = new JsonTextReader(reader)) {

                    jsonReader.FloatParseHandling = Settings.FloatParseHandling;
                    jsonReader.DateParseHandling = Settings.DateParseHandling;
                    jsonReader.DateTimeZoneHandling = Settings.DateTimeZoneHandling;
                    jsonReader.MaxDepth = Settings.MaxDepth;

                    var token = JToken.Load(jsonReader);

                    while (jsonReader.Read()) {
                        // Read to end or throw
                    }
                    return new JsonVariantValue(token, this);
                }
            }
            catch (JsonReaderException ex) {
                throw new SerializerException(ex.Message, ex);
            }
        }

        /// <inheritdoc/>
        public VariantValue FromObject(object o) {
            try {
                return new JsonVariantValue(this, o);
            }
            catch (JsonReaderException ex) {
                throw new SerializerException(ex.Message, ex);
            }
        }

        /// <summary>
        /// Token wrapper
        /// </summary>
        internal class JsonVariantValue : VariantValue {

            /// <summary>
            /// The wrapped token
            /// </summary>
            internal JToken Token { get; set; }

            /// <summary>
            /// Create value
            /// </summary>
            /// <param name="o"></param>
            /// <param name="serializer"></param>
            internal JsonVariantValue(NewtonSoftJsonSerializer serializer, object o) {
                _serializer = serializer;
                Token = o is null ? JValue.CreateNull() : FromObject(o);
            }

            /// <summary>
            /// Create value
            /// </summary>
            /// <param name="token"></param>
            /// <param name="serializer"></param>
            internal JsonVariantValue(JToken token, NewtonSoftJsonSerializer serializer) {
                _serializer = serializer;
                Token = token ?? JValue.CreateNull();
            }

            /// <inheritdoc/>
            public override VariantValueType Type {
                get {
                    switch (Token.Type) {
                        case JTokenType.Object:
                            return VariantValueType.Object;
                        case JTokenType.Array:
                            return VariantValueType.Array;
                        case JTokenType.Bytes:
                            return VariantValueType.Bytes;
                        case JTokenType.Integer:
                            return VariantValueType.Integer;
                        case JTokenType.Float:
                            if (double.IsInfinity((double)Token) ||
                                float.IsInfinity((float)Token) ||
                                double.IsNaN((double)Token) ||
                                float.IsNaN((float)Token)) {
                                return VariantValueType.Primitive;
                            }
                            return VariantValueType.Float;
                        case JTokenType.Date:
                            return VariantValueType.UtcDateTime;
                        case JTokenType.TimeSpan:
                            return VariantValueType.TimeSpan;
                        case JTokenType.Guid:
                            return VariantValueType.Guid;
                        case JTokenType.Raw:
                        case JTokenType.Uri:
                            return VariantValueType.Primitive;
                        case JTokenType.String:
                            var s = (string)Token;
                            if (string.IsNullOrEmpty(s)) {
                                return VariantValueType.Primitive;
                            }
                          // if (TimeSpan.TryParse(s, out _)) {
                          //     return VariantValueType.TimeSpan;
                          // }
                            if (DateTime.TryParse(s, out _) ||
                                DateTimeOffset.TryParse(s, out _)) {
                                return VariantValueType.UtcDateTime;
                            }
                          //  if (double.TryParse(s, out _) ||
                          //      float.TryParse(s, out _)) {
                          //      return VariantValueType.Float;
                          //  }
                          //  if (decimal.TryParse(s, out _)) {
                          //      return VariantValueType.Float;
                          //  }
                            if (Guid.TryParse(s, out _)) {
                                return VariantValueType.Guid;
                            }
                            return VariantValueType.Primitive;
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
                    return Enumerable.Empty<string>();
                }
            }

            /// <inheritdoc/>
            public override IEnumerable<VariantValue> Values {
                get {
                    if (Token is JArray array) {
                        return array.Select(i => new JsonVariantValue(i, _serializer));
                    }
                    return Enumerable.Empty<VariantValue>();
                }
            }

            /// <inheritdoc/>
            public override int Count {
                get {
                    if (Token is JArray array) {
                        return array.Count;
                    }
                    return 0;
                }
            }

            /// <inheritdoc/>
            public override VariantValue Copy(bool shallow) {
                return new JsonVariantValue(shallow ? Token :
                    Token.DeepClone(), _serializer);
            }

            /// <inheritdoc/>
            public override object ToType(Type type, IFormatProvider provider) {
                try {
                    return Token.ToObject(type,
                        JsonSerializer.CreateDefault(_serializer.Settings));
                }
                catch (JsonReaderException ex) {
                    throw new SerializerException(ex.Message, ex);
                }
            }

            /// <inheritdoc/>
            public override VariantValue SelectToken(string path) {
                try {
                    var selected = Token.SelectToken(path);
                    return new JsonVariantValue(selected, _serializer);
                }
                catch (JsonReaderException ex) {
                    throw new SerializerException(ex.Message, ex);
                }
            }

            /// <inheritdoc/>
            public override void Set(object value) {
                Token = FromObject(value);
            }

            /// <inheritdoc/>
            public override string ToString(SerializeOption format) {
                return Token.ToString(format == SerializeOption.Indented ?
                    Formatting.Indented :
                    Formatting.None,
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
            protected override VariantValue Null() {
                return new JsonVariantValue(null, _serializer);
            }

            /// <inheritdoc/>
            protected override bool TryEqualsValue(object o, out bool equality) {
                // Compare tokens
                if (!(o is JToken t)) {
                    try {
                        t = FromObject(o);
                    }
                    catch {
                        return base.TryEqualsValue(o, out equality);
                    }
                }
                equality = DeepEquals(Token, t);
                return true;
            }

            /// <inheritdoc/>
            protected override bool TryEqualsVariant(VariantValue v, out bool equality) {
                if (v is JsonVariantValue json) {
                    equality = DeepEquals(Token, json.Token);
                    return true;
                }
                return base.TryEqualsVariant(v, out equality);
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
                if (ReferenceEquals(t1, t2)) {
                    return true;
                }
                if (t1 is JObject o1 && t2 is JObject o2) {
                    // Compare properties in order of key
                    var props1 = o1.Properties().OrderBy(k => k.Name)
                        .Select(p => p.Value);
                    var props2 = o2.Properties().OrderBy(k => k.Name)
                        .Select(p => p.Value);
                    return props1.SequenceEqual(props2,
                        Compare.Using<JToken>((x, y) => DeepEquals(x, y)));
                }

                if (t1 is JContainer c1 && t2 is JContainer c2) {
                    // For all other containers - order is important
                    return c1.Children().SequenceEqual(c2.Children(),
                        Compare.Using<JToken>((x, y) => DeepEquals(x, y)));
                }

                if (t1 is JValue && t2 is JValue) {
                    if (t1.Equals(t2)) {
                        return true;
                    }
                    var s1 = t1.ToString(Formatting.None,
                        _serializer.Settings.Converters.ToArray());
                    var s2 = t2.ToString(Formatting.None,
                        _serializer.Settings.Converters.ToArray());
                    if (s1 == s2) {
                        return true;
                    }
                }
                return false;
            }

            /// <inheritdoc/>
            protected override bool TryCompareToValue(object o, out int result) {
                // Compare value token
                if (Token is JValue v1 && o is JValue v2) {
                    result = v1.CompareTo(v2);
                    return true;
                }
                result = 0;
                return false;
            }

            /// <inheritdoc/>
            protected override bool TryCompareToVariantValue(VariantValue v, out int result) {
                if (v is JsonVariantValue json) {
                    return TryCompareToValue(json.Token, out result);
                }
                result = 0;
                return false;
            }

            /// <summary>
            /// Create token from object and rethrow serializer exception
            /// </summary>
            /// <param name="o"></param>
            /// <returns></returns>
            private JToken FromObject(object o) {
                try {
                    return JToken.FromObject(o,
                        JsonSerializer.CreateDefault(_serializer.Settings));
                }
                catch (JsonReaderException ex) {
                    throw new SerializerException(ex.Message, ex);
                }
            }

            private readonly NewtonSoftJsonSerializer _serializer;
        }

        /// <summary>
        /// Json veriant converter
        /// </summary>
        internal sealed class JsonVariantConverter : JsonConverter {

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