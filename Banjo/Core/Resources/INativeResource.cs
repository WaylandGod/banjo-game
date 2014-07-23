//-----------------------------------------------------------------------
// <copyright file="INativeResource.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;

namespace Core.Resources
{
    /// <summary>Represents a wrapped, native resource</summary>
    public interface INativeResource : IResource, IDisposable
    {
        /// <summary>Gets the type of the native resource</summary>
        Type NativeType { get; }

        /// <summary>Gets the native resource</summary>
        object NativeResource { get; }
    }
}
