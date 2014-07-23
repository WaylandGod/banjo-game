//-----------------------------------------------------------------------
// <copyright file="IResourceLibrary.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Core.Data;

namespace Core.Resources.Management
{
    /// <summary>Represents a library of resources</summary>
    public interface IResourceLibrary : IDisposable
    {
        /// <summary>Gets a loaded resource by its id</summary>
        /// <typeparam name="TResource">Type of the resource</typeparam>
        /// <param name="id">Id for the resource</param>
        /// <returns>The resource</returns>
        TResource GetResource<TResource>(string id) where TResource : class, IResource;

        /// <summary>Gets all resources of a specified type</summary>
        /// <typeparam name="TResource">Type of the resources</typeparam>
        /// <returns>The resources</returns>
        IEnumerable<TResource> GetAllResources<TResource>() where TResource : class, IResource;

        /// <summary>Gets multiple resources by their ids</summary>
        /// <remarks>All resources must be of the same type</remarks>
        /// <typeparam name="TResource">Type of the resources</typeparam>
        /// <param name="ids">Ids for the resources</param>
        /// <returns>The resources</returns>
        IEnumerable<TResource> GetResources<TResource>(string[] ids) where TResource : class, IResource;

        /// <summary>Gets a resource by its URI. Loads the resource if not already loaded.</summary>
        /// <typeparam name="TResource">Type of the resource</typeparam>
        /// <param name="uri">URI for the resource</param>
        /// <returns>The resource</returns>
        TResource GetResourceByUri<TResource>(string uri) where TResource : class, IResource;

        /// <summary>Adds a resource to the library</summary>
        /// <param name="resource">The resource to add</param>
        void AddResource(IResource resource);

        /// <summary>Adds a resource to the library</summary>
        /// <param name="uri">URI from which the resource was loaded</param>
        /// <param name="resource">The resource to add</param>
        void AddResource(string uri, IResource resource);
    }
}
