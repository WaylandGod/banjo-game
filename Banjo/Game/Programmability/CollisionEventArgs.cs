//-----------------------------------------------------------------------
// <copyright file="CollisionEventArgs.cs" company="Benjamin Woodall">
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
using Core;
using Game.Data;

namespace Game
{
    /// <summary>Event data base class for collisions</summary>
    public abstract class CollisionEventArgs : EventArgs
    {
        /// <summary>Colliding collider collidable</summary>
        public readonly ICollidable Collider;

        /// <summary>Target entity</summary>
        protected readonly IEntity Target;

        /// <summary>Backing field for RelativePosition</summary>
        private Vector3D? relativePosition;

        /// <summary>Initializes a new instance of the CollisionEventArgs class</summary>
        /// <param name="target">Target entity</param>
        /// <param name="collider">Collider object</param>
        public CollisionEventArgs(IEntity target, ICollidable collider)
        {
            this.Target = target;
            this.Collider = collider;
        }

        /// <summary>Gets the position of the collider relative to the target</summary>
        public Vector3D RelativePosition
        {
            get
            {
                return (this.relativePosition ?? (this.relativePosition = this.Collider.Position - this.Target.Position)).Value;
            }
        }
    }
}
