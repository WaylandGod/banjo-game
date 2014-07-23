//-----------------------------------------------------------------------
// <copyright file="ITextResource.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Core.Resources
{
    /// <summary>Represents a text resource</summary>
    public interface ITextResource : IResource
    {
        /// <summary>Gets the resource's text</summary>
        string Text { get; }
    }
}
