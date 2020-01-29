// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------

namespace Microsoft.Azure.IIoT.Modules.OpcUa.Publisher.v2.Models {
    using Microsoft.Azure.IIoT.OpcUa.Publisher.Models;
    using System.Runtime.Serialization;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Describes the field metadata
    /// </summary>
    [DataContract]
    public class FieldMetaDataApiModel {
        /// <summary>
        /// Default constructor
        /// </summary>
        public FieldMetaDataApiModel() { }

        /// <summary>
        /// Create api model from service model
        /// </summary>
        /// <param name="model"></param>
        public FieldMetaDataApiModel(FieldMetaDataModel model) {
            if (model == null) {
                throw new ArgumentNullException(nameof(model));
            }
            Description = model.Description == null ? null :
                new LocalizedTextApiModel(model.Description);
            ArrayDimensions = model.ArrayDimensions?.ToList();
            BuiltInType = model.BuiltInType;
            DataSetFieldId = model.DataSetFieldId;
            DataTypeId = model.DataTypeId;
            FieldFlags = model.FieldFlags;
            MaxStringLength = model.MaxStringLength;
            Name = model.Name;
            Properties = model.Properties?
                .ToDictionary(k => k.Key, v => v.Value);
            ValueRank = model.ValueRank;
        }

        /// <summary>
        /// Create service model from api model
        /// </summary>
        public FieldMetaDataModel ToServiceModel() {
            return new FieldMetaDataModel {
                Description = Description?.ToServiceModel(),
                ArrayDimensions = ArrayDimensions?.ToList(),
                BuiltInType = BuiltInType,
                DataSetFieldId = DataSetFieldId,
                DataTypeId = DataTypeId,
                FieldFlags = FieldFlags,
                MaxStringLength = MaxStringLength,
                Name = Name,
                Properties = Properties?
                    .ToDictionary(k => k.Key, v => v.Value),
                ValueRank = ValueRank
            };
        }

        /// <summary>
        /// Name of the field
        /// </summary>
        [DataMember(Name = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Description for the field
        /// </summary>
        [DataMember(Name = "description",
            EmitDefaultValue = false)]
        public LocalizedTextApiModel Description { get; set; }

        /// <summary>
        /// Field Flags.
        /// </summary>
        [DataMember(Name = "fieldFlags",
            EmitDefaultValue = false)]
        public ushort? FieldFlags { get; set; }

        /// <summary>
        /// Built in type
        /// </summary>
        [DataMember(Name = "builtInType",
            EmitDefaultValue = false)]
        public string BuiltInType { get; set; }

        /// <summary>
        /// The Datatype Id
        /// </summary>
        [DataMember(Name = "dataTypeId")]
        public string DataTypeId { get; set; }

        /// <summary>
        /// ValueRank.
        /// </summary>
        [DataMember(Name = "valueRank",
            EmitDefaultValue = false)]
        public int? ValueRank { get; set; }

        /// <summary>
        /// Array dimensions
        /// </summary>
        [DataMember(Name = "arrayDimensions",
            EmitDefaultValue = false)]
        public List<uint> ArrayDimensions { get; set; }

        /// <summary>
        /// Max String Length constraint.
        /// </summary>
        [DataMember(Name = "maxStringLength",
            EmitDefaultValue = false)]
        public uint? MaxStringLength { get; set; }

        /// <summary>
        /// The unique guid of the field in the dataset.
        /// </summary>
        [DataMember(Name = "dataSetFieldId",
            EmitDefaultValue = false)]
        public Guid? DataSetFieldId { get; set; }

        /// <summary>
        /// Additional properties
        /// </summary>
        [DataMember(Name = "properties",
            EmitDefaultValue = false)]
        public Dictionary<string, string> Properties { get; set; }
    }
}
