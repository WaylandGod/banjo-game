//-----------------------------------------------------------------------
// <copyright file="TileFactoryBase.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using Core.Resources.Management;
using Game.Data;

namespace Game.Factories
{
    /// <summary>Creates implementations of ITile</summary>
    public abstract class TileFactoryBase : ResourceFactoryBase<ITile>, ITileFactory
    {
        /// <summary>Initializes a new instance of the TileFactoryBase class</summary>
        /// <param name="resources">Resource library</param>
        /// <param name="avatarFactory">Avatar factory</param>
        protected TileFactoryBase(IResourceLibrary resources, IAvatarFactory avatarFactory)
            : base(resources)
        {
            this.AvatarFactory = avatarFactory;
        }

        /// <summary>Gets the resource library</summary>
        protected IAvatarFactory AvatarFactory { get; private set; }

        /// <summary>Creates an instance of ITile</summary>
        /// <param name="definition">Tile definition</param>
        /// <returns>The created ITile instance</returns>
        protected abstract ITile Create(TileDefinition definition);
    }
}
