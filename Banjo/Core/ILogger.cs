//-----------------------------------------------------------------------
// <copyright file="ILogger.cs" company="Benjamin Woodall">
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

namespace Core
{
    /// <summary>Interface for loggers used by Core.Log</summary>
    public interface ILogger
    {
        /// <summary>Gets the log message levels supported by this logger</summary>
        Log.Level Levels { get; }

        /// <summary>Log a message</summary>
        /// <param name="timestamp">Log timestamp</param>
        /// <param name="level">Log level</param>
        /// <param name="message">Log message</param>
        /// <param name="thread">Source thread</param>
        /// <param name="stackTrace">Stack trace</param>
        void LogMessage(DateTime timestamp, Log.Level level, string message, string thread, string stackTrace);
    }
}
