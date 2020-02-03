// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------

namespace Microsoft.Azure.IIoT.Serializers {
    using System;

    /// <summary>
    /// Value extensions
    /// </summary>
    public static class ValueEx {

        /// <summary>
        /// hashes a json object
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string ToSha1Hash(this VariantValue val) {
            return val.ToString().ToSha1Hash();
        }
    }
}
