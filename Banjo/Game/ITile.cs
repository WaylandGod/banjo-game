//-----------------------------------------------------------------------
// <copyright file="ITile.cs" company="Benjamin Woodall">
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
using Game.Factories;

namespace Game
{
    /// <summary>Represents a tile</summary>
    /// <remarks>A tile is a static, immobile object which exists in many places at once</remarks>
    public interface ITile : IObject, IDisposable
    {
        /// <summary>Gets the factory used to create tile instance avatars</summary>
        IAvatarFactory AvatarFactory { get; }

        /// <summary>Gets the instances</summary>
        IEnumerable<TileInstance> Instances { get; }

        /// <summary>Gets the material</summary>
        Material Material { get; }

        /// <summary>Gets the mass</summary>
        float Mass { get; }

        /// <summary>Creates an instance of the tile</summary>
        /// <param name="position">Tile position</param>
        /// <returns>Created tile instance</returns>
        TileInstance AddInstance(Vector3 position);
    }
}
