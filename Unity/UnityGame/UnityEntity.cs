//-----------------------------------------------------------------------
// <copyright file="UnityEntity.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using Core;
using Core.Data;
using Core.Factories;
using Core.Resources.Management;
using Core.Unity;
using Game;
using Game.Data;
using Game.Factories;
using Game.Unity.Behaviours;
using UnityEngine;

namespace Game.Unity
{
    /// <summary>Represents an entity</summary>
    /// <remarks>An entity is any interactive/dynamic object in a game</remarks>
    public class UnityEntity : EntityBase, IEntity
    {
        /// <summary>Layer tag for entity GameObjects</summary>
        public const string EntityLayer = "Entities";

        /// <summary>Initializes a new instance of the UnityEntity class</summary>
        public UnityEntity(
            EntityDefinition definition,
            IResourceLibrary library,
            IAvatarFactory avatarFactory,
            IControllerFactory[] controllerFactories,
            ControllerConfig[] controllers,
            Vector3D position,
            Vector3D direction,
            Vector3D velocity)
            : base(definition, library, avatarFactory, controllerFactories, controllers, position, direction, velocity)
        {
            this.UnityAvatar.AddObjectBehaviour<EntityBehaviour>().Entity = this;
            this.UnityAvatar.Layer = EntityLayer;
        }

        /// <summary>Gets the IAvatar as a UnityAvatar</summary>
        private UnityAvatar UnityAvatar { get { return this.Avatar as UnityAvatar; } }
    }
}
