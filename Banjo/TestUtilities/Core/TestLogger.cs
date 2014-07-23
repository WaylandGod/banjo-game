//-----------------------------------------------------------------------
// <copyright file="TestLogger.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using Core;

namespace TestUtilities.Core
{
    /// <summary>Placeholder class</summary>
    public class TestLogger : ILogger
    {
        /// <summary>Messages logged</summary>
        private readonly IList<Message> messages;

        /// <summary>Initializes a new instance of the TestLogger class.</summary>
        public TestLogger() : this(Log.Level.All) { }

        /// <summary>Initializes a new instance of the TestLogger class.</summary>
        /// <param name="levels">Levels to log</param>
        public TestLogger(Log.Level levels)
        {
            this.messages = new List<Message>();
            this.Levels = levels;
        }

        /// <summary>Gets the log message levels supported by this logger</summary>
        public Log.Level Levels { get; private set; }

        /// <summary>Gets all messages</summary>
        public IEnumerable<Message> AllMessages
        {
            get { return this.messages; }
        }

        /// <summary>Gets only trace messages</summary>
        public IEnumerable<Message> TraceMessages
        {
            get { return this.GetMessagesForLevel(Log.Level.Trace); }
        }

        /// <summary>Gets only error messages</summary>
        public IEnumerable<Message> InformationMessages
        {
            get { return this.GetMessagesForLevel(Log.Level.Information); }
        }

        /// <summary>Gets only error messages</summary>
        public IEnumerable<Message> WarningMessages
        {
            get { return this.GetMessagesForLevel(Log.Level.Warning); }
        }

        /// <summary>Gets only error messages</summary>
        public IEnumerable<Message> ErrorMessages
        {
            get { return this.GetMessagesForLevel(Log.Level.Error); }
        }

        /// <summary>Initialize logging with a test logger</summary>
        /// <returns>The test logger</returns>
        public static TestLogger InitializeLog()
        {
            var logger = new TestLogger();
            InitializeLog(new[] { logger });
            return logger;
        }

        /// <summary>Initialize logging with provided loggers</summary>
        /// <param name="loggers">Loggers to initialize with</param>
        public static void InitializeLog(IEnumerable<ILogger> loggers)
        {
            Log.Initialize(loggers.ToArray());
        }

        /// <summary>Log a message</summary>
        /// <param name="timestamp">Log timestamp</param>
        /// <param name="level">Log level</param>
        /// <param name="message">Log message</param>
        /// <param name="thread">Source thread</param>
        /// <param name="stackTrace">Stack trace</param>
        public void LogMessage(DateTime timestamp, Log.Level level, string message, string thread, string stackTrace)
        {
            this.messages.Add(new Message { Timestamp = timestamp, Level = level, Text = message, Thread = thread, Stacktrace = stackTrace });
        }

        /// <summary>Gets all messages containing a substring</summary>
        /// <param name="substring">The substring</param>
        /// <returns>All messages containing the substring</returns>
        public IEnumerable<Message> GetMessagesContaining(string substring)
        {
            return this.GetMessagesContaining(substring, false);
        }

        /// <summary>Gets all messages containing a substring</summary>
        /// <param name="substring">The substring</param>
        /// <param name="ignoreCase">Whether to ignore case</param>
        /// <returns>All messages containing the substring</returns>
        public IEnumerable<Message> GetMessagesContaining(string substring, bool ignoreCase)
        {
            return this.messages.Where(msg =>
                (ignoreCase ? msg.Text.ToLowerInvariant() : msg.Text)
                .Contains(ignoreCase ? substring.ToLowerInvariant() : substring));
        }

        /// <summary>Gets all messages logged with a level</summary>
        /// <param name="level">Level of the messages to get</param>
        /// <returns>The messages</returns>
        private IEnumerable<Message> GetMessagesForLevel(Log.Level level)
        {
            return this.messages.Where(msg => msg.Level == level);
        }

        /// <summary>Test logger message</summary>
        public struct Message
        {
            /// <summary>Gets or sets the timestamp</summary>
            public DateTime Timestamp { get; set; }

            /// <summary>Gets or sets the level</summary>
            public Log.Level Level { get; set; }

            /// <summary>Gets or sets the message text</summary>
            public string Text { get; set; }

            /// <summary>Gets or sets the thread</summary>
            public string Thread { get; set; }

            /// <summary>Gets or sets the stack trace</summary>
            public string Stacktrace { get; set; }

            /// <summary>
            /// Returns a <see cref="System.String"/> that represents the current <see cref="TestUtilities.Core.TestLogger+Message"/>.
            /// </summary>
            /// <returns>A <see cref="System.String"/> that represents the current <see cref="TestUtilities.Core.TestLogger+Message"/>.</returns>
            public override string ToString()
            {
                return "[{0:yyyy-MM-dd HH:mm:ss.ffff}] {1} - {2}".FormatInvariant(this.Timestamp, this.Level, this.Text);
            }
        }
    }
}                                                                                                                        