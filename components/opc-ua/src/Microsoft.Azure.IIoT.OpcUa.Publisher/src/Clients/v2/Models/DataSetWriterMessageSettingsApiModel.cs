// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------

namespace Microsoft.Azure.IIoT.OpcUa.Publisher.Clients.v2.Models {
    using Microsoft.Azure.IIoT.OpcUa.Publisher.Models;
    using System.Runtime.Serialization;
    using System;

    /// <summary>
    /// Data set writer message model
    /// </summary>
    [DataContract]
    public class DataSetWriterMessageSettingsApiModel {
        /// <summary>
        /// Default constructor
        /// </summary>
        public DataSetWriterMessageSettingsApiModel() { }

        /// <summary>
        /// Create api model from service model
        /// </summary>
        /// <param name="model"></param>
        public DataSetWriterMessageSettingsApiModel(DataSetWriterMessageSettingsModel model) {
            if (model == null) {
                throw new ArgumentNullException(nameof(model));
            }
            ConfiguredSize = model.ConfiguredSize;
            DataSetMessageContentMask = model.DataSetMessageContentMask;
            DataSetOffset = model.DataSetOffset;
            NetworkMessageNumber = model.NetworkMessageNumber;
        }

        /// <summary>
        /// Create service model from api model
        /// </summary>
        public DataSetWriterMessageSettingsModel ToServiceModel() {
            return new DataSetWriterMessageSettingsModel {
                ConfiguredSize = ConfiguredSize,
                DataSetMessageContentMask = DataSetMessageContentMask,
                DataSetOffset = DataSetOffset,
                NetworkMessageNumber = NetworkMessageNumber
            };
        }


        /// <summary>
        /// Dataset message content
        /// </summary>
        [DataMember(Name = "dataSetMessageContentMask",
            EmitDefaultValue = false)]
        public DataSetContentMask? DataSetMessageContentMask { get; set; }

        /// <summary>
        /// Configured size of network message
        /// </summary>
        [DataMember(Name = "configuredSize",
            EmitDefaultValue = false)]
        public ushort? ConfiguredSize { get; set; }

        /// <summary>
        /// Uadp metwork message number
        /// </summary>
        [DataMember(Name = "networkMessageNumber",
            EmitDefaultValue = false)]
        public ushort? NetworkMessageNumber { get; set; }

        /// <summary>
        /// Uadp dataset offset
        /// </summary>
        [DataMember(Name = "dataSetOffset",
            EmitDefaultValue = false)]
        public ushort? DataSetOffset { get; set; }
    }
}
