//-----------------------------------------------------------------------
// <copyright file="UnityEntityFactory.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using Core.Data;
using Core.Factories;
using Core.Resources.Management;
using Game;
using Game.Data;
using Game.Factories;
using UnityEngine;

namespace Game.Unity.Factories
{
    /// <summary>Creates instances of IEntity</summary>
    public class UnityEntityFactory : EntityFactoryBase, IEntityFactory
    {
        /// <summary>Initializes a new instance of the UnityEntityFactory class</summary>
        public UnityEntityFactory(
            IResourceLibrary resources,
            IAvatarFactory avatarFactory,
            IControllerFactory[] controllerFactories)
        : base(resources, avatarFactory, controllerFactories) { }

        /// <summary>Creates an instance of IEntity</summary>
        /// <param name="definition">Entity definition</param>
        /// <param name="controllers">Additional controllers</param>
        /// <param name="position">Initial position</param>
        /// <param name="direction">Initial direction</param>
        /// <param name="velocity">Initial velocity</param>
        /// <returns>The created IEntity instance</returns>
        public override IEntity Create(
            EntityDefinition definition,
            ControllerConfig[] controllers,
            Vector3D position,
            Vector3D direction,
            Vector3D velocity)
        {
            return new UnityEntity(
                definition,
                this.Resources,
                this.AvatarFactory,
                this.ControllerFactories,
                controllers,
                position,
                direction,
                velocity);
        }
    }
}
