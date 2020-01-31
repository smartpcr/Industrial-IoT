// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------

namespace Microsoft.Azure.IIoT.Serializers {
    using Newtonsoft.Json.Converters;
    using Newtonsoft.Json.Serialization;
    using Newtonsoft.Json;
    using System.Collections.Generic;

    /// <summary>
    /// Json convert helpers
    /// </summary>
    public class NewtonSoftJsonConverters : IJsonSerializerSettingsProvider {

        /// <summary>
        /// Create provider
        /// </summary>
        /// <param name="permissive"></param>
        public NewtonSoftJsonConverters(bool permissive = false) {
            _permissive = permissive;
        }

        /// <inheritdoc/>
        public virtual JsonSerializerSettings GetSettings() {
            var defaultSettings = new JsonSerializerSettings {
                ContractResolver = new DefaultContractResolver(),
                Converters = new List<JsonConverter>().AddDefault(),
                TypeNameHandling = TypeNameHandling.None,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                MaxDepth = 20
            };
            defaultSettings.Converters.AddDefault(_permissive);
            if (!_permissive) {
                defaultSettings.ReferenceLoopHandling = ReferenceLoopHandling.Error;
            }
            return defaultSettings;
        }

        private readonly bool _permissive;
    }
}
