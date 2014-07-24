//-----------------------------------------------------------------------
// <copyright file="UnityDependencyContainerManager.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using Core;
using Core.DependencyInjection;
using Core.Input;
using Core.Programmability;
using Core.Resources.Management;
using Core.Unity;
using Core.Unity.Input;
using Game;
using Game.Factories;
using Game.Programmability;
using Game.Unity.Factories;
using Unity.Resources.Management;

namespace Unity.Runtime
{
    /// <summary>
    /// Manages the dependency injection container for Core.Unity implementations
    /// </summary>
    internal static class UnityDependencyContainerManager
    {
        /// <summary>Gets the Dependency Container</summary>
        internal static DependencyContainer Container { get; private set; }

        /// <summary>Register type mappings</summary>
        public static void Register()
        {
            Container = new DependencyContainer();
            Container.RegisterSingleton<ILogger, GenericAsyncLogger<DebugConsoleLogger>>("Unity Async Debug Logger");
            
            Container.RegisterType<IResourceLoader, TextAssetResourceLoader>("Unity TextAsset Loader");
            Container.RegisterType<IResourceLoader, PrefabAssetResourceLoader>("Unity Prefab Loader");
            
            Container.RegisterType<IAvatarFactory, UnityAvatarFactory>();
            Container.RegisterType<IEntityFactory, UnityEntityFactory>();
            Container.RegisterType<ITileFactory, UnityTileFactory>();
            Container.RegisterType<IWorldFactory, UnityWorldFactory>();
            Container.RegisterType<IGameFactory, UnityGameFactory>();

            Container.RegisterType<IControllerFactory, ReflectionControllerFactory>();
            Container.RegisterSingleton<IControllerManager, ControllerManager>();

            Container.RegisterSingleton<IInputManager, InputManager>();
            Container.RegisterSingleton<IInputSource, UnityInputManagerSource>();
        }
    }
}
