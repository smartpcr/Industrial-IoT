// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------

namespace Microsoft.Azure.IIoT.Hub.Services {
    using Microsoft.Azure.IIoT.Hub;
    using Microsoft.Azure.IIoT.Hub.Models;
    using Microsoft.Azure.IIoT.Serializer;
    using Serilog;
    using System.Collections.Generic;

    /// <summary>
    /// Module twin change events
    /// </summary>
    public sealed class IoTHubModuleLifecycleEventHandler : IoTHubDeviceTwinChangeHandlerBase {

        /// <inheritdoc/>
        public override string MessageSchema => Hub.MessageSchemaTypes.ModuleLifecycleNotification;

        /// <summary>
        /// Create handler
        /// </summary>
        /// <param name="serializer"></param>
        /// <param name="handlers"></param>
        /// <param name="logger"></param>
        public IoTHubModuleLifecycleEventHandler(IJsonSerializer serializer,
            IEnumerable<IIoTHubDeviceTwinEventHandler> handlers, ILogger logger) :
            base (serializer, handlers, logger) {
        }

        /// <summary>
        /// Get operation
        /// </summary>
        /// <param name="opType"></param>
        /// <returns></returns>
        protected override DeviceTwinEventType? GetOperation(string opType) {
            switch (opType) {
                case "createModuleIdentity":
                    return DeviceTwinEventType.New;
                case "deleteModuleIdentity":
                    return DeviceTwinEventType.Delete;
            }
            return null;
        }
    }
}
