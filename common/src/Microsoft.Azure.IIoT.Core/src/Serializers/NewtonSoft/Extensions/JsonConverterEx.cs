// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------

namespace Newtonsoft.Json.Converters {
    using System.Collections.Generic;

    /// <summary>
    /// Json convert helpers
    /// </summary>
    public static class JsonConverterEx {

        /// <summary>
        /// Get core settings
        /// </summary>
        /// <returns></returns>
        public static IList<JsonConverter> AddDefault(this IList<JsonConverter> converters,
            bool permissive = false) {
            if (converters == null) {
                converters = new List<JsonConverter>();
            }
            converters.Add(new ExceptionConverter(permissive));
            converters.Add(new IsoDateTimeConverter());
            converters.Add(new PhysicalAddressConverter());
            converters.Add(new IPAddressConverter());
            converters.Add(new StringEnumConverter());
            return converters;
        }
    }
}
