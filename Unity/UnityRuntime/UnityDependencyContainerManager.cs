//-----------------------------------------------------------------------
// <copyright file="UnityDependencyContainerManager.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using Core;
using Core.DependencyInjection;
using Core.Factories;
using Core.Programmability;
using Core.Resources.Management;
using Core.Unity;
using Game;
using Game.Factories;
using Game.Input;
using Game.Unity.Input;
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
            
            Container.RegisterSingleton<IResourceLoader, TextAssetResourceLoader>("Unity TextAsset Loader");
            Container.RegisterSingleton<IResourceLoader, PrefabAssetResourceLoader>("Unity Prefab Loader");
            
            Container.RegisterSingleton<IAvatarFactory, UnityAvatarFactory>();
            Container.RegisterSingleton<IEntityFactory, UnityEntityFactory>();
            Container.RegisterSingleton<IWorldFactory, UnityWorldFactory>();
            Container.RegisterSingleton<IGameFactory, UnityGameFactory>();

            Container.RegisterSingleton<IControllerFactory, ReflectionControllerFactory<IEntity>>("Entity Controller Factory");
            Container.RegisterSingleton<IControllerFactory, ReflectionControllerFactory<IWorld>>("World Controller Factory");
            Container.RegisterSingleton<IControllerManager, ControllerManager>();

            Container.RegisterSingleton<IInputManager, InputManager>();
            Container.RegisterSingleton<IInputSource, UnityInputSource>();
        }
    }
}
