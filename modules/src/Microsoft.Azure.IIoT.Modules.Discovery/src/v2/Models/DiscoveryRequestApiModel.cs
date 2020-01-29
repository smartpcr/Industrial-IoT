// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------

namespace Microsoft.Azure.IIoT.Modules.Discovery.v2.Models {
    using Microsoft.Azure.IIoT.OpcUa.Registry.Models;
    using System.Runtime.Serialization;
    using System;

    /// <summary>
    /// Discovery request
    /// </summary>
    [DataContract]
    public class DiscoveryRequestApiModel {

        /// <summary>
        /// Default constructor
        /// </summary>
        public DiscoveryRequestApiModel() { }

        /// <summary>
        /// Create from service model
        /// </summary>
        /// <param name="model"></param>
        public DiscoveryRequestApiModel(DiscoveryRequestModel model) {
            if (model == null) {
                throw new ArgumentNullException(nameof(model));
            }
            Id = model.Id;
            Discovery = model.Discovery;
            Configuration = model.Configuration == null ? null :
                new DiscoveryConfigApiModel(model.Configuration);
            Context = model.Context == null ? null :
                new RegistryOperationContextApiModel(model.Context);
        }

        /// <summary>
        /// Convert back to service model
        /// </summary>
        /// <returns></returns>
        public DiscoveryRequestModel ToServiceModel() {
            return new DiscoveryRequestModel {
                Id = Id,
                Configuration = Configuration?.ToServiceModel(),
                Discovery = Discovery,
                Context = Context?.ToServiceModel()
            };
        }

        /// <summary>
        /// Id of discovery request
        /// </summary>
        [DataMember(Name = "Id",
            EmitDefaultValue = false)]
        public string Id { get; set; }

        /// <summary>
        /// Discovery mode to use
        /// </summary>
        [DataMember(Name = "Discovery",
            EmitDefaultValue = false)]
        public DiscoveryMode? Discovery { get; set; }

        /// <summary>
        /// Scan configuration to use
        /// </summary>
        [DataMember(Name = "Configuration",
            EmitDefaultValue = false)]
        public DiscoveryConfigApiModel Configuration { get; set; }

        /// <summary>
        /// Operation audit context
        /// </summary>
        [DataMember(Name = "context",
            EmitDefaultValue = false)]
        public RegistryOperationContextApiModel Context { get; set; }
    }
}
