// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------

namespace Microsoft.Azure.IIoT.Serializers.MessagePack {
    using Microsoft.Azure.IIoT.Exceptions;
    using System;
    using System.Buffers;
    using System.Collections.Generic;
    using System.Linq;
    using global::MessagePack;
    using MsgPack = global::MessagePack.MessagePackSerializer;

    /// <summary>
    /// Newtonsoft json serializer
    /// </summary>
    public class MessagePackSerializer : IMessagePackSerializerOptionsProvider, ISerializer {

        /// <summary>
        /// Json serializer settings
        /// </summary>
        public MessagePackSerializerOptions Settings { get; }

        /// <summary>
        /// Create serializer
        /// </summary>
        /// <param name="providers"></param>
        public MessagePackSerializer(
            IEnumerable<IMessagePackFormatterResolverProvider> providers = null) {
            var settings = MessagePackSerializerOptions.Standard
                .WithSecurity(MessagePackSecurity.UntrustedData);
            // ...

            Settings = settings;
        }

        /// <inheritdoc/>
        public object Deserialize(ReadOnlyMemory<byte> buffer, Type type) {
            try {
                return MsgPack.Deserialize(type, buffer, Settings);
            }
            catch (MessagePackSerializationException ex) {
                throw new SerializerException(ex.Message, ex);
            }
        }

        /// <inheritdoc/>
        public void Serialize(IBufferWriter<byte> buffer, object o, SerializeOption format) {
            try {
                MsgPack.Serialize(buffer, o, Settings);
            }
            catch (MessagePackSerializationException ex) {
                throw new SerializerException(ex.Message, ex);
            }
        }

        /// <inheritdoc/>
        public VariantValue Parse(ReadOnlyMemory<byte> buffer) {
            try {
                var v = MsgPack.Deserialize<object>(buffer, Settings);
                return new MessagePackVariantValue(v, this, false);
            }
            catch (MessagePackSerializationException ex) {
                throw new SerializerException(ex.Message, ex);
            }
        }

        /// <inheritdoc/>
        public VariantValue FromObject(object o) {
            try {
                return new MessagePackVariantValue(o, this, true);
            }
            catch (MessagePackSerializationException ex) {
                throw new SerializerException(ex.Message, ex);
            }
        }

        /// <summary>
        /// Value wrapper
        /// </summary>
        internal class MessagePackVariantValue : VariantValue {

            /// <summary>
            /// Create value
            /// </summary>
            /// <param name="value"></param>
            /// <param name="serializer"></param>
            /// <param name="parse"></param>
            internal MessagePackVariantValue(object value,
                MessagePackSerializer serializer, bool parse = false) {
                _serializer = serializer;
                _value = parse ? ToTypeLess(value) : value;
            }

            /// <inheritdoc/>
            public override VariantValueType Type {
                get {
                    if (Value == null) {
                        return VariantValueType.Null;
                    }
                    if (Value is string st) {
                        if (DateTimeOffset.TryParse(st, out _)) {
                            return VariantValueType.Date;
                        }
                        if (TimeSpan.TryParse(st, out _)) {
                            return VariantValueType.TimeSpan;
                        }
                        return VariantValueType.String;
                    }

                    var type = Value.GetType();
                    if (type.IsArray) {
                        return VariantValueType.Array;
                    }
                    if (typeof(IEnumerable<>).IsAssignableFrom(type)) {
                        return VariantValueType.Array;
                    }
                    if (type.GetProperties().Length > 0) {
                        return VariantValueType.Object;
                    }
                    if (typeof(IDictionary<,>).IsAssignableFrom(type)) {
                        return VariantValueType.Object;
                    }

                    if (typeof(Nullable<>).IsAssignableFrom(type)) {
                        type = type.GetGenericArguments()[0];
                    }
                    if (typeof(bool) == type) {
                        return VariantValueType.Boolean;
                    }
                    if (typeof(byte[]) == type) {
                        return VariantValueType.Bytes;
                    }
                    if (typeof(uint[]) == type ||
                        typeof(int) == type ||
                        typeof(ulong) == type ||
                        typeof(long) == type ||
                        typeof(sbyte) == type ||
                        typeof(byte) == type ||
                        typeof(ushort) == type ||
                        typeof(short) == type ||
                        typeof(char) == type) {
                        return VariantValueType.Integer;
                    }
                    if (typeof(float) == type ||
                        typeof(double) == type ||
                        typeof(decimal) == type) {
                        return VariantValueType.Float;
                    }
                    return VariantValueType.Undefined;
                }
            }

            /// <inheritdoc/>
            public override object Value => _value;

            /// <inheritdoc/>
            public override IEnumerable<string> Keys {
                get {
                    if (Value is IDictionary<object, object> o) {
                        return o.Keys.Select(p => p.ToString());
                    }
                    return Enumerable.Empty<string>();
                }
            }

            /// <inheritdoc/>
            public override IEnumerable<VariantValue> Values {
                get {
                    if (Value is IList<object> array) {
                        return array.Select(i => new MessagePackVariantValue(i, _serializer));
                    }
                    return Enumerable.Empty<VariantValue>();
                }
            }

            /// <inheritdoc/>
            public override int Count {
                get {
                    if (Value is IList<object> array) {
                        return array.Count;
                    }
                    return 0;
                }
            }

