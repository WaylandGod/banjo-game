//-----------------------------------------------------------------------
// <copyright file="InvalidResourceException.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;

namespace Core.Resources.Management
{
    /// <summary>
    /// Exception thrown by resource framework components when a resource is invalid
    /// </summary>
    public class InvalidResourceException : ResourceException
    {
        /// <summary>Initializes a new instance of the InvalidResourceException class</summary>
        /// <param name="uri">Resource uri</param>
        /// <param name="type">Expected resource type</param>
        public InvalidResourceException(string uri, Type type) : this(uri, type, null) { }

        /// <summary>Initializes a new instance of the InvalidResourceException class</summary>
        /// <param name="uri">Resource uri</param>
        /// <param name="type">Expected resource type</param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public InvalidResourceException(string uri, Type type, Exception innerException)
            : base("Unable to load resource '{0}' ({1})".FormatInvariant(uri, type.FullName), uri, type, innerException) { }
    }
}
