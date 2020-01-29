// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------

namespace Microsoft.Azure.IIoT.OpcUa.Api.Twin.Models {
    using System.Runtime.Serialization;
    using System.ComponentModel;

    /// <summary>
    /// Method metadata request model
    /// </summary>
    [DataContract]
    public class MethodMetadataRequestApiModel {

        /// <summary>
        /// Method id of method to call.
        /// (Required)
        /// </summary>
        [DataMember(Name = "methodId")]
        [DefaultValue(null)]
        public string MethodId { get; set; }

        /// <summary>
        /// An optional component path from the node identified by
        /// MethodId to the actual method node.
        /// </summary>
        [DataMember(Name = "methodBrowsePath",
            EmitDefaultValue = false)]
        [DefaultValue(null)]
        public string[] MethodBrowsePath { get; set; }

        /// <summary>
        /// Optional request header
        /// </summary>
        [DataMember(Name = "header",
            EmitDefaultValue = false)]
        [DefaultValue(null)]
        public RequestHeaderApiModel Header { get; set; }
    }
}
