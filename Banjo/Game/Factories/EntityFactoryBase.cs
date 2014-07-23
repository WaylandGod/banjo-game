//-----------------------------------------------------------------------
// <copyright file="EntityFactoryBase.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using Core;
using Core.Resources.Management;
using Game.Data;

namespace Game.Factories
{
    /// <summary>Creates instances of IEntity</summary>
    public abstract class EntityFactoryBase : ResourceFactoryBase<IEntity>, IEntityFactory
    {
        /// <summary>Initializes a new instance of the EntityFactoryBase class</summary>
        /// <param name="resources">Resource library</param>
        /// <param name="avatarFactory">Avatar factory</param>
        /// <param name="controllerFactory">Controller factory</param>
        protected EntityFactoryBase(IResourceLibrary resources, IAvatarFactory avatarFactory, IControllerFactory controllerFactory)
            : base(resources)
        {
            this.AvatarFactory = avatarFactory;
            this.ControllerFactory = controllerFactory;
        }

        /// <summary>Gets the avatar factory</summary>
        protected IAvatarFactory AvatarFactory { get; private set; }

        /// <summary>Gets the controller factory</summary>
        protected IControllerFactory ControllerFactory { get; private set; }

        /// <summary>Creates an instance of IEntity</summary>
        /// <param name="definition">Entity definition</param>
        /// <param name="controllers">Additional controllers</param>
        /// <param name="position">Initial position</param>
        /// <param name="direction">Initial direction</param>
        /// <param name="velocity">Initial velocity</param>
        /// <returns>The created IEntity instance</returns>
        protected abstract IEntity Create(EntityDefinition definition, ControllerConfig[] controllers, Vector3 position, Vector3 direction, Vector3 velocity);
    }
}
