//-----------------------------------------------------------------------
// <copyright file="LoggingFixture.cs" company="Benjamin Woodall">
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
using System.Linq;
using System.Threading;
using Core;
using NUnit.Framework;
using Rhino.Mocks;
using TestUtilities.Core;

namespace CoreUnitTests
{
    /// <summary>Test fixture for serialization</summary>
    [TestFixture]
    public class LoggingFixture
    {
        /// <summary>Test logger</summary>
        private TestLogger testLogger;

        /// <summary>Mock logger</summary>
        private ILogger mockLogger;

        /// <summary>Per-test initialization</summary>
        [SetUp]
        public void TestInitialize()
        {
            this.InitializeLog(Log.Level.All);
        }

        /// <summary>Simple trace log message smoke test</summary>
        [Test]
        public void LogMessageSmokeTest()
        {
            var message = Guid.NewGuid().ToString();
            Log.Trace(message);

            Assert.AreEqual(1, this.testLogger.AllMessages.Count());
            Assert.AreEqual(1, this.testLogger.TraceMessages.Count());
            Assert.AreEqual(0, this.testLogger.InformationMessages.Count());
            Assert.AreEqual(0, this.testLogger.WarningMessages.Count());
            Assert.AreEqual(0, this.testLogger.ErrorMessages.Count());

            this.mockLogger.AssertWasCalled(f => f.LogMessage(
                Arg<DateTime>.Is.Anything,
                Arg<Log.Level>.Is.Equal(Log.Level.Trace),
                Arg<string>.Is.Equal(message),
                Arg<string>.Is.Anything,
                Arg<string>.Is.Anything));
        }

        /// <summary>Test logging messages asynchronously</summary>
        [Test]
        public void LogAsync()
        {
            var messages = Enumerable.Range(0, 10).Select(i => Guid.NewGuid().ToString()).ToArray();
            var asyncLogger = new AsyncLoggerWrapper(this.mockLogger);
            Log.Initialize(new ILogger[] { asyncLogger, this.testLogger });
            foreach (var message in messages)
            {
                Log.Trace(message);
            }

            // Verify background thread logs messages
            Assert.AreNotEqual(0, asyncLogger.Messages.Count());
            Thread.Sleep(2000);
            Assert.AreEqual(0, asyncLogger.Messages.Count());

            // Verify mock logger called for all messages logged
            foreach (var message in messages)
            {
                this.mockLogger.AssertWasCalled(f => f.LogMessage(
                    Arg<DateTime>.Is.Anything,
                    Arg<Log.Level>.Is.Equal(Log.Level.Trace),
                    Arg<string>.Is.Equal(message),
                    Arg<string>.Is.Anything,
                    Arg<string>.Is.Anything));
            }

            // Verify messages where logged in the correct order
            var logged = this.testLogger.AllMessages.ToArray();
            for (var i = 0; i < logged.Length; i++)
            {
                Assert.IsTrue(logged[i].Text.Contains(messages[i]));
            }
        }

        /// <summary>Test messages are only sent to loggers with the correct level</summary>
        [Test]
        public void LogLevelsTest()
        {
            var message = Guid.NewGuid().ToString();
            this.InitializeLog(Log.Level.NoTrace);

            Log.Trace(message);
            Log.Information(message);
            Log.Warning(message);
            Log.Error(message);

            Assert.AreEqual(4, this.testLogger.AllMessages.Count());
            Assert.AreEqual(1, this.testLogger.TraceMessages.Count());
            Assert.AreEqual(1, this.testLogger.InformationMessages.Count());
            Assert.AreEqual(1, this.testLogger.WarningMessages.Count());
            Assert.AreEqual(1, this.testLogger.ErrorMessages.Count());

            this.mockLogger.AssertWasNotCalled(f => f.LogMessage(
                Arg<DateTime>.Is.Anything,
                Arg<Log.Level>.Is.Equal(Log.Level.Trace),
                Arg<string>.Is.Equal(message),
                Arg<string>.Is.Anything,
                Arg<string>.Is.Anything));
            this.mockLogger.AssertWasCalled(f => f.LogMessage(
                Arg<DateTime>.Is.Anything,
                Arg<Log.Level>.Is.Equal(Log.Level.Information),
                Arg<string>.Is.Equal(message),
                Arg<string>.Is.Anything,
                Arg<string>.Is.Anything));
            this.mockLogger.AssertWasCalled(f => f.LogMessage(
                Arg<DateTime>.Is.Anything,
                Arg<Log.Level>.Is.Equal(Log.Level.Warning),
                Arg<string>.Is.Equal(message),
                Arg<string>.Is.Anything,
                Arg<string>.Is.Anything));
            this.mockLogger.AssertWasCalled(f => f.LogMessage(
                Arg<DateTime>.Is.Anything,
                Arg<Log.Level>.Is.Equal(Log.Level.Error),
                Arg<string>.Is.Equal(message),
                Arg<string>.Is.Anything,
                Arg<string>.Is.Anything));
        }

        /// <summary>Generates the mock logger and initializes logging</summary>
        /// <param name="mockLoggerLevels">Levels for the mock logger</param>
        private void InitializeLog(Log.Level mockLoggerLevels)
        {
            this.testLogger = new TestLogger();
            this.mockLogger = MockRepository.GenerateMock<ILogger>();
            this.mockLogger.Stub(f => f.Levels).Return(mockLoggerLevels);
            Log.Initialize(new[] { this.mockLogger, this.testLogger });
        }
    }
}
