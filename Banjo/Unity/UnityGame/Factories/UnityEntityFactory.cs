//-----------------------------------------------------------------------
// <copyright file="UnityEntityFactory.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using Core;
using Core.Resources.Management;
using Game;
using Game.Data;
using Game.Factories;

namespace Game.Unity.Factories
{
    /// <summary>Creates instances of IEntity</summary>
    public class UnityEntityFactory : EntityFactoryBase, IEntityFactory
    {
        /// <summary>Initializes a new instance of the UnityEntityFactory class</summary>
        /// <param name="resources">Resource library</param>
        /// <param name="avatarFactory">Avatar factory</param>
        /// <param name="controllerFactory">Controller factory</param>
        public UnityEntityFactory(
            IResourceLibrary resources,
            IAvatarFactory avatarFactory,
            IControllerFactory controllerFactory)
            : base(resources, avatarFactory, controllerFactory) { }

        /// <summary>Creates an instance of IEntity</summary>
        /// <param name="definition">Entity definition</param>
        /// <param name="controllers">Additional controllers</param>
        /// <param name="position">Initial position</param>
        /// <param name="direction">Initial direction</param>
        /// <param name="velocity">Initial velocity</param>
        /// <returns>The created IEntity instance</returns>
        protected override IEntity Create(
            EntityDefinition definition,
            ControllerConfig[] controllers,
            Vector3 position,
            Vector3 direction,
            Vector3 velocity)
        {
            return new UnityEntity(
                definition,
                this.Resources,
                this.AvatarFactory,
                this.ControllerFactory,
                controllers,
                position,
                direction,
                velocity);
        }
    }
}
