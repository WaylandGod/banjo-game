//-----------------------------------------------------------------------
// <copyright file="UnknownResourceSchemeException.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;

namespace Core.Resources.Management
{
    /// <summary>
    /// Exception thrown by resource framework components when an unknown resource type is encountered
    /// </summary>
    public class UnknownResourceSchemeException : ResourceException
    {
        /// <summary>Initializes a new instance of the UnknownResourceSchemeException class</summary>
        /// <param name="uri">Resource uri</param>
        public UnknownResourceSchemeException(string uri) : this(uri, null) { }

        /// <summary>Initializes a new instance of the UnknownResourceSchemeException class</summary>
        /// <param name="uri">Resource uri</param>
        /// <param name="type">Expected resource type</param>
        public UnknownResourceSchemeException(string uri, Type type)
            : base("Unknown Resource Scheme: '{0}'".FormatInvariant(uri), uri, type) { }
    }
}
