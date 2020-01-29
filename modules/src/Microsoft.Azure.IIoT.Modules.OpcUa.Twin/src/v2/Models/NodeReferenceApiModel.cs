// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------

namespace Microsoft.Azure.IIoT.Modules.OpcUa.Twin.v2.Models {
    using Microsoft.Azure.IIoT.OpcUa.Core.Models;
    using System.Runtime.Serialization;
    using System;

    /// <summary>
    /// reference model for module
    /// </summary>
    [DataContract]
    public class NodeReferenceApiModel {

        /// <summary>
        /// Default constructor
        /// </summary>
        public NodeReferenceApiModel() { }

        /// <summary>
        /// Create reference api model
        /// </summary>
        /// <param name="model"></param>
        public NodeReferenceApiModel(NodeReferenceModel model) {
            if (model == null) {
                throw new ArgumentNullException(nameof(model));
            }
            ReferenceTypeId = model.ReferenceTypeId;
            Direction = model.Direction;
            Target = model.Target == null ? null :
                new NodeApiModel(model.Target);
        }

        /// <summary>
        /// Reference Type identifier
        /// </summary>
        [DataMember(Name = "ReferenceTypeId",
            EmitDefaultValue = false)]
        public string ReferenceTypeId { get; set; }

        /// <summary>
        /// Browse direction of reference
        /// </summary>
        [DataMember(Name = "Direction",
            EmitDefaultValue = false)]
        public BrowseDirection? Direction { get; set; }

        /// <summary>
        /// Target node
        /// </summary>
        [DataMember(Name = "Target")]
        public NodeApiModel Target { get; set; }
    }
}
