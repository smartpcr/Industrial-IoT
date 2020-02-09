// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------

namespace Microsoft.Azure.IIoT.Agent.Framework.Serializer {
    using Microsoft.Azure.IIoT.Agent.Framework.Exceptions;
    using Microsoft.Azure.IIoT.Serializers;
    using System;
    using System.Linq;

    /// <summary>
    /// Default job configuration serializer
    /// </summary>
    public class DefaultJobSerializer : IJobSerializer {

        /// <summary>
        /// Create serializer
        /// </summary>
        /// <param name="serializer"></param>
        /// <param name="knownJobConfigProvider"></param>
        public DefaultJobSerializer(IJsonSerializer serializer,
            IKnownJobConfigProvider knownJobConfigProvider) {
            _serializer = serializer;
            _knownJobConfigProvider = knownJobConfigProvider;
        }

        /// <inheritdoc/>
        public VariantValue SerializeJobConfiguration<T>(T jobConfig, out string jobConfigurationType) {
            jobConfigurationType = typeof(T).Name;
            return _serializer.FromObject(jobConfig);
        }

        /// <inheritdoc/>
        public object DeserializeJobConfiguration(VariantValue model, string jobConfigurationType) {
            var type = _knownJobConfigProvider.KnownJobTypes
                .SingleOrDefault(t => t.Name.Equals(jobConfigurationType, StringComparison.OrdinalIgnoreCase));
            if (type is null) {
                throw new UnknownJobTypeException(jobConfigurationType);
            }
            return model.ToObject(type);
        }

        private readonly IJsonSerializer _serializer;
        private readonly IKnownJobConfigProvider _knownJobConfigProvider;
    }
}