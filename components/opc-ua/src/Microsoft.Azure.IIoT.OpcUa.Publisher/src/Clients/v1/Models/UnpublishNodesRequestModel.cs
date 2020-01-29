// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------

namespace Microsoft.Azure.IIoT.OpcUa.Edge.Publisher.Models {
    using System.Runtime.Serialization;
    using System.Collections.Generic;

    /// <summary>
    /// Unpublish nodes request
    /// </summary>
    [DataContract]
    public class UnpublishNodesRequestModel {

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
        /// Nodes to publish
        /// </summary>
        [DataMember(Name = "OpcNodes")]
        public List<PublisherNodeModel> OpcNodes { get; set; }
    }
}
