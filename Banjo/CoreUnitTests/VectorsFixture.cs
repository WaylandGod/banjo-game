//-----------------------------------------------------------------------
// <copyright file="VectorsFixture.cs" company="Benjamin Woodall">
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
using Core;
using NUnit.Framework;
using TestUtilities;
using TestUtilities.Core;

namespace CoreUnitTests
{
    /// <summary>Test fixture for vector classes</summary>
    [TestFixture]
    public class VectorsFixture
    {
        /// <summary>Per-test initialization</summary>
        [SetUp]
        public void TestInitialize()
        {
        }

        /// <summary>Test multiplying a vector by a float</summary>
        [Test]
        public void Vector3MultiplyByFloat()
        {
            var vector = Vector3.Identity * 3.14f;
            Assert.AreEqual(3.14f, vector.X, .001f);
            Assert.AreEqual(3.14f, vector.Y, .001f);
            Assert.AreEqual(3.14f, vector.Z, .001f);
        }

        /// <summary>Test converting from Vector3 to Vector2</summary>
        [Test]
        public void Vector3ToVector2()
        {
            var expected = new Vector2(1, 2);
            var v3 = new Vector3(1, 2, 3);
            var v2 = (Vector2)v3;
            Assert.AreEqual(expected, v2);
        }

        /// <summary>Test converting from Vector2 to Vector3</summary>
        [Test]
        public void Vector2ToVector3()
        {
            var expected = new Vector3(3, 2, 0);
            var v2 = new Vector2(3, 2);
            var v3 = (Vector3)v2;
            Assert.AreEqual(expected, v3);
        }

        /// <summary>Test getting a bearing from the relative position of two vectors</summary>
        [Test]
        public void BearingFromRelativePosition()
        {
            const float Expected = 45f;
            var observer = new Vector3(1, 0, 10);
            var subject = new Vector3(6, 0, 15);
            Assert.AreEqual(Expected, (subject - observer).AngleXZ);
            Assert.AreEqual(Expected - 180, (observer - subject).AngleXZ);
        }

        /// <summary>Test getting the magnitude</summary>
        [Test]
        public void Magnitude()
        {
            Assert.AreEqual(0, Vector3.Zero.Magnitude);
            Assert.AreEqual(10, new Vector3(10, 0, 0).Magnitude);
            Assert.AreEqual(10, new Vector3(0, 0, 10).Magnitude);
            Assert.AreEqual(10, new Vector3(0, 10, 0).Magnitude);

            Assert.AreEqual(
                (new Vector3(1, 3, 5) - new Vector3(8, 4, 2)).Magnitude,
                (new Vector3(8, 4, 2) - new Vector3(1, 3, 5)).Magnitude);
        }

        /// <summary>Test getting the unit vector</summary>
        [Test]
        public void UnitVector()
        {
            var u = Vector3.Identity.UnitVector;
            Assert.AreEqual(1, u.Magnitude);

            var v = new Vector3(-5, 10, 20);
            Assert.AreEqual(v, v.UnitVector * v.Magnitude);
        }
    }
}
