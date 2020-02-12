// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------

namespace Microsoft.Azure.IIoT.Serializers {
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System;
    using System.ComponentModel;
    using System.Numerics;

    /// <summary>
    /// Represents primitive or structurally complex value
    /// </summary>
    public abstract class VariantValue : ICloneable, IConvertible, IComparable {

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
            return ToObject<bool>(provider);
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
            return ToObject<byte>(provider);
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
            return ToObject<char>(provider);
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
            return ToObject<DateTime>(provider);
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
            return ToObject<decimal>(provider);
        }
        /// <inheritdoc/>
        public static explicit operator decimal(VariantValue value) {
            return value.ToObject<decimal>();
        }
        /// <inheritdoc/>
        public static explicit operator decimal?(VariantValue value) {
            return value.IsNull() ? (decimal?)null : value.ToObject<decimal>();
        }
        /// <inheritdoc/>
        public static implicit operator VariantValue(decimal value) {
            return new PrimitiveValue(value);
        }
        /// <inheritdoc/>
        public static implicit operator VariantValue(decimal? value) {
            return new PrimitiveValue(value);
        }

        /// <inheritdoc/>
        public double ToDouble(IFormatProvider provider) {
            return ToObject<double>(provider);
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
            return ToObject<short>(provider);
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
            return ToObject<int>(provider);
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
            return ToObject<long>(provider);
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
            return ToObject<ushort>(provider);
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
            return ToObject<uint>(provider);
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
            return ToObject<string>(provider);
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
            left?.Equals(right) ?? right?.Equals(null) ?? true;
        /// <inheritdoc/>
        public static bool operator !=(VariantValue left, VariantValue right) =>
            !(left == right);
        /// <inheritdoc/>
        public static bool operator >(VariantValue left, VariantValue right) =>
            left is null ? right != null : left.CompareTo(right) > 0;
        /// <inheritdoc/>
        public static bool operator <(VariantValue left, VariantValue right) =>
            left is null ? right is null : left.CompareTo(right) < 0;
        /// <inheritdoc/>
        public static bool operator >=(VariantValue left, VariantValue right) =>
            left is null ? right != null : left.CompareTo(right) >= 0;
        /// <inheritdoc/>
        public static bool operator <=(VariantValue left, VariantValue right) =>
            left is null ? right is null : left.CompareTo(right) <= 0;

        /// <inheritdoc/>
        public override bool Equals(object o) {
            if (o is null) {
                return this.IsNull();
            }
            if (o is VariantValue v) {
                if (this.IsNull() && v.IsNull()) {
                    return true;
                }

                // Variant compare
                return v.EqualsVariant(this);
            }

            // Non variant compare
            return EqualsValue(o);
        }

        /// <inheritdoc/>
        public override string ToString() {
            return ToString(SerializeOption.None);
        }

        /// <inheritdoc/>
        public override int GetHashCode() {
            return GetDeepHashCode();
        }

        /// <inheritdoc/>
        public object Clone() {
            return Copy();
        }

        /// <inheritdoc/>
        public int CompareTo(object o) {
            if (Equals(o)) {
                return 0;
            }

            if (o is VariantValue v) {
                if (this.IsNull() && v.IsNull()) {
                    return 0;
                }
                if (TryCompareToVariantValue(v, out var r)) {
                    return r;
                }
            }
            else {
                // Compare to non variant value
                if (TryCompareToValue(o, out var result)) {
                    return result;
                }
            }

            // Compare stringified version
            var s1 = this.IsNull() ? "null" : Value.ToString();
            var s2 = o is null ? "null" : o.ToString();
            return s1.CompareTo(s2);
        }

        /// <summary>
        /// Convert value to typed value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T ToObject<T>(IFormatProvider provider = null) {
            var typed = ToType(typeof(T), provider);
            return typed == null ? default : (T)typed;
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
        public abstract string ToString(SerializeOption format);

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
        protected abstract bool EqualsValue(object o);

        /// <summary>
        /// Compare to variant
        /// </summary>
        /// <param name="that"></param>
        /// <returns></returns>
        protected virtual bool EqualsVariant(VariantValue that) {
            if (ReferenceEquals(this, that)) {
                return true;
            }
            if (that.Type != Type) {
                return false;
            }
            switch (Type) {
                case VariantValueType.Null:
                case VariantValueType.Undefined:
                    return true;
                case VariantValueType.String:
                    return that.Value.ToString() == Value.ToString();
                case VariantValueType.Array:
                    return that.Values.SequenceEqual(Values, EqualityComparer);
                case VariantValueType.Object:
                    var p1 = that.Keys.OrderBy(k => k).Select(k => that[k]);
                    var p2 =      Keys.OrderBy(k => k).Select(k => this[k]);
                    return p1.SequenceEqual(p2, EqualityComparer);
                case VariantValueType.Boolean:
                    return (bool)that.Value == (bool)Value;
                case VariantValueType.Bytes:
                    return ((byte[])that.Value).SequenceEqual((byte[])Value);
                case VariantValueType.Integer:
                    return that.Value.ToString() == Value.ToString();
                case VariantValueType.Float:
                    var dbl1 = (decimal)that.Value;
                    var dbl2 = (decimal)Value;
                    return dbl1 == dbl2;
                case VariantValueType.Date:
                    var dto1 = (DateTimeOffset)that.Value;
                    var dto2 = (DateTimeOffset)Value;
                    return dto1 == dto2;
                case VariantValueType.TimeSpan:
                    var ts1 = (TimeSpan)that.Value;
                    var ts2 = (TimeSpan)Value;
                    return ts1 == ts2;
                default:
                    return false;
            }
        }

        /// <summary>
        /// Compare value
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        protected abstract bool TryCompareToValue(object obj, out int result);

        /// <summary>
        /// Compare variant value
        /// </summary>
        /// <param name="v"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        protected virtual bool TryCompareToVariantValue(VariantValue v,
            out int result) {
            // Failed - now try with inner value and compare
            var o = v.IsNull() ? null : v.Value;
            // Compare to non variant value
            return TryCompareToValue(o, out result);
        }

