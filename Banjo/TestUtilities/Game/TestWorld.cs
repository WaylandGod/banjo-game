//-----------------------------------------------------------------------
// <copyright file="TestWorld.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System.Collections.Generic;
using Core;
using Core.Data;
using Core.Factories;
using Core.Programmability;
using Core.Resources.Management;
using Game;
using Game.Data;
using Game.Factories;

namespace TestUtilities.Game
{
    /// <summary>Represents a world</summary>
    public class TestWorld : WorldBase, IWorld
    {
        /// <summary>Default number of entities for GameDataHelper generated level definitions</summary>
        public const int DefaultNumEntities = 10;

        /// <summary>Initializes a new instance of the TestEntity class</summary>
        public TestWorld(
            IResourceLibrary resources,
            IControllerManager controllerManager,
            IControllerFactory[] controllerFactories,
            IEntityFactory entityFactory,
            int numEntities = DefaultNumEntities)
        : this(
            resources.GetResource<LevelDefinition>(GameDataHelper.DeepCreateTestLevelDefinition(resources, numEntities)),
            resources,
            controllerManager,
            controllerFactories,
            entityFactory)
        {
        }

        /// <summary>Initializes a new instance of the TestEntity class</summary>
        public TestWorld(
            LevelDefinition definition,
            IResourceLibrary resources,
            IControllerManager controllerManager,
            IControllerFactory[] controllerFactories,
            IEntityFactory entityFactory)
            : base(definition, resources, controllerManager, controllerFactories, entityFactory)
        {
        }
    }
}
