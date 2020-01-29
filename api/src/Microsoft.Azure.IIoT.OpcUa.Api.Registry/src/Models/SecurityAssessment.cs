// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------

namespace Microsoft.Azure.IIoT.OpcUa.Api.Registry.Models {
    using System.Runtime.Serialization;

    /// <summary>
    /// Security assessment of the endpoint or application
    /// </summary>
    [DataContract]
    public enum SecurityAssessment {

        /// <summary>
        /// Low
        /// </summary>
        [EnumMember]
        Low,

        /// <summary>
        /// Medium
        /// </summary>
        [EnumMember]
        Medium,

        /// <summary>
        /// High
        /// </summary>
        [EnumMember]
        High
    }
}
