// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------

namespace Microsoft.Azure.IIoT.Serializers {

    /// <summary>
    /// Variant discriminator
    /// </summary>
    public enum VariantValueType {

        /// <summary>
        /// Null
        /// </summary>
        Null,

        /// <summary>
        /// Array
        /// </summary>
        Array,

        /// <summary>
        /// Object
        /// </summary>
        Object,

        /// <summary>
        /// Byte array
        /// </summary>
        Bytes,

        /// <summary>
        /// String
        /// </summary>
        Primitive
    }
}