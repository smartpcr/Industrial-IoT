// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------

namespace Microsoft.Azure.IIoT.Serializers {
    using System;
    using System.Collections.Generic;
    using System.Linq;

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
            return value == null ||
                value.Type == VariantValueType.Null ||
                value.Type == VariantValueType.Undefined;
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
                    return token.ToObject<T>();
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
                        token.Type == VariantValueType.String &&
                        Enum.TryParse<T>((string)token, out var result)) {
                        return result;
                    }
                    return token.ToObject<T>();
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
                    return value.ToObject<T>();
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
                        value.Type == VariantValueType.String &&
                        Enum.TryParse<T>((string)value, out var result)) {
                        return result;
                    }
                    return value.ToObject<T>();
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
                var len = array.Count();
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
        /// <param name="token"></param>
        /// <returns></returns>
        public static bool IsFloatValue(this VariantValue token) {
            if (token?.Type == VariantValueType.Float) {
                return true;
            }
            if (token?.Type == VariantValueType.String) {
                var val = (string)token;
                if (val == "NaN" || val == "Infinity" || val == "-Infinity") {
                    return true;
                }
            }
            return false;
        }
    }
}
