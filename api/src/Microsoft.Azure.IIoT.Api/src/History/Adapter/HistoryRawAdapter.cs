// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------

namespace Microsoft.Azure.IIoT.OpcUa.Api.History {
    using Microsoft.Azure.IIoT.OpcUa.Api.History.Models;
    using Microsoft.Azure.IIoT.OpcUa.History.Models;
    using Microsoft.Azure.IIoT.OpcUa.History;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Threading.Tasks;
    using Microsoft.Azure.IIoT.Serializers;

    /// <summary>
    /// Implements historic access services as adapter on top of api.
    /// </summary>
    public sealed class HistoryRawAdapter : IHistoricAccessServices<string> {

        /// <summary>
        /// Create service
        /// </summary>
        /// <param name="client"></param>
        /// <param name="serializer"></param>
        public HistoryRawAdapter(IHistoryServiceRawApi client, IJsonSerializer serializer) {
            _serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        /// <inheritdoc/>
        public async Task<HistoryReadResultModel<JToken>> HistoryReadAsync(
            string endpoint, HistoryReadRequestModel<JToken> request) {
            var result = await _client.HistoryReadRawAsync(endpoint,
                _serializer.Map<HistoryReadRequestApiModel<JToken>>(request));
            return _serializer.Map<HistoryReadResultModel<JToken>>(result);
        }

        /// <inheritdoc/>
        public async Task<HistoryReadNextResultModel<JToken>> HistoryReadNextAsync(
            string endpoint, HistoryReadNextRequestModel request) {
            var result = await _client.HistoryReadRawNextAsync(endpoint,
                _serializer.Map<HistoryReadNextRequestApiModel>(request));
            return _serializer.Map<HistoryReadNextResultModel<JToken>>(result);
        }

        /// <inheritdoc/>
        public async Task<HistoryUpdateResultModel> HistoryUpdateAsync(
            string endpoint, HistoryUpdateRequestModel<JToken> request) {
            var result = await _client.HistoryUpdateRawAsync(endpoint,
                _serializer.Map<HistoryUpdateRequestApiModel<JToken>>(request));
            return _serializer.Map<HistoryUpdateResultModel>(result);
        }

        private readonly IJsonSerializer _serializer;
        private readonly IHistoryServiceRawApi _client;
    }
}
