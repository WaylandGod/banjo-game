//-----------------------------------------------------------------------
// <copyright file="ResourceNotFoundException.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;

namespace Core.Resources.Management
{
    /// <summary>
    /// Exception thrown by resource framework components when a resource cannot be found
    /// </summary>
    public class ResourceNotFoundException : ResourceException
    {
        /// <summary>Initializes a new instance of the ResourceNotFoundException class</summary>
        /// <param name="uri">Resource uri</param>
        public ResourceNotFoundException(string uri) : this(uri, null, null) { }

        /// <summary>Initializes a new instance of the ResourceNotFoundException class</summary>
        /// <param name="uri">Resource uri</param>
        /// <param name="type">Expected resource type</param>
        public ResourceNotFoundException(string uri, Type type) : this(uri, type, null) { }

        /// <summary>Initializes a new instance of the ResourceNotFoundException class</summary>
        /// <param name="uri">Resource uri</param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public ResourceNotFoundException(string uri, Exception innerException) : this(uri, null, innerException) { }

        /// <summary>Initializes a new instance of the ResourceNotFoundException class</summary>
        /// <param name="uri">Resource uri</param>
        /// <param name="type">Expected resource type</param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public ResourceNotFoundException(string uri, Type type, Exception innerException)
            : base("Resource not found: '{0}'".FormatInvariant(uri), uri, type, innerException) { }
    }
}
