// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------

namespace Microsoft.Azure.IIoT.Modules.OpcUa.Publisher.v2.Models {
    using Microsoft.Azure.IIoT.OpcUa.Core.Models;
    using Microsoft.Azure.IIoT.OpcUa.Publisher.Models;
    using System.Runtime.Serialization;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Dataset Field model
    /// </summary>
    [DataContract]
    public class PublishedDataSetVariableApiModel {

        /// <summary>
        /// Default constructor
        /// </summary>
        public PublishedDataSetVariableApiModel() { }

        /// <summary>
        /// Create api model from service model
        /// </summary>
        /// <param name="model"></param>
        public PublishedDataSetVariableApiModel(PublishedDataSetVariableModel model) {
            if (model == null) {
                throw new ArgumentNullException(nameof(model));
            }
            Id = model.Id;
            PublishedVariableNodeId = model.PublishedVariableNodeId;
            BrowsePath = model.BrowsePath;
            Attribute = model.Attribute;
            DataChangeFilter = model.DataChangeFilter;
            DeadbandType = model.DeadbandType;
            DeadbandValue = model.DeadbandValue;
            DiscardNew = model.DiscardNew;
            IndexRange = model.IndexRange;
            MetaDataProperties = model.MetaDataProperties?.ToList();
            QueueSize = model.QueueSize;
            SamplingInterval = model.SamplingInterval;
            MonitoringMode = model.MonitoringMode;
            SubstituteValue = model.SubstituteValue?.DeepClone();
            TriggerId = model.TriggerId;
        }

        /// <summary>
        /// Create service model from api model
        /// </summary>
        public PublishedDataSetVariableModel ToServiceModel() {
            return new PublishedDataSetVariableModel {
                Id = Id,
                PublishedVariableNodeId = PublishedVariableNodeId,
                BrowsePath = BrowsePath,
                Attribute = Attribute,
                DataChangeFilter = DataChangeFilter,
                DeadbandType = DeadbandType,
                DeadbandValue = DeadbandValue,
                DiscardNew = DiscardNew,
                IndexRange = IndexRange,
                MonitoringMode = MonitoringMode,
                MetaDataProperties = MetaDataProperties?.ToList(),
                QueueSize = QueueSize,
                SamplingInterval = SamplingInterval,
                TriggerId = TriggerId,
                SubstituteValue = SubstituteValue?.DeepClone()
            };
        }

        /// <summary>
        /// Identifier of variable in the dataset.
        /// </summary>
        [DataMember(Name = "id",
            EmitDefaultValue = false)]
        public string Id { get; set; }

        /// <summary>
        /// Node id
        /// </summary>
        [DataMember(Name = "publishedVariableNodeId",
            EmitDefaultValue = false)]
        public string PublishedVariableNodeId { get; set; }

        /// <summary>
        /// An optional component path from the node identified by
        /// PublishedVariableNodeId to the actual node to publish
        /// (Publisher extension).
        /// </summary>
        [DataMember(Name = "browsePath",
            EmitDefaultValue = false)]
        public string[] BrowsePath { get; set; }

        /// <summary>
        /// Default is <see cref="NodeAttribute.Value"/>.
        /// </summary>
        [DataMember(Name = "attribute",
            EmitDefaultValue = false)]
        public NodeAttribute? Attribute { get; set; }

        /// <summary>
        /// Index range
        /// </summary>
        [DataMember(Name = "indexRange",
            EmitDefaultValue = false)]
        public string IndexRange { get; set; }

        /// <summary>
        /// Sampling Interval - default is best effort
        /// </summary>
        [DataMember(Name = "samplingInterval",
            EmitDefaultValue = false)]
        public TimeSpan? SamplingInterval { get; set; }

        /// <summary>
        /// Data change filter
        /// </summary>
        [DataMember(Name = "dataChangeFilter",
            EmitDefaultValue = false)]
        public DataChangeTriggerType? DataChangeFilter { get; set; }

        /// <summary>
        /// Deadband type
        /// </summary>
        [DataMember(Name = "deadbandType",
            EmitDefaultValue = false)]
        public DeadbandType? DeadbandType { get; set; }

        /// <summary>
        /// Deadband value
        /// </summary>
        [DataMember(Name = "deadbandValue",
            EmitDefaultValue = false)]
        public double? DeadbandValue { get; set; }

        /// <summary>
        /// Substitution value for empty results
        /// </summary>
        [DataMember(Name = "substituteValue",
            EmitDefaultValue = false)]
        public JToken SubstituteValue { get; set; }

        /// <summary>
        /// MetaData properties qualified names.
        /// </summary>
        [DataMember(Name = "metaDataProperties",
            EmitDefaultValue = false)]
        public List<string> MetaDataProperties { get; set; }

        /// <summary>
        /// Monitoring mode (Publisher extension)
        /// </summary>
        [DataMember(Name = "monitoringMode",
            EmitDefaultValue = false)]
        public MonitoringMode? MonitoringMode { get; set; }

        /// <summary>
        /// Queue size (Publisher extension)
        /// </summary>
        [DataMember(Name = "queueSize",
            EmitDefaultValue = false)]
        public uint? QueueSize { get; set; }

        /// <summary>
        /// Discard new values if queue is full (Publisher extension)
        /// </summary>
        [DataMember(Name = "discardNew",
            EmitDefaultValue = false)]
        public bool? DiscardNew { get; set; }

        /// <summary>
        /// Node in dataset writer that triggers reporting
        /// (Publisher extension)
        /// </summary>
        [DataMember(Name = "triggerId",
            EmitDefaultValue = false)]
        public string TriggerId { get; set; }
    }
}