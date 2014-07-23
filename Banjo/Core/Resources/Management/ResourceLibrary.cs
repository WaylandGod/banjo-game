//-----------------------------------------------------------------------
// <copyright file="ResourceLibrary.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.Serialization;
using Core.Data;

namespace Core.Resources.Management
{
    /// <summary>Library of resources</summary>
    [DataContract(Name = "ResourceLibrary", Namespace = "")]
    public class ResourceLibrary : GenericSerializable<ResourceLibrary, XmlSerializer<ResourceLibrary>>, IResourceLibrary, IDisposable
    {
        /// <summary>Format for preloaded resource uris</summary>
        internal const string PreloadUriFormat = "preload:{0}";

        /// <summary>Backing field for ResourcesByUri</summary>
        private IDictionary<string, IResource> resources;

        /// <summary>Initializes a new instance of the ResourceLibrary class</summary>
        public ResourceLibrary() : this(new IResource[0]) { }

        /// <summary>Initializes a new instance of the ResourceLibrary class</summary>
        /// <param name="preloadedResources">Preloaded resources</param>
        public ResourceLibrary(IResource[] preloadedResources)
        {
            this.Uris = new Dictionary<string, string>();
            foreach (var resource in preloadedResources.Where(res => res != null))
            {
                this.AddResource("preload:{0}".FormatInvariant(resource.Id), resource);
            }

#if TRACE
            Log.Trace(
                "Resource library loaded with {0} preloads{1}{2}",
                this.Resources.Count,
                this.Resources.Count > 0 ? ":\n" : string.Empty,
                string.Join("\n", this.Uris.Select(kvp => "{0} ({1})".FormatInvariant(kvp.Key, kvp.Value)).ToArray()));
#endif
        }

        /// <summary>Gets the dictionary of resources by uri</summary>
        private IDictionary<string, IResource> Resources
        {
            get { return this.resources ?? (this.resources = new Dictionary<string, IResource>()); }
        }

        /// <summary>Gets or sets the dictionary of resources by id</summary>
        private IDictionary<string, string> Uris { get; set; }

        /// <summary>Gets or sets the index (for serialization)</summary>
        [DataMember]
        private ResourceIndexEntry[] Index
        {
            get { return this.Uris.Select(kvp => new ResourceIndexEntry { Id = kvp.Key, Uri = kvp.Value }).ToArray(); }
            set { this.Uris = value.ToDictionary(idx => idx.Id, idx => idx.Uri); }
        }

        /// <summary>Gets all resources of a specified type</summary>
        /// <typeparam name="TResource">Type of the resources</typeparam>
        /// <returns>The resources</returns>
        public IEnumerable<TResource> GetAllResources<TResource>() where TResource : class, IResource
        {
            return this.Resources.Values.OfType<TResource>();
        }

        /// <summary>Gets multiple resources by their ids</summary>
        /// <remarks>All resources must be of the same type</remarks>
        /// <typeparam name="TResource">Type of the resources</typeparam>
        /// <param name="ids">Ids for the resources</param>
        /// <returns>The resources</returns>
        public IEnumerable<TResource> GetResources<TResource>(string[] ids) where TResource : class, IResource
        {
            return ids.Select(id => this.GetResource<TResource>(id));
        }

        /// <summary>Gets a loaded resource by its id</summary>
        /// <typeparam name="TResource">Type of the resource</typeparam>
        /// <param name="id">Id for the resource</param>
        /// <returns>The resource</returns>
        public TResource GetResource<TResource>(string id) where TResource : class, IResource
        {
#if DEBUG
            if (!this.Uris.ContainsKey(id))
            {
                throw new ArgumentOutOfRangeException("id", "No resource URI found for the id '{0}'.".FormatInvariant(id));
            }
#endif

            return this.GetResourceByUri<TResource>(this.Uris[id]);
        }

        /// <summary>Gets a resource by its URI. Loads the resource if not already loaded.</summary>
        /// <typeparam name="TResource">Type of the resource</typeparam>
        /// <param name="uri">URI for the resource</param>
        /// <returns>The resource</returns>
        public TResource GetResourceByUri<TResource>(string uri) where TResource : class, IResource
        {
            if (!this.Resources.ContainsKey(uri))
            {
                this.LoadResource<TResource>(uri);
            }

            var resource = this.Resources[uri];
#if DEBUG
            if (!(resource is TResource))
            {
                var msg = "Type mismatch. Resource '{0}' ({1}) is of type {2}. Expected {3}"
                    .FormatInvariant(resource.Id, uri, resource.GetType().FullName, typeof(TResource).FullName);
                throw new ResourceException(msg);
            }
#endif

            return (TResource)resource;
        }

        /// <summary>Adds a resource to the library</summary>
        /// <param name="resource">The resource to add</param>
        public void AddResource(IResource resource)
        {
            var uri = PreloadUriFormat.FormatInvariant(resource.Id);
            this.AddResource(uri, resource);
        }

        /// <summary>Adds a resource to the library</summary>
        /// <param name="uri">URI from which the resource was loaded</param>
        /// <param name="resource">The resource to add</param>
        public void AddResource(string uri, IResource resource)
        {
            this.Resources[uri] = resource;
            this.Uris[resource.Id] = uri;
        }

        /// <summary>Release the native resource</summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>Release the native resources in a derived class</summary>
        /// <param name="disposing">Whether the object is being disposed</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                lock (this.Resources)
                {
                    foreach (var uri in this.Resources.Keys.ToArray())
                    {
                        if (this.Resources[uri] != null)
                        {
                            if (this.Resources[uri] is IDisposable)
                            {
                                ((IDisposable)this.Resources[uri]).Dispose();
                            }
                            
                            this.Resources[uri] = null;
                        }
                    }
                }
            }
        }
        
        /// <summary>Loads a resource directly into the library</summary>
        /// <typeparam name="TResource">Resource type</typeparam>
        /// <param name="uri">URI to load the resource from</param>
        /// <returns>The loaded resource</returns>
        private TResource LoadResource<TResource>(string uri) where TResource : class, IResource
        {
            var resource = ResourceManager.LoadResource<TResource>(uri);
            this.AddResource(uri, resource);
            return resource;
        }

        /// <summary>Serialization entry for the resource index</summary>
        [DataContract(Name = "Resource", Namespace = "")]
        private sealed class ResourceIndexEntry
        {
            /// <summary>Gets or sets the id</summary>
            [DataMember]
            public string Id { get; set; }

            /// <summary>Gets or sets the uri</summary>
            [DataMember]
            public string Uri { get; set; }
        }
    }
}
