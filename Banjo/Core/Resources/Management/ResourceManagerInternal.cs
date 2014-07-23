//-----------------------------------------------------------------------
// <copyright file="ResourceManagerInternal.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Resources.Management
{
    /// <summary>Implementation of IResourceManager</summary>
    internal class ResourceManagerInternal : IResourceManager
    {
        /// <summary>Collection of resource loaders</summary>
        private readonly IList<IResourceLoader> loaders;

        /// <summary>Initializes a new instance of the ResourceManagerInternal class</summary>
        internal ResourceManagerInternal()
        {
            this.loaders = new List<IResourceLoader>();
        }

        /// <summary>Gets the resource types supported</summary>
        public IEnumerable<Type> SupportedTypes
        {
            get { return this.loaders.Select(l => l.ResourceType).Distinct(); }
        }

        /// <summary>Gets the resource schemes supported</summary>
        public IEnumerable<string> SupportedSchemes
        {
            get { return this.loaders.Select(l => l.Scheme).Distinct(); }
        }

        /// <summary>Gets the resource extensions supported</summary>
        public IEnumerable<string> SupportedExtensions
        {
            get { return this.loaders.SelectMany(l => l.Extensions).Distinct(); }
        }

        /// <summary>Loads the identified resource as the specified type</summary>
        /// <typeparam name="TResource">Resource type</typeparam>
        /// <param name="uri">Resource uri</param>
        /// <returns>The loaded resource</returns>
        /// <exception cref="ResourceNotFoundException">No resource could be found for the uri.</exception>
        /// <exception cref="InvalidResourceException">The resource is not valid for this loader's ResourceType.</exception>
        /// <exception cref="UnknownResourceTypeException">No resource loader was available for the resource type.</exception>
        public TResource LoadResource<TResource>(string uri) where TResource : IResource
        {
#if DEBUG
            if (uri == null)
            {
                throw new ArgumentNullException("uri");
            }
#endif

            var uriParts = uri.Split(new[] { ':' }, 2, StringSplitOptions.RemoveEmptyEntries);
#if DEBUG
            if (uriParts.Length < 2)
            {
                throw new ArgumentException("uri '{0}' has no scheme", "uri");
            }
#endif

            var scheme = uriParts[0];
            var identifier = uriParts[1];
            var extension = identifier.Contains('.') ? identifier.Split('.').LastOrDefault() : "?";
            var type = typeof(TResource);
            lock (this.loaders)
            {
                var loader = this.loaders
                    .Where(l => l.Scheme == scheme)
                    .Where(l => l.Extensions.Contains(extension))
                    .Where(l => type.IsAssignableFrom(l.ResourceType))
                    .FirstOrDefault();

#if DEBUG
                if (loader == null)
                {
                    if (!this.SupportedSchemes.Contains(scheme))
                    {
                        throw new UnknownResourceSchemeException(uri, type);
                    }
                    else if (!this.SupportedExtensions.Contains(extension))
                    {
                        throw new ResourceException("Loader not found for '{0}' ({1})".FormatInvariant(uri, extension));
                    }
                    else if (!this.SupportedTypes.Any(t => t.IsAssignableFrom(type)))
                    {
                        throw new UnknownResourceTypeException(uri, type);
                    }
                    else
                    {
                        throw new ResourceException("Loader not found for '{0}' ({1})".FormatInvariant(uri, type.FullName));
                    }
                }
#endif

                return (TResource)loader.LoadResource(identifier);
            }
        }

        /// <summary>Adds multiple resource loaders</summary>
        /// <param name="loaders">Resource loaders to add</param>
        public void RegisterResourceLoaders(IEnumerable<IResourceLoader> loaders)
        {
            foreach (var loader in loaders)
            {
                this.RegisterResourceLoader(loader);
            }
        }

        /// <summary>Adds a resource loader</summary>
        /// <param name="loader">Resource loader to add</param>
        public void RegisterResourceLoader(IResourceLoader loader)
        {
#if DEBUG
            if (loader == null)
            {
                throw new ArgumentNullException("loader");
            }
#endif

            lock (this.loaders)
            {
                this.loaders.Add(loader);
            }
        }
    }
}
