//-----------------------------------------------------------------------
// <copyright file="FactoriesFixture.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Linq;
using System.Text;
using Core.DependencyInjection;
using Core.Programmability;
using Core.Resources;
using Core.Resources.Management;
using Game.Data;
using Game.Factories;
using Game.Programmability;
using Game.Unity;
using NUnit.Framework;
using Rhino.Mocks;
using TestUtilities.Core;
using TestUtilities.Game;
using TestUtilities.Game.Factories;
using Unity.Runtime;

namespace UnityCoreUnitTests
{
    /// <summary>Text fixture for the Unity factories</summary>
    [TestFixture]
    public class FactoriesFixture
    {
        /// <summary>Test ResourceLibrary</summary>
        private IResourceLibrary resourceLibrary;

        /// <summary>Test ControllerManager</summary>
        private IControllerManager controllerManager;

        /// <summary>Test GameDefinition identifier</summary>
        private string gameDefinitionId;

        /// <summary>Assembly-wide initialization</summary>
        /// <param name="unused">Test Context (unused)</param>
        [TestFixtureSetUp]
        public static void Initialize()
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

        /// <summary>Per-test initialization</summary>
        [SetUp]
        public void TestInitialize()
        {
            this.resourceLibrary = new ResourceLibrary();
            this.controllerManager = new ControllerManager();
            new DependencyContainer()
                .RegisterSingleton<IResourceLibrary>(this.resourceLibrary)
                .RegisterSingleton<IControllerManager>(this.controllerManager);
            
            this.gameDefinitionId = GameDataHelper.DeepCreateTestGameDefinition(
                this.resourceLibrary, 4, 8, 3, 16, 16);
        }

        /// <summary>Per-test cleanup</summary>
        [TearDown]
        public void TestCleanup()
        {
            this.gameDefinitionId = null;
            this.resourceLibrary.Dispose();
            this.resourceLibrary = null;
            GC.Collect();
        }

        /// <summary>Test something</summary>
        [Ignore] //// TODO: Work around exception thrown when UnityAvatar == null
        [Test]
#if !DEBUG
        //// SafeECall disabled in Release which allows the ECall SecurityException to surface.
        //// In this case, since the System.Security.SecurityException is thrown in method called
        //// via reflection, it is wrapped in a System.Reflection.TargetInvocationException.
        [ExpectedException(typeof(System.Reflection.TargetInvocationException))]
#endif
        public void CreateGame()
        {
            var gameDefinition = this.resourceLibrary.GetSerializedResource<GameDefinition>(this.gameDefinitionId);
            var game = GlobalContainer.Resolve<IGameFactory>().Create(gameDefinition);
            Assert.IsInstanceOf<UnityGame>(game);

            game.LoadLevel(game.Levels.First().Id);

            var allResources = string.Join("\r\n", this.resourceLibrary.GetAllResources<IResource>().Select(resource => resource.ToString()).ToArray());
            Assert.IsNotNull(allResources);
        }
    }
}
