// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------

namespace Microsoft.Azure.IIoT.Modules.OpcUa.Publisher.v2.Models {
    using Microsoft.Azure.IIoT.OpcUa.Publisher.Models;
    using System.Runtime.Serialization;
    using System;

    /// <summary>
    /// Published dataset settings - corresponds to SubscriptionModel
    /// </summary>
    [DataContract]
    public class PublishedDataSetSettingsApiModel {
        /// <summary>
        /// Default constructor
        /// </summary>
        public PublishedDataSetSettingsApiModel() { }

        /// <summary>
        /// Create api model from service model
        /// </summary>
        /// <param name="model"></param>
        public PublishedDataSetSettingsApiModel(PublishedDataSetSettingsModel model) {
            if (model == null) {
                throw new ArgumentNullException(nameof(model));
            }
            LifeTimeCount = model.LifeTimeCount;
            MaxKeepAliveCount = model.MaxKeepAliveCount;
            MaxNotificationsPerPublish = model.MaxNotificationsPerPublish;
            Priority = model.Priority;
            PublishingInterval = model.PublishingInterval;
        }

        /// <summary>
        /// Create service model from api model
        /// </summary>
        public PublishedDataSetSettingsModel ToServiceModel() {
            return new PublishedDataSetSettingsModel {
                LifeTimeCount = LifeTimeCount,
                MaxKeepAliveCount = MaxKeepAliveCount,
                MaxNotificationsPerPublish = MaxNotificationsPerPublish,
                Priority = Priority,
                PublishingInterval = PublishingInterval
            };
        }

        /// <summary>
        /// Publishing interval
        /// </summary>
        [DataMember(Name = "publishingInterval",
            EmitDefaultValue = false)]
        public TimeSpan? PublishingInterval { get; set; }

        /// <summary>
        /// Life time
        /// </summary>
        [DataMember(Name = "lifeTimeCount",
            EmitDefaultValue = false)]
        public uint? LifeTimeCount { get; set; }

        /// <summary>
        /// Max keep alive count
        /// </summary>
        [DataMember(Name = "maxKeepAliveCount",
            EmitDefaultValue = false)]
        public uint? MaxKeepAliveCount { get; set; }

        /// <summary>
        /// Max notifications per publish
        /// </summary>
        [DataMember(Name = "maxNotificationsPerPublish",
            EmitDefaultValue = false)]
        public uint? MaxNotificationsPerPublish { get; set; }

        /// <summary>
        /// Priority
        /// </summary>
        [DataMember(Name = "priority",
            EmitDefaultValue = false)]
        public byte? Priority { get; set; }
    }
}