//-----------------------------------------------------------------------
// <copyright file="ResourcesHelper.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using Core.Resources;
using Core.Resources.Management;
using Rhino.Mocks;

namespace TestUtilities.Core
{
    /// <summary>Test helpers for working with resources</summary>
    public static class ResourcesHelper
    {
        /// <summary>Scheme for test resources</summary>
        public const string TestScheme = "test";

        /// <summary>Extensions for test resources</summary>
        public static readonly string[] TestExtensions = new[] { "tst", "?" };

        /// <summary>Generates an IResourceLoader mock for the provided scheme and resources</summary>
        /// <param name="resources">Resources to be "loaded"</param>
        /// <returns>Resource loader mock</returns>
        public static IResourceLoader GenerateResourceLoaderMock(params IResource[] resources)
        {
            return GenerateResourceLoaderMock<IResource>(TestScheme, false, resources);
        }

        /// <summary>Generates an IResourceLoader mock for the provided scheme and resources</summary>
        /// <param name="scheme">Resource scheme</param>
        /// <param name="resources">Resources to be "loaded"</param>
        /// <returns>Resource loader mock</returns>
        public static IResourceLoader GenerateResourceLoaderMock(string scheme, params IResource[] resources)
        {
            return GenerateResourceLoaderMock<IResource>(scheme, false, resources);
        }

        /// <summary>Generates an IResourceLoader mock for the provided scheme and resources</summary>
        /// <typeparam name="TResource">Type of resources to load</typeparam>
        /// <param name="resources">Resources to be "loaded"</param>
        /// <returns>Resource loader mock</returns>
        public static IResourceLoader GenerateResourceLoaderMock<TResource>(params TResource[] resources)
            where TResource : IResource
        {
            return GenerateResourceLoaderMock<TResource>(TestScheme, false, resources);
        }

        /// <summary>Generates an IResourceLoader mock for the provided scheme and resources</summary>
        /// <typeparam name="TResource">Type of resources to load</typeparam>
        /// <param name="scheme">Resource scheme</param>
        /// <param name="resources">Resources to be "loaded"</param>
        /// <returns>Resource loader mock</returns>
        public static IResourceLoader GenerateResourceLoaderMock<TResource>(string scheme, params TResource[] resources)
            where TResource : IResource
        {
            return GenerateResourceLoaderMock<TResource>(scheme, false, resources);
        }

        /// <summary>Generates an IResourceLoader mock for the provided scheme and resources</summary>
        /// <typeparam name="TResource">Type of resources to load</typeparam>
        /// <param name="register">Whether to register the loader with the global ResourceManager</param>
        /// <param name="resources">Resources to be "loaded"</param>
        /// <returns>Resource loader mock</returns>
        public static IResourceLoader GenerateResourceLoaderMock<TResource>(bool register, params TResource[] resources)
            where TResource : IResource
        {
            return GenerateResourceLoaderMock<TResource>(TestScheme, register, resources);
        }

        /// <summary>Generates an IResourceLoader mock for the provided scheme and resources</summary>
        /// <typeparam name="TResource">Type of resources to load</typeparam>
        /// <param name="scheme">Resource URI scheme</param>
        /// <param name="register">Whether to register the loader with the global ResourceManager</param>
        /// <param name="resources">Resources to be "loaded"</param>
        /// <returns>Resource loader mock</returns>
        public static IResourceLoader GenerateResourceLoaderMock<TResource>(string scheme, bool register, params TResource[] resources)
            where TResource : IResource
        {
            var loader = MockRepository.GenerateMock<IResourceLoader>();
            loader.Stub(f => f.Scheme).Return(scheme);
            loader.Stub(f => f.Extensions).Return(TestExtensions);
            loader.Stub(f => f.ResourceType).Return(typeof(IResource));
            foreach (var resource in resources)
            {
                loader.Stub(f => f.LoadResource(Arg<string>.Is.Equal(resource.Id))).Return(resource);
                loader.Stub(f => f.LoadResource(Arg<string>.Is.Equal("{0}.{1}".FormatInvariant(resource.Id, TestExtensions[0])))).Return(resource);
            }

            if (register)
            {
                ResourceManager.RegisterResourceLoader(loader);
            }

            return loader;
        }

