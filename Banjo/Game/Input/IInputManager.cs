//-----------------------------------------------------------------------
// <copyright file="IInputManager.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using Core;

namespace Game.Input
{
    /// <summary>Describes an input manager</summary>
    /// <remarks>Input managers monitor the platform's inputs and dispatch input events.</remarks>
    public interface IInputManager
    {
        /// <summary>Adds a mapped input handler</summary>
        /// <param name="mapping">Input mapping.</param>
        /// <param name="id">Identifier (used to later remove).</param>
        /// <param name="handler">Handler to add.</param>
        void AddMappedInputHandler(IInputMapping mapping, RuntimeId id, MappedInputHandler handler);

        /// <summary>Removes a mapped input handler</summary>
        /// <param name="mapping">Input mapping.</param>
        /// <param name="id">Identifier of the handler to remove.</param>
        /// <returns>True if the input was found and removed; otherwise, false.</returns>
        bool RemoveMappedInputHandler(IInputMapping mapping, RuntimeId id);

        /// <summary>Updates with the latest input states</summary>
        void Update();
    }
}
