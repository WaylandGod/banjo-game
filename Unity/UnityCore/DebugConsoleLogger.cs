//-----------------------------------------------------------------------
// <copyright file="DebugConsoleLogger.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using Core;
using UnityEngine;

namespace Core.Unity
{
    /// <summary>Logger using the Unity debug console</summary>
    internal class DebugConsoleLogger : ILogger
    {
        /// <summary>Format for debug console log messages</summary>
        private const string LogFormat = "[{0:yyyy-MM-dd HH:mm:ss.ffff}] {1} - {2}\nThread: {3}\nStack:\n{4}";

        /// <summary>Gets the log message levels supported by this logger</summary>
        public Log.Level Levels { get { return Log.Level.All; } }

        /// <summary>Log a message</summary>
        /// <param name="timestamp">Log timestamp</param>
        /// <param name="level">Log level</param>
        /// <param name="message">Log message</param>
        /// <param name="thread">Source thread</param>
        /// <param name="stackTrace">Stack trace</param>
        public void LogMessage(DateTime timestamp, Log.Level level, string message, string thread, string stackTrace)
        {
            var logMessage = string.Empty;
            try
            {
                logMessage = LogFormat.FormatInvariant(DateTime.Now, level, message, thread, stackTrace);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                logMessage = "Error formatting log message: \"" + message + "\"";
            }

            if (level == Log.Level.Error)
            {
                Debug.LogError(logMessage);
            }
            else if (level == Log.Level.Warning)
            {
                Debug.LogWarning(logMessage);
            }
            else
            {
                Debug.Log(logMessage);
            }
        }
    }
}
