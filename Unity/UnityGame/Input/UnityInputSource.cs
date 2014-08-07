//-----------------------------------------------------------------------
// <copyright file="UnityInputSource.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System.Collections.Generic;
using Core;
using Game.Input;
using UnityEngine;

namespace Game.Unity.Input
{
    /// <summary>Unity input source</summary>
    public class UnityInputSource : IInputSource
    {
        /// <summary>Attempts to get an analog input value</summary>
        /// <param name="input">Name of the input to get</param>
        /// <param name="value">Output: The value, if any; Otherwise, 0f</param>
        /// <returns>True if the input source has a value for the input; Otherwise, false.</returns>
        public bool TryGetAnalogInput(string input, out float value)
        {
            try
            {
                ////Log.Trace("AnalogInput[{0}] - Checking...", input);
                value = UnityEngine.Input.GetAxis(input);
                ////Log.Trace("AnalogInput[{0}] - Value: {1}", input, value);
                return true;
            }
            catch ////(System.Exception e)
            {
                ////Log.Trace("AnalogInput[{0}] - Failed: {1}", input, e);
                value = 0f;
                return false;
            }
        }

        /// <summary>Attempts to get a button input value</summary>
        /// <param name="input">Name of the input to get</param>
        /// <param name="value">Output: True if the input is available and active; Otherwise, false</param>
        /// <returns>True if the input source has a value for the input; Otherwise, false.</returns>
        public bool TryGetButtonInput(string input, out bool value)
        {
            try
            {
                ////Log.Trace("ButtonInput[{0}] - Checking...", input);
                value = UnityEngine.Input.GetButtonDown(input);
                ////Log.Trace("ButtonInput[{0}] - Value: {1}", input, value);
                return true;
            }
            catch ////(System.Exception e)
            {
                ////Log.Trace("ButtonInput[{0}] - Failed: {1}", input, e);
                value = false;
                return false;
            }
        }

        /// <summary>Attempts to get a key input value</summary>
        /// <param name="input">Name of the input to get</param>
        /// <param name="value">Output: True if the input is available and active; Otherwise, false</param>
        /// <returns>True if the input source has a value for the input; Otherwise, false.</returns>
        public bool TryGetKeyInput(string input, out bool value)
        {
            try
            {
                ////Log.Trace("KeyInput[{0}] - Checking...", input);
                value = UnityEngine.Input.GetKeyDown(input);
                ////Log.Trace("KeyInput[{0}] - Value: {1}", input, value);
                return true;
            }
            catch ////(System.Exception e)
            {
                ////Log.Trace("KeyInput[{0}] - Failed: {1}", input, e);
                value = false;
                return false;
            }
        }
    }
}
