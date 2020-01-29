// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------

namespace Microsoft.Azure.IIoT.OpcUa.Edge.Publisher.Models {
    using System.Runtime.Serialization;
    using System.Collections.Generic;

    /// <summary>
    /// All published nodes on an endpoint response
    /// </summary>
    [DataContract]
    public class GetNodesResponseModel {

        /// <summary>
        /// Nodes that are published
        /// </summary>
        [DataMember(Name = "OpcNodes")]
        public List<PublisherNodeModel> OpcNodes { get; set; }

        /// <summary>
        /// Continuation token
        /// </summary>
        [DataMember(Name = "ContinuationToken")]
        public ulong? ContinuationToken { get; set; }
    }
}
