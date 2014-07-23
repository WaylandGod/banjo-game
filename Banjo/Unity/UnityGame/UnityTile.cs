//-----------------------------------------------------------------------
// <copyright file="UnityTile.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using Core;
using Core.Resources.Management;
using Core.Unity;
using Game;
using Game.Data;
using Game.Factories;
using Game.Unity.Behaviours;

namespace Game.Unity
{
    /// <summary>Represents a tile</summary>
    /// <remarks>A tile is a static, immobile object which exists in many places at once</remarks>
    public class UnityTile : TileBase, ITile
    {
        /// <summary>Layer tag for tile GameObjects</summary>
        public const string TileLayer = "Tiles";

        /// <summary>Initializes a new instance of the UnityTile class.</summary>
        /// <param name="definition">Tile definition</param>
        /// <param name="resources">Resource library</param>
        /// <param name="avatarFactory">Avatar factory</param>
        public UnityTile(TileDefinition definition, IResourceLibrary resources, IAvatarFactory avatarFactory)
            : base(definition, resources, avatarFactory) { }

        /// <summary>Creates an instance of the tile</summary>
        /// <param name="position">Tile position</param>
        /// <returns>Created tile instance</returns>
        public override TileInstance AddInstance(Vector3 position)
        {
            var instance = base.AddInstance(position);
            var avatar = (UnityAvatar)instance.Avatar;
            avatar.AddObjectBehaviour<TileInstanceBehaviour>().TileInstance = instance;
            avatar.Layer = TileLayer;
            return instance;
        }
    }
}
