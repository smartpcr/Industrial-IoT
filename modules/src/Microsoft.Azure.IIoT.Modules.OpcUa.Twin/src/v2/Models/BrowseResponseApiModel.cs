// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------

namespace Microsoft.Azure.IIoT.Modules.OpcUa.Twin.v2.Models {
    using Microsoft.Azure.IIoT.OpcUa.Twin.Models;
    using System.Runtime.Serialization;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// browse response model for module
    /// </summary>
    [DataContract]
    public class BrowseResponseApiModel {

        /// <summary>
        /// Default constructor
        /// </summary>
        public BrowseResponseApiModel() { }

        /// <summary>
        /// Create from service model
        /// </summary>
        /// <param name="model"></param>
        public BrowseResponseApiModel(BrowseResultModel model) {
            if (model == null) {
                throw new ArgumentNullException(nameof(model));
            }
            Node = model.Node == null ? null :
                new NodeApiModel(model.Node);
            ErrorInfo = model.ErrorInfo == null ? null :
                new ServiceResultApiModel(model?.ErrorInfo);
            ContinuationToken = model.ContinuationToken;
            References = model.References?
                .Select(r => new NodeReferenceApiModel(r))
                .ToList();
        }

        /// <summary>
        /// Node info for the currently browsed node
        /// </summary>
        [DataMember(Name = "Node")]
        public NodeApiModel Node { get; set; }

        /// <summary>
        /// References, if included, otherwise null.
        /// </summary>
        [DataMember(Name = "References",
            EmitDefaultValue = false)]
        public List<NodeReferenceApiModel> References { get; set; }

        /// <summary>
        /// Continuation token if more results pending.
        /// </summary>
        [DataMember(Name = "ContinuationToken",
            EmitDefaultValue = false)]
        public string ContinuationToken { get; set; }

        /// <summary>
        /// Service result in case of error
        /// </summary>
        [DataMember(Name = "ErrorInfo",
            EmitDefaultValue = false)]
        public ServiceResultApiModel ErrorInfo { get; set; }
    }
}
