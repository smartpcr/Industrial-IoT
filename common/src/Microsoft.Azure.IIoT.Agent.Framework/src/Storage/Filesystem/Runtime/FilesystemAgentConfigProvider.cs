// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------

namespace Microsoft.Azure.IIoT.Agent.Framework.Storage.Filesystem {
    using Microsoft.Azure.IIoT.Agent.Framework.Models;
    using Microsoft.Azure.IIoT.Serializers;
    using System.IO;

    /// <summary>
    /// File system configuration provider
    /// </summary>
    public class FilesystemAgentConfigProvider : IAgentConfigProvider {

        /// <summary>
        /// Create provider
        /// </summary>
        /// <param name="serializer"></param>
        /// <param name="config"></param>
        public FilesystemAgentConfigProvider(ISerializer serializer,
            FilesystemAgentConfigProviderConfig config) {
            _configFilename = config.ConfigFilename;
            var json = File.ReadAllText(_configFilename);
            Config = serializer.Deserialize<AgentConfigModel>(json);
        }

        /// <inheritdoc/>
        public AgentConfigModel Config { get; }

        /// <inheritdoc/>
#pragma warning disable 0067
        public event ConfigUpdatedEventHandler OnConfigUpdated;
#pragma warning restore 0067

        private readonly string _configFilename;
    }
}