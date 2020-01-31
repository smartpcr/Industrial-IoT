// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------

namespace Microsoft.Azure.IIoT.Serializers {
    using System;
    using System.IO;

    /// <summary>
    /// Pluggable json serializer
    /// </summary>
    public interface IJsonSerializer {

        /// <summary>
        /// Serialize to writer
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="o"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        void Serialize(TextWriter writer, object o,
            JsonFormat format = JsonFormat.None);

        /// <summary>
        /// Serialize to string
        /// </summary>
        /// <param name="type"></param>
        /// <param name="reader"></param>
        /// <returns></returns>
        object Deserialize(TextReader reader, Type type);

        /// <summary>
        /// Bind token to object of type.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        object ToObject(IValue token, Type type);

        /// <summary>
        /// Convert to token.
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        IValue FromObject(object o);
    }
}