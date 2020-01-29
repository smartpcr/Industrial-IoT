// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------

namespace Microsoft.Azure.IIoT.OpcUa.Edge.Publisher.Models {
    using System.Runtime.Serialization;
    using System.Collections.Generic;

    /// <summary>
    /// Published nodes request
    /// </summary>
    [DataContract]
    public class PublishNodesRequestModel {

        /// <summary>
        /// Endpoint identifier on the publisher (optional)
        /// </summary>
        [DataMember(Name = "EndpointId",
            EmitDefaultValue = false)]
        public string EndpointId { get; set; }

        /// <summary>
        /// Endpoint url (mandatory)
        /// </summary>
        [DataMember(Name = "EndpointUrl")]
        public string EndpointUrl { get; set; }

        /// <summary>
        /// Whether to use secure connectivity (default: true)
        /// </summary>
        [DataMember(Name = "UseSecurity")]
        public bool? UseSecurity { get; set; }

        /// <summary>
        /// Endpoint security profile uri (optional)
        /// </summary>
        [DataMember(Name = "SecurityProfileUri",
            EmitDefaultValue = false)]
        public string SecurityProfileUri { get; set; }

        /// <summary>
        /// Endpoint security mode name (optional)
        /// </summary>
        [DataMember(Name = "SecurityMode",
            EmitDefaultValue = false)]
        public string SecurityMode { get; set; }

        /// <summary>
        /// User name for user name password credential
        /// </summary>
        [DataMember(Name = "UserName")]
        public string UserName { get; set; }

        /// <summary>
        /// Password for user name password credential
        /// </summary>
        [DataMember(Name = "Password")]
        public string Password { get; set; }

        /// <summary>
        /// Nodes to publish
        /// </summary>
        [DataMember(Name = "OpcNodes")]
        public List<PublisherNodeModel> OpcNodes { get; set; }
    }
}
