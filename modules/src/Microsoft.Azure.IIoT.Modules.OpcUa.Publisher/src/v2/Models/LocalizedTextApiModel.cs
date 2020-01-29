// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------

namespace Microsoft.Azure.IIoT.Modules.OpcUa.Publisher.v2.Models {
    using Microsoft.Azure.IIoT.OpcUa.Core.Models;
    using System.Runtime.Serialization;
    using System;

    /// <summary>
    /// Localized text.
    /// </summary>
    [DataContract]
    public class LocalizedTextApiModel {
        /// <summary>
        /// Default constructor
        /// </summary>
        public LocalizedTextApiModel() { }

        /// <summary>
        /// Create api model from service model
        /// </summary>
        /// <param name="model"></param>
        public LocalizedTextApiModel(LocalizedTextModel model) {
            if (model == null) {
                throw new ArgumentNullException(nameof(model));
            }
            Locale = model.Locale;
            Text = model.Text;
        }

        /// <summary>
        /// Create service model from api model
        /// </summary>
        public LocalizedTextModel ToServiceModel() {
            return new LocalizedTextModel {
                Locale = Locale,
                Text = Text
            };
        }

        /// <summary>
        /// Locale or null for default locale
        /// </summary>
        [DataMember(Name = "locale",
            EmitDefaultValue = false)]
        public string Locale { get; set; }

        /// <summary>
        /// Text
        /// </summary>
        [DataMember(Name = "text")]
        public string Text { get; set; }
    }
}
