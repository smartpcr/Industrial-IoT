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
    /// Result of node browse continuation
    /// </summary>
    [DataContract]
    public class BrowsePathResponseApiModel {

        /// <summary>
        /// Default constructor
        /// </summary>
        public BrowsePathResponseApiModel() { }

        /// <summary>
        /// Create from service model
        /// </summary>
        /// <param name="model"></param>
        public BrowsePathResponseApiModel(BrowsePathResultModel model) {
            if (model == null) {
                throw new ArgumentNullException(nameof(model));
            }
            ErrorInfo = model.ErrorInfo == null ? null :
                new ServiceResultApiModel(model.ErrorInfo);
            Targets = model.Targets?
                .Select(r => r == null ? null : new NodePathTargetApiModel(r))
                .ToList();
        }

        /// <summary>
        /// Targets
        /// </summary>
        [DataMember(Name = "Targets",
            EmitDefaultValue = false)]
        public List<NodePathTargetApiModel> Targets { get; set; }

        /// <summary>
        /// Service result in case of error
        /// </summary>
        [DataMember(Name = "ErrorInfo",
            EmitDefaultValue = false)]
        public ServiceResultApiModel ErrorInfo { get; set; }
    }
}
