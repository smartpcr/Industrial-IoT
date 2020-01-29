// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------

namespace Microsoft.Azure.IIoT.Modules.OpcUa.Twin.v2.Models {
    using Microsoft.Azure.IIoT.OpcUa.Twin.Models;
    using System.Runtime.Serialization;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Browse nodes by path
    /// </summary>
    [DataContract]
    public class BrowsePathRequestApiModel {

        /// <summary>
        /// Default constructor
        /// </summary>
        public BrowsePathRequestApiModel() { }

        /// <summary>
        /// Create from service model
        /// </summary>
        /// <param name="model"></param>
        public BrowsePathRequestApiModel(BrowsePathRequestModel model) {
            if (model == null) {
                throw new ArgumentNullException(nameof(model));
            }
            NodeIdsOnly = model.NodeIdsOnly;
            NodeId = model.NodeId;
            BrowsePaths = model.BrowsePaths;
            ReadVariableValues = model.ReadVariableValues;
            Header = model.Header == null ? null :
                new RequestHeaderApiModel(model.Header);
        }

        /// <summary>
        /// Convert back to service model
        /// </summary>
        /// <returns></returns>
        public BrowsePathRequestModel ToServiceModel() {
            return new BrowsePathRequestModel {
                NodeIdsOnly = NodeIdsOnly,
                NodeId = NodeId,
                BrowsePaths = BrowsePaths,
                ReadVariableValues = ReadVariableValues,
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
        /// The path elements of the path to browse from node.
        /// (mandatory)
        /// </summary>
        [DataMember(Name = "BrowsePaths")]
        public List<string[]> BrowsePaths { get; set; }

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
        /// Optional request header
        /// </summary>
        [DataMember(Name = "Header",
            EmitDefaultValue = false)]
        public RequestHeaderApiModel Header { get; set; }
    }
}
