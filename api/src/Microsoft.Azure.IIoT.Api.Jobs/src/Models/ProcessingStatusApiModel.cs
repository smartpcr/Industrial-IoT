// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------

namespace Microsoft.Azure.IIoT.Api.Jobs.Models {
    using System.Runtime.Serialization;
    using Newtonsoft.Json.Linq;
    using System;

    /// <summary>
    /// processing status
    /// </summary>
    [DataContract]
    public class ProcessingStatusApiModel {

        /// <summary>
        /// Last known heartbeat
        /// </summary>
        [DataMember(Name = "lastKnownHeartbeat",
            EmitDefaultValue = false)]
        public DateTime? LastKnownHeartbeat { get; set; }

        /// <summary>
        /// Last known state
        /// </summary>
        [DataMember(Name = "lastKnownState",
            EmitDefaultValue = false)]
        public JToken LastKnownState { get; set; }

        /// <summary>
        /// Processing mode
        /// </summary>
        [DataMember(Name = "processMode",
            EmitDefaultValue = false)]
        public ProcessMode? ProcessMode { get; set; }
    }
}