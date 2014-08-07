//-----------------------------------------------------------------------
// <copyright file="IEntity.cs" company="Benjamin Woodall">
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
using Game.Programmability;

namespace Game
{
    /// <summary>Represents an entity</summary>
    /// <remarks>An entity is any interactive/dynamic object in a game</remarks>
    public interface IEntity : IObject, IControllerTarget, ICollidable, IDisposable
    {
        /// <summary>Gets the avatar</summary>
        IAvatar Avatar { get; }

        /// <summary>Gets or sets the direction</summary>
        Vector3D Direction { get; set; }

        /// <summary>Gets or sets the speed (in units/second)</summary>
        new Vector3D Velocity { get; set; }

        /// <summary>Gets or sets the position</summary>
        new Vector3D Position { get; set; }

        /// <summary>Gets or sets the state</summary>
        string State { get; set; }

        /// <summary>Gets the object's controllers</summary>
        IEntityController[] Controllers { get; }
    }
}
