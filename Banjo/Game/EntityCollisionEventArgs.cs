//-----------------------------------------------------------------------
// <copyright file="EntityCollisionEventArgs.cs" company="Benjamin Woodall">
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
using Game.Data;

namespace Game
{
    /// <summary>Event data for entity/entity collisions</summary>
    public class EntityCollisionEventArgs : CollisionEventArgs
    {
        /// <summary>Collider entity</summary>
        public readonly IEntity ColliderEntity;

        /// <summary>Backing field for ColliderDirection</summary>
        private float? colliderDirection;

        /// <summary>Initializes a new instance of the EntityCollisionEventArgs class</summary>
        /// <param name="target">Target entity</param>
        /// <param name="collider">Collider entity</param>
        public EntityCollisionEventArgs(IEntity target, IEntity collider)
            : base(target, collider)
        {
            this.ColliderEntity = collider;
        }

        /// <summary>Gets the direction of the collider's motion along the X and Z axis</summary>
        public float ColliderDirection
        {
            get
            {
                return (this.colliderDirection ?? (this.colliderDirection = (float)this.ColliderEntity.Velocity.AngleXZ)).Value;
            }
        }
    }
}
