// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------

namespace Microsoft.Azure.IIoT.Serializers {
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Linq;
    using Xunit;
    using Newtonsoft.Json.Linq;
    using Newtonsoft.Json;

    public class SerializerTests {

        public virtual IJsonSerializer Serializer => new NewtonSoftJsonSerializer();

        public static IEnumerable<(VariantValue, object)> GetStrings() {
            yield return ("", "");
            yield return ("str ing", "str ing");
            yield return ("{}", "{}");
            yield return (new byte[0], new byte[0]);
            yield return (new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 }, new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 });
            yield return (Encoding.UTF8.GetBytes("utf-8-string"), Encoding.UTF8.GetBytes("utf-8-string"));
        }

        public static IEnumerable<(VariantValue, object)> GetValues() {
            yield return (true, true);
            yield return (false, false);
            yield return ((sbyte)1, (sbyte)1);
            yield return ((sbyte)-1, (sbyte)-1);
            yield return ((sbyte)0, (sbyte)0);
            yield return (sbyte.MaxValue, sbyte.MaxValue);
            yield return (sbyte.MinValue, sbyte.MinValue);
            yield return ((short)1, (short)1);
            yield return ((short)-1, (short)-1);
            yield return ((short)0, (short)0);
            yield return (short.MaxValue, short.MaxValue);
            yield return (short.MinValue, short.MinValue);
            yield return (1, 1);
            yield return (-1, -1);
            yield return (0, 0);
            yield return (int.MaxValue, int.MaxValue);
            yield return (int.MinValue, int.MinValue);
            yield return (1L, 1L);
            yield return (-1L, -1L);
            yield return (0L, 0L);
            yield return (long.MaxValue, long.MaxValue);
            yield return (long.MinValue, long.MinValue);
            yield return (1UL, 1UL);
            yield return (0UL, 0UL);
            yield return (ulong.MaxValue, ulong.MaxValue);
            yield return (1u, 1u);
            yield return (0u, 0u);
            yield return (uint.MaxValue, uint.MaxValue);
            yield return ((ushort)1, (ushort)1);
            yield return ((ushort)0, (ushort)0);
            yield return (ushort.MaxValue, ushort.MaxValue);
            yield return ((byte)1, (byte)1);
            yield return ((byte)0, (byte)0);
            yield return (byte.MaxValue, byte.MaxValue);
            yield return (1.0, 1.0);
            yield return (-1.0, -1.0);
            yield return (0.0, 0.0);
            yield return (double.MaxValue, double.MaxValue);
            yield return (double.MinValue, double.MinValue);
            yield return (double.PositiveInfinity, double.PositiveInfinity);
            yield return (double.NegativeInfinity, double.NegativeInfinity);
            yield return (1.0f, 1.0f);
            yield return (-1.0f, -1.0f);
            yield return (0.0f, 0.0f);
            yield return (float.MaxValue, float.MaxValue);
            yield return (float.MinValue, float.MinValue);
            yield return (float.PositiveInfinity, float.PositiveInfinity);
            yield return (float.NegativeInfinity, float.NegativeInfinity);
            yield return ((decimal)1.0, (decimal)1.0);
            yield return ((decimal)-1.0, (decimal)-1.0);
            yield return ((decimal)0.0, (decimal)0.0);
            yield return ((decimal)1234567, (decimal)1234567);
          //  yield return (decimal.MaxValue, decimal.MaxValue);
          //  yield return (decimal.MinValue, decimal.MinValue);
            var guid = Guid.NewGuid();
            yield return (guid, guid);
            yield return (Guid.Empty, Guid.Empty);
            var now1 = DateTime.UtcNow;
            yield return (now1, now1);
            yield return (DateTime.MaxValue, DateTime.MaxValue);
            yield return (DateTime.MinValue, DateTime.MinValue);
            var now2 = DateTimeOffset.UtcNow;
            yield return (now2, now2);
            // TODO FIX yield return (DateTimeOffset.MaxValue, DateTimeOffset.MaxValue);
            // TODO FIX yield return (DateTimeOffset.MinValue, DateTimeOffset.MinValue);
            yield return (TimeSpan.Zero, TimeSpan.Zero);
            yield return (TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(5));
            yield return (TimeSpan.FromDays(5555), TimeSpan.FromDays(5555));
            yield return (TimeSpan.MaxValue, TimeSpan.MaxValue);
            yield return (TimeSpan.MinValue, TimeSpan.MinValue);
        }


        [Theory]
        [MemberData(nameof(GetScalars))]
        [MemberData(nameof(GetEmptyArrays))]
        [MemberData(nameof(GetFilledArrays))]
        public void SerializerDeserializer(object o, Type type) {
            var result = Serializer.Deserialize(Serializer.Serialize(o), type);
            Assert.NotNull(result);
            Assert.Equal(o, result);
            Assert.Equal(o.GetType(), result.GetType());
        }

        [Theory]
        [MemberData(nameof(GetNulls))]
        public void SerializerDeserializerNullable(Type type) {
            var result = Serializer.Deserialize(Serializer.Serialize(null), type);
            Assert.Null(result);
        }

        [Theory]
        [MemberData(nameof(GetScalars))]
        [MemberData(nameof(GetEmptyArrays))]
        [MemberData(nameof(GetFilledArrays))]
        public void SerializerArrayVariant(object o, Type type) {
            var expected = type.MakeArrayType();
            var result = Serializer.FromArray(o, o, o);
            Assert.NotNull(result);
            Assert.True(result.Type == VariantValueType.Array);
            Assert.True(result.Count == 3);
        }

        [Theory]
        [MemberData(nameof(GetScalars))]
        [MemberData(nameof(GetEmptyArrays))]
        [MemberData(nameof(GetFilledArrays))]
        public void SerializerArrayVariantToObject(object o, Type type) {
            var expected = type.MakeArrayType();
            var array = Serializer.FromArray(o, o, o).ToObject(expected);

            Assert.NotNull(array);
            Assert.Equal(expected, array.GetType());
        }

        [Theory]
        [MemberData(nameof(GetScalars))]
        [MemberData(nameof(GetEmptyArrays))]
        [MemberData(nameof(GetFilledArrays))]
        public void SerializerVariant(object o, Type type) {
            var result = Serializer.FromObject(o).ToObject(type);
            Assert.NotNull(result);
            Assert.Equal(o, result);
            Assert.Equal(o.GetType(), result.GetType());
        }

        [Theory]
        [MemberData(nameof(GetNulls))]
        public void SerializerVariantNullable(Type type) {
            var result = Serializer.FromObject(null).ToObject(type);
            Assert.Null(result);
        }

        [Theory]
        [MemberData(nameof(GetVariantValueAndValue))]
        public void SerializerSerialzeValueToStringAndCompare(VariantValue v, object o) {
            var actual = Serializer.Serialize(v);
            var expected = Serializer.Serialize(o);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [MemberData(nameof(GetVariantValueAndValue))]
        public void JsonConvertToJTokenAndStringCompare(VariantValue v, object o) {
            var expected = JsonConvert.SerializeObject(JToken.FromObject(o),
                new NewtonSoftJsonConverters().GetSettings());
            var actual = Serializer.Serialize(v);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [MemberData(nameof(GetVariantValueAndValue))]
        public void JsonConvertRawAndStringCompare(VariantValue v, object o) {
            var expected = JsonConvert.SerializeObject(o,
                new NewtonSoftJsonConverters().GetSettings());
            var actual = Serializer.Serialize(v);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [MemberData(nameof(GetVariantValues))]
        public void SerializerStringParse(VariantValue v) {
            var expected = v;
            var json = Serializer.Serialize(v);
            var actual = Serializer.Parse(json);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [MemberData(nameof(GetVariantValues))]
        public void SerializerFromObject(VariantValue v) {
            var expected = v;
            var actual = Serializer.FromObject(v);
            Assert.Equal(expected, actual);
        }


        public static IEnumerable<object[]> GetNulls() {
            return GetStrings()
                .Select(v => new object[] { v.Item2.GetType() })
                .Concat(GetValues()
                .Select(v => new object[] {
                    typeof(Nullable<>).MakeGenericType(v.Item2.GetType()) }));
        }

        public static IEnumerable<object[]> GetScalars() {
            return GetStrings()
                .Select(v => new object[] { v.Item2, v.Item2.GetType() })
                .Concat(GetValues()
                .Select(v => new object[] { v.Item2, v.Item2.GetType() })
                .Concat(GetValues()
                .Select(v => new object[] { v.Item2,
                    typeof(Nullable<>).MakeGenericType(v.Item2.GetType()) })));
        }

        public static IEnumerable<object[]> GetFilledArrays() {
            return GetStrings()
                .Select(v => new object[] { CreateArray(v.Item2, v.Item2.GetType(), 10),
                    v.Item2.GetType().MakeArrayType()})
                .Concat(GetValues()
                .Select(v => new object[] { CreateArray(v.Item2, v.Item2.GetType(), 10),
                    v.Item2.GetType().MakeArrayType() }));
        }

        public static IEnumerable<object[]> GetEmptyArrays() {
            return GetStrings()
                .Select(v => new object[] { CreateArray(null, v.Item2.GetType(), 10),
                    v.Item2.GetType().MakeArrayType()})
                .Concat(GetValues()
                .Select(v => new object[] { CreateArray(null, v.Item2.GetType(), 10),
                    v.Item2.GetType().MakeArrayType() }));
        }

        public static IEnumerable<object[]> GetVariantValues() {
            return GetStrings()
                .Select(v => new object[] { v.Item1 })
                .Concat(GetValues()
                .Select(v => new object[] { v.Item1 }));
        }

        public static IEnumerable<object[]> GetVariantValueAndValue() {
            return GetStrings()
                .Select(v => new object[] { v.Item1, v.Item2 })
                .Concat(GetValues()
                .Select(v => new object[] { v.Item1, v.Item2 }));
        }

        private static object CreateArray(object value, Type type, int size) {
            var array = Array.CreateInstance(type, size);
            if (value != null) {
                for (var i = 0; i < size; i++) {
                    array.SetValue(value, i);
                }
            }
            return array;
        }
    }
}
