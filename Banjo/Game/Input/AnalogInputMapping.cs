//-----------------------------------------------------------------------
// <copyright file="AnalogInputMapping.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;

namespace Game.Input
{
    /// <summary>Axis input mapping base class</summary>
    public class AnalogInputMapping : InputMapping<float>
    {
        private readonly Func<float, float> modifier;

        /// <summary>Initializes a new instance of the AnalogInputMapping class</summary>
        /// <param name="mappingName">Name of the mapping</param>
        /// <param name="inputName">Name of the input checked</param>
        public AnalogInputMapping(string mappingName, string inputName, Func<float, float> modifier = null)
            : base(mappingName, inputName)
        {
            this.modifier = modifier ?? (f => f);
        }

        /// <summary>Get an axis input value</summary>
        /// <param name="value">The input value, if any; Otherwise, the type's default value.</param>
        /// <returns>True if the input has a value; otherwise, false.</returns>
        protected override bool TryGetInputValue(IInputSource source, out float value)
        {
            float inputValue;
            if (source.TryGetAnalogInput(this.InputName, out inputValue))
            {
                value = this.modifier(inputValue);
                return true;
            }
            else
            {
                value = 0f;
                return false;
            }
        }
    }
}
