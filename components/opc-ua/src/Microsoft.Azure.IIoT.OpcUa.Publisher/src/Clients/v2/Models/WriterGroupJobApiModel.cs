// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------

namespace Microsoft.Azure.IIoT.OpcUa.Publisher.Clients.v2.Models {
    using Microsoft.Azure.IIoT.OpcUa.Publisher.Models;
    using System.Runtime.Serialization;
    using System;

    /// <summary>
    /// Writer group job model
    /// </summary>
    [DataContract]
    public class WriterGroupJobApiModel {

        /// <summary>
        /// Default constructor
        /// </summary>
        public WriterGroupJobApiModel() { }

        /// <summary>
        /// Create api model from service model
        /// </summary>
        /// <param name="model"></param>
        public WriterGroupJobApiModel(WriterGroupJobModel model) {
            if (model?.WriterGroup == null) {
                throw new ArgumentNullException(nameof(model));
            }
            WriterGroup = new WriterGroupApiModel(model.WriterGroup);
            ConnectionString = model.ConnectionString;
            Engine = model.Engine == null ? null :
                new EngineConfigurationApiModel(model.Engine);
            MessagingMode = model.MessagingMode;
        }

        /// <summary>
        /// Create service model from api model
        /// </summary>
        public WriterGroupJobModel ToServiceModel() {
            return new WriterGroupJobModel {
                WriterGroup = WriterGroup?.ToServiceModel(),
                ConnectionString = ConnectionString,
                Engine = Engine?.ToServiceModel(),
                MessagingMode = MessagingMode
            };
        }

        /// <summary>
        /// Writer group
        /// </summary>
        [DataMember(Name = "writerGroup")]
        public WriterGroupApiModel WriterGroup { get; set; }

        /// <summary>
        /// Connection string
        /// </summary>
        [DataMember(Name = "connectionString")]
        public string ConnectionString { get; set; }

        /// <summary>
        /// Messaging mode - defaults to monitoreditem
        /// </summary>
        [DataMember(Name = "messagingMode",
            EmitDefaultValue = false)]
        public MessagingMode? MessagingMode { get; set; }

        /// <summary>
        /// Publisher engine configuration (publisher extension)
        /// </summary>
        [DataMember(Name = "engine",
            EmitDefaultValue = false)]
        public EngineConfigurationApiModel Engine { get; set; }
    }
}