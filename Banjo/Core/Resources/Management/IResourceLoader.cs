//-----------------------------------------------------------------------
// <copyright file="IResourceLoader.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;

namespace Core.Resources.Management
{
    /// <summary>Interface for resource loaders</summary>
    public interface IResourceLoader
    {
        /// <summary>Gets the type of resources loaded</summary>
        Type ResourceType { get; }

        /// <summary>Gets the resource uri scheme</summary>
        string Scheme { get; }

        /// <summary>Gets the resource extensions handled by this loader</summary>
        string[] Extensions { get; }

        /// <summary>Loads the resource for the uri</summary>
        /// <param name="identifier">Resource identifier</param>
        /// <returns>The loaded resource</returns>
        /// <exception cref="ResourceNotFoundException">No resource could be found for the uri.</exception>
        /// <exception cref="InvalidResourceException">The resource is not valid for this loader's ResourceType.</exception>
        IResource LoadResource(string identifier);
    }
}
