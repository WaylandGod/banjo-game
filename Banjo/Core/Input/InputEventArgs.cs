//-----------------------------------------------------------------------
// <copyright file="InputEventArgs.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;

namespace Core.Input
{
    /// <summary>Represents an input event</summary>
    public class InputEventArgs : EventArgs
    {
        /// <summary>Gets or sets the input phase</summary>
        public InputPhase Phase { get; set; }
    }
}
