//-----------------------------------------------------------------------
// <copyright file="MappedInputEventArgs.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Core.Input
{
    /// <summary>Input event args for mapped input</summary>
    public class MappedInputEventArgs : InputEventArgs
    {
        /// <summary>Gets or sets the mapped input name</summary>
        public string Name { get; set; }
    }
}
