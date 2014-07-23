//-----------------------------------------------------------------------
// <copyright file="ResourceManagerFixture.cs" company="Benjamin Woodall">
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
using Core.Resources;
using Core.Resources.Management;
using NUnit.Framework;
using TestUtilities.Core;

namespace CoreUnitTests
{
    /// <summary>Test fixture for ResourceManager</summary>
    [TestFixture]
    public class ResourceManagerFixture
    {
        /// <summary>Test value name</summary>
        private string resourceUri;

        /// <summary>Per-test initialization</summary>
        [SetUp]
        public void TestInitialize()
        {
            this.resourceUri = "{0}:{1}.{2}".FormatInvariant(ResourcesHelper.TestScheme, Guid.NewGuid().ToString("n"), ResourcesHelper.TestExtensions[0]);
        }

        /// <summary>Test trying to load a resource when no loaders have been registered</summary>
        [Test]
#if DEBUG
        [ExpectedException(typeof(UnknownResourceSchemeException))]
#else
        //// Preemptive error checking disabled in Release
        [ExpectedException(typeof(NullReferenceException))]
#endif
        public void NoLoaders()
        {
            var manager = new ResourceManagerInternal();
            manager.LoadResource<IResource>(this.resourceUri);
        }

        /// <summary>Test trying to load a resource when no loaders for that type have been registered</summary>
        [Test]
#if DEBUG
        [ExpectedException(typeof(UnknownResourceTypeException))]
#else
        //// Preemptive error checking disabled in Release
        [ExpectedException(typeof(NullReferenceException))]
#endif
        public void NoLoaderForType()
        {
            var manager = new ResourceManagerInternal();
            manager.RegisterResourceLoader(ResourcesHelper.TestResourceLoader<IDictionary<string, float>>(new Dictionary<string, float>()));
            manager.LoadResource<GenericNativeResource<System.IO.BufferedStream>>(this.resourceUri);
        }

        /// <summary>Test trying to load a non-existent resource</summary>
        /// <remarks>
        /// Resource loaders return null when no resource found,
        /// which gets translated to ResourceNotFoundException.
        /// </remarks>
        [Test]
        [ExpectedException(typeof(ResourceNotFoundException))]
        public void ResourceNotFound()
        {
            var manager = new ResourceManagerInternal();
            manager.RegisterResourceLoader(ResourcesHelper.TestResourceLoader<string>(id => null));
            manager.LoadResource<GenericNativeResource<string>>(this.resourceUri);
        }

        /// <summary>Test trying to load a resource when the loader blows up</summary>
        [Test]
        [ExpectedException(typeof(InvalidResourceException))]
        public void InvalidResource()
        {
            var manager = new ResourceManagerInternal();
            manager.RegisterResourceLoader(ResourcesHelper.TestResourceLoader<string>(id => { throw new Exception(); }));
            manager.LoadResource<GenericNativeResource<string>>(this.resourceUri);
        }

        /// <summary>Test trying to load a resource using a null identifier</summary>
        [Test]
#if DEBUG
        [ExpectedException(typeof(ArgumentNullException))]
#else
        //// Preemptive error checking disabled in Release
        [ExpectedException(typeof(NullReferenceException))]
#endif
        public void NullResourceIdentifier()
        {
            var manager = new ResourceManagerInternal();
            manager.RegisterResourceLoader(ResourcesHelper.TestResourceLoader<string>(string.Empty));
            manager.LoadResource<GenericNativeResource<string>>(null);
        }

        /// <summary>Test trying to load a resource using an empty identifier</summary>
        [Test]
#if DEBUG
        [ExpectedException(typeof(ArgumentException))]
#else
        //// Preemptive error checking disabled in Release
        [ExpectedException(typeof(IndexOutOfRangeException))]
#endif
        public void EmptyResourceIdentifier()
        {
            var manager = new ResourceManagerInternal();
            manager.RegisterResourceLoader(ResourcesHelper.TestResourceLoader<string>(string.Empty));
            manager.LoadResource<GenericNativeResource<string>>(string.Empty);
        }

        /// <summary>Test trying to load a resource using an invalid identifier</summary>
        [Test]
#if DEBUG
        [ExpectedException(typeof(ArgumentException))]
#else
        //// Preemptive error checking disabled in Release
        [ExpectedException(typeof(IndexOutOfRangeException))]
#endif
        public void IdentifierMissingScheme()
        {
            var manager = new ResourceManagerInternal();
            manager.RegisterResourceLoader(ResourcesHelper.TestResourceLoader<string>(string.Empty));
            manager.LoadResource<GenericNativeResource<string>>("noscheme");
        }

        /// <summary>Test trying to load a resource for a registered loader</summary>
        [Test]
        public void RegisteredLoader()
        {
            var expected = Guid.NewGuid().ToString();
            var manager = new ResourceManagerInternal();
            manager.RegisterResourceLoader(ResourcesHelper.TestResourceLoader<string>(expected));
            manager.RegisterResourceLoader(ResourcesHelper.TestResourceLoader<object>(new object()));

            var resource = manager.LoadResource<GenericNativeResource<string>>(this.resourceUri);
            Assert.AreEqual(expected, resource.NativeResource);
        }

        /// <summary>Test trying to load a resource for an unknown scheme</summary>
        [Test]
#if DEBUG
        [ExpectedException(typeof(UnknownResourceSchemeException))]
#else
        //// Preemptive error checking disabled in Release
        [ExpectedException(typeof(NullReferenceException))]
#endif
        public void UnknownScheme()
        {
            var manager = new ResourceManagerInternal();
            manager.RegisterResourceLoader(ResourcesHelper.TestResourceLoader<string>("foo", string.Empty));
            manager.RegisterResourceLoader(ResourcesHelper.TestResourceLoader<object>("bar", new object()));
            manager.LoadResource<GenericNativeResource<string>>(this.resourceUri);
        }

        /// <summary>Test trying to load a resource for an unknown scheme</summary>
        [Test]
        public void CustomScheme()
        {
            const string Scheme = "test";
            this.resourceUri = "{0}:{1}".FormatInvariant(Scheme, Guid.NewGuid().ToString("n"));

            var expected = Guid.NewGuid().ToString();
            var manager = new ResourceManagerInternal();
            manager.RegisterResourceLoader(ResourcesHelper.TestResourceLoader<object>(new object()));
            manager.RegisterResourceLoader(ResourcesHelper.TestResourceLoader<string>(expected));

            var resource = manager.LoadResource<GenericNativeResource<string>>(this.resourceUri);
            Assert.AreEqual(expected, resource.NativeResource);
        }
    }
}
