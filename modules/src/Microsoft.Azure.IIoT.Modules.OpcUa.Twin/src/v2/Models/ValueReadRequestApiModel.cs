// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------

namespace Microsoft.Azure.IIoT.Modules.OpcUa.Twin.v2.Models {
    using Microsoft.Azure.IIoT.OpcUa.Twin.Models;
    using System.Runtime.Serialization;
    using System;

    /// <summary>
    /// Node value read request twin module model
    /// </summary>
    [DataContract]
    public class ValueReadRequestApiModel {

        /// <summary>
        /// Default constructor
        /// </summary>
        public ValueReadRequestApiModel() { }

        /// <summary>
        /// Create from service model
        /// </summary>
        /// <param name="model"></param>
        public ValueReadRequestApiModel(ValueReadRequestModel model) {
            if (model == null) {
                throw new ArgumentNullException(nameof(model));
            }
            NodeId = model.NodeId;
            BrowsePath = model.BrowsePath;
            IndexRange = model.IndexRange;
            Header = model.Header == null ? null :
                new RequestHeaderApiModel(model.Header);
        }

        /// <summary>
        /// Convert back to service model
        /// </summary>
        /// <returns></returns>
        public ValueReadRequestModel ToServiceModel() {
            return new ValueReadRequestModel {
                NodeId = NodeId,
                BrowsePath = BrowsePath,
                IndexRange = IndexRange,
                Header = Header?.ToServiceModel()
            };
        }

        /// <summary>
        /// Node to read from (mandatory)
        /// </summary>
        [DataMember(Name = "NodeId",
            EmitDefaultValue = false)]
        public string NodeId { get; set; }

        /// <summary>
        /// An optional path from NodeId instance to
        /// the actual node.
        /// </summary>
        [DataMember(Name = "BrowsePath",
            EmitDefaultValue = false)]
        public string[] BrowsePath { get; set; }

        /// <summary>
        /// Index range to read, e.g. 1:2,0:1 for 2 slices
        /// out of a matrix or 0:1 for the first item in
        /// an array, string or bytestring.
        /// See 7.22 of part 4: NumericRange.
        /// </summary>
        [DataMember(Name = "IndexRange",
            EmitDefaultValue = false)]
        public string IndexRange { get; set; }

        /// <summary>
        /// Optional request header
        /// </summary>
        [DataMember(Name = "Header",
            EmitDefaultValue = false)]
        public RequestHeaderApiModel Header { get; set; }
    }
}
