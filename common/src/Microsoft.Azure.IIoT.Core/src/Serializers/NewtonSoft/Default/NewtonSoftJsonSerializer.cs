// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------

namespace Microsoft.Azure.IIoT.Serializers {
    using Microsoft.Azure.IIoT.Exceptions;
    using Newtonsoft.Json;
    using System;
    using System.IO;

    /// <summary>
    /// Newtonsoft json serializer
    /// </summary>
    public class NewtonSoftJsonSerializer : IJsonSerializer {

        /// <summary>
        /// Create serializer
        /// </summary>
        /// <param name="config"></param>
        public NewtonSoftJsonSerializer(IJsonSerializerSettingsProvider config = null) {
            _config = config ?? new NewtonSoftJsonConverters();
        }

        /// <inheritdoc/>
        public object Deserialize(TextReader reader, Type type) {
            try {
                var jsonSerializer = JsonSerializer.CreateDefault(_config.GetSettings());
                return jsonSerializer.Deserialize(reader, type);
            }
            catch (JsonReaderException ex) {
                throw new SerializerException(ex.Message, ex);
            }
        }

        /// <inheritdoc/>
        public void Serialize(TextWriter writer, object o, JsonFormat format) {
            try {
                var jsonSerializer = JsonSerializer.CreateDefault(_config.GetSettings());
                jsonSerializer.Formatting = format == JsonFormat.Indented ?
                    Formatting.Indented : Formatting.None;
                jsonSerializer.Serialize(writer, o);
            }
            catch (JsonReaderException ex) {
                throw new SerializerException(ex.Message, ex);
            }
        }

        /// <inheritdoc/>
        public object ToObject(VariantValue token, Type type) {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public VariantValue FromObject(object o) {
            throw new NotImplementedException();
        }

        private readonly IJsonSerializerSettingsProvider _config;
    }
}