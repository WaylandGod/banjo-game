//-----------------------------------------------------------------------
// <copyright file="AssemblyInit.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using Core;
using Core.DependencyInjection;
using Game.Factories;
using TestUtilities.Core;
using TestUtilities.Game.Factories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity.Runtime;

namespace UnityCoreUnitTests
{
    /// <summary>Assembly-wide test initialization</summary>
    [TestClass]
    public class AssemblyInit
    {
        /// <summary>Assembly-wide initialization</summary>
        /// <param name="unused">Test Context (unused)</param>
        [AssemblyInitialize]
        public static void Initialize(TestContext unused)
        {
            // Reset the global dependency container
            DependencyInjectionHelper.ResetGlobalContainer();

            // Register test dependency mappings
            new DependencyContainer()
                .RegisterSingleton<Game.Factories.IAvatarFactory, TestAvatarFactory>();
                ////.RegisterType<Core.Resources.Management.IResourceLoader>

            // Register the unity dependency mappings
            UnityDependencyContainerManager.Register();
            Assert.IsNotNull(UnityDependencyContainerManager.Container);

            // Initialize logging with a test logger
            TestLogger.InitializeLog();
        }
    }
}