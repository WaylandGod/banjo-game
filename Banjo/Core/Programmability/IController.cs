//-----------------------------------------------------------------------
// <copyright file="IController.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;

namespace Core.Programmability
{
    /// <summary>Represents a controller that can be applied to objects</summary>
    public interface IController : IDisposable
    {
        /// <summary>Gets the runtime identifier</summary>
        RuntimeId Id { get; }

        /// <summary>Gets the target object</summary>
        IControllerTarget Target { get; }

        /// <summary>Gets the configuration</summary>
        IConfig Config { get; }
    }
}
