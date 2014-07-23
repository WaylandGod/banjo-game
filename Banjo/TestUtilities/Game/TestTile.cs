//-----------------------------------------------------------------------
// <copyright file="TestTile.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using Core.Resources.Management;
using Game;
using Game.Data;
using Game.Factories;

namespace TestUtilities.Game
{
    /// <summary>Represents a tile</summary>
    /// <remarks>A tile is a static, immobile object which exists in many places at once</remarks>
    public class TestTile : TileBase, ITile
    {
        /// <summary>Initializes a new instance of the TestTile class.</summary>
        /// <param name="definition">Tile definition</param>
        /// <param name="resources">Resource library</param>
        /// <param name="avatarFactory">Avatar factory</param>
        public TestTile(TileDefinition definition, IResourceLibrary resources, IAvatarFactory avatarFactory)
            : base(definition, resources, avatarFactory) { }
    }
}
