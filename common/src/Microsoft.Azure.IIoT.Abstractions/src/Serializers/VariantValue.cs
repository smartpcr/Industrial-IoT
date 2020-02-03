// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------

namespace Microsoft.Azure.IIoT.Serializers {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents primitive or structurally complex value
    /// </summary>
    public class VariantValue : IEquatable<VariantValue>,
        IDictionary<string, VariantValue>, IConvertible {

        /// <summary>
        /// Raw value
        /// </summary>
        public object Value { get; }

        /// <summary>
        /// Serializer
        /// </summary>
        public IJsonSerializer Serializer { get; }

        /// <inheritdoc/>
        public ICollection<string> Keys => Object.Keys;

        /// <inheritdoc/>
        public ICollection<VariantValue> Values => Object.Values;

        /// <inheritdoc/>
        public int Count => Object.Count;

        /// <inheritdoc/>
        public bool IsReadOnly => true;

        /// <inheritdoc/>
        public VariantValue this[string key] {
            get => Object[key];
            set => throw new NotImplementedException();
        }

        /// <summary>
        /// Null variant
        /// </summary>
        public static VariantValue Null => new VariantValue(null, null);


        /// <summary>
        /// Create value
        /// </summary>
        /// <param name="value"></param>
        /// <param name="serializer"></param>
        private VariantValue(object value, IJsonSerializer serializer) {
            Value = value;
            Serializer = serializer;
        }

        /// <inheritdoc/>
        public void Add(string key, VariantValue value) {
            throw new InvalidOperationException();
        }

        /// <inheritdoc/>
        public bool ContainsKey(string key) {
            return Object.ContainsKey(key);
        }

        /// <inheritdoc/>
        public bool Remove(string key) {
            throw new InvalidOperationException();
        }

        /// <inheritdoc/>
        public bool TryGetValue(string key, out VariantValue value) {
            return Object.TryGetValue(key, out value);
        }

        /// <inheritdoc/>
        public void Add(KeyValuePair<string, VariantValue> item) {
            throw new InvalidOperationException();
        }

        /// <inheritdoc/>
        public void Clear() {
            throw new InvalidOperationException();
        }

        /// <inheritdoc/>
        public bool Contains(KeyValuePair<string, VariantValue> item) {
            return Object.Contains(item);
        }

        /// <inheritdoc/>
        public void CopyTo(KeyValuePair<string, VariantValue>[] array, int arrayIndex) {
            Object.CopyTo(array, arrayIndex);
        }

        /// <inheritdoc/>
        public bool Remove(KeyValuePair<string, VariantValue> item) {
            throw new InvalidOperationException();
        }

        /// <inheritdoc/>
        public IEnumerator<KeyValuePair<string, VariantValue>> GetEnumerator() {
            return Object.GetEnumerator();
        }

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator() {
            return Object.GetEnumerator();
        }

        /// <inheritdoc/>
        public TypeCode GetTypeCode() {
            return Convert.GetTypeCode(Value);
        }

        /// <inheritdoc/>
        public bool ToBoolean(IFormatProvider provider) {
            return Convert.ToBoolean(Value, provider);
        }
        /// <inheritdoc/>
        public static explicit operator bool(VariantValue value) {
            return Convert.ToBoolean(value.Value);
        }
        /// <inheritdoc/>
        public static explicit operator bool?(VariantValue value) {
            return value?.Value == null ? (bool?)null : Convert.ToBoolean(value.Value);
        }

        /// <inheritdoc/>
        public byte ToByte(IFormatProvider provider) {
            return Convert.ToByte(Value, provider);
        }
        /// <inheritdoc/>
        public static explicit operator byte(VariantValue value) {
            return Convert.ToByte(value.Value);
        }
        /// <inheritdoc/>
        public static explicit operator byte?(VariantValue value) {
            return value?.Value == null ? (byte?)null : Convert.ToByte(value.Value);
        }

        /// <inheritdoc/>
        public char ToChar(IFormatProvider provider) {
            return Convert.ToChar(Value, provider);
        }
        /// <inheritdoc/>
        public static explicit operator char(VariantValue value) {
            return Convert.ToChar(value.Value);
        }
        /// <inheritdoc/>
        public static explicit operator char?(VariantValue value) {
            return value?.Value == null ? (char?)null : Convert.ToChar(value.Value);
        }

        /// <inheritdoc/>
        public DateTime ToDateTime(IFormatProvider provider) {
            return Convert.ToDateTime(Value, provider);
        }
        /// <inheritdoc/>
        public static explicit operator DateTime(VariantValue value) {
            return Convert.ToDateTime(value.Value);
        }
        /// <inheritdoc/>
        public static explicit operator DateTime?(VariantValue value) {
            return value?.Value == null ? (DateTime?)null : Convert.ToDateTime(value.Value);
        }

        /// <inheritdoc/>
        public decimal ToDecimal(IFormatProvider provider) {
            return Convert.ToDecimal(Value, provider);
        }
        /// <inheritdoc/>
        public static explicit operator decimal(VariantValue value) {
            return Convert.ToDecimal(value.Value);
        }
        /// <inheritdoc/>
        public static explicit operator decimal?(VariantValue value) {
            return value?.Value == null ? (decimal?)null : Convert.ToDecimal(value.Value);
        }

        /// <inheritdoc/>
        public double ToDouble(IFormatProvider provider) {
            return Convert.ToDouble(Value, provider);
        }
        /// <inheritdoc/>
        public static explicit operator double(VariantValue value) {
            return Convert.ToDouble(value.Value);
        }
        /// <inheritdoc/>
        public static explicit operator double?(VariantValue value) {
            return value?.Value == null ? (double?)null : Convert.ToDouble(value.Value);
        }

        /// <inheritdoc/>
        public short ToInt16(IFormatProvider provider) {
            return Convert.ToInt16(Value, provider);
        }
        /// <inheritdoc/>
        public static explicit operator short(VariantValue value) {
            return Convert.ToInt16(value.Value);
        }
        /// <inheritdoc/>
        public static explicit operator short?(VariantValue value) {
            return value?.Value == null ? (short?)null : Convert.ToInt16(value.Value);
        }

        /// <inheritdoc/>
        public int ToInt32(IFormatProvider provider) {
            return Convert.ToInt32(Value, provider);
        }
        /// <inheritdoc/>
        public static explicit operator int?(VariantValue value) {
            return value?.Value == null ? (int?)null : Convert.ToInt32(value.Value);
        }

        /// <inheritdoc/>
        public long ToInt64(IFormatProvider provider) {
            return Convert.ToInt64(Value, provider);
        }
        /// <inheritdoc/>
        public static explicit operator long(VariantValue value) {
            return Convert.ToInt64(value.Value);
        }
        /// <inheritdoc/>
        public static explicit operator long?(VariantValue value) {
            return value?.Value == null ? (long?)null : Convert.ToInt64(value.Value);
        }

        /// <inheritdoc/>
        public ushort ToUInt16(IFormatProvider provider) {
            return Convert.ToUInt16(Value, provider);
        }
        /// <inheritdoc/>
        public static explicit operator ushort(VariantValue value) {
            return Convert.ToUInt16(value.Value);
        }
        /// <inheritdoc/>
        public static explicit operator ushort?(VariantValue value) {
            return value?.Value == null ? (ushort?)null : Convert.ToUInt16(value.Value);
        }

        /// <inheritdoc/>
        public uint ToUInt32(IFormatProvider provider) {
            return Convert.ToUInt32(Value, provider);
        }
        /// <inheritdoc/>
        public static explicit operator uint(VariantValue value) {
            return Convert.ToUInt32(value.Value);
        }
        /// <inheritdoc/>
        public static explicit operator uint?(VariantValue value) {
            return value?.Value == null ? (uint?)null : Convert.ToUInt32(value.Value);
        }

        /// <inheritdoc/>
        public ulong ToUInt64(IFormatProvider provider) {
            return Convert.ToUInt64(Value, provider);
        }
        /// <inheritdoc/>
        public static explicit operator ulong(VariantValue value) {
            return Convert.ToUInt64(value.Value);
        }
        /// <inheritdoc/>
        public static explicit operator ulong?(VariantValue value) {
            return value?.Value == null ? (ulong?)null : Convert.ToUInt64(value.Value);
        }

        /// <inheritdoc/>
        public sbyte ToSByte(IFormatProvider provider) {
            return Convert.ToSByte(Value, provider);
        }
        /// <inheritdoc/>
        public static explicit operator sbyte(VariantValue value) {
            return Convert.ToSByte(value.Value);
        }
        /// <inheritdoc/>
        public static explicit operator sbyte?(VariantValue value) {
            return value?.Value == null ? (sbyte?)null : Convert.ToSByte(value.Value);
        }

        /// <inheritdoc/>
        public float ToSingle(IFormatProvider provider) {
            return Convert.ToSingle(Value, provider);
        }
        /// <inheritdoc/>
        public static explicit operator float(VariantValue value) {
            return Convert.ToSingle(value.Value);
        }
        /// <inheritdoc/>
        public static explicit operator float?(VariantValue value) {
            return value?.Value == null ? (float?)null : Convert.ToSingle(value.Value);
        }

        /// <inheritdoc/>
        public object ToType(Type conversionType, IFormatProvider provider) {
            return Convert.ChangeType(Value, conversionType, provider);
        }

        /// <inheritdoc/>
        public string ToString(IFormatProvider provider) {
            return Convert.ToString(Value, provider);
        }
        /// <inheritdoc/>
        public static explicit operator string(VariantValue value) {
            return Convert.ToString(value.Value);
        }


        /// <summary>
        /// Deep clone
        /// </summary>
        /// <returns></returns>
        public VariantValue DeepClone() {
            // TODO
            return this;
        }

        /// <inheritdoc/>
        public bool Equals(VariantValue other) {
            if (other == null) {
                return false;
            }
            if (Value is IDictionary<string, VariantValue> obj) {
                if (Object.Count != obj.Count) {
                    return false;
                }
                return Object
                    .All(kv => obj.TryGetValue(kv.Key, out var v) &&
                        Equals(v, kv.Value));
            }
            return Equals(other.Value, Value);
        }

        /// <inheritdoc/>
        public static bool operator ==(VariantValue left, VariantValue right) =>
            EqualityComparer<VariantValue>.Default.Equals(left, right);
        /// <inheritdoc/>
        public static bool operator !=(VariantValue left, VariantValue right) =>
            !(left == right);

        /// <inheritdoc/>
        public override bool Equals(object o) {
            return Equals(o as VariantValue);
        }

        /// <inheritdoc/>
        public override string ToString() {
            return ToString(JsonFormat.None);
        }

        /// <inheritdoc/>
        public override int GetHashCode() {
            if (Value is IDictionary<string, VariantValue> obj) {
                var hashCode = new HashCode();
                foreach (var kv in obj) {
                    hashCode.Add(kv.Key);
                    hashCode.Add(kv.Value);
                }
                return hashCode.ToHashCode();
            }
            return HashCode.Combine(Value);
        }

        /// <summary>
        /// Convert to string
        /// </summary>
        /// <param name="format"></param>
        /// <returns></returns>
        public string ToString(JsonFormat format) {
            return Serializer.Serialize(this, format);
        }

        /// <summary>
        /// Convert value to typed value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T ToObject<T>() {
            return (T)Serializer.ToObject(this, typeof(T));
        }

        /// <summary>
        /// Convert value to typed value
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public object ToObject(Type type) {
            return Serializer.ToObject(this, type);
        }

        internal IDictionary<string, VariantValue> Object =>
            Value as IDictionary<string, VariantValue> ?? kEmptyObject;
        private static readonly IDictionary<string, VariantValue> kEmptyObject =
            new Dictionary<string, VariantValue>();
    }
}