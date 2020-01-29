// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------

namespace Microsoft.Azure.IIoT.Modules.OpcUa.Twin.v2.Models {
    using Microsoft.Azure.IIoT.OpcUa.Twin.Models;
    using System.Runtime.Serialization;
    using System;

    /// <summary>
    /// browse view model
    /// </summary>
    [DataContract]
    public class BrowseViewApiModel {

        /// <summary>
        /// Default constructor
        /// </summary>
        public BrowseViewApiModel() { }

        /// <summary>
        /// Create from service model
        /// </summary>
        /// <param name="model"></param>
        public BrowseViewApiModel(BrowseViewModel model) {
            if (model == null) {
                throw new ArgumentNullException(nameof(model));
            }
            ViewId = model.ViewId;
            Version = model.Version;
            Timestamp = model.Timestamp;
        }

        /// <summary>
        /// Convert back to service model
        /// </summary>
        /// <returns></returns>
        public BrowseViewModel ToServiceModel() {
            return new BrowseViewModel {
                ViewId = ViewId,
                Version = Version,
                Timestamp = Timestamp
            };
        }

        /// <summary>
        /// Node of the view to browse
        /// </summary>
        [DataMember(Name = "ViewId")]
        public string ViewId { get; set; }

        /// <summary>
        /// Browses specific version of the view.
        /// </summary>
        [DataMember(Name = "Version",
            EmitDefaultValue = false)]
        public uint? Version { get; set; }

        /// <summary>
        /// Browses at or before this timestamp.
        /// </summary>
        [DataMember(Name = "Timestamp",
            EmitDefaultValue = false)]
        public DateTime? Timestamp { get; set; }
    }
}
