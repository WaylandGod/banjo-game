//-----------------------------------------------------------------------
// <copyright file="IResourceManager.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace Core.Resources.Management
{
    /// <summary>Interface for resource managers</summary>
    public interface IResourceManager
    {
        /// <summary>Gets the resource types supported</summary>
        IEnumerable<Type> SupportedTypes { get; }

        /// <summary>Gets the resource schemes supported</summary>
        IEnumerable<string> SupportedSchemes { get; }

        /// <summary>Gets the resource extensions supported</summary>
        IEnumerable<string> SupportedExtensions { get; }

        /// <summary>Loads the identified resource as the specified type</summary>
        /// <typeparam name="TResource">Resource type</typeparam>
        /// <param name="uri">Resource uri</param>
        /// <returns>The loaded resource</returns>
        /// <exception cref="ResourceNotFoundException">No resource could be found for the uri.</exception>
        /// <exception cref="InvalidResourceException">The resource is not valid for this loader's ResourceType.</exception>
        /// <exception cref="UnknownResourceTypeException">No resource loader was available for the resource type.</exception>
        TResource LoadResource<TResource>(string uri) where TResource : IResource;

        /// <summary>Adds a resource loader</summary>
        /// <param name="loader">Resource loader to add</param>
        void RegisterResourceLoader(IResourceLoader loader);

        /// <summary>Adds multiple resource loaders</summary>
        /// <param name="loaders">Resource loaders to add</param>
        void RegisterResourceLoaders(IEnumerable<IResourceLoader> loaders);
    }
}
