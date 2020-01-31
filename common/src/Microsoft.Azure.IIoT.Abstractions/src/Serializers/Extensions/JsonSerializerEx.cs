// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------

namespace Microsoft.Azure.IIoT.Serializers {
    using Microsoft.Azure.IIoT.Http;
    using System;
    using System.Globalization;
    using System.IO;
    using System.Text;

    /// <summary>
    /// Serializer extensions
    /// </summary>
    public static class JsonSerializerEx {

        /// <summary>
        /// Serialize to string
        /// </summary>
        /// <param name="serializer"></param>
        /// <param name="o"></param>
        /// <param name="format"></param>
        public static string Serialize(this IJsonSerializer serializer,
            object o, JsonFormat format = JsonFormat.None) {
            var sb = new StringBuilder(256);
            using (var writer = new StringWriter(sb, CultureInfo.InvariantCulture)) {
                serializer.Serialize(writer, o, format);
                return sb.ToString();
            }
        }

        /// <summary>
        /// Serialize to stream
        /// </summary>
        /// <param name="serializer"></param>
        /// <param name="stream"></param>
        /// <param name="encoding"></param>
        /// <param name="o"></param>
        /// <param name="format"></param>
        /// <param name="bufferSize"></param>
        public static void Serialize(this IJsonSerializer serializer,
            object o, Stream stream, JsonFormat format = JsonFormat.None,
            Encoding encoding = null, int bufferSize = 512) {
            if (stream == null) {
                throw new ArgumentNullException(nameof(stream));
            }
            using (var writer = new StreamWriter(stream,
                encoding ?? Encoding.UTF8, bufferSize, true)) {
                serializer.Serialize(writer, o, format);
            }
        }

        /// <summary>
        /// Serialize to request
        /// </summary>
        /// <param name="serializer"></param>
        /// <param name="request"></param>
        /// <param name="o"></param>
        /// <returns></returns>
        public static void SerializeToRequest(this IJsonSerializer serializer,
            IHttpRequest request, object o) {
            if (request == null) {
                throw new ArgumentNullException(nameof(request));
            }
            request.SetStringContent(serializer.Serialize(o));
        }

        /// <summary>
        /// Serialize into indented string
        /// </summary>
        /// <param name="serializer"></param>
        /// <param name="o"></param>
        /// <returns></returns>
        public static string SerializePretty(
            this IJsonSerializer serializer, object o) {
            return serializer.Serialize(o, JsonFormat.Indented);
        }

        /// <summary>
        /// Deserialize from reader
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serializer"></param>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static T Deserialize<T>(this IJsonSerializer serializer, TextReader reader) {
            return (T)serializer.Deserialize(reader, typeof(T));
        }

        /// <summary>
        /// Deserialize from response
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serializer"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        public static T DeserializeResponse<T>(this IJsonSerializer serializer, IHttpResponse response) {
            return (T)serializer.Deserialize(response.GetContentAsString(), typeof(T));
        }

        /// <summary>
        /// Deserialize from string
        /// </summary>
        /// <param name="serializer"></param>
        /// <param name="json"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object Deserialize(this IJsonSerializer serializer,
            string json, Type type) {
            using (var reader = new StringReader(json)) {
                return serializer.Deserialize(reader, type);
            }
        }

        /// <summary>
        /// Deserialize from string
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serializer"></param>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T Deserialize<T>(this IJsonSerializer serializer, string json) {
            return (T)serializer.Deserialize(json, typeof(T));
        }

        /// <summary>
        /// Deserialize from string
        /// </summary>
        /// <param name="serializer"></param>
        /// <param name="type"></param>
        /// <param name="stream"></param>
        /// <param name="encoding"></param>
        /// <param name="bufferSize"></param>
        /// <param name="detectEncoding"></param>
        /// <returns></returns>
        public static object Deserialize(this IJsonSerializer serializer,
            Type type, Stream stream, Encoding encoding = null,
            int bufferSize = 512, bool detectEncoding = false) {
            using (var reader = new StreamReader(stream, encoding ?? Encoding.UTF8,
                detectEncoding, bufferSize, true)) {
                return serializer.Deserialize(reader, type);
            }
        }

        /// <summary>
        /// Deserialize from string
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serializer"></param>
        /// <param name="stream"></param>
        /// <param name="encoding"></param>
        /// <param name="bufferSize"></param>
        /// <param name="detectEncoding"></param>
        /// <returns></returns>
        public static T Deserialize<T>(this IJsonSerializer serializer,
            Stream stream, Encoding encoding = null,
            int bufferSize = 512, bool detectEncoding = false) {
            return (T)serializer.Deserialize(typeof(T), stream, encoding,
                bufferSize, detectEncoding);
        }

        /// <summary>
        /// Convert from to
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serializer"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public static T Map<T>(this IJsonSerializer serializer, object model) {
            if (model == null) {
                return default;
            }
            return serializer.Deserialize<T>(serializer.Serialize(model));
        }
    }
}