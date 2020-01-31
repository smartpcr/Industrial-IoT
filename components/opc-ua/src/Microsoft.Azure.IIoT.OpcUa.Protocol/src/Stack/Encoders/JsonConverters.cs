// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------

namespace Opc.Ua.Encoders {
    using Opc.Ua;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using Newtonsoft.Json.Serialization;
    using System.Runtime.Serialization;

    /// <summary>
    /// Adds Ua converters to default json converters
    /// </summary>
    public class JsonConverters : Microsoft.Azure.IIoT.Serializers.NewtonSoftJsonConverters {

        /// <summary>
        /// Create configuration
        /// </summary>
        /// <param name="context"></param>
        /// <param name="permissive"></param>
        public JsonConverters(ServiceMessageContext context = null, 
            bool permissive = false) : base(permissive) {
            _context = context;
        }

        /// <inheritdoc/>
        public override JsonSerializerSettings GetSettings() {
            var settings = base.GetSettings();
            settings.Converters.AddUaConverters();
            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            if (_context != null) {
                settings.Context = new StreamingContext(StreamingContextStates.File, _context);
            }
            return settings;
        }

        private readonly ServiceMessageContext _context;
    }
}
