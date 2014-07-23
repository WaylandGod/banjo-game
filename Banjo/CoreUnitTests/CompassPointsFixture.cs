//-----------------------------------------------------------------------
// <copyright file="CompassPointsFixture.cs" company="Benjamin Woodall">
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

using Core;
using NUnit.Framework;

namespace CoreUnitTests
{
    /// <summary>Test fixture for vector classes</summary>
    [TestFixture]
    public class CompassPointsFixture
    {
        /// <summary>Per-test initialization</summary>
        [SetUp]
        public void TestInitialize()
        {
        }

        /// <summary>Test getting the angle of a compass point</summary>
        [Test]
        public void AngleFromCompassPoint()
        {
            var expected = CoreExtensions.CompassPointAngles[CompassPoints.Southeast];
            Assert.AreEqual(expected, CompassPoints.Southeast.ToAngle());
        }

        /// <summary>Test getting the compass point of an angle</summary>
        [Test]
        public void CompassPointFromAngle()
        {
            Assert.AreEqual(CompassPoints.West, (315f - 22.5f).ToCompassPoint());
            Assert.AreEqual(CompassPoints.Northwest, (315f - 22.49f).ToCompassPoint());
            Assert.AreEqual(CompassPoints.Northwest, 315f.ToCompassPoint());
            Assert.AreEqual(CompassPoints.Northwest, (315f + 22.5f).ToCompassPoint());
            Assert.AreEqual(CompassPoints.North, (315f + 22.51f).ToCompassPoint());
        }

        /// <summary>Test getting the compass point of an angle</summary>
        [Test]
        public void NorthWrap()
        {
            var angle = CoreExtensions.CompassPointAngles[CompassPoints.Northwest] + 22.5f;
            for (float f = 0.01f; f < 45f; f += 0.01f)
            {
                Assert.AreEqual(CompassPoints.North, (angle + f).ToCompassPoint());
            }
        }
    }
}
