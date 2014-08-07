//-----------------------------------------------------------------------
// <copyright file="KeyInputMapping.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;

namespace Game.Input
{
    /// <summary>Key input mapping</summary>
    public class KeyInputMapping : InputMapping<bool>
    {
        /// <summary>Initializes a new instance of the KeyInputMapping class</summary>
        /// <param name="mappingName">Name of the mapping</param>
        /// <param name="inputName">Name of the input checked</param>
        public KeyInputMapping(string mappingName, string inputName)
            : base(mappingName, inputName) { }

        /// <summary>Get a key input value</summary>
        /// <param name="value">The input value, if any; Otherwise, the type's default value.</param>
        /// <returns>True if the input has a value; otherwise, false.</returns>
        protected override bool TryGetInputValue(IInputSource source, out bool value)
        {
            return source.TryGetKeyInput(this.InputName, out value);
        }
    }
}
