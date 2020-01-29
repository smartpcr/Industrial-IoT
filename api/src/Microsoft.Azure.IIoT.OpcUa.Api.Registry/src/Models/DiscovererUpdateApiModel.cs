// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------

namespace Microsoft.Azure.IIoT.OpcUa.Api.Registry.Models {
    using System.Runtime.Serialization;
    using System.ComponentModel;

    /// <summary>
    /// Discoverer update request
    /// </summary>
    [DataContract]
    public class DiscovererUpdateApiModel {

        /// <summary>
        /// Site the discoverer is part of
        /// </summary>
        [DataMember(Name = "siteId",
            EmitDefaultValue = false)]
        [DefaultValue(null)]
        public string SiteId { get; set; }

        /// <summary>
        /// Discovery mode of discoverer
        /// </summary>
        [DataMember(Name = "discovery",
            EmitDefaultValue = false)]
        [DefaultValue(DiscoveryMode.Off)]
        public DiscoveryMode? Discovery { get; set; }

        /// <summary>
        /// Discoverer discovery config
        /// </summary>
        [DataMember(Name = "discoveryConfig",
            EmitDefaultValue = false)]
        [DefaultValue(null)]
        public DiscoveryConfigApiModel DiscoveryConfig { get; set; }

        /// <summary>
        /// Current log level
        /// </summary>
        [DataMember(Name = "logLevel",
            EmitDefaultValue = false)]
        [DefaultValue(null)]
        public TraceLogLevel? LogLevel { get; set; }
    }
}
