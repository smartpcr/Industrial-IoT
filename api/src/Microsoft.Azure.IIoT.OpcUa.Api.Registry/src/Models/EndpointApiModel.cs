// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------

namespace Microsoft.Azure.IIoT.OpcUa.Api.Registry.Models {
    using System.Runtime.Serialization;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Endpoint model
    /// </summary>
    [DataContract]
    public class EndpointApiModel {

        /// <summary>
        /// Endpoint url to use to connect with
        /// </summary>
        [DataMember(Name = "url")]
        [Required]
        public string Url { get; set; }

        /// <summary>
        /// Alternative endpoint urls that can be used for
        /// accessing and validating the server
        /// </summary>
        [DataMember(Name = "alternativeUrls",
            EmitDefaultValue = false)]
        public HashSet<string> AlternativeUrls { get; set; }

        /// <summary>
        /// Security Mode to use for communication
        /// default to best.
        /// </summary>
        [DataMember(Name = "securityMode",
            EmitDefaultValue = false)]
        [DefaultValue(Models.SecurityMode.Best)]
        public SecurityMode? SecurityMode { get; set; }

        /// <summary>
        /// Security policy uri to use for communication
        /// default to best.
        /// </summary>
        [DataMember(Name = "securityPolicy",
            EmitDefaultValue = false)]
        [DefaultValue(null)]
        public string SecurityPolicy { get; set; }

        /// <summary>
        /// Endpoint certificate thumbprint
        /// </summary>
        [DataMember(Name = "certificate",
            EmitDefaultValue = false)]
        public string Certificate { get; set; }
    }
}
