// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------

namespace Microsoft.Azure.IIoT.OpcUa.Edge.Publisher.Models {
    using System.Runtime.Serialization;

    /// <summary>
    /// Published node model
    /// </summary>
    [DataContract]
    public class PublisherNodeModel {

        /// <summary>
        /// Node id
        /// </summary>
        [DataMember(Name = "Id")]
        public string Id { get; set; }

        /// <summary>
        /// Publishing interval
        /// </summary>
        [DataMember(Name = "OpcPublishingInterval")]
        public int? OpcPublishingInterval { get; set; }

        /// <summary>
        /// Sampling interval
        /// </summary>
        [DataMember(Name = "OpcSamplingInterval")]
        public int? OpcSamplingInterval { get; set; }

        /// <summary>
        /// Display name to use
        /// </summary>
        [DataMember(Name = "DisplayName")]
        public string DisplayName { get; set; }
    }
}
