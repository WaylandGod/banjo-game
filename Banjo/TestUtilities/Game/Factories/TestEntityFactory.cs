//-----------------------------------------------------------------------
// <copyright file="TestEntityFactory.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using Core;
using Core.Data;
using Core.Factories;
using Core.Resources.Management;
using Game;
using Game.Data;
using Game.Factories;

namespace TestUtilities.Game.Factories
{
    /// <summary>Creates instances of IEntity</summary>
    public class TestEntityFactory : EntityFactoryBase, IEntityFactory
    {
        /// <summary>Initializes a new instance of the TestEntityFactory class</summary>
        public TestEntityFactory(
            IResourceLibrary resources,
            IAvatarFactory avatarFactory,
            IControllerFactory[] controllerFactories)
            : base(resources, avatarFactory, controllerFactories) { }

        /// <summary>Creates an instance of IEntity</summary>
        /// <param name="definition">Entity definition</param>
        /// <param name="position">Initial position</param>
        /// <param name="direction">Initial direction</param>
        /// <param name="velocity">Initial velocity</param>
        /// <param name="controllers">Additional controllers</param>
        /// <returns>The created IEntity instance</returns>
        public override IEntity Create(
            EntityDefinition definition,
            ControllerConfig[] controllers,
            Vector3D position,
            Vector3D direction,
            Vector3D velocity)
        {
            return new TestEntity(
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
