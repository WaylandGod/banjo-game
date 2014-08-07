//-----------------------------------------------------------------------
// <copyright file="GameBase.cs" company="Benjamin Woodall">
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
using System.Linq;
using Core;
using Core.Programmability;
using Core.Resources.Management;
using Game.Data;
using Game.Factories;
using Game.Input;

namespace Game
{
    /// <summary>Base class for IGame implementations</summary>
    public abstract class GameBase : IGame
    {
        /// <summary>Resource library</summary>
        protected readonly IResourceLibrary Resources;

        /// <summary>World factory</summary>
        protected readonly IWorldFactory WorldFactory;

        /// <summary>Input manager</summary>
        protected readonly IInputManager InputManager;

        /// <summary>Resource ids for the levels</summary>
        private readonly string[] levelIds;

        /// <summary>Backing field for Levels</summary>
        private LevelSummary[] levels;

        /// <summary>Backing field for LevelDefinitions</summary>
        private LevelDefinition[] levelDefinitions;
        
        /// <summary>Initializes a new instance of the GameBase class</summary>
        /// <param name="definition">Game definition</param>
        /// <param name="resources">Resource library</param>
        /// <param name="worldFactory">World factory</param>
        /// <param name="inputManager">Input manager</param>
        protected GameBase(GameDefinition definition, IResourceLibrary resources, IWorldFactory worldFactory, IInputManager inputManager)
        {
            this.Id = new RuntimeId(definition.Id);
            this.Title = definition.Title;
            this.Resources = resources;
            this.WorldFactory = worldFactory;
            this.InputManager = inputManager;
            this.levelIds = definition.LevelIds;
        }

        /// <summary>Gets summaries of available levels</summary>
        public LevelSummary[] Levels
        {
            get
            {
                return this.levels ??
                    (this.levels = this.LevelDefinitions.Select(leveldef => leveldef.Summary).ToArray());
            }
        }

        /// <summary>Gets the runtime identifier</summary>
        public RuntimeId Id { get; private set; }

        /// <summary>Gets the title</summary>
        public string Title { get; private set; }

        /// <summary>Gets the current world</summary>
        public IWorld World { get; private set; }

        /// <summary>Gets a value indicating whether the current game is finished</summary>
        /// <remarks>Completed and Failed properties should be used to determine what to do next</remarks>
        public bool Done { get { return this.Completed || this.Failed; } }

        /// <summary>Gets a value indicating whether the objective has been completed</summary>
        /// <remarks>When true the game should exit play and enter level results UI</remarks>
        public bool Completed { get { return this.World.Objectives.All(o => o.Completed); } }

        /// <summary>Gets a value indicating whether the objective has failed</summary>
        /// <remarks>When true the game should reset the level</remarks>
        public bool Failed { get { return this.World.Objectives.Any(o => o.Failed); } }

        /// <summary>Gets the world definitions</summary>
        protected IEnumerable<LevelDefinition> LevelDefinitions
        {
            get
            {
                return this.levelDefinitions ??
                    (this.levelDefinitions = this.levelIds.Select(id => this.Resources.GetSerializedResource<LevelDefinition>(id)).ToArray());
            }
        }

        /// <summary>Loads the specified world</summary>
        /// <param name="levelId">World identifier</param>
        public virtual void LoadLevel(string levelId)
        {
            var levelDefinition = this.LevelDefinitions.FirstOrDefault(leveldef => leveldef.Id == levelId);
            if (levelDefinition == null)
            {
                var msg = "No world found with id '{0}'.".FormatInvariant(levelId);
                throw new ArgumentOutOfRangeException("levelId", msg);
            }

            // Unload the current world (if any)
            this.UnloadLevel();

            // Create a world with the new world
            this.World = this.WorldFactory.Create(levelDefinition);
        }

        /// <summary>Unloads the current world</summary>
        /// <returns>True if a world was unloaded; otherwise, false</returns>
        public bool UnloadLevel()
        {
            if (this.World == null)
            {
                return false;
            }

            this.World.Dispose();
            this.World = null;
            GC.Collect();
            return true;
        }

        /// <summary>Updates the game</summary>
        /// <param name="deltaTime">Time elapsed since last update</param>
        public void Update(float deltaTime)
        {
            this.InputManager.Update();
        }

        /// <summary>Disposes of resources</summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>Disposes of resources</summary>
        /// <param name="disposing">Whether the object is being disposed</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.UnloadLevel();
            }
        }
    }
}
