// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------

namespace Microsoft.Azure.IIoT.Serializers {
    using System;
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// Represents primitive or structurally complex value
    /// </summary>
    public abstract class VariantValue : ICloneable, IConvertible {

        /// <summary>
        /// Get type of value
        /// </summary>
        /// <inheritdoc/>
        public abstract VariantValueType Type { get; }

        /// <summary>
        /// Property names of object
        /// </summary>
        public abstract IEnumerable<string> Keys { get; }

        /// <summary>
        /// Values of array
        /// </summary>
        public abstract IEnumerable<VariantValue> Values { get; }

        /// <summary>
        /// Provide raw value or null
        /// </summary>
        public abstract object Value { get; }

        /// <inheritdoc/>
        public VariantValue this[string key] {
            get => TryGetValue(key, out var result) ? result : Null();
        }

        /// <inheritdoc/>
        public VariantValue this[int index] {
            get => TryGetValue(index, out var result) ? result : null;
        }

        /// <summary>
        /// Length of array
        /// </summary>
        public abstract int Count { get; }

        /// <inheritdoc/>
        public virtual TypeCode GetTypeCode() {
            switch (Type) {
                case VariantValueType.String:
                    return TypeCode.String;
                case VariantValueType.Null:
                    return TypeCode.Empty;
                case VariantValueType.Integer:
                    return TypeCode.Int64;
                case VariantValueType.Boolean:
                    return TypeCode.Boolean;
                case VariantValueType.Float:
                    return TypeCode.Decimal;
                case VariantValueType.Date:
                    return TypeCode.DateTime;
                default:
                    return TypeCode.Object;
            }
        }
        /// <inheritdoc/>
        public bool ToBoolean(IFormatProvider provider) {
            return ToObject<bool>();
        }
        /// <inheritdoc/>
        public static explicit operator bool(VariantValue value) {
            return value.ToObject<bool>();
        }
        /// <inheritdoc/>
        public static explicit operator bool?(VariantValue value) {
            return value.IsNull() ? (bool?)null : value.ToObject<bool>();
        }
        /// <inheritdoc/>
        public static implicit operator VariantValue(bool value) {
            return new PrimitiveValue(value);
        }
        /// <inheritdoc/>
        public static implicit operator VariantValue(bool? value) {
            return new PrimitiveValue(value);
        }

        /// <inheritdoc/>
        public byte ToByte(IFormatProvider provider) {
            return ToObject<byte>();
        }
        /// <inheritdoc/>
        public static explicit operator byte(VariantValue value) {
            return value.ToObject<byte>();
        }
        /// <inheritdoc/>
        public static explicit operator byte?(VariantValue value) {
            return value.IsNull() ? (byte?)null : value.ToObject<byte>();
        }
        /// <inheritdoc/>
        public static implicit operator VariantValue(byte value) {
            return new PrimitiveValue(value);
        }
        /// <inheritdoc/>
        public static implicit operator VariantValue(byte? value) {
            return new PrimitiveValue(value);
        }

        /// <inheritdoc/>
        public char ToChar(IFormatProvider provider) {
            return ToObject<char>();
        }
        /// <inheritdoc/>
        public static explicit operator char(VariantValue value) {
            return value.ToObject<char>();
        }
        /// <inheritdoc/>
        public static explicit operator char?(VariantValue value) {
            return value.IsNull() ? (char?)null : value.ToObject<char>();
        }
        /// <inheritdoc/>
        public static implicit operator VariantValue(char value) {
            return new PrimitiveValue(value);
        }
        /// <inheritdoc/>
        public static implicit operator VariantValue(char? value) {
            return new PrimitiveValue(value);
        }

        /// <inheritdoc/>
        public DateTime ToDateTime(IFormatProvider provider) {
            return ToObject<DateTime>();
        }
        /// <inheritdoc/>
        public static explicit operator DateTime(VariantValue value) {
            return value.ToObject<DateTime>();
        }
        /// <inheritdoc/>
        public static explicit operator DateTime?(VariantValue value) {
            return value.IsNull() ? (DateTime?)null : value.ToObject<DateTime>();
        }
        /// <inheritdoc/>
        public static implicit operator VariantValue(DateTime value) {
            return new PrimitiveValue(value);
        }
        /// <inheritdoc/>
        public static implicit operator VariantValue(DateTime? value) {
            return new PrimitiveValue(value);
        }

