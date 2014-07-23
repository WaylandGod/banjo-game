//-----------------------------------------------------------------------
// <copyright file="SerializedResource.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Runtime.Serialization;
using Core.Data;

namespace Core.Resources
{
    /// <summary>Base class for serialized resources</summary>
    /// <typeparam name="TType">Derived serializable resource type</typeparam>
    [DataContract(Namespace = "")]
    public abstract class SerializedResource<TType> : GenericSerializable<TType, XmlSerializer<TType>>, ITextResource, IResource
        where TType : SerializedResource<TType>, IResource
    {
        /// <summary>Gets or sets the resource's identifier</summary>
        [DataMember]
        public string Id { get; set; }

        /// <summary>Gets the resource's text</summary>
        [IgnoreDataMember]
        public string Text { get { return this.ToString(); } }
    }
}
