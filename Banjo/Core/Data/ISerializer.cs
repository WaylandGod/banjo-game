//-----------------------------------------------------------------------
// <copyright file="ISerializer.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Core.Data
{
    /// <summary>Implemented by serializers for use with GenericSerializable</summary>
    /// <typeparam name="TType">Type of object instances to be serialized</typeparam>
    public interface ISerializer<TType>
    {
        /// <summary>Gets a value indicating whether data is serialized as UTF8 text</summary>
        bool IsUtf8 { get; }

        /// <summary>Gets the default file extension for the serialized data</summary>
        string DefaultExtension { get; }

        /// <summary>Serialize an instance to bytes</summary>
        /// <param name="instance">Instance to serialize</param>
        /// <returns>Serialized instance as bytes</returns>
        byte[] Serialize(TType instance);

        /// <summary>Deserialize an instance from bytes</summary>
        /// <param name="data">Instance serialized as bytes</param>
        /// <returns>Deserialized instance</returns>
        TType Deserialize(byte[] data);
    }
}
