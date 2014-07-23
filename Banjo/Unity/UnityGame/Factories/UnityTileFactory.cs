//-----------------------------------------------------------------------
// <copyright file="UnityTileFactory.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using Core.Resources.Management;
using Game;
using Game.Data;
using Game.Factories;

namespace Game.Unity.Factories
{
    /// <summary>Creates implementations of ITile</summary>
    public class UnityTileFactory : TileFactoryBase, ITileFactory
    {
        /// <summary>Initializes a new instance of the UnityTileFactory class</summary>
        /// <param name="resources">Resource library</param>
        /// <param name="avatarFactory">Avatar factory</param>
        public UnityTileFactory(
            IResourceLibrary resources,
            IAvatarFactory avatarFactory)
            : base(resources, avatarFactory) { }

        /// <summary>Creates an instance of ITile</summary>
        /// <param name="definition">Tile definition</param>
        /// <returns>The created ITile instance</returns>
        protected override ITile Create(TileDefinition definition)
        {
            return new UnityTile(
                definition,
                this.Resources,
                this.AvatarFactory);
        }
    }
}
