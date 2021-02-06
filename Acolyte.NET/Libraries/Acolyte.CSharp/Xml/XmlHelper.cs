using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Acolyte.Assertions;

namespace Acolyte.Xml
{
    /// <summary>
    /// Provides serialization and deserialization methods to work with XML.
    /// </summary>
    public static class XmlHelper
    {
        /// <summary>
        /// Serializes passed value to XML as string.
        /// </summary>
        /// <typeparam name="T">Type of the value to serialize.</typeparam>
        /// <param name="value">Value to serialize.</param>
        /// <returns>Serialized string of passed class.</returns>
        /// <exception cref="InvalidOperationException">
        /// An error occurred during serialization. The original exception is available using the
        /// <see cref="Exception.InnerException" /> property.
        /// </exception>
        public static string SerializeToStringXml<T>([AllowNull] T value)
        {
            var xmlSerializer = new XmlSerializer(typeof(T));

            string stringXml;

            using (var sww = new StringWriter())
            using (XmlWriter writer = XmlWriter.Create(sww))
            {
                xmlSerializer.Serialize(writer, value);
                stringXml = sww.ToString();
            }

            return stringXml;
        }

        /// <summary>
        /// Deserializes passed value from XML data.
        /// </summary>
        /// <typeparam name="T">Type of the value to deserialize.</typeparam>
        /// <param name="xmlData">XML data to deserialize.</param>
        /// <returns>Deserialized type of specified class.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="xmlData" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// An error occurred during deserialization. The original exception is available using the
        /// <see cref="Exception.InnerException" /> property.
        /// </exception>
        public static T DeserializeFromStringXml<T>(string xmlData)
        {
            xmlData.ThrowIfNull(nameof(xmlData));

            var xmlSerializer = new XmlSerializer(typeof(T));

            object result;

            using (var sww = new StringReader(xmlData))
            using (XmlReader reader = XmlReader.Create(sww))
            {
                result = xmlSerializer.Deserialize(reader);
            }

            return (T) result;
        }
    }
}
