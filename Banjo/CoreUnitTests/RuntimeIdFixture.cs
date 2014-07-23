//-----------------------------------------------------------------------
// <copyright file="RuntimeIdFixture.cs" company="Benjamin Woodall">
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
using Core;
using Core.Resources;
using NUnit.Framework;
using Rhino.Mocks;

namespace CoreUnitTests
{
    /// <summary>Test fixture for RuntimeId</summary>
    [TestFixture]
    public class RuntimeIdFixture
    {
        /// <summary>Test resource</summary>
        private IResource testResource;

        /// <summary>Test resource identifier</summary>
        private string testResourceId;

        /// <summary>Per-test initialization</summary>
        [SetUp]
        public void TestInitialize()
        {
            this.testResourceId = Guid.NewGuid().ToString("n");
            this.testResource = MockRepository.GenerateMock<IResource>();
            this.testResource.Stub(f => f.Id).Return(this.testResourceId);
        }

        /// <summary>Test RuntimeIds for the same resource are unique</summary>
        [Test]
        public void RuntimeIdsAreUnique()
        {
            var ridA = new RuntimeId(this.testResource);
            var ridB = new RuntimeId(this.testResource);
            Assert.AreNotEqual(ridA, ridB);
        }

        /// <summary>Test that equal RuntimeIds are not the same instance</summary>
        [Test]
        public void RuntimeIdsAreByValue()
        {
            var ridA = new RuntimeId(this.testResource);
            var ridB = ridA;
            Assert.AreEqual(ridA, ridB);
            Assert.AreNotSame(ridA, ridB);
        }

        /// <summary>Test that RuntiemIds contain correct information regarding their resource</summary>
        [Test]
        public void RuntimeIdFromResource()
        {
            var rid = new RuntimeId(this.testResource);
            Assert.AreEqual(this.testResourceId, rid.ResourceId);
            Assert.AreEqual(this.testResource.GetType().FullName, rid.TypeName);
        }

        /// <summary>Test that RuntimeId strings contain the expected substrings</summary>
        [Test]
        public void RuntimeIdAsString()
        {
            var rid = new RuntimeId(this.testResource);
            Assert.AreEqual(rid.AsString, rid.ToString());
            Assert.IsTrue(rid.AsString.Contains(rid.Guid.ToString("N")));
            Assert.IsTrue(rid.AsString.Contains(rid.TypeName));
            Assert.IsTrue(rid.AsString.Contains(this.testResource.GetType().FullName));
            Assert.IsTrue(rid.AsString.Contains(rid.ResourceId));
            Assert.IsTrue(rid.AsString.Contains(this.testResource.Id));
        }

        /// <summary>Test creating a RuntimeId from a string (takes the caller for its type)</summary>
        [Test]
        public void RuntimeIdFromString()
        {
            var rid = new RuntimeId(this.testResourceId);
            Assert.AreEqual(this.testResourceId, rid.ResourceId);
            Assert.AreEqual(this.GetType().FullName, rid.TypeName);
        }
    }
}
