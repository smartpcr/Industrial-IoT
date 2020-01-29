// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------

namespace Microsoft.Azure.IIoT.Modules.OpcUa.Publisher.v2.Models {
    using Microsoft.Azure.IIoT.OpcUa.Core.Models;
    using System.Runtime.Serialization;
    using Newtonsoft.Json.Linq;
    using System;

    /// <summary>
    /// Filter operand
    /// </summary>
    [DataContract]
    public class FilterOperandApiModel {

        /// <summary>
        /// Default constructor
        /// </summary>
        public FilterOperandApiModel() {
        }

        /// <summary>
        /// Create api model from service model
        /// </summary>
        /// <param name="model"></param>
        public FilterOperandApiModel(FilterOperandModel model) {
            if (model == null) {
                throw new ArgumentNullException(nameof(model));
            }
            NodeId = model.NodeId;
            AttributeId = model.AttributeId;
            BrowsePath = model.BrowsePath;
            IndexRange = model.IndexRange;
            Index = model.Index;
            Alias = model.Alias;
            Value = model.Value;
            NodeId = model.NodeId;
        }

        /// <summary>
        /// Create service model from api model
        /// </summary>
        public FilterOperandModel ToServiceModel() {
            return new FilterOperandModel {
                Index = Index,
                Alias = Alias,
                Value = Value,
                NodeId = NodeId,
                AttributeId = AttributeId,
                BrowsePath = BrowsePath,
                IndexRange = IndexRange
            };
        }

        /// <summary>
        /// Element reference in the outer list if
        /// operand is an element operand
        /// </summary>
        [DataMember(Name = "index",
            EmitDefaultValue = false)]
        public uint? Index { get; set; }

        /// <summary>
        /// Variant value if operand is a literal
        /// </summary>
        [DataMember(Name = "value",
            EmitDefaultValue = false)]
        public JToken Value { get; set; }

        /// <summary>
        /// Type definition node id if operand is
        /// simple or full attribute operand.
        /// </summary>
        [DataMember(Name = "nodeId",
            EmitDefaultValue = false)]
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

        /// <summary>
        /// Optional alias to refer to it makeing it a
        /// full blown attribute operand
        /// </summary>
        [DataMember(Name = "alias",
            EmitDefaultValue = false)]
        public string Alias { get; set; }
    }
}