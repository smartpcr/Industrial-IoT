// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------

namespace Microsoft.Azure.IIoT.OpcUa.Subscriber.Models {
    using Microsoft.Azure.IIoT.Serializers;
    using System;
    using System.Linq;

    /// <summary>
    /// Publisher sample model extensions
    /// </summary>
    public static class MonitoredItemMessageModelEx {

        /// <summary>
        /// Clone sample
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static MonitoredItemSampleModel Clone(this MonitoredItemSampleModel model) {
            if (model is null) {
                return null;
            }
            return new MonitoredItemSampleModel {
                SubscriptionId = model.SubscriptionId,
                EndpointId = model.EndpointId,
                DataSetId = model.DataSetId,
                NodeId = model.NodeId,
                ServerPicoseconds = model.ServerPicoseconds,
                ServerTimestamp = model.ServerTimestamp,
                SourcePicoseconds = model.SourcePicoseconds,
                SourceTimestamp = model.SourceTimestamp,
                Timestamp = model.Timestamp,
                TypeId = model.TypeId,
                Value = model.Value,
                Status = model.Status
            };
        }

        /// <summary>
        /// Try to convert json to sample model
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        internal static MonitoredItemSampleModel ToServiceModel(this VariantValue message) {
            if (message.Type != VariantValueType.Object) {
                // Not a publisher sample object - not accepted
                return null;
            }

            if (!message.TryGetValue("Value", out var value)) {
                // No value
                return null;
            }

            //
            // Check if the value is a data value or if the value was flattened into the root.
            //
            var sampleRoot = message;
            if (IsDataValue(value)) {
                sampleRoot = value;
                value = sampleRoot.GetValueOrDefault<VariantValue>("Value",
                    StringComparison.InvariantCultureIgnoreCase);
            }

            // check if value comes from the legacy publisher:
            var applicationUri = sampleRoot.GetValueOrDefault<string>("ApplicationUri",
                StringComparison.InvariantCultureIgnoreCase);
            if (applicationUri is null || applicationUri == string.Empty) {
                // this is not a legacy publisher message
                return new MonitoredItemSampleModel {
                    Value = GetValue(value, out var typeId),
                    TypeId = typeId?.ToString(),
                    Status = sampleRoot.GetValueOrDefault<string>(
                        nameof(MonitoredItemSampleModel.Status),
                            StringComparison.InvariantCultureIgnoreCase),
                    DataSetId = sampleRoot.GetValueOrDefault<string>(
                        nameof(MonitoredItemSampleModel.DataSetId),
                            StringComparison.InvariantCultureIgnoreCase),
                    Timestamp = sampleRoot.GetValueOrDefault<DateTime?>(
                        nameof(MonitoredItemSampleModel.Timestamp),
                            StringComparison.InvariantCultureIgnoreCase),
                    SubscriptionId = sampleRoot.GetValueOrDefault<string>(
                        nameof(MonitoredItemSampleModel.SubscriptionId),
                            StringComparison.InvariantCultureIgnoreCase),
                    EndpointId = sampleRoot.GetValueOrDefault<string>(
                        nameof(MonitoredItemSampleModel.EndpointId),
                            StringComparison.InvariantCultureIgnoreCase),
                    NodeId = sampleRoot.GetValueOrDefault<string>(
                        nameof(MonitoredItemSampleModel.NodeId),
                            StringComparison.InvariantCultureIgnoreCase),
                    SourcePicoseconds = sampleRoot.GetValueOrDefault<ushort?>(
                        nameof(MonitoredItemSampleModel.SourcePicoseconds),
                            StringComparison.InvariantCultureIgnoreCase),
                    ServerPicoseconds = sampleRoot.GetValueOrDefault<ushort?>(
                        nameof(MonitoredItemSampleModel.ServerPicoseconds),
                            StringComparison.InvariantCultureIgnoreCase),
                    SourceTimestamp = sampleRoot.GetValueOrDefault<DateTime?>(
                        nameof(MonitoredItemSampleModel.SourceTimestamp),
                            StringComparison.InvariantCultureIgnoreCase),
                    ServerTimestamp = sampleRoot.GetValueOrDefault<DateTime?>(
                        nameof(MonitoredItemSampleModel.ServerTimestamp),
                            StringComparison.InvariantCultureIgnoreCase),
                };
            }
            else {
                // legacy publisher message
                return new MonitoredItemSampleModel {
                    Value = GetValue(value, out var typeId),
                    TypeId = typeId?.ToString(),
                    DataSetId = sampleRoot.GetValueOrDefault<string>("DisplayName",
                        StringComparison.InvariantCultureIgnoreCase),
                    Timestamp = sampleRoot.GetValueOrDefault<DateTime?>(
                    nameof(MonitoredItemSampleModel.Timestamp),
                        StringComparison.InvariantCultureIgnoreCase),
                    SubscriptionId = "LegacyPublisher",
                    EndpointId = applicationUri,
                    NodeId = sampleRoot.GetValueOrDefault<string>(
                    nameof(MonitoredItemSampleModel.NodeId),
                        StringComparison.InvariantCultureIgnoreCase),
                    SourcePicoseconds = sampleRoot.GetValueOrDefault<ushort?>(
                    nameof(MonitoredItemSampleModel.SourcePicoseconds),
                        StringComparison.InvariantCultureIgnoreCase),
                    ServerPicoseconds = sampleRoot.GetValueOrDefault<ushort?>(
                    nameof(MonitoredItemSampleModel.ServerPicoseconds),
                        StringComparison.InvariantCultureIgnoreCase),
                    SourceTimestamp = sampleRoot.GetValueOrDefault<DateTime?>(
                    nameof(MonitoredItemSampleModel.SourceTimestamp),
                        StringComparison.InvariantCultureIgnoreCase),
                    ServerTimestamp = sampleRoot.GetValueOrDefault<DateTime?>(
                    nameof(MonitoredItemSampleModel.ServerTimestamp),
                        StringComparison.InvariantCultureIgnoreCase),
                };
            }
        }

        /// <summary>
        /// Get value from value object
        /// </summary>
        /// <param name="token"></param>
        /// <param name="typeId"></param>
        /// <returns></returns>
        private static VariantValue GetValue(VariantValue token, out VariantValue typeId) {
            if (token.Type != VariantValueType.Object) {
                typeId = null;
            }
            else if (
                token.TryGetValue("Type", out typeId)) {
                token.TryGetValue("Body", out token);
            }

            return token;
        }

        /// <summary>
        /// Is this a datavalue object?
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        private static bool IsDataValue(this VariantValue token) {
            var properties = new[] {
                "SourcePicoseconds", "ServerPicoseconds",
                "ServerTimestamp", "SourceTimestamp",
            };
            if (token.Type != VariantValueType.Object) {
                // Not a publisher sample object - not accepted
                return false;
            }
            return token.Keys.Any(p => p.AnyOf(properties, true));
        }
    }
}
