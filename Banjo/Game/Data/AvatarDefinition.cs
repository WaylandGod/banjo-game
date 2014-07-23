//-----------------------------------------------------------------------
// <copyright file="AvatarDefinition.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.Serialization;
using Core.Data;
using Core.Resources;

namespace Game.Data
{
    /// <summary>Definition for avatars</summary>
    [DataContract(Namespace = "")]
    public class AvatarDefinition : SerializedResource<AvatarDefinition>
    {
        /// <summary>Gets or sets the available states</summary>
        [IgnoreDataMember]
        public AvatarState[] States { get; set; }

        /// <summary>Gets or sets the resource id</summary>
        [DataMember]
        public string ResourceId { get; set; }

        /// <summary>Gets or sets the default state id</summary>
        [DataMember]
        public string DefaultState { get; set; }

        /// <summary>Gets or sets the list of available states</summary>
        [DataMember(Name = "States")]
        private StatesCollection StatesList
        {
            get { return new StatesCollection(this.States.Select(state => new StatesCollection.Entry(state.Id, state.Name))); }
            set { this.States = value.Select(entry => new AvatarState { Id = entry.Id, Name = entry.Name }).ToArray(); }
        }

        /// <summary>Collection contract for avatar states</summary>
        ////[SuppressMessage("Microsoft.Usage", "CA2237", Justification = "For DataContract serialization only")]
        [CollectionDataContract(Name = "States", Namespace = "", ItemName = "State")]
        private class StatesCollection : List<StatesCollection.Entry>
        {
            /// <summary>Initializes a new instance of the StatesCollection class</summary>
            public StatesCollection() : base() { }

            /// <summary>Initializes a new instance of the StatesCollection class</summary>
            /// <param name="collection">The collection whose elements are copied to the new list</param>
            public StatesCollection(IEnumerable<StatesCollection.Entry> collection) : base(collection) { }

            /// <summary>Data contract for entries in the list</summary>
            [DataContract(Name = "State", Namespace = "")]
            public class Entry
            {
                /// <summary>Initializes a new instance of the Entry class</summary>
                public Entry() : this(-1, null) { }

                /// <summary>Initializes a new instance of the Entry class</summary>
                /// <param name="id">State id</param>
                /// <param name="name">State name</param>
                public Entry(int id, string name)
                {
                    this.Id = id;
                    this.Name = name;
                }

                /// <summary>Gets or sets the identifier</summary>
                [DataMember]
                public int Id { get; set; }

                /// <summary>Gets or sets the name</summary>
                [DataMember]
                public string Name { get; set; }
            }
        }
    }
}
