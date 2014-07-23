//-----------------------------------------------------------------------
// <copyright file="CoreException.cs" company="Benjamin Woodall">
//  Copyright 2013-2014 Benjamin Woodall
//
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
//
//       http://www.apache.org/licenses/LICENSE-2.0
//
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Diagnostics.CodeAnalysis;

namespace Core
{
    /// <summary>Exception thrown by Banjo framework components</summary>
    [SuppressMessage("Microsoft.Usage", "CA2237", Justification = "Exception serialization is not used.")]
    public class CoreException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the CoreException class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public CoreException(string message) : base(message) { }

        /// <summary>
        /// Initializes a new instance of the CoreException class with a specified error message
        /// and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public CoreException(string message, Exception innerException) : base(message, innerException) { }

        /// <summary>
        /// Prevents a default instance of the CoreException class from being created.
        /// </summary>
        private CoreException() : base() { }
    }
}