        /// <summary>Generates an IResourceLibrary mock for the provided scheme and resources</summary>
        /// <param name="resources">Resources for the library</param>
        /// <returns>Resource library mock</returns>
        public static IResourceLibrary GenerateResourceLibraryMock(params IResource[] resources)
        {
            return GenerateResourceLibraryMock("mock", resources);
        }

        /// <summary>Generates an IResourceLibrary mock for the provided scheme and resources</summary>
        /// <param name="scheme">URI scheme for GetResourcesByUri calls</param>
        /// <param name="resources">Resources for the library</param>
        /// <returns>Resource library mock</returns>
        public static IResourceLibrary GenerateResourceLibraryMock(string scheme, params IResource[] resources)
        {
            var library = MockRepository.GenerateMock<IResourceLibrary>();
            foreach (var resource in resources)
            {
                library.Stub(f => f.GetResource<IResource>(Arg<string>.Is.Equal(resource.Id))).Return(resource);
                library.Stub(f => f.GetResourceByUri<IResource>(Arg<string>.Is.Equal("{0}:{1}".FormatInvariant(scheme, resource.Id)))).Return(resource);
            }

            return library;
        }

        /// <summary>Creates a generic resource loader for testing</summary>
        /// <typeparam name="T">Type of resources to load</typeparam>
        /// <param name="value">Value to be returned in a GenericNativeResource</param>
        /// <returns>Generic resources loader</returns>
        public static GenericResourceLoader<GenericNativeResource<T>> TestResourceLoader<T>(T value)
            where T : class
        {
            return TestResourceLoader(id => new GenericNativeResource<T>(value));
        }

        /// <summary>Creates a generic resource loader for testing</summary>
        /// <typeparam name="T">Type of resources to load</typeparam>
        /// <param name="scheme">Resource uri scheme</param>
        /// <param name="value">Value to be returned in a GenericNativeResource</param>
        /// <returns>Generic resources loader</returns>
        public static GenericResourceLoader<GenericNativeResource<T>> TestResourceLoader<T>(string scheme, T value)
            where T : class
        {
            return TestResourceLoader(scheme, id => new GenericNativeResource<T>(value));
        }

        /// <summary>Creates a generic resource loader for testing</summary>
        /// <typeparam name="T">Type of resources to load</typeparam>
        /// <param name="loadResource">Function that loads the resource type. If not found, should return null.</param>
        /// <returns>Generic resources loader</returns>
        public static GenericResourceLoader<GenericNativeResource<T>> TestResourceLoader<T>(
            Func<string, GenericNativeResource<T>> loadResource)
            where T : class
        {
            return TestResourceLoader(TestScheme, loadResource);
        }

        /// <summary>Creates a generic resource loader for testing</summary>
        /// <typeparam name="T">Type of resources to load</typeparam>
        /// <param name="scheme">Resource uri scheme</param>
        /// <param name="loadResource">Function that loads the resource type. If not found, should return null.</param>
        /// <returns>Generic resources loader</returns>
        public static GenericResourceLoader<GenericNativeResource<T>> TestResourceLoader<T>(
            string scheme,
            Func<string, GenericNativeResource<T>> loadResource)
            where T : class
        {
            return new GenericResourceLoader<GenericNativeResource<T>>(scheme, TestExtensions, loadResource);
        }

        /// <summary>Creates a new test resource id</summary>
        /// <returns>Created test resource id</returns>
        public static string NewTestResourceId()
        {
            return Guid.NewGuid().ToString("N");
        }

        /// <summary>Creates a new test resource uri</summary>
        /// <returns>Created test resource uri</returns>
        public static string NewTestResourceUri()
        {
            return NewTestResourceUri(NewTestResourceId());
        }

        /// <summary>Creates a new test resource uri</summary>
        /// <param name="id">Identifier for the resource</param>
        /// <returns>Created test resource uri</returns>
        public static string NewTestResourceUri(string id)
        {
            return "{0}:{1}.{2}".FormatInvariant(TestScheme, id, TestExtensions[0]);
        }
    }
}
