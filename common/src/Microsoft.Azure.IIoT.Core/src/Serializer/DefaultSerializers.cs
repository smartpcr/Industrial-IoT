// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------

namespace Microsoft.Azure.IIoT {
    using Microsoft.Azure.IIoT.Serializer;
    using Autofac;

    /// <summary>
    /// All pluggable serializers
    /// </summary>
    public class DefaultSerializers : Autofac.Module {

        /// <inheritdoc/>
        protected override void Load(ContainerBuilder builder) {

            builder.RegisterType<NewtonSoftJsonSerializer>()
                .AsImplementedInterfaces().SingleInstance()
                .IfNotRegistered(typeof(IJsonSerializer));

            base.Load(builder);
        }
    }
}