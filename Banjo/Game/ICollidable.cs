﻿//-----------------------------------------------------------------------
// <copyright file="ICollidable.cs" company="Benjamin Woodall">
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

using Core;
using Game.Data;

namespace Game
{
    /// <summary>Describes a collidable object</summary>
    public interface ICollidable
    {
        /// <summary>Gets the position</summary>
        Vector3 Position { get; }

        /// <summary>Gets the material</summary>
        Material Material { get; }

        /// <summary>Gets the mass</summary>
        float Mass { get; }

        /// <summary>Gets the clipping distance</summary>
        /// <remarks>
        /// Colliders closer than clipping distance may be
        /// 'declipped' to ensure that they do not overlap.
        /// </remarks>
        float ClippingDistance { get; }

        /// <summary>Gets the velocity (in units/second)</summary>
        Vector3 Velocity { get; }
    }
}