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
        /// Undefined
        /// </summary>
        Undefined,

        /// <summary>
        /// Null
        /// </summary>
        Null,

        /// <summary>
        /// Array
        /// </summary>
        Array,

        /// <summary>
        /// Byte array
        /// </summary>
        Bytes,

        /// <summary>
        /// Object
        /// </summary>
        Object,

        /// <summary>
        /// String
        /// </summary>
        String,

        /// <summary>
        /// Number
        /// </summary>
        Integer,

        /// <summary>
        /// Floating point
        /// </summary>
        Float,

        /// <summary>
        /// Bool
        /// </summary>
        Boolean,

        /// <summary>
        /// Guid
        /// </summary>
        Guid,

        /// <summary>
        /// Absolute Date
        /// </summary>
        UtcDateTime,

        /// <summary>
        /// Duration
        /// </summary>
        TimeSpan
    }
}