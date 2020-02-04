// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------

namespace Microsoft.Azure.IIoT.Serializers {
    using System;
    using System.Collections.Generic;
    using System.Dynamic;
    using System.Linq.Expressions;

    /// <summary>
    /// Represents a primitive value for assignment purposes
    /// </summary>
    public class PrimitiveValue : VariantValue {

        /// <inheritdoc/>
        public override VariantValueType Type { get; }

        /// <inheritdoc/>
        public override object Value { get; }

        /// <inheritdoc/>
        public override IEnumerable<string> Keys {
            get => throw new NotSupportedException("Not an object");
        }

        /// <inheritdoc/>
        public override int Count {
            get => throw new NotSupportedException("Not an array");
        }

        /// <summary>
        /// Clone
        /// </summary>
        /// <param name="value"></param>
        /// <param name="type"></param>
        private PrimitiveValue(object value, VariantValueType type) {
            Value = value;
            Type = type;
        }

        /// <inheritdoc/>
        public PrimitiveValue(string value) {
            Value = value;
            Type = VariantValueType.String;
        }

        /// <inheritdoc/>
        public PrimitiveValue(byte[] value) {
            Value = value;
            Type = VariantValueType.Bytes;
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
        public PrimitiveValue(byte? value) {
            Value = value;
            Type = VariantValueType.Integer;
        }

        /// <inheritdoc/>
        public PrimitiveValue(sbyte? value) {
            Value = value;
            Type = VariantValueType.Integer;
        }

        /// <inheritdoc/>
        public PrimitiveValue(short? value) {
            Value = value;
            Type = VariantValueType.Integer;
        }

        /// <inheritdoc/>
        public PrimitiveValue(ushort? value) {
            Value = value;
            Type = VariantValueType.Integer;
        }

        /// <inheritdoc/>
        public PrimitiveValue(int? value) {
            Value = value;
            Type = VariantValueType.Integer;
        }

        /// <inheritdoc/>
        public PrimitiveValue(uint? value) {
            Value = value;
            Type = VariantValueType.Integer;
        }

        /// <inheritdoc/>
        public PrimitiveValue(long? value) {
            Value = value;
            Type = VariantValueType.Integer;
        }

        /// <inheritdoc/>
        public PrimitiveValue(ulong? value) {
            Value = value;
            Type = VariantValueType.Integer;
        }

        /// <inheritdoc/>
        public PrimitiveValue(float? value) {
            Value = value;
            Type = VariantValueType.Float;
        }

        /// <inheritdoc/>
        public PrimitiveValue(double? value) {
            Value = value;
            Type = VariantValueType.Float;
        }

        /// <inheritdoc/>
        public PrimitiveValue(decimal? value) {
            Value = value;
            Type = VariantValueType.Float;
        }

        /// <inheritdoc/>
        public PrimitiveValue(Guid? value) {
            Value = value.ToString();
            Type = VariantValueType.String;
        }

        /// <inheritdoc/>
        public PrimitiveValue(DateTime? value) {
            Value = value;
            Type = VariantValueType.Date;
        }

        /// <inheritdoc/>
        public override VariantValue Copy(bool shallow = false) {
            return new PrimitiveValue(Value, Type);
        }

        /// <inheritdoc/>
        public override IEnumerator<VariantValue> GetEnumerator() {
            throw new NotSupportedException("Not an array");
        }

        /// <inheritdoc/>
        public override DynamicMetaObject GetMetaObject(Expression parameter) {
            return new DynamicMetaObject(parameter, BindingRestrictions.Empty, this);
        }

        /// <inheritdoc/>
        public override string ToString(Formatting format) {
            return Value.ToString();
        }

        /// <inheritdoc/>
        protected override bool DeepEquals(object o) {
            return Value == o;
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
}