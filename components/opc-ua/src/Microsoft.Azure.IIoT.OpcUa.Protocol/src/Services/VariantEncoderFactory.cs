// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------

namespace Microsoft.Azure.IIoT.OpcUa.Protocol.Services {
    using Microsoft.Azure.IIoT.Exceptions;
    using Microsoft.Azure.IIoT.Serializers;
    using Microsoft.Azure.IIoT.Utils;
    using Opc.Ua;
    using Opc.Ua.Encoders;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Json variant codec
    /// </summary>
    public class VariantEncoderFactory : IVariantEncoderFactory {

        /// <summary>
        /// Create encoder
        /// </summary>
        /// <param name="serializer"></param>
        public VariantEncoderFactory(IJsonSerializer serializer = null) {
            _serializer = serializer ?? new NewtonSoftJsonSerializer();
        }

        /// <inheritdoc/>
        public IVariantEncoder Default =>
            new JsonVariantEncoder(new ServiceMessageContext(), _serializer);

        /// <inheritdoc/>
        public IVariantEncoder Create(ServiceMessageContext context) {
            return new JsonVariantEncoder(context, _serializer);
        }

        /// <summary>
        /// Variant encoder implementation
        /// </summary>
        private sealed class JsonVariantEncoder : IVariantEncoder {

            /// <inheritdoc/>
            public ServiceMessageContext Context { get; }

            /// <inheritdoc/>
            public IJsonSerializer Serializer { get; }

            /// <summary>
            /// Create encoder
            /// </summary>
            /// <param name="context"></param>
            /// <param name="serializer"></param>
            internal JsonVariantEncoder(ServiceMessageContext context, IJsonSerializer serializer) {
                Context = context ?? throw new ArgumentNullException(nameof(context));
                Serializer = serializer ?? new NewtonSoftJsonSerializer();
            }

            /// <inheritdoc/>
            public VariantValue Encode(Variant value, out BuiltInType builtinType) {

                if (value == Variant.Null) {
                    builtinType = BuiltInType.Null;
                    return Serializer.FromObject(null);
                }
                using (var stream = new MemoryStream()) {
                    using (var encoder = new JsonEncoderEx(stream, Context) {
                        UseAdvancedEncoding = true
                    }) {
                        encoder.WriteVariant(nameof(value), value);
                    }
                    var json = Encoding.UTF8.GetString(stream.ToArray());
                    try {
                        var token = Serializer.Parse(json);
                        Enum.TryParse((string)token.SelectToken("value.Type"),
                            true, out builtinType);
                        return token.SelectToken("value.Body");
                    }
                    catch (SerializerException se) {
                        throw new SerializerException($"Failed to parse '{json}'. " +
                            "See inner exception for more details.", se);
                    }
                }
            }

            /// <inheritdoc/>
            public Variant Decode(VariantValue value, BuiltInType builtinType) {

                //
                // Sanitize json input from user
                //
                value = Sanitize(value, builtinType == BuiltInType.String);

                VariantValue json;
                if (builtinType == BuiltInType.Null ||
                    (builtinType == BuiltInType.Variant &&
                        value.Type == VariantValueType.Object)) {
                    //
                    // Let the decoder try and decode the json variant.
                    //
                    json = Serializer.FromObject(new { value });
                }
                else {
                    //
                    // Give decoder a hint as to the type to use to decode.
                    //
                    json = Serializer.FromObject(new {
                        value = new {
                            Body = value,
                            Type = (byte)builtinType
                        }
                    });
                }

                //
                // Decode json to a real variant
                //
                using (var text = new StringReader(json.ToString()))
                using (var reader = new Newtonsoft.Json.JsonTextReader(text))
                using (var decoder = new JsonDecoderEx(reader, Context)) {
                    return decoder.ReadVariant(nameof(value));
                }
            }

            /// <summary>
            /// Sanitizes user input by removing quotes around non strings,
            /// or adding array brackets to comma seperated values that are
            /// not string type and recursing through arrays to do the same.
            /// The output is a pure json token that can be passed to the
            /// json decoder.
            /// </summary>
            /// <param name="value"></param>
            /// <param name="isString"></param>
            /// <returns></returns>
            internal VariantValue Sanitize(VariantValue value, bool isString) {
                if (value is null || value.Type == VariantValueType.Null) {
                    return value;
                }

                var asString = value.Type == VariantValueType.String ?
                    (string)value : value.ToString(Formatting.None);

                if (value.Type != VariantValueType.Object &&
                    value.Type != VariantValueType.Array &&
                    value.Type != VariantValueType.String) {
                    //
                    // If this should be a string - return as such
                    //
                    return isString ? asString : value;
                }

                if (string.IsNullOrWhiteSpace(asString)) {
                    return value;
                }

                //
                // Try to parse string as json
                //
                if (value.Type != VariantValueType.String) {
                    asString = asString.Replace("\\\"", "\"");
                }
                var token = Try.Op(() => Serializer.Parse(asString));
                if (token != null) {
                    value = token;
                }

                if (value.Type == VariantValueType.String) {

                    //
                    // try to split the string as comma seperated list
                    //
                    var elements = asString.Split(',');
                    if (isString) {
                        //
                        // If all elements are quoted, then this is a
                        // string array
                        //
                        if (elements.Length > 1) {
                            var array = new List<string>();
                            foreach (var element in elements) {
                                var trimmed = element.Trim().TrimQuotes();
                                if (trimmed == element) {
                                    // Treat entire string as value
                                    return value;
                                }
                                array.Add(trimmed);
                            }
                            return Serializer.FromObject(array); // No need to sanitize contents
                        }
                    }
                    else {
                        //
                        // First trim any quotes from string before splitting.
                        //
                        if (elements.Length > 1) {
                            //
                            // Parse all contained elements and return as array
                            //
                            value = Serializer.FromObject(elements
                                .Select(s => s.Trim()));
                        }
                        else {
                            //
                            // Try to remove next layer of quotes and try again.
                            //
                            var trimmed = asString.Trim().TrimQuotes();
                            if (trimmed != asString) {
                                return Sanitize(trimmed, isString);
                            }
                        }
                    }
                }

                if (value.Type == VariantValueType.Array) {
                    //
                    // Sanitize each element accordingly
                    //
                    return Serializer.FromObject(value.Values
                        .Select(t => Sanitize(t, isString)));
                }
                return value;
            }
        }

        private readonly IJsonSerializer _serializer;
    }
}
