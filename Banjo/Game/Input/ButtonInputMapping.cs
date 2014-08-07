//-----------------------------------------------------------------------
// <copyright file="ButtonInputMapping.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;

namespace Game.Input
{
    /// <summary>Digital input mapping</summary>
    public class ButtonInputMapping : InputMapping<bool>
    {
        /// <summary>Initializes a new instance of the ButtonInputMapping class</summary>
        /// <param name="mappingName">Name of the mapping</param>
        /// <param name="inputName">Name of the input checked</param>
        public ButtonInputMapping(string mappingName, string inputName)
            : base(mappingName, inputName) { }

        /// <summary>Get a digital input value</summary>
        /// <param name="value">The input value, if any; Otherwise, the type's default value.</param>
        /// <returns>True if the input has a value; otherwise, false.</returns>
        protected override bool TryGetInputValue(IInputSource source, out bool value)
        {
            return source.TryGetButtonInput(this.InputName, out value);
        }
    }
}
