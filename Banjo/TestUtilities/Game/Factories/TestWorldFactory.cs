//-----------------------------------------------------------------------
// <copyright file="TestWorldFactory.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using Core;
using Core.Data;
using Core.Factories;
using Core.Programmability;
using Core.Resources.Management;
using Game;
using Game.Data;
using Game.Factories;

namespace TestUtilities.Game.Factories
{
    /// <summary>Creates instances of IWorld</summary>
    public class TestWorldFactory : WorldFactoryBase, IWorldFactory
    {
        /// <summary>Initializes a new instance of the TestWorldFactory class</summary>
        public TestWorldFactory(
            IResourceLibrary resources,
            IControllerManager controllerManager,
            IControllerFactory[] controllerFactories,
            IEntityFactory entityFactory)
        : base(resources, controllerManager, controllerFactories, entityFactory) { }

        /// <summary>Creates an instance of IWorld</summary>
        /// <param name="definition">Level definition</param>
        /// <returns>The created IWorld instance</returns>
        public override IWorld Create(LevelDefinition definition)
        {
            return new TestWorld(
                definition,
                this.Resources,
                this.ControllerManager,
                this.ControllerFactories,
                this.EntityFactory);
        }
    }
}
