// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------

namespace Microsoft.Azure.IIoT.Serializer {
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using Newtonsoft.Json.Linq;
    using Newtonsoft.Json.Serialization;
    using System;
    using System.Collections.Generic;
    using System.IO;

    /// <summary>
    /// Newtonsoft json serializer
    /// </summary>
    public class NewtonSoftJsonSerializer : IJsonSerializer {

        /// <summary>
        /// Get settings
        /// </summary>
        public JsonSerializerSettings Settings { get; }

        /// <summary>
        /// Create serializer
        /// </summary>
        /// <param name="permissive"></param>
        public NewtonSoftJsonSerializer(bool permissive = false) {
            Settings = GetSettings(permissive);
        }

        /// <inheritdoc/>
        public object Deserialize(TextReader reader, Type type) {
            var jsonSerializer = JsonSerializer.CreateDefault(Settings);
            return jsonSerializer.Deserialize(reader, type);
        }

        /// <inheritdoc/>
        public void Serialize(TextWriter writer, object o, JsonFormat format) {
            var jsonSerializer = JsonSerializer.CreateDefault(Settings);
            jsonSerializer.Formatting = format == JsonFormat.Pretty ?
                Formatting.Indented : Formatting.None;
            jsonSerializer.Serialize(writer, o);
        }

        /// <inheritdoc/>
        public object ToObject(dynamic token, Type type) {
            if (!(token is JToken t)) {
                t = JToken.FromObject(token);
            }
            var jsonSerializer = JsonSerializer.CreateDefault(Settings);
            return t.ToObject(type, jsonSerializer);
        }

        /// <inheritdoc/>
        public dynamic FromObject(object o) {
            return JToken.FromObject(o);
        }

        /// <summary>
        /// Get core settings
        /// </summary>
        /// <returns></returns>
        internal static JsonSerializerSettings GetDefaultSettings() {
            return new JsonSerializerSettings {
                ContractResolver = new DefaultContractResolver(),
                Converters = new List<JsonConverter>(),
                TypeNameHandling = TypeNameHandling.None,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                MaxDepth = 20
            };
        }

        /// <summary>
        /// Get settings
        /// </summary>
        /// <param name="permissive"></param>
        /// <returns></returns>
        internal static JsonSerializerSettings GetSettings(bool permissive) {
            var defaultSettings = GetDefaultSettings();
            defaultSettings.Converters.Add(new ExceptionConverter(permissive));
            defaultSettings.Converters.Add(new IsoDateTimeConverter());
            defaultSettings.Converters.Add(new PhysicalAddressConverter());
            defaultSettings.Converters.Add(new IPAddressConverter());
            defaultSettings.Converters.Add(new StringEnumConverter());
            if (!permissive) {
                defaultSettings.ReferenceLoopHandling = ReferenceLoopHandling.Error;
            }
            return defaultSettings;
        }
    }
}