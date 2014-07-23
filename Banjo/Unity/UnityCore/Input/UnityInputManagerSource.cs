//-----------------------------------------------------------------------
// <copyright file="UnityInputManagerSource.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System.Collections.Generic;
using Core.Input;
using UnityEngine;

namespace Core.Unity.Input
{
    /// <summary>Unity managed input source</summary>
    public class UnityInputManagerSource : IInputSource
    {
        /// <summary>Gets the joystick (analog) input states</summary>
        public IDictionary<string, Vector3> Joysticks
        {
            get
            {
                return new Dictionary<string, Vector3>
                {
                    {
                        "Direction",
                        new Vector3(
                            UnityEngine.Input.GetAxis("Horizontal"),
                            UnityEngine.Input.GetAxis("Vertical"),
                            0f)
                    },
                    {
                        "Mouse",
                        new Vector3(
                            UnityEngine.Input.GetAxis("Mouse X"),
                            UnityEngine.Input.GetAxis("Mouse Y"),
                            UnityEngine.Input.GetAxis("Mouse ScrollWheel"))
                    },
                };
            }
        }

        /// <summary>Gets the button (digital) input states</summary>
        public IDictionary<string, bool> Buttons { get { return null; } }
    }
}
