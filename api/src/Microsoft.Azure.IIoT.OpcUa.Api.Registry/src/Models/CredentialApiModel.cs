// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------

namespace Microsoft.Azure.IIoT.OpcUa.Api.Registry.Models {
    using System.Runtime.Serialization;
    using Newtonsoft.Json.Linq;
    using System.ComponentModel;

    /// <summary>
    /// Credential model
    /// </summary>
    [DataContract]
    public class CredentialApiModel {

        /// <summary>
        /// Type of credential
        /// </summary>
        [DataMember(Name = "type",
            EmitDefaultValue = false)]
        [DefaultValue(CredentialType.None)]
        public CredentialType? Type { get; set; }

        /// <summary>
        /// Value to pass to server
        /// </summary>
        [DataMember(Name = "value",
            EmitDefaultValue = false)]
        [DefaultValue(null)]
        public JToken Value { get; set; }
    }
}
