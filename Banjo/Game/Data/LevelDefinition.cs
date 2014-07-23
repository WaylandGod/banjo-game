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
            this.Tiles = new TileCollection();
            this.Map = new TileMap();
        }

        /// <summary>Gets the world summary</summary>
        [IgnoreDataMember]
        public LevelSummary Summary { get { return new LevelSummary(this); } }

        /// <summary>Gets or sets the world description</summary>
        [DataMember(IsRequired = true)]
        public string Description { get; set; }

        /// <summary>Gets the entity definitions</summary>
        [DataMember(IsRequired = true)]
        public EntityCollection Entities { get; internal set; }

        /// <summary>Gets or sets the tile map</summary>
        [DataMember(IsRequired = false)]
        public TileMap Map { get; set; }

        /// <summary>Gets the settings</summary>
        [IgnoreDataMember]
        public IDictionary<string, string> Settings { get; internal set; }

        /// <summary>Gets the tile definitions</summary>
        [DataMember(IsRequired = false)]
        public TileCollection Tiles { get; internal set; }

        /// <summary>Gets or sets the world title</summary>
        [DataMember(IsRequired = true)]
        public string Title { get; set; }

        /// <summary>Represents the map of tiles</summary>
        [DataContract(Namespace = "")]
        public class TileMap
        {
            /// <summary>Gets the tiles</summary>
            /// <remarks>Values are LevelDefinition.Tiles indices</remarks>
            [IgnoreDataMember]
            public int[] Tiles { get; internal set; }

            /// <summary>Gets or sets the breadth of the map (in tiles)</summary>
            [DataMember(IsRequired = true)]
            public int Breadth { get; set; }

            /// <summary>Gets or sets the depth of the map (in tiles)</summary>
            [DataMember(IsRequired = true)]
            public int Depth { get; set; }

            /// <summary>Gets the settings</summary>
            [IgnoreDataMember]
            public IDictionary<string, string> Settings { get; internal set; }

            /// <summary>Gets or sets the spacing between tiles (in world coordinate system units)</summary>
            [DataMember(IsRequired = true)]
            public float TileSpacing { get; set; }

            /// <summary>Gets or sets the list of tiles</summary>
            [DataMember(IsRequired = true, Name = "Tiles")]
            private string TilesText
            {
                get
                {
                    var sb = new System.Text.StringBuilder();
                    var tiles = this.Tiles.Select(i => i.ToString(CultureInfo.InvariantCulture)).ToArray();
                    for (var i = 0; i < tiles.Length; i += this.Breadth)
                    {
                        var count = i + this.Breadth < tiles.Length ? this.Breadth : tiles.Length - i;
                        sb.AppendLine(string.Join(",", tiles, i, count));
                    }

                    return sb.ToString();
                }
                
                set
                {
                    this.Tiles = value.Trim()
                        .Split(new[] { ",", "\n" }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(s => s.Trim())
                        .Select(s => int.Parse(s))
                        .ToArray();
                }
            }

            /// <summary>Gets or sets the settings (as text)</summary>
            [DataMember(Name = "Settings")]
            private string SettingsText
            {
                get
                {
                    return this.Settings == null ?
                        string.Empty :
                        string.Join(";", this.Settings.Select(kvp => "{0}={1}".FormatInvariant(kvp.Key, kvp.Value)).ToArray());
                }

                set
                {
                    this.Settings = value
                        .Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(s => s.Trim())
                        .Where(s => !string.IsNullOrEmpty(s))
                        .Select(s => s.Split(new[] { '=' }, 2))
                        .ToDictionary(kvp => kvp[0].Trim(), kvp => kvp[1].Trim());
                }
            }
        }

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
                public Vector3 Position { get; set; }

                /// <summary>Gets or sets the heading</summary>
                [IgnoreDataMember]
                public Vector3 Direction { get; set; }

                /// <summary>Gets or sets the heading</summary>
                [IgnoreDataMember]
                public Vector3 Velocity { get; set; }

                /// <summary>Gets or sets the controllers</summary>
                [DataMember(IsRequired = false, Order = 0)]
                public ControllerConfig[] Controllers { get; set; }

                /// <summary>Gets or sets the position (as string)</summary>
                [DataMember(IsRequired = true, Name = "Position")]
                private string PositionText
                {
                    get { return this.Position.ToString(); }
                    set { this.Position = Vector3.Parse(value); }
                }

                /// <summary>Gets or sets the direction (as string)</summary>
                [DataMember(IsRequired = true, Name = "Direction")]
                private string DirectionText
                {
                    get { return this.Direction.ToString(); }
                    set { this.Direction = Vector3.Parse(value); }
                }

                /// <summary>Gets or sets the velocity (as string)</summary>
                [DataMember(IsRequired = true, Name = "Velocity")]
                private string VelocityText
                {
                    get { return this.Velocity.ToString(); }
                    set { this.Velocity = Vector3.Parse(value); }
                }
            }
        }

        /// <summary>Data contract for the entity list</summary>
        [CollectionDataContract(Name = "Tiles", Namespace = "", ItemName = "TileId")]
        public class TileCollection : List<string>
        {
            /// <summary>Initializes a new instance of the TileCollection class</summary>
            public TileCollection() : base() { }

            /// <summary>Initializes a new instance of the TileCollection class</summary>
            /// <param name="collection">The collection whose elements are copied to the new list</param>
            public TileCollection(IEnumerable<string> collection) : base(collection) { }
        }
    }
}