        /// <inheritdoc/>
        public static explicit operator DateTimeOffset(VariantValue value) {
            return value.ToObject<DateTimeOffset>();
        }
        /// <inheritdoc/>
        public static explicit operator DateTimeOffset?(VariantValue value) {
            return value.IsNull() ? (DateTimeOffset?)null : value.ToObject<DateTimeOffset>();
        }
        /// <inheritdoc/>
        public static implicit operator VariantValue(DateTimeOffset value) {
            return new PrimitiveValue(value);
        }
        /// <inheritdoc/>
        public static implicit operator VariantValue(DateTimeOffset? value) {
            return new PrimitiveValue(value);
        }

        /// <inheritdoc/>
        public decimal ToDecimal(IFormatProvider provider) {
            return ToObject<decimal>();
        }
        /// <inheritdoc/>
        public static explicit operator decimal(VariantValue value) {
            return value.ToObject<decimal>();
        }
        /// <inheritdoc/>
        public static implicit operator decimal?(VariantValue value) {
            return value.IsNull() ? (decimal?)null : value.ToObject<decimal>();
        }
        /// <inheritdoc/>
        public static implicit operator VariantValue(decimal value) {
            return new PrimitiveValue(value);
        }
        /// <inheritdoc/>
        public static explicit operator VariantValue(decimal? value) {
            return new PrimitiveValue(value);
        }

        /// <inheritdoc/>
        public double ToDouble(IFormatProvider provider) {
            return ToObject<double>();
        }
        /// <inheritdoc/>
        public static explicit operator double(VariantValue value) {
            return value.ToObject<double>();
        }
        /// <inheritdoc/>
        public static explicit operator double?(VariantValue value) {
            return value.IsNull() ? (double?)null : value.ToObject<double>();
        }
        /// <inheritdoc/>
        public static implicit operator VariantValue(double value) {
            return new PrimitiveValue(value);
        }
        /// <inheritdoc/>
        public static implicit operator VariantValue(double? value) {
            return new PrimitiveValue(value);
        }

        /// <inheritdoc/>
        public short ToInt16(IFormatProvider provider) {
            return ToObject<short>();
        }
        /// <inheritdoc/>
        public static explicit operator short(VariantValue value) {
            return value.ToObject<short>();
        }
        /// <inheritdoc/>
        public static explicit operator short?(VariantValue value) {
            return value.IsNull() ? (short?)null : value.ToObject<short>();
        }
        /// <inheritdoc/>
        public static implicit operator VariantValue(short value) {
            return new PrimitiveValue(value);
        }
        /// <inheritdoc/>
        public static implicit operator VariantValue(short? value) {
            return new PrimitiveValue(value);
        }

        /// <inheritdoc/>
        public int ToInt32(IFormatProvider provider) {
            return ToObject<int>();
        }
        /// <inheritdoc/>
        public static explicit operator int(VariantValue value) {
            return value.ToObject<int>();
        }
        /// <inheritdoc/>
        public static explicit operator int?(VariantValue value) {
            return value.IsNull() ? (int?)null : value.ToObject<int>();
        }
        /// <inheritdoc/>
        public static implicit operator VariantValue(int value) {
            return new PrimitiveValue(value);
        }
        /// <inheritdoc/>
        public static implicit operator VariantValue(int? value) {
            return new PrimitiveValue(value);
        }

        /// <inheritdoc/>
        public long ToInt64(IFormatProvider provider) {
            return ToObject<long>();
        }
        /// <inheritdoc/>
        public static explicit operator long(VariantValue value) {
            return value.ToObject<long>();
        }
        /// <inheritdoc/>
        public static explicit operator long?(VariantValue value) {
            return value.IsNull() ? (long?)null : value.ToObject<long>();
        }
        /// <inheritdoc/>
        public static implicit operator VariantValue(long value) {
            return new PrimitiveValue(value);
        }
        /// <inheritdoc/>
        public static implicit operator VariantValue(long? value) {
            return new PrimitiveValue(value);
        }

