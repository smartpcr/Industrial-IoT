// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------

namespace Microsoft.Azure.IIoT.OpcUa.Api.Core.Models {
    using System.Runtime.Serialization;
    using Newtonsoft.Json.Linq;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Authentication Method model
    /// </summary>
    [DataContract]
    public class AuthenticationMethodApiModel {

        /// <summary>
        /// Method id
        /// </summary>
        [DataMember(Name = "id")]
        [Required]
        public string Id { get; set; }

        /// <summary>
        /// Type of credential
        /// </summary>
        [DataMember(Name = "credentialType",
            EmitDefaultValue = false)]
        [DefaultValue(Models.CredentialType.None)]
        public CredentialType? CredentialType { get; set; }

        /// <summary>
        /// Security policy to use when passing credential.
        /// </summary>
        [DataMember(Name = "securityPolicy",
            EmitDefaultValue = false)]
        [DefaultValue(null)]
        public string SecurityPolicy { get; set; }

        /// <summary>
        /// Method specific configuration
        /// </summary>
        [DataMember(Name = "configuration",
            EmitDefaultValue = false)]
        [DefaultValue(null)]
        public JToken Configuration { get; set; }
    }
}
