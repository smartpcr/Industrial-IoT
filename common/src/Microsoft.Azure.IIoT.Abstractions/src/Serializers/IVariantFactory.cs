// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------

namespace Microsoft.Azure.IIoT.Serializers {
    /// <summary>
    /// Convert an object to a variant value.
    /// </summary>
    public interface IVariantFactory {

        /// <summary>
        /// Convert to token.
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        VariantValue FromObject(object o);
    }
}