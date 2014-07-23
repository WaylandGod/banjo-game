//-----------------------------------------------------------------------
// <copyright file="XmlSerializer.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;

namespace Core.Data
{
    /// <summary>Base class for XML serializable data types</summary>
    /// <typeparam name="TType">Type of object instances to be serialized</typeparam>
    public class XmlSerializer<TType> : ISerializer<TType>
    {
        /// <summary>Backing field for Serializer</summary>
        private static DataContractSerializer serializer;

        /// <summary>Gets a value indicating whether data is serialized as UTF8 text</summary>
        public bool IsUtf8 { get { return true; } }

        /// <summary>Gets the default file extension for the serialized data</summary>
        public string DefaultExtension { get { return ".xml"; } }

        /// <summary>Gets the global XML serializer for the type</summary>
        private static DataContractSerializer Serializer
        {
            get
            {
                // return serializer != null ? serializer : (serializer = new DataContractSerializer(typeof(TType)));
                return XmlSerializer<TType>.serializer ??
                    (XmlSerializer<TType>.serializer = new DataContractSerializer(typeof(TType)));
            }
        }

        /// <summary>Serialize an instance to bytes</summary>
        /// <param name="instance">Instance to serialize</param>
        /// <returns>Serialized instance as bytes</returns>
        public byte[] Serialize(TType instance)
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new XmlTextWriter(stream, Encoding.UTF8) { Formatting = Formatting.Indented })
                {
                    Serializer.WriteObject(writer, instance);
                }

                return stream.ToArray();
            }
        }

        /// <summary>Deserialize an instance from bytes</summary>
        /// <param name="data">Instance serialized as bytes</param>
        /// <returns>Deserialized instance</returns>
        public TType Deserialize(byte[] data)
        {
            using (var stream = new MemoryStream(data))
            {
                using (var reader = new XmlTextReader(stream))
                {
                    return (TType)Serializer.ReadObject(reader);
                }
            }
        }
    }
}
