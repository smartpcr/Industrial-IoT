// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------

namespace Microsoft.Azure.IIoT.AspNetCore.Cors {
    using Microsoft.Azure.IIoT.Serializers;
    using Microsoft.Extensions.DependencyInjection;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System;

    /// <summary>
    /// Cors setup extensions
    /// </summary>
    public static class MvcBuilderEx {

        /// <summary>
        /// Add json serializer
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IMvcBuilder AddFormatters(this IMvcBuilder builder) {
            if (builder is null) {
                throw new ArgumentNullException(nameof(builder));
            }
            var services = builder.Services.BuildServiceProvider();
            var provider = services.GetService<IJsonSerializerSettingsProvider>();
            var serializer = services.GetService<IJsonSerializer>();
            // Add newton soft json if the serializer is such - otherwise use default;
            if (serializer is null || serializer is NewtonSoftJsonSerializer) {
                builder = builder.AddNewtonsoftJson(options => {
                    options.SerializerSettings.Update(provider?.GetSettings());
                });
            }
            return builder;
        }

        /// <summary>
        /// Update settings
        /// </summary>
        /// <param name="setting"></param>
        /// <param name="update"></param>
        private static void Update(this JsonSerializerSettings setting,
            JsonSerializerSettings update) {
            if (update is null) {
                return;
            }
            setting.Formatting = update.Formatting;
            setting.NullValueHandling = update.NullValueHandling;
            setting.DefaultValueHandling = update.DefaultValueHandling;
            setting.ContractResolver = update.ContractResolver;
            setting.DateFormatHandling = update.DateFormatHandling;
            setting.MaxDepth = update.MaxDepth;
            setting.ConstructorHandling = update.ConstructorHandling;
            setting.CheckAdditionalContent = update.CheckAdditionalContent;
            setting.DateTimeZoneHandling = update.DateTimeZoneHandling;
            setting.Context = update.Context;
            setting.SerializationBinder = update.SerializationBinder;
            setting.TraceWriter = update.TraceWriter;
            setting.EqualityComparer = update.EqualityComparer;
            setting.Error = update.Error;
            // ...
            setting.Converters.AddRange(update.Converters);
        }
    }
}
