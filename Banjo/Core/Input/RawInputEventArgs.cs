//-----------------------------------------------------------------------
// <copyright file="RawInputEventArgs.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;

namespace Core.Input
{
    /// <summary>Event args for "raw" input taken directly from sources</summary>
    public class RawInputEventArgs : InputEventArgs
    {
        /// <summary>Gets the joystick (analog) input states</summary>
        public readonly IDictionary<string, Vector3> Joysticks;

        /// <summary>Gets the button (digital) input states</summary>
        public readonly IDictionary<string, bool> Buttons;

        /// <summary>Initializes a new instance of the RawInputEventArgs class</summary>
        /// <param name="sources">Input sources</param>
        public RawInputEventArgs(IEnumerable<IInputSource> sources)
        {
            this.Phase = InputPhase.Unknown;
            this.Joysticks = sources
                .Where(s => s != null && s.Joysticks != null)
                .SelectMany(s => s.Joysticks)
                .ToDictionary(true);
            this.Buttons = sources
                .Where(s => s != null && s.Buttons != null)
                .SelectMany(s => s.Buttons)
                .Distinct((a, b) => a.Key == b.Key)
                .ToDictionary(true);
        }
    }
}
