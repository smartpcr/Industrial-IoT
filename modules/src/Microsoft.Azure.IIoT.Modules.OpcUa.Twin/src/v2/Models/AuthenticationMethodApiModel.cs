// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------

namespace Microsoft.Azure.IIoT.Modules.OpcUa.Twin.v2.Models {
    using Microsoft.Azure.IIoT.OpcUa.Core.Models;
    using System.Runtime.Serialization;
    using Newtonsoft.Json.Linq;
    using System;

    /// <summary>
    /// Authentication Method model
    /// </summary>
    [DataContract]
    public class AuthenticationMethodApiModel {

        /// <summary>
        /// Default constructor
        /// </summary>
        public AuthenticationMethodApiModel() { }

        /// <summary>
        /// Create api model from service model
        /// </summary>
        /// <param name="model"></param>
        public AuthenticationMethodApiModel(AuthenticationMethodModel model) {
            if (model == null) {
                throw new ArgumentNullException(nameof(model));
            }
            Id = model.Id;
            SecurityPolicy = model.SecurityPolicy;
            Configuration = model.Configuration;
            CredentialType = model.CredentialType ==
                IIoT.OpcUa.Core.Models.CredentialType.None ?
                    null : model.CredentialType;
        }

        /// <summary>
        /// Create service model from api model
        /// </summary>
        public AuthenticationMethodModel ToServiceModel() {
            return new AuthenticationMethodModel {
                Id = Id,
                SecurityPolicy = SecurityPolicy,
                Configuration = Configuration,
                CredentialType = CredentialType ?? IIoT.OpcUa.Core.Models.CredentialType.None
            };
        }

        /// <summary>
        /// Method id
        /// </summary>
        [DataMember(Name = "Id")]
        public string Id { get; set; }

        /// <summary>
        /// Type of credential
        /// </summary>
        [DataMember(Name = "CredentialType",
            EmitDefaultValue = false)]
        public CredentialType? CredentialType { get; set; }

        /// <summary>
        /// Security policy to use when passing credential.
        /// </summary>
        [DataMember(Name = "SecurityPolicy",
            EmitDefaultValue = false)]
        public string SecurityPolicy { get; set; }

        /// <summary>
        /// Method specific configuration
        /// </summary>
        [DataMember(Name = "Configuration",
            EmitDefaultValue = false)]
        public JToken Configuration { get; set; }
    }
}
