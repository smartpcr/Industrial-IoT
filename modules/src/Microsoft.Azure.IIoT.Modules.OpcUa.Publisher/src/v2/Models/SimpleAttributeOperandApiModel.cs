// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------

namespace Microsoft.Azure.IIoT.Modules.OpcUa.Publisher.v2.Models {
    using Microsoft.Azure.IIoT.OpcUa.Core.Models;
    using System.Runtime.Serialization;
    using System;

    /// <summary>
    /// Simple attribute operand model
    /// </summary>
    [DataContract]
    public class SimpleAttributeOperandApiModel {

        /// <summary>
        /// Default constructor
        /// </summary>
        public SimpleAttributeOperandApiModel() {
        }

        /// <summary>
        /// Create api model from service model
        /// </summary>
        /// <param name="model"></param>
        public SimpleAttributeOperandApiModel(SimpleAttributeOperandModel model) {
            if (model == null) {
                throw new ArgumentNullException(nameof(model));
            }
            NodeId = model.NodeId;
            AttributeId = model.AttributeId;
            BrowsePath = model.BrowsePath;
            IndexRange = model.IndexRange;
        }

        /// <summary>
        /// Create service model from api model
        /// </summary>
        public SimpleAttributeOperandModel ToServiceModel() {
            return new SimpleAttributeOperandModel {
                NodeId = NodeId,
                AttributeId = AttributeId,
                BrowsePath = BrowsePath,
                IndexRange = IndexRange
            };
        }

        /// <summary>
        /// Type definition node id if operand is
        /// simple or full attribute operand.
        /// </summary>
        [DataMember(Name = "nodeId")]
        public string NodeId { get; set; }

        /// <summary>
        /// Browse path of attribute operand
        /// </summary>
        [DataMember(Name = "browsePath",
            EmitDefaultValue = false)]
        public string[] BrowsePath { get; set; }

        /// <summary>
        /// Attribute id
        /// </summary>
        [DataMember(Name = "attributeId",
            EmitDefaultValue = false)]
        public NodeAttribute? AttributeId { get; set; }

        /// <summary>
        /// Index range of attribute operand
        /// </summary>
        [DataMember(Name = "indexRange",
            EmitDefaultValue = false)]
        public string IndexRange { get; set; }
    }
}