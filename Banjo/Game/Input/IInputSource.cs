//-----------------------------------------------------------------------
// <copyright file="IInputSource.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Game.Input
{
    /// <summary>Interface for input sources</summary>
    public interface IInputSource
    {
        /// <summary>Attempts to get an analog input value</summary>
        /// <param name="input">Name of the input to get</param>
        /// <param name="value">Output: The value, if any; Otherwise, 0f</param>
        /// <returns>True if the input source has a value for the input; Otherwise, false.</returns>
        bool TryGetAnalogInput(string input, out float value);

        /// <summary>Attempts to get a button input value</summary>
        /// <param name="input">Name of the input to get</param>
        /// <param name="value">Output: True if the input is available and active; Otherwise, false</param>
        /// <returns>True if the input source has a value for the input; Otherwise, false.</returns>
        bool TryGetButtonInput(string input, out bool value);

        /// <summary>Attempts to get a key input value</summary>
        /// <param name="input">Name of the input to get</param>
        /// <param name="value">Output: True if the input is available and active; Otherwise, false</param>
        /// <returns>True if the input source has a value for the input; Otherwise, false.</returns>
        bool TryGetKeyInput(string input, out bool value);
    }
}
