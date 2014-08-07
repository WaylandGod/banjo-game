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
using Game;
using NUnit.Framework;
using TestUtilities;
using TestUtilities.Core;
using UnityEngine;

namespace GameUnitTests
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
        public void Vector3DMultiplyByFloat()
        {
            var vector = Vector3D.Identity * 3.14f;
            Assert.AreEqual(3.14f, vector.X, .001f);
            Assert.AreEqual(3.14f, vector.Y, .001f);
            Assert.AreEqual(3.14f, vector.Z, .001f);
        }

        /// <summary>Test converting from Vector3D to Vector2D</summary>
        [Test]
        public void Vector3DToVector2D()
        {
            var expected = new Vector2D(1, 2);
            var v3 = new Vector3D(1, 2, 3);
            var v2 = (Vector2D)v3;
            Assert.AreEqual(expected, v2);
        }

        /// <summary>Test casting a Game.Vector3D to a UnityEngine.Vector3 and back</summary>
        [Test]
        public void Vector3DToUnityVector()
        {
            const double tolerance = 0.0001;

            var expected = new Vector3D(1.23, 4.567, 8.9);
            var unityVector = (UnityEngine.Vector3)expected;
            var gameVector = (Game.Vector3D)unityVector;

            Assert.AreEqual(expected.X, gameVector.X, tolerance);
            Assert.AreEqual(expected.Y, gameVector.Y, tolerance);
            Assert.AreEqual(expected.Z, gameVector.Z, tolerance);
        }

        /// <summary>Test converting from Vector2D to Vector3D</summary>
        [Test]
        public void Vector2DToVector3D()
        {
            var expected = new Vector3D(3, 2, 0);
            var v2 = new Vector2D(3, 2);
            var v3 = (Vector3D)v2;
            Assert.AreEqual(expected, v3);
        }

        /// <summary>Test getting the magnitude</summary>
        [Test]
        public void Magnitude()
        {
            Assert.AreEqual(0, Vector3D.Zero.Magnitude);
            Assert.AreEqual(10, new Vector3D(10, 0, 0).Magnitude);
            Assert.AreEqual(10, new Vector3D(0, 0, 10).Magnitude);
            Assert.AreEqual(10, new Vector3D(0, 10, 0).Magnitude);

            Assert.AreEqual(
                (new Vector3D(1, 3, 5) - new Vector3D(8, 4, 2)).Magnitude,
                (new Vector3D(8, 4, 2) - new Vector3D(1, 3, 5)).Magnitude);
        }

        /// <summary>Test getting the normalized vector</summary>
        [Test]
        public void NormalizedVector()
        {
            var u = Vector3D.Identity.Normalized;
            Assert.AreEqual(1f, u.Magnitude, 0.00001f);

            var v = new Vector3D(-5, 10, 20);
            Assert.AreEqual(v, v.Normalized * v.Magnitude);
        }
    }
}
