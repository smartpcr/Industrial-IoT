// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------

namespace Microsoft.Azure.IIoT.Modules.OpcUa.Twin.v2.Models {
    using Microsoft.Azure.IIoT.OpcUa.Twin.Models;
    using System.Runtime.Serialization;
    using System;

    /// <summary>
    /// Request node browsing continuation
    /// </summary>
    [DataContract]
    public class BrowseNextRequestApiModel {

        /// <summary>
        /// Default constructor
        /// </summary>
        public BrowseNextRequestApiModel() { }

        /// <summary>
        /// Create from service model
        /// </summary>
        /// <param name="model"></param>
        public BrowseNextRequestApiModel(BrowseNextRequestModel model) {
            if (model == null) {
                throw new ArgumentNullException(nameof(model));
            }
            NodeIdsOnly = model.NodeIdsOnly;
            Abort = model.Abort;
            ContinuationToken = model.ContinuationToken;
            TargetNodesOnly = model.TargetNodesOnly;
            ReadVariableValues = model.ReadVariableValues;
            Header = model.Header == null ? null :
                new RequestHeaderApiModel(model.Header);
        }

        /// <summary>
        /// Convert back to service model
        /// </summary>
        /// <returns></returns>
        public BrowseNextRequestModel ToServiceModel() {
            return new BrowseNextRequestModel {
                NodeIdsOnly = NodeIdsOnly,
                Abort = Abort,
                TargetNodesOnly = TargetNodesOnly,
                ReadVariableValues = ReadVariableValues,
                ContinuationToken = ContinuationToken,
                Header = Header?.ToServiceModel()
            };
        }

        /// <summary>
        /// Continuation token from previews browse request.
        /// (mandatory)
        /// </summary>
        [DataMember(Name = "ContinuationToken")]
        public string ContinuationToken { get; set; }

        /// <summary>
        /// Whether to abort browse and release.
        /// (default: false)
        /// </summary>
        [DataMember(Name = "Abort",
            EmitDefaultValue = false)]
        public bool? Abort { get; set; }

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
        /// Optional request header
        /// </summary>
        [DataMember(Name = "Header",
            EmitDefaultValue = false)]
        public RequestHeaderApiModel Header { get; set; }
    }
}
