// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------

namespace Microsoft.Azure.IIoT.Hub {
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Event client extensions
    /// </summary>
    public static class EventClientEx {

        /// <summary>
        /// Send json
        /// </summary>
        /// <param name="client"></param>
        /// <param name="message"></param>
        /// <param name="eventSchema"></param>
        /// <returns></returns>
        public static Task SendJsonEventAsync(this IEventClient client, string message,
            string eventSchema) {
            return client.SendEventAsync(Encoding.UTF8.GetBytes(message),
                    ContentMimeType.Json, eventSchema, "utf-8");
        }

        /// <summary>
        /// Send json
        /// </summary>
        /// <param name="client"></param>
        /// <param name="messages"></param>
        /// <param name="eventSchema"></param>
        /// <returns></returns>
        public static Task SendJsonEventsAsync(this IEventClient client,
            IEnumerable<string> messages, string eventSchema) {
            return client.SendEventAsync(messages
                    .Select(message => Encoding.UTF8.GetBytes(message)),
                ContentMimeType.Json, eventSchema, "utf-8");
        }
    }
}
