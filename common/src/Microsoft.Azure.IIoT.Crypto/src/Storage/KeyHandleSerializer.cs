// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------

namespace Microsoft.Azure.IIoT.Crypto.Storage {
    using Microsoft.Azure.IIoT.Crypto.Storage.Models;
    using Microsoft.Azure.IIoT.Crypto.Models;
    using Microsoft.Azure.IIoT.Serializers;
    using System;
    using System.Text;

    /// <summary>
    /// Key document key handle serializer
    /// </summary>
    public class KeyHandleSerializer : IKeyHandleSerializer {

        /// <summary>
        /// Create serializer
        /// </summary>
        /// <param name="serializer"></param>
        public KeyHandleSerializer(IJsonSerializer serializer) {
            _serializer = serializer;
        }

        /// <inheritdoc/>
        public byte[] SerializeHandle(KeyHandle handle) {
            if (handle is KeyId id) {
                return Encoding.UTF8.GetBytes(_serializer.Serialize(id));
            }
            throw new ArgumentException("Bad handle type");
        }

        /// <inheritdoc/>
        public KeyHandle DeserializeHandle(byte[] token) {
            if (token == null) {
                return null;
            }
            return _serializer.Deserialize<KeyId>(
                Encoding.UTF8.GetString(token));
        }

        private readonly IJsonSerializer _serializer;
    }
}

