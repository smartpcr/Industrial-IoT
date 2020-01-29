// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------

namespace Microsoft.Azure.IIoT.OpcUa.Api.Publisher.Models {
    using System.Runtime.Serialization;
    using System.Collections.Generic;
    using System.ComponentModel;

    /// <summary>
    /// Request header model
    /// </summary>
    [DataContract]
    public class RequestHeaderApiModel {

        /// <summary>
        /// Optional User elevation
        /// </summary>
        [DataMember(Name = "elevation",
            EmitDefaultValue = false)]
        [DefaultValue(null)]
        public CredentialApiModel Elevation { get; set; }

        /// <summary>
        /// Optional list of locales in preference order.
        /// </summary>
        [DataMember(Name = "locales",
            EmitDefaultValue = false)]
        [DefaultValue(null)]
        public List<string> Locales { get; set; }

        /// <summary>
        /// Optional diagnostics configuration
        /// </summary>
        [DataMember(Name = "diagnostics",
            EmitDefaultValue = false)]
        [DefaultValue(null)]
        public DiagnosticsApiModel Diagnostics { get; set; }
    }
}
