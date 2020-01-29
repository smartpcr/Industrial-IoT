// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------

namespace Microsoft.Azure.IIoT.Modules.OpcUa.Twin.v2.Models {
    using Microsoft.Azure.IIoT.OpcUa.Registry.Models;
    using System.Runtime.Serialization;
    using System;

    /// <summary>
    /// Endpoint Activation status model
    /// </summary>
    [DataContract]
    public class EndpointActivationStatusApiModel {

        /// <summary>
        /// Default constructor
        /// </summary>
        public EndpointActivationStatusApiModel() { }

        /// <summary>
        /// Create from service model
        /// </summary>
        /// <param name="model"></param>
        public EndpointActivationStatusApiModel(EndpointActivationStatusModel model) {
            if (model == null) {
                throw new ArgumentNullException(nameof(model));
            }
            Id = model.Id;
            ActivationState = model.ActivationState;
        }

        /// <summary>
        /// Convert back to service model
        /// </summary>
        /// <returns></returns>
        public EndpointActivationStatusModel ToServiceModel() {
            return new EndpointActivationStatusModel {
                Id = Id,
                ActivationState = ActivationState
            };
        }

        /// <summary>
        /// Identifier of the endoint
        /// </summary>
        [DataMember(Name = "Id")]
        public string Id { get; set; }

        /// <summary>
        /// Activation state
        /// </summary>
        [DataMember(Name = "ActivationState",
            EmitDefaultValue = false)]
        public EndpointActivationState? ActivationState { get; set; }
    }
}
