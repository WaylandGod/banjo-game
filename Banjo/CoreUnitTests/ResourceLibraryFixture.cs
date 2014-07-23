//-----------------------------------------------------------------------
// <copyright file="ResourceLibraryFixture.cs" company="Benjamin Woodall">
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
using System.Text;
using Core.Resources;
using Core.Resources.Management;
using NUnit.Framework;
using Rhino.Mocks;
using TestUtilities.Core;

namespace CoreUnitTests
{
    /// <summary>Test fixture for ResourceLibrary</summary>
    [TestFixture]
    public class ResourceLibraryFixture
    {
        /// <summary>Per-test initialization</summary>
        [SetUp]
        public void TestInitialize()
        {
        }

        /// <summary>Roundtrip a serialized resource library</summary>
        [Test]
        public void SerializedResourceLibraryRoundtrip()
        {
            var expected = Guid.NewGuid().ToString();
            var expectedResource = new GenericNativeResource<string>(expected);
            var resourceUri = ResourcesHelper.NewTestResourceUri(expectedResource.Id);
            ResourceManager.RegisterResourceLoader(ResourcesHelper.TestResourceLoader<string>(id => expectedResource));

            var library = new ResourceLibrary();
            var resource = library.GetResourceByUri<GenericNativeResource<string>>(resourceUri);
            Assert.IsNotNull(resource);
            Assert.AreEqual(expected, resource.NativeResource);

            var libraryXml = Encoding.UTF8.GetString(library.ToBytes());
            Assert.IsFalse(string.IsNullOrEmpty(libraryXml));

            var savedLibrary = ResourceLibrary.FromBytes(Encoding.UTF8.GetBytes(libraryXml));
            Assert.IsNotNull(savedLibrary);

            var resourceById = savedLibrary.GetResource<GenericNativeResource<string>>(expectedResource.Id);
            var resourceByUri = savedLibrary.GetResourceByUri<GenericNativeResource<string>>(resourceUri);
            Assert.AreSame(resourceById, resourceByUri);
            Assert.AreEqual(expected, resourceById.NativeResource);
        }

        /// <summary>Verify native resources get disposed with the resource library</summary>
        [Test]
        public void ResourceLibraryDisposesNativeResources()
        {
            var disposable = MockRepository.GenerateMock<INativeResource>();
            disposable.Stub(f => f.Id).Return(Guid.NewGuid().ToString());
            var disposableUri = ResourcesHelper.NewTestResourceUri(disposable.Id);

            var nondisposable = MockRepository.GenerateMock<IResource>();
            nondisposable.Stub(f => f.Id).Return(Guid.NewGuid().ToString());
            var nondisposableUri = ResourcesHelper.NewTestResourceUri(nondisposable.Id);

            ResourcesHelper.GenerateResourceLoaderMock(true, disposable, nondisposable);

            using (var library = new ResourceLibrary())
            {
                Assert.IsNotNull(library.GetResourceByUri<IResource>(disposableUri));
                Assert.IsNotNull(library.GetResourceByUri<IResource>(nondisposableUri));
            }            
            
            disposable.AssertWasCalled(f => f.Dispose());
        }
    }
}
