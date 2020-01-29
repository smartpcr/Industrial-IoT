// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------

namespace Microsoft.Azure.IIoT.Modules.OpcUa.Twin.v2.Models {
    using Microsoft.Azure.IIoT.OpcUa.Twin.Models;
    using System.Runtime.Serialization;
    using Newtonsoft.Json.Linq;
    using System;

    /// <summary>
    /// method arg model
    /// </summary>
    [DataContract]
    public class MethodCallArgumentApiModel {

        /// <summary>
        /// Default constructor
        /// </summary>
        public MethodCallArgumentApiModel() { }

        /// <summary>
        /// Create from service model
        /// </summary>
        /// <param name="model"></param>
        public MethodCallArgumentApiModel(MethodCallArgumentModel model) {
            if (model == null) {
                throw new ArgumentNullException(nameof(model));
            }
            Value = model.Value;
            DataType = model.DataType;
        }

        /// <summary>
        /// Convert back to service model
        /// </summary>
        /// <returns></returns>
        public MethodCallArgumentModel ToServiceModel() {
            return new MethodCallArgumentModel {
                Value = Value,
                DataType = DataType
            };
        }

        /// <summary>
        /// Initial value or value to use
        /// </summary>
        [DataMember(Name = "Value")]
        public JToken Value { get; set; }

        /// <summary>
        /// Data type Id of the value (from meta data)
        /// </summary>
        [DataMember(Name = "DataType")]
        public string DataType { get; set; }
    }
}
