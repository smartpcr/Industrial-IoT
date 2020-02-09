// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------

namespace Microsoft.Azure.IIoT.OpcUa.Protocol.Models {

    /// <summary>
    /// Subscription model extensions
    /// </summary>
    public static class SubscriptionConfigurationModelEx {

        /// <summary>
        /// Clone
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static SubscriptionConfigurationModel Clone(this SubscriptionConfigurationModel model) {
            if (model is null) {
                return null;
            }
            return new SubscriptionConfigurationModel {
                PublishingInterval = model.PublishingInterval,
                LifetimeCount = model.LifetimeCount,
                KeepAliveCount = model.KeepAliveCount,
                MaxNotificationsPerPublish = model.MaxNotificationsPerPublish,
                Priority = model.Priority
            };
        }

        /// <summary>
        /// Compare items
        /// </summary>
        /// <param name="model"></param>
        /// <param name="other"></param>
        /// <returns></returns>
        public static bool IsSameAs(this SubscriptionConfigurationModel model,
            SubscriptionConfigurationModel other) {
            if (model is null && other is null) {
                return true;
            }
            if (model is null || other is null) {
                return false;
            }
            if (model.PublishingInterval != other.PublishingInterval) {
                return false;
            }
            if (model.LifetimeCount != other.LifetimeCount) {
                return false;
            }
            if (model.KeepAliveCount != other.KeepAliveCount) {
                return false;
            }
            if (model.MaxNotificationsPerPublish != other.MaxNotificationsPerPublish) {
                return false;
            }
            if (model.Priority != other.Priority) {
                return false;
            }
            return true;
        }
    }
}