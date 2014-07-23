//-----------------------------------------------------------------------
// <copyright file="IInputSource.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System.Collections.Generic;
using Core;

namespace Core.Input
{
    /// <summary>Interface for input sources</summary>
    public interface IInputSource
    {
        /// <summary>Gets the joystick (analog) input states</summary>
        IDictionary<string, Vector3> Joysticks { get; }

        /// <summary>Gets the button (digital) input states</summary>
        IDictionary<string, bool> Buttons { get; }
    }
}
