//-----------------------------------------------------------------------
// <copyright file="SerializationFixture.cs" company="Benjamin Woodall">
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

using Core.Data;
using NUnit.Framework;

namespace CoreUnitTests
{
    /// <summary>Test fixture for serialization</summary>
    [TestFixture]
    public class SerializationFixture
    {
        /// <summary>Per-test initialization</summary>
        [SetUp]
        public void TestInitialize()
        {
        }

        /// <summary>Test something</summary>
        [Test]
        public void Roundtrip()
        {
            var expected = new TestSerializable { Switch = true, Text = "Hello world", Value = 42f };
            
            var xml = expected.ToString();
            Assert.IsFalse(string.IsNullOrEmpty(xml));

            var actual = TestSerializable.FromString(xml);
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.Switch, actual.Switch);
            Assert.AreEqual(expected.Text, actual.Text);
            Assert.AreEqual(expected.Value, actual.Value);
        }

        /// <summary>Test serializable</summary>
        public class TestSerializable : GenericSerializable<TestSerializable, XmlSerializer<TestSerializable>>
        {
            /// <summary>Gets or sets a value indicating whether true or false</summary>
            public bool Switch { get; set; }

            /// <summary>Gets or sets a text value</summary>
            public string Text { get; set; }

            /// <summary>Gets or sets a numeric value</summary>
            public float Value { get; set; }
        }
    }
}
