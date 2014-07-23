//-----------------------------------------------------------------------
// <copyright file="Log.cs" company="Benjamin Woodall">
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
using System.Linq;
using System.Threading;
using Core.DependencyInjection;

namespace Core
{
    /// <summary>Global log manager</summary>
    public static class Log
    {
        /// <summary>Backing field for Loggers</summary>
        private static ILogger[] loggers;

        /// <summary>Initializes static members of the Log class</summary>
        static Log()
        {
            Log.GetStackTrace = () => new System.Diagnostics.StackTrace(3, true).ToString();
        }

        /// <summary>Log message levels</summary>
        public enum Level : byte
        {
            /// <summary>No messages</summary>
            None = 0,

            /// <summary>Trace level messages</summary>
            Trace = 1,

            /// <summary>Information level messages</summary>
            Information = 2,

            /// <summary>Warning level messages</summary>
            Warning = 4,

            /// <summary>Error level messages</summary>
            Error = 8,

            /// <summary>All level messages</summary>
            All = Trace | Information | Warning | Error,

            /// <summary>All level messages except Trace</summary>
            NoTrace = Information | Warning | Error,
        }

        /// <summary>Gets or sets the action for getting stack traces</summary>
        public static Func<string> GetStackTrace { get; set; }

        /// <summary>Gets the loggers used to log messages</summary>
        private static ILogger[] Loggers
        {
            get
            {
                return loggers =
                    (loggers == null || loggers.Length == 0) ?
                    GlobalContainer.ResolveAll<ILogger>().ToArray() : loggers;
            }
        }

        /// <summary>Logs a trace message</summary>
        /// <param name="format">Message format</param>
        /// <param name="args">Message parameters</param>
#if !TRACE
        [SuppressMessage("Microsoft.Usage", "CA1801", Justification = "Tracing disabled")]
#endif
        public static void Trace(string format, params object[] args)
        {
#if TRACE
            LogMessage(Level.Trace, format, args);
#endif
        }

        /// <summary>Logs an information message</summary>
        /// <param name="format">Message format</param>
        /// <param name="args">Message parameters</param>
        public static void Information(string format, params object[] args)
        {
            LogMessage(Level.Information, format, args);
        }

        /// <summary>Logs a warning message</summary>
        /// <param name="format">Message format</param>
        /// <param name="args">Message parameters</param>
        public static void Warning(string format, params object[] args)
        {
            LogMessage(Level.Warning, format, args);
        }

        /// <summary>Logs an error message</summary>
        /// <param name="format">Message format</param>
        /// <param name="args">Message parameters</param>
        public static void Error(string format, params object[] args)
        {
            LogMessage(Level.Error, format, args);
        }

        /// <summary>Initializes loggers (for testing)</summary>
        /// <param name="loggers">The loggers</param>
        internal static void Initialize(ILogger[] loggers)
        {
            Log.loggers = loggers;
        }

        /// <summary>Logs a message</summary>
        /// <param name="level">Log level</param>
        /// <param name="format">Message format</param>
        /// <param name="args">Message parameters</param>
        ////[SuppressMessage("", "", Justification = "Exception is logged")]
        private static void LogMessage(Level level, string format, params object[] args)
        {
            var timestamp = DateTime.Now;
            var thread = "{0} '{1}'".FormatInvariant(Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.Name);
            var message = string.Empty;
            var stackTrace = Log.GetStackTrace != null ? Log.GetStackTrace() : string.Empty;
            try
            {
                message = format.FormatInvariant(args);
            }
            catch (Exception e)
            {
                message = "Error formatting message: '{0}'\n{1}".FormatInvariant(format, e);
            }

            foreach (var logger in Loggers.Where(logger => (logger.Levels & level) != 0))
            {
                try
                {
                    logger.LogMessage(timestamp, level, message, thread, stackTrace);
                }
                catch
                {
                }
            }
        }
    }
}
