//-----------------------------------------------------------------------
// <copyright file="IControllerFactory.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using Core.Programmability;

namespace Core.Factories
{
    /// <summary>Controller factory interface</summary>
    public interface IControllerFactory : IFactory<IController>
    {
        /// <summary>Gets the type of the controller targets</summary>
        Type TargetType { get; }

        /// <summary>Creates an entity controller instance</summary>
        /// <param name="controllerId">Controller identifier</param>
        /// <param name="settings">Controller configuration</param>
        /// <param name="target">Controller target</param>
        /// <returns>The created IGame instance</returns>
        IController Create(string controllerId, IConfig settings, IControllerTarget target);
    }
}
