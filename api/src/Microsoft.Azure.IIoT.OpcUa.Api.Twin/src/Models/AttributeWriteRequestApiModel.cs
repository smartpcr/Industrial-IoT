// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------

namespace Microsoft.Azure.IIoT.OpcUa.Api.Twin.Models {
    using System.Runtime.Serialization;
    using System.ComponentModel.DataAnnotations;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Attribute and value to write to it
    /// </summary>
    [DataContract]
    public class AttributeWriteRequestApiModel {

        /// <summary>
        /// Node to write to (mandatory)
        /// </summary>
        [DataMember(Name = "nodeId")]
        [Required]
        public string NodeId { get; set; }

        /// <summary>
        /// Attribute to write (mandatory)
        /// </summary>
        [DataMember(Name = "attribute")]
        [Required]
        public NodeAttribute Attribute { get; set; }

        /// <summary>
        /// Value to write (mandatory)
        /// </summary>
        [DataMember(Name = "value")]
        [Required]
        public JToken Value { get; set; }
    }
}
