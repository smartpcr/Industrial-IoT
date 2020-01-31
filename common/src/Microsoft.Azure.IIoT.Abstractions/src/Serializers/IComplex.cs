// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------

namespace Microsoft.Azure.IIoT.Serializers {
    using System.Collections.Generic;

    /// <summary>
    /// Complex value interface
    /// </summary>
    public interface IComplex : IValue, IDictionary<string, IValue> {
    }
}