// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------

namespace Microsoft.Azure.IIoT {
    using Microsoft.Azure.IIoT.Serializers;
    using Autofac;

    /// <summary>
    /// All pluggable serializers
    /// </summary>
    public class JsonSerializer : Autofac.Module {

        /// <inheritdoc/>
        protected override void Load(ContainerBuilder builder) {

            builder.RegisterType<NewtonSoftJsonSerializer>()
                .AsImplementedInterfaces().SingleInstance()
                .IfNotRegistered(typeof(IJsonSerializer));
            builder.RegisterType<NewtonSoftJsonConverters>()
                .AsImplementedInterfaces().SingleInstance()
                .IfNotRegistered(typeof(IJsonSerializerSettingsProvider));

            base.Load(builder);
        }
    }
}