        /// <inheritdoc/>
        public ushort ToUInt16(IFormatProvider provider) {
            return ToObject<ushort>();
        }
        /// <inheritdoc/>
        public static explicit operator ushort(VariantValue value) {
            return value.ToObject<ushort>();
        }
        /// <inheritdoc/>
        public static explicit operator ushort?(VariantValue value) {
            return value.IsNull() ? (ushort?)null : value.ToObject<ushort>();
        }
        /// <inheritdoc/>
        public static implicit operator VariantValue(ushort value) {
            return new PrimitiveValue(value);
        }
        /// <inheritdoc/>
        public static implicit operator VariantValue(ushort? value) {
            return new PrimitiveValue(value);
        }

        /// <inheritdoc/>
        public uint ToUInt32(IFormatProvider provider) {
            return ToObject<uint>();
        }
        /// <inheritdoc/>
        public static explicit operator uint(VariantValue value) {
            return value.ToObject<uint>();
        }
        /// <inheritdoc/>
        public static explicit operator uint?(VariantValue value) {
            return value.IsNull() ? (uint?)null : value.ToObject<uint>();
        }
        /// <inheritdoc/>
        public static implicit operator VariantValue(uint value) {
            return new PrimitiveValue(value);
        }
        /// <inheritdoc/>
        public static implicit operator VariantValue(uint? value) {
            return new PrimitiveValue(value);
        }

        /// <inheritdoc/>
        public ulong ToUInt64(IFormatProvider provider) {
            return ToObject<ulong>(provider);
        }
        /// <inheritdoc/>
        public static explicit operator ulong(VariantValue value) {
            return value.ToObject<ulong>();
        }
        /// <inheritdoc/>
        public static explicit operator ulong?(VariantValue value) {
            return value.IsNull() ? (ulong?)null : value.ToObject<ulong>();
        }
        /// <inheritdoc/>
        public static implicit operator VariantValue(ulong value) {
            return new PrimitiveValue(value);
        }
        /// <inheritdoc/>
        public static implicit operator VariantValue(ulong? value) {
            return new PrimitiveValue(value);
        }

        /// <inheritdoc/>
        public sbyte ToSByte(IFormatProvider provider) {
            return ToObject<sbyte>(provider);
        }
        /// <inheritdoc/>
        public static explicit operator sbyte(VariantValue value) {
            return value.ToObject<sbyte>();
        }
        /// <inheritdoc/>
        public static explicit operator sbyte?(VariantValue value) {
            return value.IsNull() ? (sbyte?)null : value.ToObject<sbyte>();
        }
        /// <inheritdoc/>
        public static implicit operator VariantValue(sbyte value) {
            return new PrimitiveValue(value);
        }
        /// <inheritdoc/>
        public static implicit operator VariantValue(sbyte? value) {
            return new PrimitiveValue(value);
        }

        /// <inheritdoc/>
        public float ToSingle(IFormatProvider provider) {
            return ToObject<float>(provider);
        }
        /// <inheritdoc/>
        public static explicit operator float(VariantValue value) {
            return value.ToObject<float>();
        }
        /// <inheritdoc/>
        public static explicit operator float?(VariantValue value) {
            return value.IsNull() ? (float?)null : value.ToObject<float>();
        }
        /// <inheritdoc/>
        public static implicit operator VariantValue(float value) {
            return new PrimitiveValue(value);
        }
        /// <inheritdoc/>
        public static implicit operator VariantValue(float? value) {
            return new PrimitiveValue(value);
        }

        /// <inheritdoc/>
        public string ToString(IFormatProvider provider) {
            return ToObject<string>();
        }
        /// <inheritdoc/>
        public static explicit operator string(VariantValue value) {
            return value.ToObject<string>();
        }
        /// <inheritdoc/>
        public static implicit operator VariantValue(string value) {
            return new PrimitiveValue(value);
        }

        /// <inheritdoc/>
        public static explicit operator byte[](VariantValue value) {
            return value.ToObject<byte[]>();
        }
        /// <inheritdoc/>
        public static implicit operator VariantValue(byte[] value) {
            return new PrimitiveValue(value);
        }

        /// <inheritdoc/>
        public static explicit operator Guid(VariantValue value) {
            return value.ToObject<Guid>();
        }
        /// <inheritdoc/>
        public static explicit operator Guid?(VariantValue value) {
            return value.IsNull() ? (Guid?)null : value.ToObject<Guid>();
        }
        /// <inheritdoc/>
        public static implicit operator VariantValue(Guid value) {
            return new PrimitiveValue(value);
        }
        /// <inheritdoc/>
        public static implicit operator VariantValue(Guid? value) {
            return new PrimitiveValue(value);
        }

