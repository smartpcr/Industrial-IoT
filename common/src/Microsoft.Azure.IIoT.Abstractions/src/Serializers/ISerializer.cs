// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------

namespace Microsoft.Azure.IIoT.Serializers {
    using System;
    using System.Buffers;

    /// <summary>
    /// Pluggable serializer
    /// </summary>
    public interface ISerializer : IVariantFactory {

        /// <summary>
        /// Serialize to writer
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="o"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        void Serialize(IBufferWriter<byte> buffer, object o,
            SerializeOption format = SerializeOption.None);

        /// <summary>
        /// Serialize to string
        /// </summary>
        /// <param name="type"></param>
        /// <param name="buffer"></param>
        /// <returns></returns>
        object Deserialize(ReadOnlyMemory<byte> buffer, Type type);

        /// <summary>
        /// Deserialize to variant value
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        VariantValue Parse(ReadOnlyMemory<byte> buffer);
    }
}