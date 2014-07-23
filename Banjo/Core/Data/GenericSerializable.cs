//-----------------------------------------------------------------------
// <copyright file="GenericSerializable.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Runtime.Serialization;
using System.Text;

namespace Core.Data
{
    /// <summary>Base class for generic serializable data types</summary>
    /// <typeparam name="TType">Derived serializable type</typeparam>
    /// <typeparam name="TSerializer">ISerializer implementation</typeparam>
    [DataContract]
    public abstract class GenericSerializable<TType, TSerializer>
        where TType : GenericSerializable<TType, TSerializer>
        where TSerializer : ISerializer<TType>, new()
    {
        /// <summary>Backing field for Serializer</summary>
        private static ISerializer<TType> serializer;

        /// <summary>Gets the default file extension for the serialized data</summary>
        public string DefaultFileExtension { get { return Serializer.DefaultExtension; } }

        /// <summary>Gets the global XML serializer for the type</summary>
        private static ISerializer<TType> Serializer
        {
            get { return serializer ?? (serializer = new TSerializer()); }
        }

        /// <summary>Serialize an instance to bytes</summary>
        /// <param name="instance">Instance to serialize</param>
        /// <returns>Serialized instance as bytes</returns>
        public static byte[] ToBytes(TType instance)
        {
            return Serializer.Serialize(instance);
        }

        /// <summary>Deserialize an instance from bytes</summary>
        /// <param name="data">Instance serialized as bytes</param>
        /// <returns>Deserialized instance</returns>
        public static TType FromBytes(byte[] data)
        {
            return (TType)Serializer.Deserialize(data);
        }

        /// <summary>Deserialize an instance from a string</summary>
        /// <param name="text">Instance serialized as text</param>
        /// <returns>Deserialized instance</returns>
        public static TType FromString(string text)
        {
            if (!Serializer.IsUtf8)
            {
                throw new NotSupportedException("Serializer '{0}' does not support UTF8 text.".FormatInvariant(typeof(TSerializer).FullName));
            }

            return (TType)Serializer.Deserialize(Encoding.UTF8.GetBytes(text));
        }

        /// <summary>Serialize to bytes</summary>
        /// <returns>Serialized bytes</returns>
        public byte[] ToBytes()
        {
            return ToBytes((TType)this);
        }

        /// <summary>Gets a string representation of the object</summary>
        /// <returns>The string representation</returns>
        public override string ToString()
        {
            return Serializer.IsUtf8 ?
                Encoding.UTF8.GetString(ToBytes((TType)this)) :
                base.ToString();
        }
    }
}
