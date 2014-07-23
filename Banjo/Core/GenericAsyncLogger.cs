//-----------------------------------------------------------------------
// <copyright file="GenericAsyncLogger.cs" company="Benjamin Woodall">
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
using System.Collections.Generic;
using System.Threading;

namespace Core
{
    /// <summary>Generic, asynchronous wrapper for loggers</summary>
    /// <typeparam name="TLogger">Type of the internal logger</typeparam>
    public sealed class GenericAsyncLogger<TLogger> : AsyncLoggerWrapper, ILogger, IDisposable
        where TLogger : class, ILogger, new()
    {
        /// <summary>Initializes a new instance of the GenericAsyncLogger class</summary>
        public GenericAsyncLogger() : base(new TLogger()) { }

        /// <summary>Initializes a new instance of the GenericAsyncLogger class</summary>
        /// <param name="logger">Logger to be wrapped</param>
        /// <param name="wait">Time to wait between checks for messages (in milliseconds)</param>
        public GenericAsyncLogger(TLogger logger, int wait) : base(logger, wait) { }
    }
}
