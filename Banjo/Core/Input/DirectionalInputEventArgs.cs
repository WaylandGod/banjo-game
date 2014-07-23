//-----------------------------------------------------------------------
// <copyright file="DirectionalInputEventArgs.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Core.Input
{
    /// <summary>Input event args for mapped directional input</summary>
    public class DirectionalInputEventArgs : MappedInputEventArgs
    {
        /// <summary>Gets or sets the position</summary>
        public Vector3 Position { get; set; }
    }
}
