//-----------------------------------------------------------------------
// <copyright file="LevelDefinition.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using Core;
using Core.Data;
using Core.Resources;

namespace Game.Data
{
    /// <summary>Represents a world definition</summary>
    [DataContract(Namespace = "")]
    public class LevelDefinition : SerializedResource<LevelDefinition>
    {
        /// <summary>Initializes a new instance of the LevelDefinition class</summary>
        public LevelDefinition()
        {
            this.Entities = new EntityCollection();
            this.Controllers = new ControllerConfig[0];
        }

        /// <summary>Gets or sets the controller configurations</summary>
        [DataMember(IsRequired = true, Order = 0)]
        public ControllerConfig[] Controllers { get; set; }

        /// <summary>Gets or sets the world description</summary>
        [DataMember(IsRequired = true)]
        public string Description { get; set; }

        /// <summary>Gets the entity definitions</summary>
        [DataMember(IsRequired = true)]
        public EntityCollection Entities { get; internal set; }

        /// <summary>Gets the settings</summary>
        [IgnoreDataMember]
        public IDictionary<string, string> Settings { get; internal set; }

        /// <summary>Gets the world summary</summary>
        [IgnoreDataMember]
        public LevelSummary Summary { get { return new LevelSummary(this); } }

        /// <summary>Gets or sets the world title</summary>
        [DataMember(IsRequired = true)]
        public string Title { get; set; }

        /// <summary>Data contract for the entity list</summary>
        [CollectionDataContract(Name = "Entities", Namespace = "", ItemName = "Entity")]
        public class EntityCollection : List<EntityCollection.Entry>
        {
            /// <summary>Initializes a new instance of the EntityCollection class</summary>
            public EntityCollection() : base() { }

            /// <summary>Initializes a new instance of the EntityCollection class</summary>
            /// <param name="collection">The collection whose elements are copied to the new list</param>
            public EntityCollection(IEnumerable<EntityCollection.Entry> collection) : base(collection) { }

            /// <summary>Data contract for entries in the list</summary>
            [DataContract(Name = "Entity", Namespace = "")]
            public class Entry
            {
                /// <summary>Initializes a new instance of the Entry class</summary>
                public Entry() : this(null) { }

                /// <summary>Initializes a new instance of the Entry class</summary>
                /// <param name="entityId">EntityDefinition identifier</param>
                public Entry(string entityId)
                {
                    this.EntityId = entityId;
                    this.Controllers = new ControllerConfig[0];
                }

                /// <summary>Gets or sets the identifier</summary>
                [DataMember(IsRequired = true)]
                public string EntityId { get; set; }

                /// <summary>Gets or sets the position</summary>
                [IgnoreDataMember]
                public Vector3D Position { get; set; }

                /// <summary>Gets or sets the heading</summary>
                [IgnoreDataMember]
                public Vector3D Direction { get; set; }

                /// <summary>Gets or sets the heading</summary>
                [IgnoreDataMember]
                public Vector3D Velocity { get; set; }

                /// <summary>Gets or sets the controllers</summary>
                [DataMember(IsRequired = false, Order = 0)]
                public ControllerConfig[] Controllers { get; set; }

                /// <summary>Gets or sets the position (as string)</summary>
                [DataMember(IsRequired = true, Name = "Position")]
                private string PositionText
                {
                    get { return this.Position.ToString(); }
                    set { this.Position = Vector3D.Parse(value); }
                }

                /// <summary>Gets or sets the direction (as string)</summary>
                [DataMember(IsRequired = true, Name = "Direction")]
                private string DirectionText
                {
                    get { return this.Direction.ToString(); }
                    set { this.Direction = Vector3D.Parse(value); }
                }

                /// <summary>Gets or sets the velocity (as string)</summary>
                [DataMember(IsRequired = true, Name = "Velocity")]
                private string VelocityText
                {
                    get { return this.Velocity.ToString(); }
                    set { this.Velocity = Vector3D.Parse(value); }
                }
            }
        }
    }
}