        /// <summary>
        /// Internal equality comparer
        /// </summary>
        internal static VariantValueEqualityComparer EqualityComparer =>
            new VariantValueEqualityComparer();

        /// <inheritdoc/>
        internal class VariantValueEqualityComparer : IEqualityComparer<VariantValue> {

            /// <inheritdoc/>
            public bool Equals(VariantValue x, VariantValue y) {
                return x.EqualsVariant(y);
            }

            /// <inheritdoc/>
            public int GetHashCode(VariantValue obj) {
                return obj.GetDeepHashCode();
            }
        }

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
                Enumerable.Empty<string>();

            /// <inheritdoc/>
            public override IEnumerable<VariantValue> Values =>
                Enumerable.Empty<VariantValue>();

            /// <inheritdoc/>
            public override int Count => 0;

            /// <summary>
            /// Clone
            /// </summary>
            /// <param name="value"></param>
            /// <param name="type"></param>
            internal PrimitiveValue(object value, VariantValueType type) {
                Value = value;
                Type = value is null ? VariantValueType.Null : type;
            }

            /// <inheritdoc/>
            public PrimitiveValue(string value) :
                this (value, VariantValueType.String) {
            }

            /// <inheritdoc/>
            public PrimitiveValue(byte[] value) :
                this(value, VariantValueType.Bytes) {
            }

            /// <inheritdoc/>
            public PrimitiveValue(bool value) :
                this(value, VariantValueType.Boolean) {
            }

            /// <inheritdoc/>
            public PrimitiveValue(byte value) :
                this(value, VariantValueType.Integer) {
            }

            /// <inheritdoc/>
            public PrimitiveValue(sbyte value) :
                this(value, VariantValueType.Integer) {
            }

            /// <inheritdoc/>
            public PrimitiveValue(short value) :
                this(value, VariantValueType.Integer) {
            }

            /// <inheritdoc/>
            public PrimitiveValue(ushort value) :
                this(value, VariantValueType.Integer) {
            }

            /// <inheritdoc/>
            public PrimitiveValue(int value) :
                this(value, VariantValueType.Integer) {
            }

            /// <inheritdoc/>
            public PrimitiveValue(uint value) :
                this(value, VariantValueType.Integer) {
            }

            /// <inheritdoc/>
            public PrimitiveValue(long value) :
                this(value, VariantValueType.Integer) {
            }

            /// <inheritdoc/>
            public PrimitiveValue(ulong value) :
                this(value, VariantValueType.Integer) {
            }

            /// <inheritdoc/>
            public PrimitiveValue(float value) :
                this(value, VariantValueType.Float) {
            }

            /// <inheritdoc/>
            public PrimitiveValue(double value) :
                this(value, VariantValueType.Float) {
            }

            /// <inheritdoc/>
            public PrimitiveValue(decimal value) :
                this(value, VariantValueType.Float) {
            }

            /// <inheritdoc/>
            public PrimitiveValue(Guid value) :
                this(value, VariantValueType.String) {
            }

            /// <inheritdoc/>
            public PrimitiveValue(DateTime value) :
                this(value, VariantValueType.Date) {
            }

            /// <inheritdoc/>
            public PrimitiveValue(DateTimeOffset value) :
                this(value, VariantValueType.Date) {
            }

            /// <inheritdoc/>
            public PrimitiveValue(TimeSpan value) :
                this(value, VariantValueType.TimeSpan) {
            }

            /// <inheritdoc/>
            public PrimitiveValue(bool? value) :
                this(value, VariantValueType.Boolean) {
            }

            /// <inheritdoc/>
            public PrimitiveValue(byte? value) :
                this(value, VariantValueType.Integer) {
            }

