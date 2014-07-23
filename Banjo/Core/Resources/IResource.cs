//-----------------------------------------------------------------------
// <copyright file="IResource.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Core.Resources
{
    /// <summary>Represents a resource</summary>
    public interface IResource
    {
        /// <summary>Gets the resource's identifier</summary>
        string Id { get; }
    }
}
