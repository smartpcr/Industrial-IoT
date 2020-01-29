// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------

namespace Microsoft.Azure.IIoT.Modules.OpcUa.Publisher.v2.Models {
    using Microsoft.Azure.IIoT.OpcUa.Publisher.Models;
    using System.Runtime.Serialization;
    using System;

    /// <summary>
    /// Pub/sub job description
    /// </summary>
    [DataContract]
    public class DataSetWriterApiModel {

        /// <summary>
        /// Default constructor
        /// </summary>
        public DataSetWriterApiModel() { }

        /// <summary>
        /// Create api model from service model
        /// </summary>
        /// <param name="model"></param>
        public DataSetWriterApiModel(DataSetWriterModel model) {
            if (model == null) {
                throw new ArgumentNullException(nameof(model));
            }
            DataSetWriterId = model.DataSetWriterId;
            DataSet = model.DataSet == null ? null :
                new PublishedDataSetApiModel(model.DataSet);
            DataSetFieldContentMask = model.DataSetFieldContentMask;
            MessageSettings = model.MessageSettings == null ? null :
                new DataSetWriterMessageSettingsApiModel(model.MessageSettings);
            KeyFrameInterval = model.KeyFrameInterval;
            DataSetMetaDataSendInterval = model.DataSetMetaDataSendInterval;
            KeyFrameCount = model.KeyFrameCount;
        }

        /// <summary>
        /// Create service model from api model
        /// </summary>
        public DataSetWriterModel ToServiceModel() {
            return new DataSetWriterModel {
                DataSetWriterId = DataSetWriterId,
                DataSet = DataSet?.ToServiceModel(),
                DataSetFieldContentMask = DataSetFieldContentMask,
                DataSetMetaDataSendInterval = DataSetMetaDataSendInterval,
                KeyFrameCount = KeyFrameCount,
                KeyFrameInterval = KeyFrameInterval,
                MessageSettings = MessageSettings?.ToServiceModel()
            };
        }

        /// <summary>
        /// Dataset writer id
        /// </summary>
        [DataMember(Name = "DataSetWriterId")]
        public string DataSetWriterId { get; set; }

        /// <summary>
        /// Published dataset inline definition
        /// </summary>
        [DataMember(Name = "dataSet",
            EmitDefaultValue = false)]
        public PublishedDataSetApiModel DataSet { get; set; }

        /// <summary>
        /// Dataset field content mask
        /// </summary>
        [DataMember(Name = "dataSetFieldContentMask",
            EmitDefaultValue = false)]
        public DataSetFieldContentMask? DataSetFieldContentMask { get; set; }

        /// <summary>
        /// Data set message settings
        /// </summary>
        [DataMember(Name = "messageSettings",
            EmitDefaultValue = false)]
        public DataSetWriterMessageSettingsApiModel MessageSettings { get; set; }

        /// <summary>
        /// Keyframe count
        /// </summary>
        [DataMember(Name = "keyFrameCount",
            EmitDefaultValue = false)]
        public uint? KeyFrameCount { get; set; }

        /// <summary>
        /// Or keyframe timer interval (publisher extension)
        /// </summary>
        [DataMember(Name = "keyFrameInterval",
            EmitDefaultValue = false)]
        public TimeSpan? KeyFrameInterval { get; set; }

        /// <summary>
        /// Metadata message sending interval (publisher extension)
        /// </summary>
        [DataMember(Name = "dataSetMetaDataSendInterval",
            EmitDefaultValue = false)]
        public TimeSpan? DataSetMetaDataSendInterval { get; set; }
    }
}