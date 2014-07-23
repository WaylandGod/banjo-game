//-----------------------------------------------------------------------
// <copyright file="IControllerFactory.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using Core;
using Game.Programmability;

namespace Game.Factories
{
    /// <summary>Controller factory interface</summary>
    public interface IControllerFactory : IFactory<IEntityController> { }
}
