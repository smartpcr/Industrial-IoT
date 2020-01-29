// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------

namespace Microsoft.Azure.IIoT.Modules.OpcUa.Publisher.v2.Models {
    using Microsoft.Azure.IIoT.OpcUa.Publisher.Models;
    using System.Runtime.Serialization;
    using System;

    /// <summary>
    /// Publisher processing engine configuration
    /// </summary>
    [DataContract]
    public class EngineConfigurationApiModel {

        /// <summary>
        /// Default constructor
        /// </summary>
        public EngineConfigurationApiModel() { }

        /// <summary>
        /// Create api model from service model
        /// </summary>
        /// <param name="model"></param>
        public EngineConfigurationApiModel(EngineConfigurationModel model) {
            if (model == null) {
                throw new ArgumentNullException(nameof(model));
            }
            BatchSize = model.BatchSize;
            DiagnosticsInterval = model.DiagnosticsInterval;
        }

        /// <summary>
        /// Create service model from api model
        /// </summary>
        public EngineConfigurationModel ToServiceModel() {
            return new EngineConfigurationModel {
                BatchSize = BatchSize,
                DiagnosticsInterval = DiagnosticsInterval
            };
        }

        /// <summary>
        /// Buffer size
        /// </summary>
        [DataMember(Name = "batchSize",
            EmitDefaultValue = false)]
        public int? BatchSize { get; set; }

        /// <summary>
        /// Interval for diagnostic messages
        /// </summary>
        [DataMember(Name = "diagnosticsInterval",
            EmitDefaultValue = false)]
        public TimeSpan? DiagnosticsInterval { get; set; }
    }
}