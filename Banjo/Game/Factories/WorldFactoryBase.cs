//-----------------------------------------------------------------------
// <copyright file="WorldFactoryBase.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using Core.Factories;
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
        protected WorldFactoryBase(
            IResourceLibrary resources,
            IControllerManager controllerManager,
            IControllerFactory[] controllerFactories,
            IEntityFactory entityFactory)
        : base(resources)
        {
            this.ControllerManager = controllerManager;
            this.ControllerFactories = controllerFactories;
            this.EntityFactory = entityFactory;
        }

        /// <summary>Gets the controller manager</summary>
        protected IControllerManager ControllerManager { get; private set; }

        /// <summary>Gets the controller factories</summary>
        protected IControllerFactory[] ControllerFactories { get; private set; }

        /// <summary>Gets the entity factory</summary>
        protected IEntityFactory EntityFactory { get; private set; }

        /// <summary>Creates an instance of IWorld</summary>
        /// <param name="definition">World definition</param>
        /// <returns>The created IWorld instance</returns>
        public abstract IWorld Create(LevelDefinition definition);
    }
}
