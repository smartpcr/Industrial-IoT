// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------

namespace Microsoft.Azure.IIoT.Modules.OpcUa.Twin.v2.Models {
    using Microsoft.Azure.IIoT.OpcUa.Registry.Models;
    using System.Runtime.Serialization;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Supervisor runtime status
    /// </summary>
    [DataContract]
    public class SupervisorStatusApiModel {

        /// <summary>
        /// Default constructor
        /// </summary>
        public SupervisorStatusApiModel() { }

        /// <summary>
        /// Create from service model
        /// </summary>
        /// <param name="model"></param>
        public SupervisorStatusApiModel(SupervisorStatusModel model) {
            if (model == null) {
                throw new ArgumentNullException(nameof(model));
            }
            DeviceId = model.DeviceId;
            ModuleId = model.ModuleId;
            SiteId = model.SiteId;
            Endpoints = model.Endpoints?
                .Select(e => e == null ? null : new EndpointActivationStatusApiModel(e))
                .ToList();
        }

        /// <summary>
        /// Edge device id
        /// </summary>
        [DataMember(Name = "DeviceId")]
        public string DeviceId { get; set; }

        /// <summary>
        /// Module id
        /// </summary>
        [DataMember(Name = "ModuleId",
            EmitDefaultValue = false)]
        public string ModuleId { get; set; }

        /// <summary>
        /// Site id
        /// </summary>
        [DataMember(Name = "SiteId",
            EmitDefaultValue = false)]
        public string SiteId { get; set; }

        /// <summary>
        /// Endpoint activation status
        /// </summary>
        [DataMember(Name = "Endpoints",
            EmitDefaultValue = false)]
        public List<EndpointActivationStatusApiModel> Endpoints { get; set; }
    }
}
