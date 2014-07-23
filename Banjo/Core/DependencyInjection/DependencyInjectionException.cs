//-----------------------------------------------------------------------
// <copyright file="DependencyInjectionException.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;

namespace Core.DependencyInjection
{
    /// <summary>Exception for Dependency Injection related errors</summary>
    [Serializable]
    public class DependencyInjectionException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the DependencyInjectionException class
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="args">Optional message arguments</param>
        public DependencyInjectionException(string message, params object[] args)
            : base(message.FormatInvariant(args))
        {
        }
    }
}
