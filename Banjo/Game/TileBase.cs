//-----------------------------------------------------------------------
// <copyright file="TileBase.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using Core.Resources.Management;
using Game.Data;
using Game.Factories;

namespace Game
{
    /// <summary>Base class for ITile implementations</summary>
    /// <remarks>A tile is a static, immobile object which exists in many places at once</remarks>
    public abstract class TileBase : ObjectBase, ITile, IDisposable
    {
        /// <summary>List of tile instances</summary>
        private readonly IList<TileInstance> instances = new List<TileInstance>();

        /// <summary>Initializes a new instance of the TileBase class</summary>
        /// <param name="definition">Tile definition</param>
        /// <param name="library">Resource library</param>
        /// <param name="avatarFactory">Avatar factory</param>
        protected TileBase(TileDefinition definition, IResourceLibrary library, IAvatarFactory avatarFactory)
            : base(
            library.GetSerializedResource<AvatarDefinition>(definition.AvatarId),
            library.GetSerializedResource<Material>(definition.MaterialId),
            definition.Volume)
        {
            this.AvatarFactory = avatarFactory;
        }

        /// <summary>Gets the avatar factory</summary>
        public IAvatarFactory AvatarFactory { get; private set; }

        /// <summary>Gets the instances of this tile</summary>
        public IEnumerable<TileInstance> Instances { get { return this.instances; } }

        /// <summary>Creates an instance of the tile</summary>
        /// <param name="position">Tile position</param>
        /// <returns>Created tile instance</returns>
        public virtual TileInstance AddInstance(Vector3 position)
        {
            var avatar = this.AvatarFactory.Create(this.AvatarDefinition);
            var instance = new TileInstance(this, avatar, position);
            this.instances.Add(instance);
            return instance;
        }

        /// <summary>Dispose of native/managed resources</summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>Dispose of native/managed resources</summary>
        /// <param name="disposing">Whether the object is being disposed</param>
        protected virtual void Dispose(bool disposing)
        {
            while (this.instances.Count > 0)
            {
                var instance = this.instances.First();
                if (instance != null)
                {
                    instance.Dispose();
                }

                this.instances.RemoveAt(0);
            }
        }
    }
}
