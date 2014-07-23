//-----------------------------------------------------------------------
// <copyright file="UnityEntity.cs" company="Benjamin Woodall">
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
    /// <summary>Represents an entity</summary>
    /// <remarks>An entity is any interactive/dynamic object in a game</remarks>
    public class UnityEntity : EntityBase, IEntity
    {
        /// <summary>Layer tag for entity GameObjects</summary>
        public const string EntityLayer = "Entities";

        /// <summary>Initializes a new instance of the UnityEntity class</summary>
        /// <param name="definition">Entity definition</param>
        /// <param name="library">Resource library</param>
        /// <param name="avatarFactory">Avatar factory</param>
        /// <param name="controllerFactory">Controller factory</param>
        /// <param name="controllers">Additional controllers</param>
        /// <param name="position">Initial position</param>
        /// <param name="direction">Initial direction</param>
        /// <param name="velocity">Initial velocity</param>
        public UnityEntity(
            EntityDefinition definition,
            IResourceLibrary library,
            IAvatarFactory avatarFactory,
            IControllerFactory controllerFactory,
            ControllerConfig[] controllers,
            Vector3 position,
            Vector3 direction,
            Vector3 velocity)
            : base(definition, library, avatarFactory, controllerFactory, controllers, position, direction, velocity)
        {
            this.UnityAvatar.AddObjectBehaviour<EntityBehaviour>().Entity = this;
            this.UnityAvatar.Layer = EntityLayer;
        }

        /// <summary>Gets the IAvatar as a UnityAvatar</summary>
        private UnityAvatar UnityAvatar { get { return this.Avatar as UnityAvatar; } }
    }
}
