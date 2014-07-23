//-----------------------------------------------------------------------
// <copyright file="AsyncLoggerWrapper.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading;

namespace Core
{
    /// <summary>Asynchronous wrapper for loggers</summary>
    public class AsyncLoggerWrapper : ILogger, IDisposable
    {
        /// <summary>Default time to wait between checks for async messages (in milliseconds)</summary>
        private const int DefaultWait = 10;

        /// <summary>How long to wait between checks for async messages (in milliseconds)</summary>
        private readonly int wait;

        /// <summary>Internal logger used to log the messages</summary>
        private ILogger logger;

        /// <summary>Thread in which messages are logged</summary>
        private Thread pump;

        /// <summary>Initializes a new instance of the AsyncLoggerWrapper class</summary>
        /// <param name="logger">Logger to be wrapped</param>
        public AsyncLoggerWrapper(ILogger logger) : this(logger, DefaultWait) { }

        /// <summary>Initializes a new instance of the AsyncLoggerWrapper class</summary>
        /// <param name="logger">Logger to be wrapped</param>
        /// <param name="wait">Time to wait between checks for messages (in milliseconds)</param>
        public AsyncLoggerWrapper(ILogger logger, int wait)
        {
            this.Messages = new Stack<AsyncMessage>();
            this.logger = logger;
            this.wait = wait;
            (this.pump = new Thread(this.PumpProc)).Start();
        }

        /// <summary>Gets the log message levels supported by this logger</summary>
        public Log.Level Levels { get { return this.logger.Levels; } }

        /// <summary>Gets the stack of messages waiting to be logged</summary>
        internal Stack<AsyncMessage> Messages { get; private set; }

        /// <summary>Log a message</summary>
        /// <param name="timestamp">Log timestamp</param>
        /// <param name="level">Log level</param>
        /// <param name="message">Log message</param>
        /// <param name="thread">Source thread</param>
        /// <param name="stackTrace">Stack trace</param>
        public void LogMessage(DateTime timestamp, Log.Level level, string message, string thread, string stackTrace)
        {
            this.Messages.Push(new AsyncMessage
            {
                Timestamp = timestamp,
                Level = level,
                Message = message,
                Thread = thread,
                Stacktrace = stackTrace,
            });
        }

        /// <summary>Dispose of native/managed resources</summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>Dispose of native/managed resources</summary>
        /// <param name="disposing">Whether the object is being disposed</param>
        protected virtual void Dispose(bool disposing)
        {
            if (this.pump != null)
            {
                this.pump.Abort();
                this.pump = null;
            }

            if (this.logger != null)
            {
                if (this.logger is IDisposable)
                {
                    ((IDisposable)this.logger).Dispose();
                }

                this.logger = null;
            }
        }

        /// <summary>Pump process</summary>
        private void PumpProc()
        {
            while (true)
            {
                while (this.Messages.Count > 0)
                {
                    var message = this.Messages.Pop();
                    this.logger.LogMessage(
                        message.Timestamp,
                        message.Level,
                        message.Message,
                        message.Thread,
                        message.Stacktrace);
                }

                Thread.Sleep(this.wait);
            }
        }

        /// <summary>Async log message</summary>
        internal struct AsyncMessage
        {
            /// <summary>Message timestamp</summary>
            public DateTime Timestamp;

            /// <summary>Message level</summary>
            public Log.Level Level;

            /// <summary>Message content</summary>
            public string Message;

            /// <summary>Message source thread</summary>
            public string Thread;

            /// <summary>Message stack trace</summary>
            public string Stacktrace;
        }
    }
}
