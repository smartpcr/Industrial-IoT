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
    /// Request node attribute write
    /// </summary>
    [DataContract]
    public class WriteRequestApiModel {

        /// <summary>
        /// Default constructor
        /// </summary>
        public WriteRequestApiModel() { }

        /// <summary>
        /// Create from service model
        /// </summary>
        /// <param name="model"></param>
        public WriteRequestApiModel(WriteRequestModel model) {
            if (model == null) {
                throw new ArgumentNullException(nameof(model));
            }
            Attributes = model.Attributes?
                .Select(a => a == null ? null : new AttributeWriteRequestApiModel(a))
                .ToList();
            Header = model.Header == null ? null :
                new RequestHeaderApiModel(model.Header);
        }

        /// <summary>
        /// Convert back to service model
        /// </summary>
        /// <returns></returns>
        public WriteRequestModel ToServiceModel() {
            return new WriteRequestModel {
                Attributes = Attributes?.Select(a => a?.ToServiceModel()).ToList(),
                Header = Header?.ToServiceModel()
            };
        }

        /// <summary>
        /// Attributes to update
        /// </summary>
        [DataMember(Name = "Attributes")]
        public List<AttributeWriteRequestApiModel> Attributes { get; set; }

        /// <summary>
        /// Optional request header
        /// </summary>
        [DataMember(Name = "Header",
            EmitDefaultValue = false)]
        public RequestHeaderApiModel Header { get; set; }
    }
}
