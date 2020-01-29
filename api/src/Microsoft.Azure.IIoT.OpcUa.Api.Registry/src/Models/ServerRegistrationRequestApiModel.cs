// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------

namespace Microsoft.Azure.IIoT.OpcUa.Api.Registry.Models {
    using System.Runtime.Serialization;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Server registration request
    /// </summary>
    [DataContract]
    public class ServerRegistrationRequestApiModel {


        /// <summary>
        /// Discovery url to use for registration
        /// </summary>
        [DataMember(Name = "discoveryUrl")]
        [Required]
        public string DiscoveryUrl { get; set; }

        /// <summary>
        /// Registration id
        /// </summary>
        [DataMember(Name = "id",
            EmitDefaultValue = false)]
        [DefaultValue(null)]
        public string Id { get; set; }

        /// <summary>
        /// Upon discovery, activate all endpoints with this filter.
        /// </summary>
        [DataMember(Name = "activationFilter",
           EmitDefaultValue = false)]
        [DefaultValue(null)]
        public EndpointActivationFilterApiModel ActivationFilter { get; set; }
    }
}
