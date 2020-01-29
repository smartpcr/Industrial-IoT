// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------

namespace Microsoft.Azure.IIoT.Modules.OpcUa.Twin.v2.Models {
    using Microsoft.Azure.IIoT.OpcUa.Core.Models;
    using System.Runtime.Serialization;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Request header model for module
    /// </summary>
    [DataContract]
    public class RequestHeaderApiModel {

        /// <summary>
        /// Default constructor
        /// </summary>
        public RequestHeaderApiModel() { }

        /// <summary>
        /// Create from service model
        /// </summary>
        /// <param name="model"></param>
        public RequestHeaderApiModel(RequestHeaderModel model) {
            if (model == null) {
                throw new ArgumentNullException(nameof(model));
            }
            Elevation = model.Elevation == null ? null :
                new CredentialApiModel(model.Elevation);
            Diagnostics = model.Diagnostics == null ? null :
                new DiagnosticsApiModel(model.Diagnostics);
            Locales = model.Locales;
        }

        /// <summary>
        /// Convert back to service model
        /// </summary>
        /// <returns></returns>
        public RequestHeaderModel ToServiceModel() {
            return new RequestHeaderModel {
                Diagnostics = Diagnostics?.ToServiceModel(),
                Elevation = Elevation?.ToServiceModel(),
                Locales = Locales
            };
        }

        /// <summary>
        /// Optional User elevation
        /// </summary>
        [DataMember(Name = "Elevation",
            EmitDefaultValue = false)]
        public CredentialApiModel Elevation { get; set; }

        /// <summary>
        /// Optional list of locales in preference order.
        /// </summary>
        [DataMember(Name = "Locales",
            EmitDefaultValue = false)]
        public List<string> Locales { get; set; }

        /// <summary>
        /// Optional diagnostics configuration
        /// </summary>
        [DataMember(Name = "Diagnostics",
            EmitDefaultValue = false)]
        public DiagnosticsApiModel Diagnostics { get; set; }
    }
}
