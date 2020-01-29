// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------

namespace Microsoft.Azure.IIoT.OpcUa.Publisher.Clients.v2.Models {
    using Microsoft.Azure.IIoT.OpcUa.Core.Models;
    using System.Runtime.Serialization;
    using System;

    /// <summary>
    /// Connection model
    /// </summary>
    [DataContract]
    public class ConnectionApiModel {

        /// <summary>
        /// Default constructor
        /// </summary>
        public ConnectionApiModel() { }

        /// <summary>
        /// Create api model from service model
        /// </summary>
        /// <param name="model"></param>
        public ConnectionApiModel(ConnectionModel model) {
            if (model == null) {
                throw new ArgumentNullException(nameof(model));
            }
            Endpoint = model.Endpoint == null ? null :
                new EndpointApiModel(model.Endpoint);
            User = model.User == null ? null :
                new CredentialApiModel(model.User);
            Diagnostics = model.Diagnostics == null ? null :
                new DiagnosticsApiModel(model.Diagnostics);
        }

        /// <summary>
        /// Create service model from api model
        /// </summary>
        public ConnectionModel ToServiceModel() {
            return new ConnectionModel {
                Endpoint = Endpoint?.ToServiceModel(),
                User = User?.ToServiceModel(),
                Diagnostics = Diagnostics?.ToServiceModel()
            };
        }

        /// <summary>
        /// Endpoint information
        /// </summary>
        [DataMember(Name = "endpoint")]
        public EndpointApiModel Endpoint { get; set; }

        /// <summary>
        /// Elevation
        /// </summary>
        [DataMember(Name = "user",
            EmitDefaultValue = false)]
        public CredentialApiModel User { get; set; }

        /// <summary>
        /// Diagnostics configuration
        /// </summary>
        [DataMember(Name = "diagnostics",
             EmitDefaultValue = false)]
        public DiagnosticsApiModel Diagnostics { get; set; }
    }
}