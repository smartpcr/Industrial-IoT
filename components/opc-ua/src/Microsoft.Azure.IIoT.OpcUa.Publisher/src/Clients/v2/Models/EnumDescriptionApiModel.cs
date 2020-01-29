// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------

namespace Microsoft.Azure.IIoT.OpcUa.Publisher.Clients.v2.Models {
    using Microsoft.Azure.IIoT.OpcUa.Core.Models;
    using System.Runtime.Serialization;
    using System;

    /// <summary>
    /// Describes the enumeration
    /// </summary>
    [DataContract]
    public class EnumDescriptionApiModel {
        /// <summary>
        /// Default constructor
        /// </summary>
        public EnumDescriptionApiModel() { }

        /// <summary>
        /// Create api model from service model
        /// </summary>
        /// <param name="model"></param>
        public EnumDescriptionApiModel(EnumDescriptionModel model) {
            if (model == null) {
                throw new ArgumentNullException(nameof(model));
            }
            EnumDefinition = model.EnumDefinition == null ? null :
                new EnumDefinitionApiModel(model.EnumDefinition);
            Name = model.Name;
            BuiltInType = model.BuiltInType;
            DataTypeId = model.DataTypeId;
        }

        /// <summary>
        /// Create service model from api model
        /// </summary>
        public EnumDescriptionModel ToServiceModel() {
            return new EnumDescriptionModel {
                Name = Name,
                BuiltInType = BuiltInType,
                DataTypeId = DataTypeId,
                EnumDefinition = EnumDefinition?.ToServiceModel()
            };
        }

        /// <summary>
        /// Data type id
        /// </summary>
        [DataMember(Name = "dataTypeId")]
        public string DataTypeId { get; set; }

        /// <summary>
        /// The qualified name of the enum
        /// </summary>
        [DataMember(Name = "name",
            EmitDefaultValue = false)]
        public string Name { get; set; }

        /// <summary>
        /// Enum definition
        /// </summary>
        [DataMember(Name = "enumDefinition")]
        public EnumDefinitionApiModel EnumDefinition { get; set; }

        /// <summary>
        /// The built in type of the enum
        /// </summary>
        [DataMember(Name = "builtInType",
            EmitDefaultValue = false)]
        public string BuiltInType { get; set; }
    }
}
