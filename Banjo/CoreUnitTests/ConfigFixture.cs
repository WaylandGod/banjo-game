//-----------------------------------------------------------------------
// <copyright file="ConfigFixture.cs" company="Benjamin Woodall">
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

namespace CoreUnitTests
{
    using System;
    using System.Collections.Generic;
    using Core;
    using NUnit.Framework;

    /// <summary>Test fixture for Config</summary>
    [TestFixture]
    public class ConfigFixture
    {
        /// <summary>Test value name</summary>
        private string valueName;

        /// <summary>Test enumeration</summary>
        private enum TestEnum
        {
            /// <summary>The value a.</summary>
            ValueA,
            
            /// <summary>The value b.</summary>
            ValueB,

            /// <summary>The value c.</summary>
            ValueC
        }

        /// <summary>Gets an empty config</summary>
        private static IConfig EmptyConfig
        {
            get { return new DictionaryConfig(new Dictionary<string, string>()); }
        }

        /// <summary>Per-test initialization</summary>
        [SetUp]
        public void TestInitialize()
        {
            this.valueName = Guid.NewGuid().ToString("N");
        }

        /// <summary>Test getting a raw value</summary>
        [Test]
        public void GetDefaultIntValue()
        {
            var defaultvalue = 24;
            var config = this.CreateConfigWithValues("foo", "42");
            var value = config.GetValue<int>("bar", defaultvalue);
            Assert.AreEqual(defaultvalue, value);
        }

        /// <summary>Test getting a raw value</summary>
        [Test]
        public void GetDefaultRawValue()
        {
            var defaultValue = "24";
            var config = this.CreateConfigWithValues("foo", "42");
            var value = config.GetValue("bar", defaultValue);
            Assert.AreEqual(defaultValue, value);
        }

        /// <summary>Test getting a default</summary>
        [Test]
        public void GetRawValue()
        {
            var expected = Guid.NewGuid().ToString();
            var config = this.CreateConfigWithValues(this.valueName, expected);
            var value = config.GetValue(this.valueName);
            Assert.AreEqual(expected, value);
        }

        /// <summary>Test getting a text value</summary>
        [Test]
        public void GetStringValue()
        {
            this.TestGetValueHelper(Guid.NewGuid().ToString());
            this.TestGetValueHelper(string.Empty);
        }

        /// <summary>Test getting a short value</summary>
        [Test]
        public void GetShortValue()
        {
            this.TestGetValueHelper(short.MaxValue);
            this.TestGetValueHelper(short.MinValue);
        }

        /// <summary>Test getting an integer value</summary>
        [Test]
        public void GetIntegerValue()
        {
            this.TestGetValueHelper(int.MaxValue);
            this.TestGetValueHelper(int.MinValue);
        }

        /// <summary>Test getting a long value</summary>
        [Test]
        public void GetLongValue()
        {
            this.TestGetValueHelper(long.MaxValue);
            this.TestGetValueHelper(long.MinValue);
        }

        /// <summary>Test getting a double value</summary>
        [Test]
        public void GetDoubleValue()
        {
            this.TestGetValueHelper(0.0);
            this.TestGetValueHelper(0.000001);
            this.TestGetValueHelper(0.999999);
            this.TestGetValueHelper(3.14159265359);
            this.TestGetValueHelper(31415926535.9);
        }

        /// <summary>Test getting an enum value</summary>
        [Test]
        public void GetEnumValue()
        {
            this.TestGetValueHelper(TestEnum.ValueA);
            this.TestGetValueHelper(TestEnum.ValueB);
            this.TestGetValueHelper(TestEnum.ValueC);
        }

        /// <summary>Test getting a raw value that does not exist</summary>
        [Test]
        public void GetNonexistentRawValue()
        {
            var value = EmptyConfig.GetValue(this.valueName);
            Assert.IsNull(value);
        }

        /// <summary>Test getting a text value that does not exist</summary>
        [Test]
        public void GetNonexistentStringValue()
        {
            var value = EmptyConfig.GetValue<string>(this.valueName);
            Assert.IsNull(value);
        }

        /// <summary>Test getting a short value that does not exist</summary>
        [Test]
        public void GetNonexistentShortValue()
        {
            this.TestDefaultGetValueHelper<short>();
        }

        /// <summary>Test getting an integer value that does not exist</summary>
        [Test]
        public void GetNonexistentIntegerValue()
        {
            this.TestDefaultGetValueHelper<int>();
        }

        /// <summary>Test getting a long value that does not exist</summary>
        [Test]
        public void GetNonexistentLongValue()
        {
            this.TestDefaultGetValueHelper<long>();
        }

        /// <summary>Test getting a double value that does not exist</summary>
        [Test]
        public void GetNonexistentDoubleValue()
        {
            this.TestDefaultGetValueHelper<double>();
        }

        /// <summary>Test getting an enum value that does not exist</summary>
        [Test]
        public void GetNonexistentEnumValue()
        {
            this.TestDefaultGetValueHelper<TestEnum>();
        }

        /// <summary>Helper for testing generic GetValue expecting type's default value</summary>
        /// <typeparam name="TValue">Value type</typeparam>
        /// <param name="config">Config being tested</param>
        /// <param name="name">Value name</param>
        private static void TestDefaultGetValueHelper<TValue>(IConfig config, string name)
        {
            TestGetValueHelper<TValue>(config, name, default(TValue));
        }

        /// <summary>Helper for testing generic GetValue</summary>
        /// <typeparam name="TValue">Value type</typeparam>
        /// <param name="config">Config being tested</param>
        /// <param name="name">Value name</param>
        /// <param name="expected">Expected value</param>
        private static void TestGetValueHelper<TValue>(IConfig config, string name, TValue expected)
        {
            var value = config.GetValue<TValue>(name);
            Assert.AreEqual(value, expected);
        }

        /// <summary>Helper for testing generic GetValue expecting type's default value</summary>
        /// <typeparam name="TValue">Value type</typeparam>
        private void TestDefaultGetValueHelper<TValue>()
        {
            TestGetValueHelper<TValue>(EmptyConfig, this.valueName, default(TValue));
        }

        /// <summary>Helper for testing generic GetValue</summary>
        /// <typeparam name="TValue">Value type</typeparam>
        /// <param name="expected">Expected value</param>
        private void TestGetValueHelper<TValue>(TValue expected)
        {
            var config = this.CreateConfigWithValues(
                this.valueName, expected.ToString());
            var value = config.GetValue<TValue>(this.valueName);
            Assert.AreEqual(expected, value);
        }

        /// <summary>
        /// Creates a test config containing the specified names and values
        /// </summary>
        /// <param name="namesAndValues">Config names and values</param>
        /// <returns>The test config</returns>
        /// <exception cref="ArgumentException">If the number of strings is not even</exception>
        private IConfig CreateConfigWithValues(params string[] namesAndValues)
        {
            if (namesAndValues.Length % 2 != 0)
            {
                throw new ArgumentException("namesAndValues");
            }

            var values = new Dictionary<string, string>();
            var i = 0;
            while (i < namesAndValues.Length)
            {
                values.Add(namesAndValues[i++], namesAndValues[i++]);
            }

            return new DictionaryConfig(values);
        }
    }
}
