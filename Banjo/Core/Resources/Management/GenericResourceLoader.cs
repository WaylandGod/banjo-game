//-----------------------------------------------------------------------
// <copyright file="GenericResourceLoader.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;

namespace Core.Resources.Management
{
    /// <summary>Generic resource loader</summary>
    /// <typeparam name="TResource">Type of resources loaded</typeparam>
    public class GenericResourceLoader<TResource> : IResourceLoader
        where TResource : class, IResource
    {
        /// <summary>Function that loads resources</summary>
        private readonly Func<string, object[], TResource> loadResource;

        /// <summary>Context for the load resource function</summary>
        private readonly object[] context;

        /// <summary>Initializes a new instance of the GenericResourceLoader class</summary>
        /// <param name="scheme">Resource uri scheme handled by the loader</param>
        /// <param name="extensions">Resource extensions handled by the loader</param>
        /// <remarks>Requires derived class override LoadGenericResource</remarks>
        public GenericResourceLoader(string scheme, string[] extensions)
            : this(scheme, extensions, (id, ctx) => null)
        {
            this.loadResource = (id, ctx) => this.LoadGenericResource(id);
        }

        /// <summary>Initializes a new instance of the GenericResourceLoader class</summary>
        /// <param name="scheme">Resource uri scheme handled by the loader</param>
        /// <param name="extensions">Resource extensions handled by the loader</param>
        /// <param name="loadResource">Function that loads the resource type. If not found, should return null.</param>
        public GenericResourceLoader(string scheme, string[] extensions, Func<string, TResource> loadResource)
            : this(scheme, extensions, (id, ctx) => { return loadResource(id); }) { }

        /// <summary>Initializes a new instance of the GenericResourceLoader class</summary>
        /// <param name="scheme">Resource uri scheme handled by the loader</param>
        /// <param name="extensions">Resource extensions handled by the loader</param>
        /// <param name="loadResource">Function that loads the resource type. If not found, should return null.</param>
        /// <param name="context">Context passed to the loading function</param>
        public GenericResourceLoader(string scheme, string[] extensions, Func<string, object[], TResource> loadResource, params object[] context)
        {
            if (string.IsNullOrEmpty(scheme))
            {
                throw scheme == null ? new ArgumentNullException("scheme") : new ArgumentException("scheme cannot be empty", "scheme");
            }

            this.ResourceType = typeof(TResource);
            this.Scheme = scheme;
            this.Extensions = extensions;
            this.loadResource = loadResource;
            this.context = context;
        }

        /// <summary>Gets the type of resources loaded</summary>
        public Type ResourceType { get; private set; }

        /// <summary>Gets the resource uri scheme</summary>
        public string Scheme { get; private set; }

        /// <summary>Gets the resource extensions handled by this loader</summary>
        public string[] Extensions { get; private set; }

        /// <summary>Loads the resource for the identifier</summary>
        /// <param name="identifier">Resource identifier</param>
        /// <returns>The loaded resource</returns>
        /// <exception cref="ResourceNotFoundException">No resource could be found for the uri.</exception>
        /// <exception cref="InvalidResourceException">The resource is not valid for this loader's ResourceType.</exception>
        public IResource LoadResource(string identifier)
        {
#if DEBUG
            if (string.IsNullOrEmpty(identifier))
            {
                throw identifier == null ? new ArgumentNullException("identifier") : new ArgumentException("identifier cannot be empty", "identifier");
            }
#endif

            try
            {
                var resource = this.loadResource(identifier, this.context);
                if (resource == null)
                {
                    throw new ResourceNotFoundException(identifier, this.ResourceType);
                }

                return resource;
            }
            catch (ResourceNotFoundException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new InvalidResourceException(identifier, this.ResourceType, e);
            }
        }

        /// <summary>When overridden in a derived class, loads the resource for the identifier</summary>
        /// <param name="identifier">Resource identifier</param>
        /// <returns>The loaded resource</returns>
        protected virtual TResource LoadGenericResource(string identifier)
        {
            throw new NotImplementedException();
        }
    }
}
