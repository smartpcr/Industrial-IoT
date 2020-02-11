// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------

namespace Microsoft.Azure.IIoT.Serializers.MessagePack {
    using System.Collections.Generic;
    using global::MessagePack;

    /// <summary>
    /// Default formatters
    /// </summary>
    public class MessagePackFormatters : IMessagePackFormatterResolverProvider {

        /// <inheritdoc/>
        public virtual IEnumerable<IFormatterResolver> GetFormatters() {
            var converters = new List<IFormatterResolver> {
                // TODO
            };
            return converters;
        }
    }
}
