//-----------------------------------------------------------------------
// <copyright file="ResourceException.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using Core;

namespace Core.Resources.Management
{
    /// <summary>Exception thrown by resource framework components</summary>
    public class ResourceException : CoreException
    {
        /// <summary>Expected type of resource (if available)</summary>
        public readonly Type ResourceType;

        /// <summary>uri for the resource (if available)</summary>
        public readonly string ResourceUri;

        /// <summary>Initializes a new instance of the ResourceException class</summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public ResourceException(string message) : base(message) { }

        /// <summary>Initializes a new instance of the ResourceException class</summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public ResourceException(string message, Exception innerException) : base(message, innerException) { }

        /// <summary>Initializes a new instance of the ResourceException class</summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="uri">Resource uri</param>
        public ResourceException(string message, string uri) : this(message, uri, null) { }

        /// <summary>Initializes a new instance of the ResourceException class</summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="type">Expected resource type</param>
        public ResourceException(string message, Type type) : this(message, null, type) { }

        /// <summary>Initializes a new instance of the ResourceException class</summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="uri">Resource uri</param>
        /// <param name="type">Expected resource type</param>
        public ResourceException(string message, string uri, Type type) : this(message, uri, type, null) { }

        /// <summary>Initializes a new instance of the ResourceException class</summary>
        /// <param name="type">Expected resource type</param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public ResourceException(Type type, Exception innerException) : this(null, type, innerException) { }

        /// <summary>Initializes a new instance of the ResourceException class</summary>
        /// <param name="uri">Resource uri</param>
        /// <param name="type">Expected resource type</param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public ResourceException(string uri, Type type, Exception innerException)
            : this("Resource: '{0}'; Type: '{1}'".FormatInvariant(uri, type), uri, type, innerException) { }

        /// <summary>Initializes a new instance of the ResourceException class</summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="uri">Resource uri</param>
        /// <param name="type">Expected resource type</param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public ResourceException(string message, string uri, Type type, Exception innerException)
            : base(message, innerException)
        {
            this.ResourceUri = uri;
            this.ResourceType = type;
        }
    }
}
