// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------

namespace Microsoft.Azure.IIoT.OpcUa.Api.Registry.Models {
    using System.Runtime.Serialization;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Discoverer registration model
    /// </summary>
    [DataContract]
    public class DiscovererApiModel {

        /// <summary>
        /// Discoverer id
        /// </summary>
        [DataMember(Name = "id")]
        [Required]
        public string Id { get; set; }

        /// <summary>
        /// Site of the discoverer
        /// </summary>
        [DataMember(Name = "siteId",
            EmitDefaultValue = false)]
        [DefaultValue(null)]
        public string SiteId { get; set; }

        /// <summary>
        /// Whether the discoverer is in discovery mode
        /// </summary>
        [DataMember(Name = "discovery",
            EmitDefaultValue = false)]
        [DefaultValue(DiscoveryMode.Off)]
        public DiscoveryMode? Discovery { get; set; }

        /// <summary>
        /// Discoverer configuration
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
        [DefaultValue(TraceLogLevel.Information)]
        public TraceLogLevel? LogLevel { get; set; }

        /// <summary>
        /// Whether the registration is out of sync between
        /// client (module) and server (service) (default: false).
        /// </summary>
        [DataMember(Name = "outOfSync",
            EmitDefaultValue = false)]
        [DefaultValue(null)]
        public bool? OutOfSync { get; set; }

        /// <summary>
        /// Whether discoverer is connected on this registration
        /// </summary>
        [DataMember(Name = "connected",
            EmitDefaultValue = false)]
        [DefaultValue(null)]
        public bool? Connected { get; set; }
    }
}
