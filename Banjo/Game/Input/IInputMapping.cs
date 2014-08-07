//-----------------------------------------------------------------------
// <copyright file="IInputMapping.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;

namespace Game.Input
{
    /// <summary>Describes an input mapping</summary>
    public interface IInputMapping
    {
        /// <summary>Gets the mapping name</summary>
        string Name { get; }

        /// <summary>Checks the input</summary>
        /// <param name="source">The input source to be checked.</param>
        /// <param name="value">The current value, if any; otherwise null.</param>
        /// <returns>The current InputPhase, if any; Otherwise, InputPhase.Unknown.</returns>
        InputPhase CheckInput(IInputSource source, out object value);
    }
}
