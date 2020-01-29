// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------

namespace Microsoft.Azure.IIoT.Modules.OpcUa.Twin.v2.Models {
    using Microsoft.Azure.IIoT.OpcUa.Twin.Models;
    using Microsoft.Azure.IIoT.OpcUa.Core.Models;
    using System.Runtime.Serialization;
    using System;

    /// <summary>
    /// Browse request model for module
    /// </summary>
    [DataContract]
    public class BrowseRequestApiModel {

        /// <summary>
        /// Default constructor
        /// </summary>
        public BrowseRequestApiModel() { }

        /// <summary>
        /// Create from service model
        /// </summary>
        /// <param name="model"></param>
        public BrowseRequestApiModel(BrowseRequestModel model) {
            if (model == null) {
                throw new ArgumentNullException(nameof(model));
            }
            NodeIdsOnly = model.NodeIdsOnly;
            NodeId = model.NodeId;
            MaxReferencesToReturn = model.MaxReferencesToReturn;
            Direction = model.Direction;
            ReferenceTypeId = model.ReferenceTypeId;
            NoSubtypes = model.NoSubtypes;
            TargetNodesOnly = model.TargetNodesOnly;
            ReadVariableValues = model.ReadVariableValues;
            View = model.View == null ? null :
                new BrowseViewApiModel(model.View);
            Header = model.Header == null ? null :
                new RequestHeaderApiModel(model.Header);
        }

        /// <summary>
        /// Convert back to service model
        /// </summary>
        /// <returns></returns>
        public BrowseRequestModel ToServiceModel() {
            return new BrowseRequestModel {
                NodeIdsOnly = NodeIdsOnly,
                NodeId = NodeId,
                MaxReferencesToReturn = MaxReferencesToReturn,
                Direction = Direction,
                View = View?.ToServiceModel(),
                ReferenceTypeId = ReferenceTypeId,
                TargetNodesOnly = TargetNodesOnly,
                ReadVariableValues = ReadVariableValues,
                NoSubtypes = NoSubtypes,
                Header = Header?.ToServiceModel()
            };
        }

        /// <summary>
        /// Node to browse.
        /// (defaults to root folder).
        /// </summary>
        [DataMember(Name = "NodeId")]
        public string NodeId { get; set; }

        /// <summary>
        /// Direction to browse in
        /// (default: forward)
        /// </summary>
        [DataMember(Name = "Direction",
            EmitDefaultValue = false)]
        public BrowseDirection? Direction { get; set; }

        /// <summary>
        /// View to browse
        /// (default: null = new view = All nodes).
        /// </summary>
        [DataMember(Name = "View",
            EmitDefaultValue = false)]
        public BrowseViewApiModel View { get; set; }

        /// <summary>
        /// Reference types to browse.
        /// (default: hierarchical).
        /// </summary>
        [DataMember(Name = "ReferenceTypeId",
            EmitDefaultValue = false)]
        public string ReferenceTypeId { get; set; }

        /// <summary>
        /// Whether to include subtypes of the reference type.
        /// (default is false)
        /// </summary>
        [DataMember(Name = "NoSubtypes",
            EmitDefaultValue = false)]
        public bool? NoSubtypes { get; set; }

        /// <summary>
        /// Max number of references to return. There might
        /// be less returned as this is up to the client
        /// restrictions.  Set to 0 to return no references
        /// or target nodes.
        /// (default is decided by client e.g. 60)
        /// </summary>
        [DataMember(Name = "MaxReferencesToReturn",
            EmitDefaultValue = false)]
        public uint? MaxReferencesToReturn { get; set; }

        /// <summary>
        /// Whether to collapse all references into a set of
        /// unique target nodes and not show reference
        /// information.
        /// (default is false)
        /// </summary>
        [DataMember(Name = "TargetNodesOnly",
           EmitDefaultValue = false)]
        public bool? TargetNodesOnly { get; set; }

        /// <summary>
        /// Whether to read variable values on target nodes.
        /// (default is false)
        /// </summary>
        [DataMember(Name = "ReadVariableValues",
            EmitDefaultValue = false)]
        public bool? ReadVariableValues { get; set; }

        /// <summary>
        /// Whether to only return the raw node id
        /// information and not read the target node.
        /// (default is false)
        /// </summary>
        [DataMember(Name = "NodeIdsOnly",
            EmitDefaultValue = false)]
        public bool? NodeIdsOnly { get; set; }

        /// <summary>
        /// Optional header
        /// </summary>
        [DataMember(Name = "Header",
             EmitDefaultValue = false)]
        public RequestHeaderApiModel Header { get; set; }
    }
}
