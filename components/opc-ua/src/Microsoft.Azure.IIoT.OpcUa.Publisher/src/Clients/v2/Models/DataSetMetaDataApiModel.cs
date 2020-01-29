// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------

namespace Microsoft.Azure.IIoT.OpcUa.Publisher.Clients.v2.Models {
    using Microsoft.Azure.IIoT.OpcUa.Publisher.Models;
    using System.Runtime.Serialization;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Metadata for the published dataset
    /// </summary>
    [DataContract]
    public class DataSetMetaDataApiModel {

        /// <summary>
        /// Default constructor
        /// </summary>
        public DataSetMetaDataApiModel() { }

        /// <summary>
        /// Create api model from service model
        /// </summary>
        /// <param name="model"></param>
        public DataSetMetaDataApiModel(DataSetMetaDataModel model) {
            if (model == null) {
                throw new ArgumentNullException(nameof(model));
            }
            Name = model.Name;
            ConfigurationVersion = model.ConfigurationVersion == null ? null :
                new ConfigurationVersionApiModel(model.ConfigurationVersion);
            DataSetClassId = model.DataSetClassId;
            Description = model.Description == null ? null :
                new LocalizedTextApiModel(model.Description);
            Fields = model.Fields?
                .Select(f => new FieldMetaDataApiModel(f))
                .ToList();
            EnumDataTypes = model.EnumDataTypes?
                .Select(f => new EnumDescriptionApiModel(f))
                .ToList();
            StructureDataTypes = model.StructureDataTypes?
                .Select(f => new StructureDescriptionApiModel(f))
                .ToList();
            SimpleDataTypes = model.SimpleDataTypes?
                .Select(f => new SimpleTypeDescriptionApiModel(f))
                .ToList();
            Namespaces = model.Namespaces?
                .ToList();
        }

        /// <summary>
        /// Create service model from api model
        /// </summary>
        public DataSetMetaDataModel ToServiceModel() {
            return new DataSetMetaDataModel {
                Name = Name,
                ConfigurationVersion = ConfigurationVersion?.ToServiceModel(),
                DataSetClassId = DataSetClassId,
                Description = Description?.ToServiceModel(),
                Fields = Fields?
                    .Select(f => f.ToServiceModel())
                    .ToList(),
                EnumDataTypes = EnumDataTypes?
                    .Select(f => f.ToServiceModel())
                    .ToList(),
                StructureDataTypes = StructureDataTypes?
                    .Select(f => f.ToServiceModel())
                    .ToList(),
                SimpleDataTypes = SimpleDataTypes?
                    .Select(f => f.ToServiceModel())
                    .ToList(),
                Namespaces = Namespaces?.ToList()
            };
        }


        /// <summary>
        /// Name of the dataset
        /// </summary>
        [DataMember(Name = "name",
            EmitDefaultValue = false)]
        public string Name { get; set; }

        /// <summary>
        /// Description of the dataset
        /// </summary>
        [DataMember(Name = "description",
            EmitDefaultValue = false)]
        public LocalizedTextApiModel Description { get; set; }

        /// <summary>
        /// Metadata for the data set fiels
        /// </summary>
        [DataMember(Name = "fields",
            EmitDefaultValue = false)]
        public List<FieldMetaDataApiModel> Fields { get; set; }

        /// <summary>
        /// Dataset class id
        /// </summary>
        [DataMember(Name = "dataSetClassId",
            EmitDefaultValue = false)]
        public Guid DataSetClassId { get; set; }

        /// <summary>
        /// Dataset version
        /// </summary>
        [DataMember(Name = "configurationVersion",
            EmitDefaultValue = false)]
        public ConfigurationVersionApiModel ConfigurationVersion { get; set; }

        /// <summary>
        /// Namespaces in the metadata description
        /// </summary>
        [DataMember(Name = "namespaces",
            EmitDefaultValue = false)]
        public List<string> Namespaces { get; set; }

        /// <summary>
        /// Structure data types
        /// </summary>
        [DataMember(Name = "structureDataTypes",
            EmitDefaultValue = false)]
        public List<StructureDescriptionApiModel> StructureDataTypes { get; set; }

        /// <summary>
        /// Enum data types
        /// </summary>
        [DataMember(Name = "enumDataTypes",
            EmitDefaultValue = false)]
        public List<EnumDescriptionApiModel> EnumDataTypes { get; set; }

        /// <summary>
        /// Simple data type.
        /// </summary>
        [DataMember(Name = "simpleDataTypes",
            EmitDefaultValue = false)]
        public List<SimpleTypeDescriptionApiModel> SimpleDataTypes { get; set; }
    }
}
