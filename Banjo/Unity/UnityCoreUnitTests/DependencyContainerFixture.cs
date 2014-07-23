//-----------------------------------------------------------------------
// <copyright file="DependencyContainerFixture.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System.Linq;
using Core;
using Core.DependencyInjection;
using Core.Resources.Management;
using NUnit.Framework;
using TestUtilities.Core;
using Unity.Resources.Management;
using Unity.Runtime;

namespace UnityCoreUnitTests
{
    /// <summary>Text fixture for the DependencyContainer</summary>
    [TestFixture]
    public class DependencyContainerFixture
    {
        /// <summary>Assembly-wide initialization</summary>
        [TestFixtureSetUp]
        public void Initialize()
        {
            DependencyInjectionHelper.ResetGlobalContainer();
            UnityDependencyContainerManager.Register();
            Assert.IsNotNull(UnityDependencyContainerManager.Container);
        }

        /// <summary>Per-test initialization</summary>
        [SetUp]
        public void TestInitialize()
        {
        }

        /// <summary>Test logger implementations are registered correctly</summary>
        [Test]
        public void ResolveRegisteredLoggers()
        {
            var loggers = GlobalContainer.ResolveAll<ILogger>();
            Assert.AreEqual(1, loggers.Count());
            
            var loggerA = loggers.Single();
            Assert.IsInstanceOf<Core.GenericAsyncLogger<Core.Unity.DebugConsoleLogger>>(loggerA);

            var loggerB = GlobalContainer.Resolve<ILogger>();
            Assert.AreSame(loggerA, loggerB);

            var loggerC = GlobalContainer.Resolve<ILogger>("Unity Async Debug Logger");
            Assert.AreSame(loggerB, loggerC);
        }

        /// <summary>Test resource loaders are registered correctly</summary>
        [Test]
        public void ResolveRegisteredLoaders()
        {
            var loaders = GlobalContainer.ResolveAll<IResourceLoader>();
            Assert.AreEqual(2, loaders.Count());

            var textAssetLoader = GlobalContainer.Resolve<IResourceLoader>("Unity TextAsset Loader");
            Assert.IsInstanceOf<TextAssetResourceLoader>(textAssetLoader);

            var prefabLoader = GlobalContainer.Resolve<IResourceLoader>("Unity Prefab Loader");
            Assert.IsInstanceOf<PrefabAssetResourceLoader>(prefabLoader);
        }
    }
}