            /// <inheritdoc/>
            public PrimitiveValue(sbyte? value) :
                this(value, VariantValueType.Integer) {
            }

            /// <inheritdoc/>
            public PrimitiveValue(short? value) :
                this(value, VariantValueType.Integer) {
            }

            /// <inheritdoc/>
            public PrimitiveValue(ushort? value) :
                this(value, VariantValueType.Integer) {
            }

            /// <inheritdoc/>
            public PrimitiveValue(int? value) :
                this(value, VariantValueType.Integer) {
            }

            /// <inheritdoc/>
            public PrimitiveValue(uint? value) :
                this(value, VariantValueType.Integer) {
            }

            /// <inheritdoc/>
            public PrimitiveValue(long? value) :
                this(value, VariantValueType.Integer) {
            }

            /// <inheritdoc/>
            public PrimitiveValue(ulong? value) :
                this(value, VariantValueType.Integer) {
            }

            /// <inheritdoc/>
            public PrimitiveValue(float? value) :
                this(value, VariantValueType.Float) {
            }

            /// <inheritdoc/>
            public PrimitiveValue(double? value) :
                this(value, VariantValueType.Float) {
            }

            /// <inheritdoc/>
            public PrimitiveValue(decimal? value) :
                this(value, VariantValueType.Float) {
            }

            /// <inheritdoc/>
            public PrimitiveValue(Guid? value) :
                this(value, VariantValueType.String) {
            }

            /// <inheritdoc/>
            public PrimitiveValue(DateTime? value) :
                this(value, VariantValueType.Date) {
            }

            /// <inheritdoc/>
            public PrimitiveValue(DateTimeOffset? value) :
                this(value, VariantValueType.Date) {
            }

            /// <inheritdoc/>
            public PrimitiveValue(TimeSpan? value) :
                this(value, VariantValueType.TimeSpan) {
            }

            /// <inheritdoc/>
            public override VariantValue Copy(bool shallow = false) {
                return new PrimitiveValue(Value, Type);
            }

            /// <inheritdoc/>
            public override string ToString(SerializeOption format) {
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
            protected override bool EqualsValue(object o) {
                if (ReferenceEquals(Value, o)) {
                    return true;
                }

                if (o is byte[] b1 && Value is byte[] b2) {
                    return b1.AsSpan().SequenceEqual(b2);
                }

                if (o.Equals(Value)) {
                    return true;
                }

                if (Value is IConvertible co1) {
                    try {
                        var compare = co1.ToType(o.GetType(),
                            CultureInfo.InvariantCulture);
                        return compare.Equals(o);
                    }
                    catch {
                    }
                }
                if (o is IConvertible co2) {
                    try {
                        var compare = co2.ToType(Value.GetType(),
                            CultureInfo.InvariantCulture);
                        return compare.Equals(Value);
                    }
                    catch {
                    }
                }
                return false;
            }

            /// <inheritdoc/>
            protected override bool EqualsVariant(VariantValue v) {
                if (ReferenceEquals(this, v)) {
                    return true;
                }

                // Compare to our primitive value
                return v.EqualsValue(Value);
            }

            /// <inheritdoc/>
            protected override int GetDeepHashCode() {
                return Value?.GetHashCode() ?? 0;
            }

            /// <inheritdoc/>
            protected override bool TryCompareToValue(object obj, out int result) {
                result = 0;
                if (Value is IComparable cv1 && obj is IConvertible co1) {
                    try {
                        result = cv1.CompareTo(co1.ToType(Value.GetType(),
                            CultureInfo.InvariantCulture));
                        return true;
                    }
                    catch {
                        result = -1;
                    }
                }
                // Compare the other way around
                if (obj is IComparable cv2 && Value is IConvertible co2) {
                    try {
                        var compared = cv2.CompareTo(co2.ToType(Value.GetType(),
                            CultureInfo.InvariantCulture));
                        result = compared > 0 ? -1 : compared < 0 ? 1 : 0;
                        return true;
                    }
                    catch {
                        result = 1;
                    }
                }
                return false;
            }

            /// <inheritdoc/>
            public override object ToType(Type conversionType,
                IFormatProvider provider) {
                if (Value is null || Type == VariantValueType.Null) {
                    if (conversionType.IsValueType) {
                        return Activator.CreateInstance(conversionType);
                    }
                    return null;
                }
                if (conversionType.IsAssignableFrom(Value.GetType())) {
                    return Value;
                }
                if (Value is IConvertible c) {
                    return c.ToType(conversionType,
                        provider ?? CultureInfo.InvariantCulture);
                }
                var converter = TypeDescriptor.GetConverter(conversionType);
                return converter.ConvertFrom(Value);
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
                value = null;
                return false;
            }

            /// <inheritdoc/>
            public override bool TryGetValue(int index, out VariantValue value) {
                value = null;
                return false;
            }

            /// <inheritdoc/>
            protected override VariantValue Null() {
                return new PrimitiveValue(null, VariantValueType.Null);
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