// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------

namespace Microsoft.Azure.IIoT.Serializers {
    using Microsoft.Azure.IIoT.Http;
    using System;
    using System.Buffers;
    using System.IO;
    using System.Linq;

    /// <summary>
    /// Serializer extensions
    /// </summary>
    public static class SerializerEx {

        /// <summary>
        /// Serialize to string
        /// </summary>
        /// <param name="serializer"></param>
        /// <param name="o"></param>
        /// <param name="format"></param>
        public static string Serialize(this ISerializer serializer,
            object o, SerializeOption format = SerializeOption.None) {
            var writer = new ArrayBufferWriter<byte>();
            serializer.Serialize(writer, o, format);
            return serializer.ContentEncoding?.GetString(writer.WrittenSpan)
                ?? Convert.ToBase64String(writer.WrittenSpan);
        }

        /// <summary>
        /// Serialize to string
        /// </summary>
        /// <param name="serializer"></param>
        /// <param name="a"></param>
        public static string SerializeArray(this ISerializer serializer,
            params object[] a) {
            return serializer.Serialize(a);
        }

        /// <summary>
        /// Serialize to request
        /// </summary>
        /// <param name="serializer"></param>
        /// <param name="request"></param>
        /// <param name="o"></param>
        /// <returns></returns>
        public static void SerializeToRequest(this ISerializer serializer,
            IHttpRequest request, object o) {
            if (request is null) {
                throw new ArgumentNullException(nameof(request));
            }
            request.SetStringContent(serializer.Serialize(o));
        }

        /// <summary>
        /// Serialize to request
        /// </summary>
        /// <param name="serializer"></param>
        /// <param name="request"></param>
        /// <param name="a"></param>
        /// <returns></returns>
        public static void SerializeArrayToRequest(this ISerializer serializer,
            IHttpRequest request, params object[] a) {
            serializer.SerializeToRequest(request, a);
        }

        /// <summary>
        /// Serialize into indented string
        /// </summary>
        /// <param name="serializer"></param>
        /// <param name="o"></param>
        /// <returns></returns>
        public static string SerializePretty(
            this ISerializer serializer, object o) {
            return serializer.Serialize(o, SerializeOption.Indented);
        }

        /// <summary>
        /// Serialize into indented string
        /// </summary>
        /// <param name="serializer"></param>
        /// <param name="a"></param>
        /// <returns></returns>
        public static string SerializeArrayPretty(
            this ISerializer serializer, params object[] a) {
            return serializer.Serialize(a, SerializeOption.Indented);
        }

        /// <summary>
        /// Deserialize from string
        /// </summary>
        /// <param name="serializer"></param>
        /// <param name="str"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object Deserialize(this ISerializer serializer,
            string str, Type type) {
            var buffer = serializer.ContentEncoding?.GetBytes(str)
                ?? Convert.FromBase64String(str);
            return serializer.Deserialize(buffer, type);
        }

        /// <summary>
        /// Deserialize from reader
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serializer"></param>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static T Deserialize<T>(this ISerializer serializer,
            TextReader reader) {
            return serializer.Deserialize<T>(reader.ReadToEnd());
        }

        /// <summary>
        /// Deserialize from string
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serializer"></param>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T Deserialize<T>(this ISerializer serializer,
            string json) {
            var typed = serializer.Deserialize(json, typeof(T));
            return typed == null ? default : (T)typed;
        }

        /// <summary>
        /// Deserialize from response
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serializer"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        public static T DeserializeResponse<T>(this ISerializer serializer,
            IHttpResponse response) {
            var typed = serializer.Deserialize(response.Content, typeof(T));
            return typed == null ? default : (T)typed;
        }

        /// <summary>
        /// Convert to token.
        /// </summary>
        /// <param name="serializer"></param>
        /// <param name="a"></param>
        /// <returns></returns>
        public static VariantValue FromArray(this ISerializer serializer,
            params object[] a) {
            return serializer.FromObject(a);
        }

        /// <summary>
        /// Parse string
        /// </summary>
        /// <param name="serializer"></param>
        /// <param name="str"></param>
        /// <returns></returns>
        public static VariantValue Parse(this ISerializer serializer,
            string str) {
            var buffer = serializer.ContentEncoding?.GetBytes(str)
                ?? Convert.FromBase64String(str);
            return serializer.Parse(buffer);
        }

        /// <summary>
        /// Parse response
        /// </summary>
        /// <param name="serializer"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        public static VariantValue ParseResponse(this ISerializer serializer,
            IHttpResponse response) {
            return serializer.Parse(response.Content);
        }

        /// <summary>
        /// Convert from to
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serializer"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public static T Map<T>(this ISerializer serializer, object model) {
            if (model is null) {
                return default;
            }
            return serializer.Deserialize<T>(serializer.Serialize(model));
        }
    }
}