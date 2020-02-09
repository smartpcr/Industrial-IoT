﻿// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------

namespace Microsoft.Azure.IIoT.App.Data {
    using Microsoft.Azure.IIoT.App.Models;
    using Microsoft.Azure.IIoT.OpcUa.Api.Registry.Models;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Handle event
    /// </summary>
    public static class EndpointInfoEx {

        /// <summary>
        /// Update a list of endpoints from a received event
        /// </summary>
        /// <param name="results"></param>
        /// <param name="ev"></param>
        public static void Update(this IList<EndpointInfo> results, EndpointEventApiModel ev) {
            var endpoint = results.FirstOrDefault(e => e.EndpointModel.Registration.Id == ev.Id);
            if (endpoint is null &&
                ev.EventType != EndpointEventType.New &&
                ev.EventType != EndpointEventType.Enabled) {
                return;
            }
            switch (ev.EventType) {
                case EndpointEventType.New:
                case EndpointEventType.Enabled:
                    if (endpoint is null) {
                        // Add if not already in list
                        results.Add(new EndpointInfo {
                            EndpointModel = ev.Endpoint
                        });
                    }
                    break;
                case EndpointEventType.Activated:
                case EndpointEventType.Deactivated:
                    break;
                case EndpointEventType.Updated:
                    ev.Endpoint.Patch(endpoint.EndpointModel, ev.IsPatch ?? false);
                    break;
                case EndpointEventType.Disabled:
                case EndpointEventType.Deleted:
                    results.Remove(endpoint);
                    break;
            }
        }
    }
}