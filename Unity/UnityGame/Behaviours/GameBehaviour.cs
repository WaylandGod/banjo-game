//-----------------------------------------------------------------------
// <copyright file="GameBehaviour.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Core;
using Core.DependencyInjection;
using Core.Resources.Management;
using Game;
using Game.Data;
using Game.Factories;
using Unity.Resources;
using Unity.Resources.Behaviours;
using Unity.Resources.Management;
using UnityEngine;

namespace Game.Unity.Behaviours
{
    /// <summary>Behaviour for the game</summary>
    /// <remarks>TODO: MAJOR CLEANUP NEEDED HERE!!!!!!</remarks>
    [AddComponentMenu("Banjo/Game/Game")]
    public class GameBehaviour : MonoBehaviour
    {
        /// <summary>Resource Library Index XML</summary>
        public TextAsset ResourceLibraryXml;

        /// <summary>GameDefinition Resource Identifier</summary>
        public string GameDefinitionId;

        /// <summary>The Game</summary>
        private UnityGame game;

        /// <summary>Gets the GameBehaviour singleton</summary>
        public static GameBehaviour Instance { get; private set; }

        /// <summary>Called when behaviour instance is being loaded</summary>
        public void Awake()
        {
            // Set the singleton instance
            if (GameBehaviour.Instance != null)
            {
                throw new InvalidOperationException("Only one instance of GameBehaviour is allowed per scene");
            }

            GameBehaviour.Instance = this;

            // Resolve the asset loaders
            ResourceManager.RegisterResourceLoaders(GlobalContainer.ResolveAll<IResourceLoader>());

            // Register the resource library singleton
            var resources = ResourceLibrary.FromString(this.ResourceLibraryXml.text);
            new DependencyContainer().RegisterSingleton<IResourceLibrary>(resources);

            // Create the IGame instance
            var gameDefinition = resources.GetSerializedResource<GameDefinition>(this.GameDefinitionId);
            this.game = (UnityGame)GlobalContainer.Resolve<IGameFactory>().Create(gameDefinition);
            
            //// Log.Trace("Loaded game '{0}' with {1} levels:\n{2}", this.game.Title, this.game.Levels.Length, string.Join("\n", this.game.Levels.Select(l => "{0} ({1})".FormatInvariant(l.Title, l.Id)).ToArray()));

            //// TODO: Drive level loading from a menu system
            // Load the first level
            var firstLevel = this.game.Levels.First().Id;
            Log.Trace("Loading level '{0}'...", firstLevel);
            this.game.LoadLevel(firstLevel);
        }

        /// <summary>Called at the end of every frame</summary>
        public void Update()
        {
            if (this.game != null)
            {
                this.game.Update(Time.deltaTime);
            }
        }

        /// <summary>Called when the behaviour instance is being destroyed</summary>
        public void OnDestroy()
        {
            GameBehaviour.Instance = null;
        }
    }
}
