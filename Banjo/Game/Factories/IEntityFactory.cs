//-----------------------------------------------------------------------
// <copyright file="IEntityFactory.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using Core;
using Game.Data;

namespace Game.Factories
{
    /// <summary>Entity factory interface</summary>
    public interface IEntityFactory : IFactory<IEntity>
    {
        /// <summary>Creates an instance of IEntity</summary>
        /// <param name="definition">Entity definition</param>
        /// <param name="controllers">Additional controllers</param>
        /// <param name="position">Initial position</param>
        /// <param name="direction">Initial direction</param>
        /// <param name="velocity">Initial velocity</param>
        /// <returns>The created IEntity instance</returns>
        IEntity Create(
            EntityDefinition definition,
            ControllerConfig[] controllers,
            Vector3 position,
            Vector3 direction,
            Vector3 velocity);
    }
}
