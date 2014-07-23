//-----------------------------------------------------------------------
// <copyright file="UnknownResourceTypeException.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;

namespace Core.Resources.Management
{
    /// <summary>
    /// Exception thrown by resource framework components when an unknown resource type is encountered
    /// </summary>
    public class UnknownResourceTypeException : ResourceException
    {
        /// <summary>Initializes a new instance of the UnknownResourceTypeException class</summary>
        /// <param name="type">Expected resource type</param>
        public UnknownResourceTypeException(Type type) : this(null, type) { }

        /// <summary>Initializes a new instance of the UnknownResourceTypeException class</summary>
        /// <param name="uri">Resource uri</param>
        /// <param name="type">Expected resource type</param>
        public UnknownResourceTypeException(string uri, Type type)
            : base("Unknown Resource Type: '{0}'".FormatInvariant(type), uri, type) { }
    }
}
