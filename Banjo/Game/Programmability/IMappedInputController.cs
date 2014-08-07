//-----------------------------------------------------------------------
// <copyright file="IMappedInputController.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System.Collections.Generic;
using Game.Input;
using Core.Programmability;

namespace Game
{
    /// <summary>Represents a controller with input mappings</summary>
    public interface IMappedInputController : IController
    {
        /// <summary>Gets the controller's input mappings</summary>
        IDictionary<IInputMapping, MappedInputHandler> InputMappings { get; }
    }
}

