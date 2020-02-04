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
    public static class SubscriberSampleModelEx {

        /// <summary>
        /// Clone sample
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static SubscriberSampleModel Clone(this SubscriberSampleModel model) {
            return new SubscriberSampleModel {
                SubscriptionId = model.SubscriptionId,
                EndpointId = model.EndpointId,
                DataSetId = model.DataSetId,
                NodeId = model.NodeId,
                ServerPicoseconds = model.ServerPicoseconds,
                ServerTimestamp = model.ServerTimestamp,
                SourcePicoseconds = model.SourcePicoseconds,
                SourceTimestamp = model.SourceTimestamp,
                Timestamp = model.Timestamp,
                Value = model.Value
            };
        }

        /// <summary>
        /// Try to convert json to sample model
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        internal static SubscriberSampleModel ToSubscriberSampleModel(this VariantValue message) {
            if (message.Type != VariantValueType.Object) {
                // Not a publisher sample object - not accepted
                return null;
            }

            if (!message.TryGetValue("Value", out var value)) {
                // No value
                return null;
            }

            var result = new SubscriberSampleModel() {
                Timestamp = DateTime.Now
            };

            // check if value comes from the legacy publisher:
            var applicationUri = message.GetValueOrDefault<string>("ApplicationUri",
                StringComparison.InvariantCultureIgnoreCase);
            if (applicationUri == null || applicationUri == string.Empty) {
                result.EndpointId = message.GetValueOrDefault<string>("EndpointId",
                    StringComparison.InvariantCultureIgnoreCase);
                result.SubscriptionId = message.GetValueOrDefault<string>("SubscriptionId",
                    StringComparison.InvariantCultureIgnoreCase);
                result.DataSetId = message.GetValueOrDefault<string>("DataSetId",
                    StringComparison.InvariantCultureIgnoreCase);
            }
            else {
                result.EndpointId = applicationUri;
                result.SubscriptionId = "LegacyPublisher";
                result.DataSetId = message.GetValueOrDefault<string>("DisplayName",
                    StringComparison.InvariantCultureIgnoreCase);
            }

            result.NodeId = message.GetValueOrDefault<string>("NodeId",
                StringComparison.InvariantCultureIgnoreCase);

            // Check if the value is a data value or if the value was flattened into the root.
            var dataValue = message;
            if (IsDataValue(value)) {
                dataValue = value;
                result.Value = value.GetValueOrDefault<VariantValue>("Value",
                    StringComparison.InvariantCultureIgnoreCase);
            }
            else {
                result.Value = value;
            }

            result.SourcePicoseconds = dataValue.GetValueOrDefault<ushort?>("SourcePicoseconds",
                StringComparison.InvariantCultureIgnoreCase);
            result.ServerPicoseconds = dataValue.GetValueOrDefault<ushort?>("ServerPicoseconds",
                StringComparison.InvariantCultureIgnoreCase);
            result.SourceTimestamp = dataValue.GetValueOrDefault<DateTime?>("SourceTimestamp",
                StringComparison.InvariantCultureIgnoreCase);
            result.ServerTimestamp = dataValue.GetValueOrDefault<DateTime?>("ServerTimestamp",
                StringComparison.InvariantCultureIgnoreCase);
            return result;
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
