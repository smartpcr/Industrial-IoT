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
        /// String
        /// </summary>
        String,

        /// <summary>
        /// Array
        /// </summary>
        Array,

        /// <summary>
        /// Null
        /// </summary>
        Null,

        /// <summary>
        /// Number
        /// </summary>
        Integer,

        /// <summary>
        /// Object
        /// </summary>
        Object,

        /// <summary>
        /// Bool
        /// </summary>
        Boolean,

        /// <summary>
        /// bytes
        /// </summary>
        Bytes,

        /// <summary>
        /// Bool
        /// </summary>
        Float,

        /// <summary>
        /// Date
        /// </summary>
        Date,

        /// <summary>
        /// Duration
        /// </summary>
        TimeSpan
    }
}