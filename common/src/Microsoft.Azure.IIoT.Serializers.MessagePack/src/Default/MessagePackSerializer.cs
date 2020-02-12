// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------

namespace Microsoft.Azure.IIoT.Serializers.MessagePack {
    using Microsoft.Azure.IIoT.Serializers;
    using Microsoft.Azure.IIoT.Exceptions;
    using System.Collections.Concurrent;
    using global::MessagePack.Formatters;
    using global::MessagePack.Resolvers;
    using global::MessagePack;
    using System;
    using System.Buffers;
    using System.Collections.Generic;
    using System.Linq;
    using System.Numerics;
    using System.Text;
    using MsgPack = global::MessagePack.MessagePackSerializer;

    /// <summary>
    /// Message pack serializer
    /// </summary>
    public class MessagePackSerializer : IMessagePackSerializerOptionsProvider,
        ISerializer {

        /// <inheritdoc/>
        public string MimeType => ContentMimeType.MsgPack;

        /// <inheritdoc/>
        public Encoding ContentEncoding => null;

        /// <summary>
        /// Message pack options
        /// </summary>
        public MessagePackSerializerOptions Options { get; }

        /// <summary>
        /// Create serializer
        /// </summary>
        /// <param name="providers"></param>
        public MessagePackSerializer(
            IEnumerable<IMessagePackFormatterResolverProvider> providers = null) {
            // Create options
            var resolvers = new List<IFormatterResolver> {
                MessagePackVariantFormatterResolver.Instance
            };
            if (providers != null) {
                foreach (var provider in providers) {
                    var providedResolvers = provider.GetResolvers();
                    if (providedResolvers != null) {
                        resolvers.AddRange(providedResolvers);
                    }
                }
            }
            resolvers.Add(StandardResolver.Instance);
            Options = MessagePackSerializerOptions.Standard
                .WithSecurity(MessagePackSecurity.UntrustedData)
                .WithResolver(CompositeResolver.Create(resolvers.ToArray()))
                ;
        }

        /// <inheritdoc/>
        public object Deserialize(ReadOnlyMemory<byte> buffer, Type type) {
            try {
                return MsgPack.Deserialize(type, buffer, Options);
            }
            catch (MessagePackSerializationException ex) {
                throw new SerializerException(ex.Message, ex);
            }
        }

        /// <inheritdoc/>
        public void Serialize(IBufferWriter<byte> buffer, object o, SerializeOption format) {
            try {
                MsgPack.Serialize(buffer, o, Options);
            }
            catch (MessagePackSerializationException ex) {
                throw new SerializerException(ex.Message, ex);
            }
        }

        /// <inheritdoc/>
        public VariantValue Parse(ReadOnlyMemory<byte> buffer) {
            try {
                var o = MsgPack.Deserialize<object>(buffer, Options);
                if (o is VariantValue v) {
                    return v;
                }
                return new MessagePackVariantValue(o, Options, false);
            }
            catch (MessagePackSerializationException ex) {
                throw new SerializerException(ex.Message, ex);
            }
        }

        /// <inheritdoc/>
        public VariantValue FromObject(object o) {
            try {
                return new MessagePackVariantValue(o, Options, true);
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
            /// <param name="typed">Whether the object is the
            /// original type or the generated one</param>
            internal MessagePackVariantValue(object value,
                MessagePackSerializerOptions serializer, bool typed) {
                _options = serializer;
                _value = typed ? ToTypeLess(value) : value;
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
                    if (typeof(byte[]) == type) {
                        return VariantValueType.Bytes;
                    }
                    if (typeof(Guid) == type ||
                        typeof(Uri) == type) {
                        return VariantValueType.String;
                    }
                    if (type.IsArray ||
                        typeof(IList<object>).IsAssignableFrom(type) ||
                        typeof(IEnumerable<object>).IsAssignableFrom(type)) {
                        return VariantValueType.Array;
                    }
                    if (typeof(IDictionary<object, object>).IsAssignableFrom(type)) {
                        return VariantValueType.Object;
                    }
                    if (type.IsGenericType &&
                        type.GetGenericTypeDefinition() == typeof(Nullable<>)) {
                        type = type.GetGenericArguments()[0];
                    }
                    if (typeof(bool) == type) {
                        return VariantValueType.Boolean;
                    }
                    if (typeof(uint) == type ||
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
                        typeof(BigInteger) == type ||
                        typeof(decimal) == type) {
                        return VariantValueType.Float;
                    }
                    if (type.GetProperties().Length > 0) {
                        return VariantValueType.Object;
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
                        return array.Select(i =>
                            new MessagePackVariantValue(i, _options, false));
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
                    return Null();
                }
                try {
                    return new MessagePackVariantValue(Value, _options, true);
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
                if (type.IsAssignableFrom(Value.GetType())) {
                    return Value;
                }
                try {
                    var buffer = new ArrayBufferWriter<byte>();
                    MsgPack.Serialize(buffer, Value, _options);
                    // Special case - convert byte array to buffer if not bin to begin.
                    if (type == typeof(byte[]) && Value.GetType().IsArray) {
                        return ((IList<byte>)MsgPack.Deserialize(typeof(IList<byte>),
                            buffer.WrittenMemory, _options)).ToArray();
                    }
                    return MsgPack.Deserialize(type, buffer.WrittenMemory, _options);
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
                    var buffer = MsgPack.Serialize(Value, _options);
                    return MsgPack.ConvertToJson(buffer, _options);
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
                        value = new MessagePackVariantValue(
                            success.Value, _options, false);
                        return true;
                    }
                }

                // TODO: use reflection

                value = Null();
                return false;
            }

            /// <inheritdoc/>
            public override bool TryGetValue(int index, out VariantValue value) {
                if (index >= 0 && Value is IList<object> o && index < o.Count) {
                    value = new MessagePackVariantValue(o[index], _options, false);
                    return true;
                }
                value = Null();
                return false;
            }

            /// <inheritdoc/>
            protected override int GetDeepHashCode() {
                return Value.GetHashCode();
            }

            /// <inheritdoc/>
            protected override VariantValue Null() {
                return new MessagePackVariantValue(null, _options, false);
            }

            /// <inheritdoc/>
            protected override bool TryCompareInnerValueTo(object o, out int result) {
                // Compare value token
                if (Value is IComparable v1 && o is IComparable v2) {
                    result = v1.CompareTo(v2);
                    return true;
                }
                if (Value is object[] a1 && a1.Length > 0 && a1[0] is IComparable c1 &&
                    Value is object[] a2 && a2.Length > 0 && a2[0] is IComparable c2) {
                    result = c1.CompareTo(c2);
                    return true;
                }
                result = 0;
                return false;
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
            /// Compare tokens in more consistent fashion
            /// </summary>
            /// <param name="t1"></param>
            /// <param name="t2"></param>
            /// <returns></returns>
            internal bool DeepEquals(object t1, object t2) {
                if (t1 is null || t2 is null) {
                    return t1 == t2;
                }

                if (TypeLessEquals(t1, t2)) {
                    return true;
                }

                t1 = ToTypeLess(t1);
                t2 = ToTypeLess(t2);
                if (TypeLessEquals(t1, t2)) {
                    return true;
                }
                return false;
            }

            /// <summary>
            /// Compare tokens in more consistent fashion
            /// </summary>
            /// <param name="t1"></param>
            /// <param name="t2"></param>
            /// <returns></returns>
            internal bool TypeLessEquals(object t1, object t2) {
                // Test object equals
                if (t1 is IDictionary<object, object> o1 &&
                    t2 is IDictionary<object, object> o2) {
                    // Compare properties in order of key
                    var props1 = o1.OrderBy(k => k.Key).Select(k => k.Value);
                    var props2 = o2.OrderBy(k => k.Key).Select(k => k.Value);
                    return props1.SequenceEqual(props2,
                        Compare.Using<object>((x, y) => DeepEquals(x, y)));
                }

                // Test array
                if (t1 is object[] c1 && t2 is object[] c2) {
                    return c1.SequenceEqual(c2,
                        Compare.Using<object>((x, y) => DeepEquals(x, y)));
                }

                // Test value equals
                if (t1.Equals(t2)) {
                    return true;
                }
                return false;
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
                    MsgPack.Serialize(buffer, value, _options);
                    return MsgPack.Deserialize<object>(buffer.WrittenMemory, _options);
                }
                catch (MessagePackSerializationException ex) {
                    throw new SerializerException(ex.Message, ex);
                }
            }

            private readonly MessagePackSerializerOptions _options;
            private object _value;
        }

        /// <summary>
        /// Message pack resolver
        /// </summary>
        private class MessagePackVariantFormatterResolver : IFormatterResolver {

            public static readonly MessagePackVariantFormatterResolver Instance =
                new MessagePackVariantFormatterResolver();

            /// <inheritdoc/>
            public IMessagePackFormatter<T> GetFormatter<T>() {
                if (typeof(VariantValue).IsAssignableFrom(typeof(T))) {
                    return (IMessagePackFormatter<T>)GetVariantFormatter(typeof(T));
                }
                return null;
            }

            /// <summary>
            /// Create Message pack variant formater of specifed type
            /// </summary>
            /// <param name="type"></param>
            /// <returns></returns>
            internal IMessagePackFormatter GetVariantFormatter(Type type) {
                return _cache.GetOrAdd(type,
                    (IMessagePackFormatter)Activator.CreateInstance(
                        typeof(MessagePackVariantFormatter<>).MakeGenericType(type)));
            }

            /// <summary>
            /// Variant formatter
            /// </summary>
            private sealed class MessagePackVariantFormatter<T> : IMessagePackFormatter<T>
                where T : VariantValue {

                /// <inheritdoc/>
                public void Serialize(ref MessagePackWriter writer, T value,
                    MessagePackSerializerOptions options) {
                    if (value is MessagePackVariantValue packed) {
                        MsgPack.Serialize(ref writer, packed.Value, options);
                    }
                    else if (value is VariantValue variant) {
                        switch (variant.Type) {
                            case VariantValueType.Null:
                                writer.WriteNil();
                                break;
                            case VariantValueType.Array:
                                writer.WriteArrayHeader(variant.Count);
                                foreach (var item in variant.Values) {
                                    MsgPack.Serialize(ref writer, item, options);
                                }
                                break;
                            case VariantValueType.Object:
                                // Serialize objects as key value pairs
                                var dict = variant.Keys
                                    .ToDictionary(k => k, k => variant[k]);
                                MsgPack.Serialize(ref writer, dict, options);
                                break;
                            default:
                                // Serialize value using primitive serializer
                                MsgPack.Serialize(ref writer, variant.Value, options);
                                break;
                        }
                    }
                }

                /// <inheritdoc/>
                public T Deserialize(ref MessagePackReader reader,
                    MessagePackSerializerOptions options) {

                    // Read variant from reader
                    var o = MsgPack.Deserialize<object>(ref reader, options);
                    return new MessagePackVariantValue(o, options, false) as T;
                }
            }

            private readonly ConcurrentDictionary<Type, IMessagePackFormatter> _cache =
                new ConcurrentDictionary<Type, IMessagePackFormatter>();
        }
    }
}