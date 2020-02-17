// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------

namespace Microsoft.Azure.IIoT.Serializers {
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Numerics;

    /// <summary>
    /// Variant extensions
    /// </summary>
    public static class VariantValueEx {

        /// <summary>
        /// Test for null
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNull(this VariantValue value) {
            return value is null ||
                value.Type == VariantValueType.Null;
        }

        /// <summary>
        /// Helper to get values from token dictionary
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dict"></param>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static T GetValueOrDefault<T>(this Dictionary<string, VariantValue> dict,
            string key, T defaultValue) {
            if (dict != null && dict.TryGetValue(key, out var token)) {
                try {
                    return token.As<T>();
                }
                catch {
                    return defaultValue;
                }
            }
            return defaultValue;
        }

        /// <summary>
        /// Helper to get values from token dictionary
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dict"></param>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static T? GetValueOrDefault<T>(this Dictionary<string, VariantValue> dict,
            string key, T? defaultValue) where T : struct {
            if (dict != null && dict.TryGetValue(key, out var token)) {
                try {
                    // Handle enumerations serialized as string
                    if (typeof(T).IsEnum &&
                        token.Type == VariantValueType.Primitive &&
                        Enum.TryParse<T>((string)token, out var result)) {
                        return result;
                    }
                    return token.As<T>();
                }
                catch {
                    return defaultValue;
                }
            }
            return defaultValue;
        }

        /// <summary>
        /// Helper to get values from object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <param name="compare"></param>
        /// <returns></returns>
        public static T GetValueOrDefault<T>(this VariantValue t, string key, T defaultValue,
            StringComparison compare = StringComparison.Ordinal) {
            return GetValueOrDefault(t, key, () => defaultValue, compare);
        }

        /// <summary>
        /// Helper to get values from object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="key"></param>
        /// <param name="compare"></param>
        /// <returns></returns>
        public static T GetValueOrDefault<T>(this VariantValue t, string key,
            StringComparison compare = StringComparison.Ordinal) {
            return GetValueOrDefault(t, key, () => default(T), compare);
        }

        /// <summary>
        /// Helper to get values from object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <param name="compare"></param>
        /// <returns></returns>
        public static T GetValueOrDefault<T>(this VariantValue t,
            string key, Func<T> defaultValue,
            StringComparison compare = StringComparison.Ordinal) {
            if (t.Type == VariantValueType.Object &&
                t.TryGetValue(key, out var value, compare) &&
                !(value is null)) {
                try {
                    return value.As<T>();
                }
                catch {
                    return defaultValue();
                }
            }
            return defaultValue();
        }

        /// <summary>
        /// Helper to get values from object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <param name="compare"></param>
        /// <returns></returns>
        public static T? GetValueOrDefault<T>(this VariantValue t,
            string key, T? defaultValue,
            StringComparison compare = StringComparison.Ordinal) where T : struct {
            return GetValueOrDefault(t, key, () => defaultValue, compare);
        }

        /// <summary>
        /// Helper to get values from object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <param name="compare"></param>
        /// <returns></returns>
        public static T? GetValueOrDefault<T>(this VariantValue t,
            string key, Func<T?> defaultValue,
            StringComparison compare = StringComparison.Ordinal) where T : struct {
            if (t.Type == VariantValueType.Object &&
                t.TryGetValue(key, out var value, compare) &&
                !(value is null)) {
                try {
                    // Handle enumerations serialized as string
                    if (typeof(T).IsEnum &&
                        value.Type == VariantValueType.Primitive &&
                        Enum.TryParse<T>((string)value, out var result)) {
                        return result;
                    }
                    return value.As<T>();
                }
                catch {
                    return defaultValue();
                }
            }
            return defaultValue();
        }

        /// <summary>
        /// Replace whitespace in a property name
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string SanitizePropertyName(string value) {
            var chars = new char[value.Length];
            for (var i = 0; i < value.Length; i++) {
                chars[i] = !char.IsLetterOrDigit(value[i]) ? '_' : value[i];
            }
            return new string(chars);
        }

        /// <summary>
        /// Returns dimensions of the multi dimensional array assuming
        /// it is not jagged.
        /// </summary>
        /// <param name="array"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static int[] GetDimensions(this VariantValue array, out VariantValueType type) {
            var dimensions = new List<int>();
            type = VariantValueType.Null;
            while (array?.Type == VariantValueType.Array) {
                var len = array.Count;
                if (len == 0) {
                    break;
                }
                dimensions.Add(len);
                array = array[0];
                type = array.Type;
            }
            return dimensions.ToArray();
        }

        /// <summary>
        /// Returns whether the token is a float type
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsFloat(this VariantValue value) {
            if (value.IsNull()) {
                return false;
            }
            if (value.Type != VariantValueType.Primitive) {
                return false;
            }
            switch (value.Value) {
                case int _:
                case uint _:
                case long _:
                case ulong _:
                case short _:
                case ushort _:
                case sbyte _:
                case byte _:
                case char _:
                case BigInteger _:
                    return true;
                case float _:
                case double _:
                case decimal _:
                    return true;
                case string s:
                    return decimal.TryParse(s, out _);
                case IConvertible c:
                    try {
                        c.ToDecimal(CultureInfo.InvariantCulture);
                        return true;
                    }
                    catch {
                        return false;
                    }
                default:
                    return decimal.TryParse(value.Value.ToString(), out _);
            }
        }

        /// <summary>
        /// Returns whether the token is a float type
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsInteger(this VariantValue value) {
            if (value.IsNull()) {
                return false;
            }
            if (value.Type != VariantValueType.Primitive) {
                return false;
            }
            switch (value.Value) {
                case int _:
                case uint _:
                case long _:
                case ulong _:
                case short _:
                case ushort _:
                case sbyte _:
                case byte _:
                case char _:
                case BigInteger _:
                    return true;
                case string s:
                    return BigInteger.TryParse(s, out _);
                case decimal dec:
                    return decimal.Floor(dec).Equals(dec);
                case float f:
                    return Math.Floor(f).Equals(f);
                case double d:
                    return Math.Floor(d).Equals(d);
                case IConvertible c:
                    try {
                        // Handles any float
                        var dec = c.ToDecimal(CultureInfo.InvariantCulture);
                        return decimal.Floor(dec) == dec;
                    }
                    catch {
                        return false;
                    }
                default:
                    return BigInteger.TryParse(value.Value.ToString(), out _);
            }
        }

        /// <summary>
        /// Returns whether the token is a float type
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsTimeSpan(this VariantValue value) {
            if (value.IsNull()) {
                return false;
            }
            if (value.Type != VariantValueType.Primitive) {
                return false;
            }
            switch (value.Value) {
                case TimeSpan _:
                    return true;
                case string s:
                    return TimeSpan.TryParse(s, out _);
                default:
                    return TimeSpan.TryParse(value.Value.ToString(), out _);
            }
        }

        /// <summary>
        /// Returns whether the token is a float type
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsDateTime(this VariantValue value) {
            if (value.IsNull()) {
                return false;
            }
            if (value.Type != VariantValueType.Primitive) {
                return false;
            }
            switch (value.Value) {
                case DateTime _:
                case DateTimeOffset _:
                    return true;
                case string s:
                    return
                        DateTime.TryParse(s, out _) ||
                        DateTimeOffset.TryParse(s, out _);
                case IConvertible c:
                    try {
                        var dt = c.ToDateTime(CultureInfo.InvariantCulture);
                        return true;
                    }
                    catch {
                        return false;
                    }
                default:
                    return
                       DateTime.TryParse(value.Value.ToString(), out _) ||
                       DateTimeOffset.TryParse(value.Value.ToString(), out _);
            }
        }

        /// <summary>
        /// Returns whether the token is a float type
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsBoolean(this VariantValue value) {
            if (value.IsNull()) {
                return false;
            }
            if (value.Type != VariantValueType.Primitive) {
                return false;
            }
            switch (value.Value) {
                case bool _:
                    return true;
                case string s:
                    return bool.TryParse(s, out _);
                case IConvertible c:
                    try {
                        var dt = c.ToBoolean(CultureInfo.InvariantCulture);
                        return true;
                    }
                    catch {
                        return false;
                    }
                default:
                    return bool.TryParse(value.Value.ToString(), out _);
            }
        }

        /// <summary>
        /// Returns whether the token is a float type
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsGuid(this VariantValue value) {
            if (value.IsNull()) {
                return false;
            }
            if (value.Type != VariantValueType.Primitive) {
                return false;
            }
            switch (value.Value) {
                case Guid _:
                    return true;
                case string s:
                    return Guid.TryParse(s, out _);
                default:
                    return Guid.TryParse(value.Value.ToString(), out _);
            }
        }

        /// <summary>
        /// Returns whether the token is a float type
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsString(this VariantValue value) {
            if (value.IsNull()) {
                return false;
            }
            if (value.Type != VariantValueType.Primitive) {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Returns whether the token is a array
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsArray(this VariantValue value) {
            if (value.IsNull()) {
                return false;
            }
            if (value.Type == VariantValueType.Array ||
                value.Type == VariantValueType.Bytes) {
                return true;
            }
            if (value.Value.GetType().IsArray) {
                return true;
            }
            var s = (string)value;
            try {
                Convert.FromBase64String(s);
                return true;
            }
            catch {
                return false;
            }
        }

        /// <summary>
        /// Returns whether the token is a object type
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsObject(this VariantValue value) {
            if (value.IsNull()) {
                return false;
            }
            if (value.Type == VariantValueType.Object) {
                return true;
            }
            return false;
        }
    }
}
