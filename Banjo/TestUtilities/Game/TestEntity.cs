//-----------------------------------------------------------------------
// <copyright file="TestEntity.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System.Collections.Generic;
using Core;
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
        /// <param name="library">Resource library</param>
        /// <param name="avatarFactory">Avatar factory</param>
        /// <param name="controllerFactory">Controller factory</param>
        /// <param name="controllers">Additional controllers</param>
        /// <param name="position">Initial position</param>
        /// <param name="direction">Initial direction</param>
        /// <param name="velocity">Initial velocity</param>
        public TestEntity(
            IResourceLibrary library,
            IAvatarFactory avatarFactory,
            IControllerFactory controllerFactory,
            ControllerConfig[] controllers,
            Vector3 position,
            Vector3 direction,
            Vector3 velocity)
            : this(library.GetResource<EntityDefinition>(GameDataHelper.DeepCreateTestEntityDefinition(library)), library, avatarFactory, controllerFactory, controllers, position, direction, velocity) { }

        /// <summary>Initializes a new instance of the TestEntity class</summary>
        /// <param name="definition">Entity definition</param>
        /// <param name="library">Resource library</param>
        /// <param name="avatarFactory">Avatar factory</param>
        /// <param name="controllerFactory">Controller factory</param>
        /// <param name="controllers">Additional controllers</param>
        /// <param name="position">Initial position</param>
        /// <param name="direction">Initial direction</param>
        /// <param name="velocity">Initial velocity</param>
        public TestEntity(
            EntityDefinition definition,
            IResourceLibrary library,
            IAvatarFactory avatarFactory,
            IControllerFactory controllerFactory,
            ControllerConfig[] controllers,
            Vector3 position,
            Vector3 direction,
            Vector3 velocity)
            : base(definition, library, avatarFactory, controllerFactory, controllers, position, direction, velocity) { }
    }
}
