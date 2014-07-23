//-----------------------------------------------------------------------
// <copyright file="IGame.cs" company="Benjamin Woodall">
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
using Core;
using Game.Data;

namespace Game
{
    /// <summary>Represents a Game</summary>
    public interface IGame : IDisposable
    {
        /// <summary>Gets the runtime identifier</summary>
        RuntimeId Id { get; }

        /// <summary>Gets the title</summary>
        string Title { get; }

        /// <summary>Gets the levels</summary>
        LevelSummary[] Levels { get; }

        /// <summary>Gets the current world</summary>
        IWorld World { get; }

        /// <summary>Loads the specified world</summary>
        /// <param name="levelId">World identifier</param>
        void LoadLevel(string levelId);

        /// <summary>Unloads the current world</summary>
        /// <returns>True if a world was unloaded; otherwise, false</returns>
        bool UnloadLevel();
        
        /// <summary>Updates the game</summary>
        /// <param name="deltaTime">Time elapsed since last update</param>
        void Update(float deltaTime);
    }
}
