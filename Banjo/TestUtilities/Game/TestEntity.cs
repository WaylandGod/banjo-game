//-----------------------------------------------------------------------
// <copyright file="TestEntity.cs" company="Benjamin Woodall">
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
    /// <summary>Represents an entity</summary>
    /// <remarks>An entity is any interactive/dynamic object in a game</remarks>
    public class TestEntity : EntityBase, IEntity
    {
        /// <summary>Initializes a new instance of the TestEntity class</summary>
        public TestEntity(
            IResourceLibrary library,
            IAvatarFactory avatarFactory,
            IControllerFactory[] controllerFactories,
            ControllerConfig[] controllers,
            Vector3D position,
            Vector3D direction,
            Vector3D velocity)
            : this(library.GetResource<EntityDefinition>(GameDataHelper.DeepCreateTestEntityDefinition(library)), library, avatarFactory, controllerFactories, controllers, position, direction, velocity) { }

        /// <summary>Initializes a new instance of the TestEntity class</summary>
        public TestEntity(
            EntityDefinition definition,
            IResourceLibrary library,
            IAvatarFactory avatarFactory,
            IControllerFactory[] controllerFactories,
            ControllerConfig[] controllers,
            Vector3D position,
            Vector3D direction,
            Vector3D velocity)
            : base(definition, library, avatarFactory, controllerFactories, controllers, position, direction, velocity) { }
    }
}