        /// <inheritdoc/>
        public static explicit operator TimeSpan(VariantValue value) {
            return value.ToObject<TimeSpan>();
        }
        /// <inheritdoc/>
        public static explicit operator TimeSpan?(VariantValue value) {
            return value.IsNull() ? (TimeSpan?)null : value.ToObject<TimeSpan>();
        }
        /// <inheritdoc/>
        public static implicit operator VariantValue(TimeSpan value) {
            return new PrimitiveValue(value);
        }
        /// <inheritdoc/>
        public static implicit operator VariantValue(TimeSpan? value) {
            return new PrimitiveValue(value);
        }

        /// <inheritdoc/>
        public abstract object ToType(Type conversionType, IFormatProvider provider);

        /// <inheritdoc/>
        public static bool operator ==(VariantValue left, VariantValue right) =>
            EqualityComparer<VariantValue>.Default.Equals(left, right);
        /// <inheritdoc/>
        public static bool operator !=(VariantValue left, VariantValue right) =>
            !(left == right);
        /// <inheritdoc/>
        public static bool operator ==(VariantValue left, object right) =>
            left.Equals(right);
        /// <inheritdoc/>
        public static bool operator !=(VariantValue left, object right) =>
            !left.Equals(right);
        /// <inheritdoc/>
        public static bool operator ==(object left, VariantValue right) =>
            right.Equals(left);
        /// <inheritdoc/>
        public static bool operator !=(object left, VariantValue right) =>
            !right.Equals(left);

        /// <inheritdoc/>
        public override bool Equals(object o) {
            if (o is null) {
                return this.IsNull();
            }
            if (o is VariantValue v) {
                if (this.IsNull() && v.IsNull()) {
                    return true;
                }
                return v.DeepEquals(this);
            }
            return ValueEquals(o);
        }

        /// <inheritdoc/>
        public override string ToString() {
            return ToString(Formatting.None);
        }

        /// <inheritdoc/>
        public override int GetHashCode() {
            return GetDeepHashCode();
        }

        /// <inheritdoc/>
        public object Clone() {
            return Copy();
        }

        /// <summary>
        /// Clone this item or entire tree
        /// </summary>
        /// <returns></returns>
        public VariantValue DeepClone() {
            return Copy();
        }

        /// <summary>
        /// Convert value to typed value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T ToObject<T>(IFormatProvider provider = null) {
            return (T)ToType(typeof(T), provider);
        }

        /// <summary>
        /// Convert value to typed value
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public object ToObject(Type type) {
            return ToType(type, null);
        }

        /// <summary>
        /// Convert to string
        /// </summary>
        /// <param name="format"></param>
        /// <returns></returns>
        public abstract string ToString(Formatting format);

        /// <summary>
        /// Convert typed object to value
        /// </summary>
        /// <param name="value"></param>
        public abstract void Set(object value);

        /// <summary>
        /// Create new value which is set to null.
        /// </summary>
        /// <returns></returns>
        protected abstract VariantValue Null();

        /// <summary>
        /// Clone this item or entire tree
        /// </summary>
        /// <returns></returns>
        public abstract VariantValue Copy(bool shallow = false);

        /// <summary>
        /// Get value for property
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="compare"></param>
        /// <returns></returns>
        public abstract bool TryGetValue(string key, out VariantValue value,
            StringComparison compare = StringComparison.InvariantCultureIgnoreCase);

        /// <summary>
        /// Get value from array index
        /// </summary>
        /// <param name="index"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public abstract bool TryGetValue(int index, out VariantValue value);

        /// <summary>
        /// Select value using path
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public abstract VariantValue SelectToken(string path);

        /// <summary>
        /// Create hash code for this or entire tree.
        /// </summary>
        /// <returns></returns>
        protected abstract int GetDeepHashCode();

        /// <summary>
        /// Compare to object
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        protected abstract bool ValueEquals(object o);

        /// <summary>
        /// Compare to variant
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        protected abstract bool DeepEquals(VariantValue o);


        /// <summary>
        /// Represents a primitive value for assignment purposes
        /// </summary>
        internal sealed class PrimitiveValue : VariantValue {

