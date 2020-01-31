// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------


namespace Microsoft.Azure.IIoT.Serializers {
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents a primitive or complext value
    /// </summary>
    public interface IValue : IEquatable<IValue>, IDictionary<string, IValue> {

        /// <summary>
        /// Raw value
        /// </summary>
        object Value { get; }

        /// <summary>
        /// Convert to string
        /// </summary>
        /// <param name="format"></param>
        /// <returns></returns>
        string ToString(JsonFormat format);

        /// <summary>
        /// Deep clone
        /// </summary>
        /// <returns></returns>
        IValue DeepClone();
    }
}