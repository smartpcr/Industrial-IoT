// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------

namespace Microsoft.Azure.IIoT.OpcUa.Subscriber.Handlers {
    using Microsoft.Azure.IIoT.OpcUa.Subscriber.Models;
    using Microsoft.Azure.IIoT.Messaging;
    using Microsoft.Azure.IIoT.Serializer;
    using System;
    using System.Threading.Tasks;
    using System.Text;

    /// <summary>
    /// Forwards samples to another event hub
    /// </summary>
    public sealed class MonitoredItemSampleForwarder : IMonitoredItemSampleProcessor,
        IDisposable {

        /// <summary>
        /// Create forwarder
        /// </summary>
        /// <param name="queue"></param>
        /// <param name="serializer"></param>
        public MonitoredItemSampleForwarder(IEventQueueService queue,
            IJsonSerializer serializer) {
            _serializer = serializer ??
                throw new ArgumentNullException(nameof(serializer));
            if (queue == null) {
                throw new ArgumentNullException(nameof(queue));
            }
            _client = queue.OpenAsync().Result;
        }

        /// <inheritdoc/>
        public Task HandleSampleAsync(MonitoredItemSampleModel sample) {
            sample = sample.Clone();
            // Set timestamp as source timestamp
            // TODO: Make configurablew
            sample.Timestamp = sample.SourceTimestamp;
            return _client.SendAsync(Encoding.UTF8.GetBytes(
                _serializer.SerializeObject(sample)));
        }

        /// <inheritdoc/>
        public void Dispose() {
            _client.Dispose();
        }

        private readonly IEventQueueClient _client;
        private readonly IJsonSerializer _serializer;
    }
}
