// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------

namespace Microsoft.Azure.IIoT.OpcUa.Api.Registry.Models {
    using Microsoft.Azure.IIoT.OpcUa.Api.Core.Models;
    using System.Runtime.Serialization;
    using System.ComponentModel;

    /// <summary>
    /// Endpoint query
    /// </summary>
    [DataContract]
    public class EndpointRegistrationQueryApiModel {

        /// <summary>
        /// Endoint url for direct server access
        /// </summary>
        [DataMember(Name = "url",
            EmitDefaultValue = false)]
        [DefaultValue(null)]
        public string Url { get; set; }

        /// <summary>
        /// Endpoint certificate thumbprint
        /// </summary>
        [DataMember(Name = "certificate",
            EmitDefaultValue = false)]
        [DefaultValue(null)]
        public string Certificate { get; set; }

        /// <summary>
        /// Security Mode
        /// </summary>
        [DataMember(Name = "securityMode",
            EmitDefaultValue = false)]
        [DefaultValue(null)]
        public SecurityMode? SecurityMode { get; set; }

        /// <summary>
        /// Security policy uri
        /// </summary>
        [DataMember(Name = "securityPolicy",
            EmitDefaultValue = false)]
        [DefaultValue(null)]
        public string SecurityPolicy { get; set; }

        /// <summary>
        /// Whether the endpoint was activated
        /// </summary>
        [DataMember(Name = "activated",
            EmitDefaultValue = false)]
        [DefaultValue(null)]
        public bool? Activated { get; set; }

        /// <summary>
        /// Whether the endpoint is connected on supervisor.
        /// </summary>
        [DataMember(Name = "connected",
            EmitDefaultValue = false)]
        [DefaultValue(null)]
        public bool? Connected { get; set; }

        /// <summary>
        /// The last state of the the activated endpoint
        /// </summary>
        [DataMember(Name = "endpointState",
            EmitDefaultValue = false)]
        [DefaultValue(EndpointConnectivityState.Disconnected)]
        public EndpointConnectivityState? EndpointState { get; set; }

        /// <summary>
        /// Whether to include endpoints that were soft deleted
        /// </summary>
        [DataMember(Name = "includeNotSeenSince",
            EmitDefaultValue = false)]
        [DefaultValue(null)]
        public bool? IncludeNotSeenSince { get; set; }

        /// <summary>
        /// Discoverer id to filter with
        /// </summary>
        [DataMember(Name = "discovererId",
            EmitDefaultValue = false)]
        [DefaultValue(null)]
        public string DiscovererId { get; set; }

        /// <summary>
        /// Application id to filter
        /// </summary>
        [DataMember(Name = "applicationId",
            EmitDefaultValue = false)]
        [DefaultValue(null)]
        public string ApplicationId { get; set; }

        /// <summary>
        /// Supervisor id to filter with
        /// </summary>
        [DataMember(Name = "supervisorId",
            EmitDefaultValue = false)]
        [DefaultValue(null)]
        public string SupervisorId { get; set; }

        /// <summary>
        /// Site or gateway id to filter with
        /// </summary>
        [DataMember(Name = "siteOrGatewayId",
            EmitDefaultValue = false)]
        [DefaultValue(null)]
        public string SiteOrGatewayId { get; set; }
    }
}