            /// <inheritdoc/>
            public override VariantValueType Type { get; }

            /// <inheritdoc/>
            public override object Value { get; }

            /// <inheritdoc/>
            public override IEnumerable<string> Keys =>
                throw new NotSupportedException("Not an object");

            /// <inheritdoc/>
            public override IEnumerable<VariantValue> Values =>
                throw new NotSupportedException("Not an array");

            /// <inheritdoc/>
            public override int Count =>
                throw new NotSupportedException("Not an array");

            /// <summary>
            /// Clone
            /// </summary>
            /// <param name="value"></param>
            /// <param name="type"></param>
            internal PrimitiveValue(object value, VariantValueType type) {
                Value = value;
                Type = type;
            }

            /// <inheritdoc/>
            public PrimitiveValue(string value) {
                Value = value;
                Type = value == null ? VariantValueType.Null : VariantValueType.String;
            }

            /// <inheritdoc/>
            public PrimitiveValue(byte[] value) {
                Value = value;
                Type = value == null ? VariantValueType.Null : VariantValueType.Bytes;
            }

            /// <inheritdoc/>
            public PrimitiveValue(bool value) {
                Value = value;
                Type = VariantValueType.Boolean;
            }

            /// <inheritdoc/>
            public PrimitiveValue(byte value) {
                Value = value;
                Type = VariantValueType.Integer;
            }

            /// <inheritdoc/>
            public PrimitiveValue(sbyte value) {
                Value = value;
                Type = VariantValueType.Integer;
            }

            /// <inheritdoc/>
            public PrimitiveValue(short value) {
                Value = value;
                Type = VariantValueType.Integer;
            }

            /// <inheritdoc/>
            public PrimitiveValue(ushort value) {
                Value = value;
                Type = VariantValueType.Integer;
            }

            /// <inheritdoc/>
            public PrimitiveValue(int value) {
                Value = value;
                Type = VariantValueType.Integer;
            }

            /// <inheritdoc/>
            public PrimitiveValue(uint value) {
                Value = value;
                Type = VariantValueType.Integer;
            }

            /// <inheritdoc/>
            public PrimitiveValue(long value) {
                Value = value;
                Type = VariantValueType.Integer;
            }

            /// <inheritdoc/>
            public PrimitiveValue(ulong value) {
                Value = value;
                Type = VariantValueType.Integer;
            }

            /// <inheritdoc/>
            public PrimitiveValue(float value) {
                Value = value;
                Type = VariantValueType.Float;
            }

            /// <inheritdoc/>
            public PrimitiveValue(double value) {
                Value = value;
                Type = VariantValueType.Float;
            }

            /// <inheritdoc/>
            public PrimitiveValue(decimal value) {
                Value = value;
                Type = VariantValueType.Float;
            }

            /// <inheritdoc/>
            public PrimitiveValue(Guid value) {
                Value = value.ToString();
                Type = VariantValueType.String;
            }

            /// <inheritdoc/>
            public PrimitiveValue(DateTime value) {
                Value = value;
                Type = VariantValueType.Date;
            }

            /// <inheritdoc/>
            public PrimitiveValue(DateTimeOffset value) {
                Value = value;
                Type = VariantValueType.Date;
            }

            /// <inheritdoc/>
            public PrimitiveValue(TimeSpan value) {
                Value = value;
                Type = VariantValueType.Date;
            }

            /// <inheritdoc/>
            public PrimitiveValue(bool? value) {
                Value = value;
                Type = value == null ? VariantValueType.Null : VariantValueType.Boolean;
            }

            /// <inheritdoc/>
            public PrimitiveValue(byte? value) {
                Value = value;
                Type = value == null ? VariantValueType.Null : VariantValueType.Integer;
            }

            /// <inheritdoc/>
            public PrimitiveValue(sbyte? value) {
                Value = value;
                Type = value == null ? VariantValueType.Null : VariantValueType.Integer;
            }

            /// <inheritdoc/>
            public PrimitiveValue(short? value) {
                Value = value;
                Type = value == null ? VariantValueType.Null : VariantValueType.Integer;
            }

            /// <inheritdoc/>
            public PrimitiveValue(ushort? value) {
                Value = value;
                Type = value == null ? VariantValueType.Null : VariantValueType.Integer;
            }