            /// <inheritdoc/>
            public override VariantValue Copy(bool shallow) {
                if (Value is null) {
                    return new MessagePackVariantValue(null, _serializer);
                }
                try {
                    return new MessagePackVariantValue(Value, _serializer, true);
                }
                catch (MessagePackSerializationException ex) {
                    throw new SerializerException(ex.Message, ex);
                }
            }

            /// <inheritdoc/>
            public override object ToType(Type type, IFormatProvider provider) {
                if (Value is null) {
                    return null;
                }
                try {
                    var buffer = new ArrayBufferWriter<byte>();
                    MsgPack.Serialize(buffer, Value, _serializer.Settings);
                    return MsgPack.Deserialize(type, buffer.WrittenMemory,
                        _serializer.Settings);
                }
                catch (MessagePackSerializationException ex) {
                    throw new SerializerException(ex.Message, ex);
                }
            }

            /// <inheritdoc/>
            public override VariantValue SelectToken(string path) {
                throw new NotSupportedException("Path not supported");
            }

            /// <inheritdoc/>
            public override void Set(object value) {
                _value = value;
            }

            /// <inheritdoc/>
            public override string ToString(SerializeOption format) {
                try {
                    var buffer = MsgPack.Serialize(Value, _serializer.Settings);
                    return MsgPack.ConvertToJson(buffer, _serializer.Settings);
                }
                catch (MessagePackSerializationException ex) {
                    throw new SerializerException(ex.Message, ex);
                }
            }

            /// <inheritdoc/>
            public override bool TryGetValue(string key, out VariantValue value,
                StringComparison compare) {
                if (Value is IDictionary<string, object> o) {
                    var success = o.FirstOrDefault(kv => key.Equals(kv.Key, compare));
                    if (success.Value != null) {
                        value = new MessagePackVariantValue(success.Value, _serializer);
                        return true;
                    }
                }

                // TODO: use reflection

                value = new MessagePackVariantValue(null, _serializer);
                return false;
            }

            /// <inheritdoc/>
            public override bool TryGetValue(int index, out VariantValue value) {
                if (index >= 0 && Value is IList<object> o && index < o.Count) {
                    value = new MessagePackVariantValue(o[index], _serializer);
                    return true;
                }
                value = new MessagePackVariantValue(null, _serializer);
                return false;
            }

            /// <inheritdoc/>
            protected override int GetDeepHashCode() {
                return Value.GetHashCode();
            }

            /// <inheritdoc/>
            protected override VariantValue Null() {
                return new MessagePackVariantValue(null, _serializer);
            }

            /// <inheritdoc/>
            protected override bool ValueEquals(object o) {
                // Compare tokens
                if (ReferenceEquals(o, Value)) {
                    return true;
                }
                if (!DeepEquals(Value, o)) {
                    return false;
                }
                return true;
            }

            /// <inheritdoc/>
            protected override bool DeepEquals(VariantValue v) {
                return ValueEquals(v.Value);
            }

            /// <summary>
            /// Convert to typeless object
            /// </summary>
            /// <param name="value"></param>
            /// <returns></returns>
            internal object ToTypeLess(object value) {
                if (value is null) {
                    return null;
                }
                try {
                    var buffer = new ArrayBufferWriter<byte>();
                    MsgPack.Serialize(buffer, value, _serializer.Settings);
                    return MsgPack.Deserialize<object>(buffer.WrittenMemory,
                        _serializer.Settings);
                }
                catch (MessagePackSerializationException ex) {
                    throw new SerializerException(ex.Message, ex);
                }
            }

            /// <summary>
            /// Compare tokens in more consistent fashion
            /// </summary>
            /// <param name="t1"></param>
            /// <param name="t2"></param>
            /// <returns></returns>
            internal bool DeepEquals(object t1, object t2) {
                if (t1 is null || t2 is null) {
                    return t1 == t2;
                }

                if (t1 is IDictionary<object, object> o1 &&
                    t2 is IDictionary<object, object> o2) {
                    // Compare properties in order of key
                    var props1 = o1.OrderBy(k => k.Key).Select(k => k.Value);
                    var props2 = o2.OrderBy(k => k.Key).Select(k => k.Value);
                    return props1.SequenceEqual(props2,
                        Compare.Using<object>((x, y) => DeepEquals(x, y)));
                }

                if (t1 is IList<object> c1 && t2 is IList<object> c2) {
                    // For all other containers - order is important
                    return c1.SequenceEqual(c2,
                        Compare.Using<object>((x, y) => DeepEquals(x, y)));
                }

                // TODO: use reflection

                if (t1.Equals(t2)) {
                    return true;
                }
                var s1 = t1.ToString();
                var s2 = t2.ToString();
                if (s1 == s2) {
                    return true;
                }
                return false;
            }

            /// <inheritdoc/>
            protected override bool TryCompareInnerValueTo(object o, out int result) {
                // Compare value token
                if (Value is IComparable v1 && o is IComparable v2) {
                    result = v1.CompareTo(v2);
                    return true;
                }
                result = 0;
                return false;
            }

            private readonly MessagePackSerializer _serializer;
            private object _value;
        }
    }
}