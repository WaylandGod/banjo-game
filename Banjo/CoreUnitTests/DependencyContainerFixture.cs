//-----------------------------------------------------------------------
// <copyright file="DependencyContainerFixture.cs" company="Benjamin Woodall">
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
using System.Linq;
using Core.DependencyInjection;
using NUnit.Framework;

namespace CoreUnitTests
{
    /// <summary>Text fixture for the DependencyContainer</summary>
    [TestFixture]
    public class DependencyContainerFixture
    {
        /// <summary>Random number generator</summary>
        private static readonly Random R = new Random();

        /// <summary>Interface for testing dependency injection</summary>
        private interface ITestTypeA
        {
            /// <summary>Gets A</summary>
            int A { get; }
        }

        /// <summary>Per-test initialization</summary>
        [SetUp]
        public void TestInitialize()
        {
            // Reset the global container
            GlobalContainer.Instance = null;
        }

        /// <summary>
        /// Test correct exception thrown when attempting to resolve a type when none have been registered.
        /// </summary>
        [Test]
        [ExpectedException(typeof(DependencyInjectionException))]
        public void ResolveNoRegisteredTypes()
        {
            var container = new DependencyContainer();
            container.Resolve<ITestTypeA>();
        }

        /// <summary>
        /// Roundtrip test for registering and resolving a type
        /// </summary>
        [Test]
        public void RegisterAndResolveType()
        {
            var container = new DependencyContainer()
                .RegisterType<ITestTypeA, TestTypeA1>();
            var instance = container.Resolve<ITestTypeA>();
            Assert.IsNotNull(instance);
            Assert.IsInstanceOf<TestTypeA1>(instance);
        }

        /// <summary>
        /// Roundtrip test for registering and resolving a singleton type
        /// </summary>
        [Test]
        public void RegisterAndResolveSingletonType()
        {
            var container = new DependencyContainer()
                .RegisterSingleton<ITestTypeA, TestTypeA1>();

            var instanceA = container.Resolve<ITestTypeA>();
            Assert.IsNotNull(instanceA);
            Assert.IsInstanceOf<TestTypeA1>(instanceA);

            var instanceB = container.Resolve<ITestTypeA>();
            Assert.IsNotNull(instanceB);
            Assert.IsInstanceOf<TestTypeA1>(instanceB);

            Assert.AreSame(instanceA, instanceB);
        }

        /// <summary>
        /// Roundtrip test for registering and resolving a non-singleton type
        /// </summary>
        [Test]
        public void RegisterAndResolveNonSingletonType()
        {
            var container = new DependencyContainer()
                .RegisterType<ITestTypeA, TestTypeA1>();

            var instanceA = container.Resolve<ITestTypeA>();
            Assert.IsNotNull(instanceA);
            Assert.IsInstanceOf<TestTypeA1>(instanceA);

            var instanceB = container.Resolve<ITestTypeA>();
            Assert.IsNotNull(instanceB);
            Assert.IsInstanceOf<TestTypeA1>(instanceB);

            Assert.AreNotSame(instanceA, instanceB);
        }

        /// <summary>
        /// Roundtrip test for registering and resolving a singleton instance
        /// </summary>
        [Test]
        public void RegisterAndResolveSingletonInstance()
        {
            var expected = new TestTypeA1();
            var container = new DependencyContainer()
             .RegisterSingleton<ITestTypeA>(expected);

            var instanceA = container.Resolve<ITestTypeA>();
            var instanceB = container.Resolve<ITestTypeA>();
            Assert.AreSame(expected, instanceA);
            Assert.AreSame(expected, instanceB);
        }

        /// <summary>
        /// Roundtrip test for registering and resolving a type with labels
        /// </summary>
        [Test]
        public void RegisterAndResolveTypeWithLabels()
        {
            var container = new DependencyContainer()
                .RegisterType<ITestTypeA, TestTypeA1>("One")
                .RegisterType<ITestTypeA, TestTypeA2>("Two");
            
            var instance1 = container.Resolve<ITestTypeA>("One");
            Assert.IsNotNull(instance1);
            Assert.IsInstanceOf<TestTypeA1>(instance1);

            var instance2 = container.Resolve<ITestTypeA>("Two");
            Assert.IsNotNull(instance2);
            Assert.IsInstanceOf<TestTypeA2>(instance2);
        }

        /// <summary>
        /// Roundtrip test for registering and resolving all implementations of a type
        /// </summary>
        [Test]
        public void RegisterAndResolveAll()
        {
            var container = new DependencyContainer()
                .RegisterType<ITestTypeA, TestTypeA1>("One")
                .RegisterType<ITestTypeA, TestTypeA2>("Two");
            
            var instances = container.ResolveAll<ITestTypeA>();
            Assert.IsNotNull(instances);
            Assert.AreEqual(2, instances.Count());
            Assert.IsTrue(new[] { typeof(TestTypeA1), typeof(TestTypeA2) }
                .All(t => instances.Any(i => i.GetType() == t)));
        }

        /// <summary>Test resolving a type using the global container</summary>
        [Test]
        public void ResolveUsingGlobalContainer()
        {
            new DependencyContainer()
                .RegisterType<ITestTypeA, TestTypeA1>();

            var instance = GlobalContainer.Resolve<ITestTypeA>();
            Assert.IsNotNull(instance);
            Assert.IsInstanceOf<TestTypeA1>(instance);
        }

        /// <summary>
        /// Roundtrip test for registering and resolving all implementations of a type
        /// </summary>
        [Test]
        public void ResolveAllUsingGlobalContainer()
        {
            new DependencyContainer()
                .RegisterType<ITestTypeA, TestTypeA1>("One");
            new DependencyContainer()
                .RegisterType<ITestTypeA, TestTypeA2>("Two");

            var instances = GlobalContainer.ResolveAll<ITestTypeA>();
            Assert.IsNotNull(instances);
            Assert.AreEqual(2, instances.Count());
            Assert.IsTrue(new[] { typeof(TestTypeA1), typeof(TestTypeA2) }
                .All(t => instances.Any(i => i.GetType() == t)));
        }

        /// <summary>
        /// Test realizing an instance with array parameters in its constructor
        /// </summary>
        [Test]
        public void RealizeWithArrayParameterConstructor()
        {
            new DependencyContainer()
                .RegisterType<ITestTypeA, TestTypeA1>("one")
                .RegisterType<ITestTypeA, TestTypeA2>("two")
                .RegisterType<TestTypeB, TestTypeB>();
            
            var instance = GlobalContainer.Resolve<TestTypeB>();
            Assert.IsNotNull(instance);
            Assert.IsNotNull(instance.A1);
            Assert.IsInstanceOf<TestTypeA1>(instance.A1);
            Assert.IsNotNull(instance.As);
            Assert.AreEqual(2, instance.As.Length);
            Assert.IsInstanceOf<TestTypeA1>(instance.As[0]);
            Assert.IsInstanceOf<TestTypeA2>(instance.As[1]);
        }

        private class TestTypeA1 : ITestTypeA
        {
            public int A { get { return R.Next(); } }
        }

        private class TestTypeA2 : ITestTypeA
        {
            public int A { get { return R.Next(); } }
        }

        private class TestTypeB
        {
            public readonly ITestTypeA A1;
            public readonly ITestTypeA[] As;
            public TestTypeB(ITestTypeA testA, ITestTypeA[] testAs)
            {
                this.A1 = testA; this.As = testAs;
            }
        }
    }
}
