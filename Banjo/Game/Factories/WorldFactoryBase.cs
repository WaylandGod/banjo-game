//-----------------------------------------------------------------------
// <copyright file="WorldFactoryBase.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using Core.Programmability;
using Core.Resources.Management;
using Game.Data;
using Game.Factories;

namespace Game.Factories
{
    /// <summary>Creates implementations of IWorld</summary>
    public abstract class WorldFactoryBase : ResourceFactoryBase<IWorld>, IWorldFactory
    {
        /// <summary>Initializes a new instance of the WorldFactoryBase class</summary>
        /// <param name="resources">Resource library</param>
        /// <param name="controllerManager">Controller manager</param>
        /// <param name="entityFactory">Entity factory</param>
        /// <param name="tileFactory">Tile factory</param>
        protected WorldFactoryBase(IResourceLibrary resources, IControllerManager controllerManager, IEntityFactory entityFactory, ITileFactory tileFactory)
            : base(resources)
        {
            this.ControllerManager = controllerManager;
            this.EntityFactory = entityFactory;
            this.TileFactory = tileFactory;
        }

        /// <summary>Gets the controller manager</summary>
        protected IControllerManager ControllerManager { get; private set; }

        /// <summary>Gets the entity factory</summary>
        protected IEntityFactory EntityFactory { get; private set; }

        /// <summary>Gets the tile factory</summary>
        protected ITileFactory TileFactory { get; private set; }

        /// <summary>Creates an instance of IWorld</summary>
        /// <param name="definition">World definition</param>
        /// <returns>The created IWorld instance</returns>
        protected abstract IWorld Create(LevelDefinition definition);
    }
}
