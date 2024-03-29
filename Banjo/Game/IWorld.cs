﻿//-----------------------------------------------------------------------
// <copyright file="IWorld.cs" company="Benjamin Woodall">
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
using Core.Programmability;
using Game.Data;
using Game.Programmability;

namespace Game
{
    /// <summary>Represents a game world</summary>
    public interface IWorld : IControllerTarget, IDisposable
    {
        /// <summary>Gets the world's world summary</summary>
        LevelSummary Summary { get; }

        /// <summary>Gets all entities in the world</summary>
        IEnumerable<IEntity> Entities { get; }

        /// <summary>Gets all entity controllers in the world</summary>
        IEnumerable<IEntityController> EntityControllers { get; }

        /// <summary>Gets the world's controllers</summary>
        IWorldController[] Controllers { get; }

        /// <summary>Gets the player information</summary>
        IPlayer Player { get; }

        /// <summary>Gets the current state of the objectives</summary>
        IEnumerable<IObjective> Objectives { get; }

        /// <summary>Gets the current state of the required objectives</summary>
        IEnumerable<IObjective> RequiredObjectives { get; }

        /// <summary>Gets the current state of the optional objectives</summary>
        IEnumerable<IObjective> OptionalObjectives { get; }
    }
}
