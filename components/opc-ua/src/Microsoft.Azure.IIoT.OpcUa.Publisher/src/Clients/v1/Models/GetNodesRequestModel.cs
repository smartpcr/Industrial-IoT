// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------

namespace Microsoft.Azure.IIoT.OpcUa.Edge.Publisher.Models {
    using System.Runtime.Serialization;

    /// <summary>
    /// Request to get all published nodes on an endpoint
    /// </summary>
    [DataContract]
    public class GetNodesRequestModel {

        /// <summary>
        /// Endpoint url
        /// </summary>
        [DataMember(Name = "EndpointUrl")]
        public string EndpointUrl { get; set; }

        /// <summary>
        /// Endpoint identifier on the publisher (optional)
        /// </summary>
        [DataMember(Name = "EndpointId",
            EmitDefaultValue = false)]
        public string EndpointId { get; set; }

        /// <summary>
        /// Continuation token
        /// </summary>
        [DataMember(Name = "ContinuationToken")]
        public ulong? ContinuationToken { get; set; }
    }
}
