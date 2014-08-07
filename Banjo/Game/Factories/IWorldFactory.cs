//-----------------------------------------------------------------------
// <copyright file="IWorldFactory.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using Core.Factories;
using Game.Data;

namespace Game.Factories
{
    /// <summary>World factory interface</summary>
    public interface IWorldFactory : IFactory<IWorld>
    {
        /// <summary>Creates an instance of IWorld</summary>
        /// <param name="definition">Level definition</param>
        /// <returns>The created IWorld instance</returns>
        IWorld Create(LevelDefinition definition);
    }
}
