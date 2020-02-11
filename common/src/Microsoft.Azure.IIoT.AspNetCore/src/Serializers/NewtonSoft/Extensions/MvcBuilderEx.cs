// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------

namespace Microsoft.Azure.IIoT.AspNetCore.Cors {
    using Microsoft.Azure.IIoT.Serializers;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;
    using System.Collections.Generic;
    using System;
    using System.Linq;
    using Newtonsoft.Json;

    /// <summary>
    /// Cors setup extensions
    /// </summary>
    public static class MvcBuilderEx {

        /// <summary>
        /// Add json serializer
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IMvcBuilder AddNewtonsoftJsonSerializer(this IMvcBuilder builder) {
            if (builder is null) {
                throw new ArgumentNullException(nameof(builder));
            }

            builder = builder.AddNewtonsoftJson();

            // Configure json serializer settings transiently to pick up all converters
            builder.Services.AddTransient<IConfigureOptions<MvcNewtonsoftJsonOptions>>(services =>
                new ConfigureNamedOptions<MvcNewtonsoftJsonOptions>(Options.DefaultName, options => {
                    var provider = services.GetService<IJsonSerializerSettingsProvider>();
                    var settings = provider?.Settings;
                    if (settings is null) {
                        return;
                    }

                    options.SerializerSettings.Formatting = settings.Formatting;
                    options.SerializerSettings.NullValueHandling = settings.NullValueHandling;
                    options.SerializerSettings.DefaultValueHandling = settings.DefaultValueHandling;
                    options.SerializerSettings.ContractResolver = settings.ContractResolver;
                    options.SerializerSettings.DateFormatHandling = settings.DateFormatHandling;
                    options.SerializerSettings.MaxDepth = settings.MaxDepth;
                    options.SerializerSettings.Context = settings.Context;

                    var set = new HashSet<JsonConverter>(options.SerializerSettings.Converters);
                    if (!set.IsProperSupersetOf(settings.Converters)) {
                        options.SerializerSettings.Converters =
                            set.MergeWith(settings.Converters).ToList();
                    }
                }));
            return builder;
        }
    }
}
