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
            yield return (new byte[1000], new byte[1000]);
            yield return (new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 }, new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 });
            yield return (Encoding.UTF8.GetBytes("utf-8-string"), Encoding.UTF8.GetBytes("utf-8-string"));
        }

        public static IEnumerable<(VariantValue, object)> GetValues() {
            yield return (true, true);
            yield return (false, false);
            yield return ((bool?)null, (bool?)null);
            yield return ((sbyte)1, (sbyte)1);
            yield return ((sbyte)-1, (sbyte)-1);
            yield return ((sbyte)0, (sbyte)0);
            yield return (sbyte.MaxValue, sbyte.MaxValue);
            yield return (sbyte.MinValue, sbyte.MinValue);
            yield return ((sbyte?)null, (sbyte?)null);
            yield return ((short)1, (short)1);
            yield return ((short)-1, (short)-1);
            yield return ((short)0, (short)0);
            yield return (short.MaxValue, short.MaxValue);
            yield return (short.MinValue, short.MinValue);
            yield return ((short?)null, (short?)null);
            yield return (1, 1);
            yield return (-1, -1);
            yield return (0, 0);
            yield return (int.MaxValue, int.MaxValue);
            yield return (int.MinValue, int.MinValue);
            yield return ((int?)null, (int?)null);
            yield return (1L, 1L);
            yield return (-1L, -1L);
            yield return (0L, 0L);
            yield return (long.MaxValue, long.MaxValue);
            yield return (long.MinValue, long.MinValue);
            yield return ((long?)null, (long?)null);
            yield return (1UL, 1UL);
            yield return (0UL, 0UL);
            yield return (ulong.MaxValue, ulong.MaxValue);
            yield return ((ulong?)null, (ulong?)null);
            yield return (1u, 1u);
            yield return (0u, 0u);
            yield return (uint.MaxValue, uint.MaxValue);
            yield return ((uint?)null, (uint?)null);
            yield return ((ushort)1, (ushort)1);
            yield return ((ushort)0, (ushort)0);
            yield return (ushort.MaxValue, ushort.MaxValue);
            yield return ((ushort?)null, (ushort?)null);
            yield return ((byte)1, (byte)1);
            yield return ((byte)0, (byte)0);
            yield return (1.0, 1.0);
            yield return (-1.0, -1.0);
            yield return (0.0, 0.0);
            yield return (byte.MaxValue, byte.MaxValue);
            yield return ((byte?)null, (byte?)null);
            yield return (double.MaxValue, double.MaxValue);
            yield return (double.MinValue, double.MinValue);
            yield return (double.PositiveInfinity, double.PositiveInfinity);
            yield return (double.NegativeInfinity, double.NegativeInfinity);
            yield return ((double?)null, (double?)null);
            yield return (1.0f, 1.0f);
            yield return (-1.0f, -1.0f);
            yield return (0.0f, 0.0f);
            yield return (float.MaxValue, float.MaxValue);
            yield return (float.MinValue, float.MinValue);
            yield return (float.PositiveInfinity, float.PositiveInfinity);
            yield return (float.NegativeInfinity, float.NegativeInfinity);
            yield return ((float?)null, (float?)null);
            yield return ((decimal)1.0, (decimal)1.0);
            yield return ((decimal)-1.0, (decimal)-1.0);
            yield return ((decimal)0.0, (decimal)0.0);
            yield return ((decimal)1234567, (decimal)1234567);
            yield return ((decimal?)null, (decimal?)null);
            //  yield return (decimal.MaxValue, decimal.MaxValue);
            //  yield return (decimal.MinValue, decimal.MinValue);
            var guid = Guid.NewGuid();
            yield return (guid, guid);
            yield return (Guid.Empty, Guid.Empty);
            var now1 = DateTime.UtcNow;
            yield return (now1, now1);
            yield return (DateTime.MaxValue, DateTime.MaxValue);
            yield return (DateTime.MinValue, DateTime.MinValue);
            yield return ((DateTime?)null, (DateTime?)null);
            var now2 = DateTimeOffset.UtcNow;
            yield return (now2, now2);
            // TODO FIX yield return (DateTimeOffset.MaxValue, DateTimeOffset.MaxValue);
            // TODO FIX yield return (DateTimeOffset.MinValue, DateTimeOffset.MinValue);
            yield return ((DateTimeOffset?)null, (DateTimeOffset?)null);
            yield return (TimeSpan.Zero, TimeSpan.Zero);
            yield return (TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(5));
            yield return (TimeSpan.FromDays(5555), TimeSpan.FromDays(5555));
            yield return (TimeSpan.MaxValue, TimeSpan.MaxValue);
            yield return (TimeSpan.MinValue, TimeSpan.MinValue);
            yield return ((TimeSpan?)null, (TimeSpan?)null);
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
            var expected = JsonConvert.SerializeObject(
                o == null ? JValue.CreateNull() : JToken.FromObject(o),
                new NewtonSoftJsonSerializer().Settings);
            var actual = Serializer.Serialize(v);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [MemberData(nameof(GetVariantValueAndValue))]
        public void JsonConvertRawAndStringCompare(VariantValue v, object o) {
            var expected = JsonConvert.SerializeObject(o,
                new NewtonSoftJsonSerializer().Settings);
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

        [Fact]
        public void SerializeFromObjectsWithSameContent1() {
            var expected = Serializer.FromObject(new {
                Test = "Text",
                Locale = "de"
            });
            var actual = Serializer.FromObject(new {
                Locale = "de",
                Test = "Text"
            });
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void SerializeFromObjectsWithSameContent2() {
            var expected = Serializer.FromObject(new {
                Test = 1,
                LoCale = "de"
            });
            var actual = Serializer.FromObject(new {
                Locale = "de",
                TeSt = 1
            });
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void NullCompareTests() {
            VariantValue i1 = null;
            VariantValue i2 = null;
            VariantValue i3 = "test";
            VariantValue i4 = 0;
            VariantValue i5 = TimeSpan.FromSeconds(1);

            Assert.True(i1 == null);
            Assert.True(i1 is null);
            Assert.True(null == i1);
            Assert.True(i1 == i2);
            Assert.True(i1 != i3);
            Assert.True(i3 != i1);
            Assert.True(i1 != i4);
            Assert.True(i4 != i1);
            Assert.True(i4 != null);
            Assert.False(i4 == null);
            Assert.True(i3 != null);
            Assert.False(i3 == null);
            Assert.True(i5 != null);
            Assert.False(i5 == null);
        }

        [Fact]
        public void IntCompareTests() {
            VariantValue i1 = 1;
            VariantValue i2 = 2;
            VariantValue i3 = 2;

            Assert.True(i1 < i2);
            Assert.True(i1 <= i2);
            Assert.True(i2 > i1);
            Assert.True(i2 >= i1);
            Assert.True(i2 < 3);
            Assert.True(i2 <= 3);
            Assert.True(i2 <= 2);
            Assert.True(i2 <= i3);
            Assert.True(i2 >= 2);
            Assert.True(i2 >= i3);
            Assert.True(i2 != i1);
            Assert.True(i1 == 1);
            Assert.True(i2 == i3);
            Assert.True(i1 != 2);
            Assert.False(i2 == i1);
            Assert.False(i1 == 2);
        }

        [Fact]
        public void TimeSpanCompareTests() {
            VariantValue i1 = TimeSpan.FromSeconds(1);
            VariantValue i2 = TimeSpan.FromSeconds(2);
            VariantValue i3 = TimeSpan.FromSeconds(2);

            Assert.True(i1 < i2);
            Assert.True(i1 <= i2);
            Assert.True(i2 > i1);
            Assert.True(i2 >= i1);
            Assert.True(i2 < TimeSpan.FromSeconds(3));
            Assert.True(i2 <= TimeSpan.FromSeconds(3));
            Assert.True(i2 <= TimeSpan.FromSeconds(2));
            Assert.True(i2 <= i3);
            Assert.True(i2 >= TimeSpan.FromSeconds(2));
            Assert.True(i2 >= i3);
            Assert.True(i2 != i1);
            Assert.True(i1 == TimeSpan.FromSeconds(1));
            Assert.True(i2 == i3);
            Assert.True(i1 != TimeSpan.FromSeconds(2));
            Assert.False(i2 == i1);
            Assert.False(i1 == TimeSpan.FromSeconds(2));
        }

        [Fact]
        public void DateCompareTests() {
            VariantValue i1 = DateTime.MinValue;
            VariantValue i2 = DateTime.UtcNow;
            var i2a = i2.Copy();
            VariantValue i3 = DateTime.MaxValue;

            Assert.True(i1 < i2);
            Assert.True(i1 <= i2);
            Assert.True(i2 > i1);
            Assert.True(i2 >= i1);
            Assert.True(i2 < DateTime.MaxValue);
            Assert.True(i2 <= DateTime.MaxValue);
            Assert.True(i2 <= DateTime.UtcNow);
            Assert.True(i2 <= i3);
            Assert.True(i2 >= i2a);
            Assert.True(i2 == i2a);
            Assert.True(i2 >= DateTime.MinValue);
            Assert.False(i2 >= i3);
            Assert.True(i2 != i1);
            Assert.True(i1 == DateTime.MinValue);
            Assert.False(i2 == i3);
            Assert.True(i2 != i3);
            Assert.True(i1 != DateTime.UtcNow);
            Assert.False(i2 == i1);
            Assert.False(i1 == DateTime.UtcNow);
        }

        [Fact]
        public void FloatCompareTests() {
            VariantValue i1 = -0.123f;
            VariantValue i2 = 0.0f;
            VariantValue i2a = 0.0f;
            VariantValue i3 = 0.123f;

            Assert.True(i1 < i2);
            Assert.True(i1 <= i2);
            Assert.True(i2 > i1);
            Assert.True(i2 >= i1);
            Assert.True(i2 < 0.123f);
            Assert.True(i2 <= 0.123f);
            Assert.True(i2 <= 0.0f);
            Assert.True(i2 <= i3);
            Assert.True(i2 >= i2a);
            Assert.True(i2 == i2a);
            Assert.True(i2 >= -0.123f);
            Assert.False(i2 >= i3);
            Assert.True(i2 != i1);
            Assert.True(i1 == -0.123f);
            Assert.False(i2 == i3);
            Assert.True(i2 != i3);
            Assert.True(i1 != 0.0f);
            Assert.False(i2 == i1);
            Assert.False(i1 == 0.0f);
        }

        [Fact]
        public void DecimalCompareTests() {
            VariantValue i1 = -0.123m;
            VariantValue i2 = 0.0m;
            VariantValue i2a = 0.0m;
            VariantValue i3 = 0.123m;

            Assert.True(i1 < i2);
            Assert.True(i1 <= i2);
            Assert.True(i2 > i1);
            Assert.True(i2 >= i1);
            Assert.True(i2 < 0.123m);
            Assert.True(i2 < 0.123f);
            Assert.True(i2 <= 0.123m);
            Assert.True(i2 <= 0.123f);
            Assert.True(i2 <= 0.0m);
            Assert.True(i2 <= 0.0f);
            Assert.True(i2 <= 0.0);
            Assert.True(i2 <= i3);
            Assert.True(i2 >= i2a);
            Assert.True(i2 == i2a);
            Assert.True(i2 >= -0.123m);
            Assert.False(i2 >= i3);
            Assert.True(i2 != i1);
            Assert.True(i1 == -0.123m);
            Assert.True(i1 == -0.123f);
            Assert.False(i2 == i3);
            Assert.True(i2 != i3);
            Assert.True(i1 != 0.0m);
            Assert.True(i1 != 0.0f);
            Assert.False(i2 == i1);
            Assert.False(i1 == 0.0m);
        }

        [Fact]
        public void UlongCompareTests() {
            VariantValue i1 = 1ul;
            VariantValue i2 = 2ul;
            VariantValue i3 = 2ul;

            Assert.True(i1 < i2);
            Assert.True(i2 > i1);
            Assert.True(i2 < 3);
            Assert.True(i2 <= 2);
            Assert.True(i2 <= i3);
            Assert.True(i2 >= 2);
            Assert.True(i2 >= i3);
            Assert.True(i2 != i1);
            Assert.True(i1 == 1);
            Assert.True(i1 >= 1);
            Assert.True(i1 <= 1);
            Assert.True(i2 == i3);
            Assert.True(i1 != 2);
            Assert.True(i1 <= 2);
            Assert.False(i2 == i1);
            Assert.False(i1 == 2);
        }

        [Fact]
        public void UlongAndIntGreaterThanTests() {
            VariantValue i1 = -1;
            VariantValue i2 = 2ul;
            VariantValue i3 = 2;

            Assert.True(i1 < i2);
            Assert.True(i2 > i1);
            Assert.True(i2 < 3);
            Assert.True(i2 <= 2);
            Assert.True(i2 >= 2);
            Assert.True(i2 <= i3);
            Assert.True(i2 >= i3);
            Assert.True(i2 != i1);
            Assert.True(i1 < 0);
            Assert.True(i1 <= 0);
            Assert.True(i1 == -1);
            Assert.True(i1 >= -1);
            Assert.True(i1 <= -1);
            Assert.True(i2 == i3);
            Assert.True(i1 != 2);
            Assert.False(i2 == i1);
            Assert.False(i1 == 2);
        }

        public static IEnumerable<object[]> GetNulls() {
            return GetStrings()
                .Select(v => new object[] { v.Item2.GetType() })
                .Concat(GetValues()
                .Where(v => v.Item2 != null)
                .Select(v => new object[] {
                    typeof(Nullable<>).MakeGenericType(v.Item2.GetType()) }));
        }

        public static IEnumerable<object[]> GetScalars() {
            return GetStrings()
                .Select(v => new object[] { v.Item2, v.Item2.GetType() })
                .Concat(GetValues()
                .Where(v => v.Item2 != null)
                .Select(v => new object[] { v.Item2, v.Item2.GetType() })
                .Concat(GetValues()
                .Where(v => v.Item2 != null)
                .Select(v => new object[] { v.Item2,
                    typeof(Nullable<>).MakeGenericType(v.Item2.GetType()) })));
        }

        public static IEnumerable<object[]> GetFilledArrays() {
            return GetStrings()
                .Select(v => new object[] { CreateArray(v.Item2, v.Item2.GetType(), 10),
                    v.Item2.GetType().MakeArrayType()})
                .Concat(GetValues()
                .Where(v => v.Item2 != null)
                .Select(v => new object[] { CreateArray(v.Item2, v.Item2.GetType(), 10),
                    v.Item2.GetType().MakeArrayType() }));
        }

        public static IEnumerable<object[]> GetEmptyArrays() {
            return GetStrings()
                .Select(v => new object[] { CreateArray(null, v.Item2.GetType(), 10),
                    v.Item2.GetType().MakeArrayType()})
                .Concat(GetValues()
                .Where(v => v.Item2 != null)
                .Select(v => new object[] { CreateArray(null, v.Item2.GetType(), 10),
                    v.Item2.GetType().MakeArrayType() }));
        }

        public static IEnumerable<object[]> GetVariantValues() {
            return GetValues()
                .Select(v => new object[] { v.Item1 })
                .Concat(GetStrings()
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