            /// <inheritdoc/>
            public PrimitiveValue(int? value) {
                Value = value;
                Type = value == null ? VariantValueType.Null : VariantValueType.Integer;
            }

            /// <inheritdoc/>
            public PrimitiveValue(uint? value) {
                Value = value;
                Type = value == null ? VariantValueType.Null : VariantValueType.Integer;
            }

            /// <inheritdoc/>
            public PrimitiveValue(long? value) {
                Value = value;
                Type = value == null ? VariantValueType.Null : VariantValueType.Integer;
            }

            /// <inheritdoc/>
            public PrimitiveValue(ulong? value) {
                Value = value;
                Type = value == null ? VariantValueType.Null : VariantValueType.Integer;
            }

            /// <inheritdoc/>
            public PrimitiveValue(float? value) {
                Value = value;
                Type = value == null ? VariantValueType.Null : VariantValueType.Float;
            }

            /// <inheritdoc/>
            public PrimitiveValue(double? value) {
                Value = value;
                Type = value == null ? VariantValueType.Null : VariantValueType.Float;
            }

            /// <inheritdoc/>
            public PrimitiveValue(decimal? value) {
                Value = value;
                Type = value == null ? VariantValueType.Null : VariantValueType.Float;
            }

            /// <inheritdoc/>
            public PrimitiveValue(Guid? value) {
                Value = value.ToString();
                Type = value == null ? VariantValueType.Null : VariantValueType.String;
            }

            /// <inheritdoc/>
            public PrimitiveValue(DateTime? value) {
                Value = value;
                Type = value == null ? VariantValueType.Null : VariantValueType.Date;
            }

            /// <inheritdoc/>
            public PrimitiveValue(DateTimeOffset? value) {
                Value = value;
                Type = value == null ? VariantValueType.Null : VariantValueType.Date;
            }

            /// <inheritdoc/>
            public PrimitiveValue(TimeSpan? value) {
                Value = value;
                Type = value == null ? VariantValueType.Null : VariantValueType.Date;
            }

            /// <inheritdoc/>
            public override VariantValue Copy(bool shallow = false) {
                return new PrimitiveValue(Value, Type);
            }

            /// <inheritdoc/>
            public override string ToString(Formatting format) {
                switch (Value) {
                    case null:
                        return "null";
                    case byte[] b:
                        return Convert.ToBase64String(b);
                    default:
                        return Value.ToString();
                }
            }

            /// <inheritdoc/>
            protected override bool ValueEquals(object o) {
                if (ReferenceEquals(Value, o)) {
                    return true;
                }

                if (o is byte[] b1 && Value is byte[] b2) {
                    return b1.AsSpan().SequenceEqual(b2);
                }

                // ...

                // Default to generic comparison
                return o.Equals(Value);
            }

            /// <inheritdoc/>
            protected override bool DeepEquals(VariantValue v) {
                if (ReferenceEquals(this, v)) {
                    return true;
                }

                // Compare to our primitive value
                return v.ValueEquals(Value);
            }

            /// <inheritdoc/>
            protected override int GetDeepHashCode() {
                return Value?.GetHashCode() ?? 0;
            }

            /// <inheritdoc/>
            public override object ToType(Type conversionType, IFormatProvider provider) {
                if (Value is null) {
                    return null;
                }
                if (Value is IConvertible c) {
                    return c.ToType(conversionType, provider);
                }
                return null;
            }

            /// <inheritdoc/>
            public override VariantValue SelectToken(string path) {
                throw new NotSupportedException("Not an object");
            }

            /// <inheritdoc/>
            public override void Set(object value) {
                throw new NotSupportedException("Not an object");
            }

            /// <inheritdoc/>
            public override bool TryGetValue(string key, out VariantValue value,
                StringComparison compare) {
                throw new NotSupportedException("Not an object");
            }

            /// <inheritdoc/>
            public override bool TryGetValue(int index, out VariantValue value) {
                throw new NotSupportedException("Not an array");
            }

            /// <inheritdoc/>
            protected override VariantValue Null() {
                throw new NotSupportedException("Not an object");
            }
        }

        /// <summary>
        /// Helper methods
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns></returns>
        public static bool DeepEquals(VariantValue value1, VariantValue value2) {
            if (value1.IsNull() && value2.IsNull()) {
                return true;
            }
            return value1?.Equals(value2) ?? false;
        }
    }
}