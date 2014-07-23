//-----------------------------------------------------------------------
// <copyright file="ResourceManager.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System.Collections.Generic;

namespace Core.Resources.Management
{
    /// <summary>Global resource manager</summary>
    public sealed class ResourceManager
    {
        /// <summary>Backing field for Instance</summary>
        private static IResourceManager instance;

        /// <summary>Gets the ResourceManagerImpl instance</summary>
        private static IResourceManager Instance
        {
            get { return instance ?? (instance = new ResourceManagerInternal()); }
        }

        /// <summary>Loads the identified resource as the specified type</summary>
        /// <typeparam name="TResource">Resource type</typeparam>
        /// <param name="uri">Resource uri</param>
        /// <returns>The loaded resource</returns>
        /// <exception cref="ResourceNotFoundException">No resource could be found for the uri.</exception>
        /// <exception cref="InvalidResourceException">The resource is not valid for this loader's ResourceType.</exception>
        /// <exception cref="UnknownResourceTypeException">No resource loader was available for the resource type.</exception>
        public static TResource LoadResource<TResource>(string uri) where TResource : IResource
        {
            return Instance.LoadResource<TResource>(uri);
        }

        /// <summary>Adds a resource loader</summary>
        /// <param name="loader">Resource loader to add</param>
        public static void RegisterResourceLoader(IResourceLoader loader)
        {
            Instance.RegisterResourceLoader(loader);
        }

        /// <summary>Adds multiple resource loaders</summary>
        /// <param name="loaders">Resource loaders to add</param>
        public static void RegisterResourceLoaders(IEnumerable<IResourceLoader> loaders)
        {
            Instance.RegisterResourceLoaders(loaders);
        }
    }
}
