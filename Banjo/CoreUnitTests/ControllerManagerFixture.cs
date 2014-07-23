//-----------------------------------------------------------------------
// <copyright file="ControllerManagerFixture.cs" company="Benjamin Woodall">
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

using Core.Programmability;
using NUnit.Framework;

namespace CoreUnitTests
{
    /// <summary>Text fixture for ControllerManager and Controller classes</summary>
    [TestFixture]
    public class ControllerManagerFixture
    {
        /// <summary>Per-test initialization</summary>
        [SetUp]
        public void TestInitialize()
        {
        }

        /// <summary>Test initializing a ControllerManager</summary>
        [Test]
        public void InitializeControllerManager()
        {
            var manager = new ControllerManager();
            Assert.IsNotNull(manager);
        }
    }
}
