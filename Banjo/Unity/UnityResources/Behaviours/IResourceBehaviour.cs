//-----------------------------------------------------------------------
// <copyright file="IResourceBehaviour.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using Core.Resources;

namespace Unity.Resources.Behaviours
{
    /// <summary>Interface for resource behaviours</summary>
    /// <typeparam name="TResource">Resource type</typeparam>
    public interface IResourceBehaviour<TResource>
        where TResource : class, IResource
    {
        /// <summary>Gets the resource provided by the behaviour</summary>
        TResource Resource { get; }
    }
}
