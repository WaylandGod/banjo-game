//-----------------------------------------------------------------------
// <copyright file="EntityBase.cs" company="Benjamin Woodall">
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
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Core;
using Core.Data;
using Core.Factories;
using Core.Resources.Management;
using Game.Data;
using Game.Factories;
using Game.Programmability;

namespace Game
{
    /// <summary>Base class for IEntity implementations</summary>
    /// <remarks>An entity is any interactive/dynamic object in a game</remarks>
    public abstract class EntityBase : ObjectBase<IEntityController>, IEntity
    {
        /// <summary>Initializes a new instance of the EntityBase class</summary>
        /// <param name="definition">Entity definition (includes primary controllers)</param>
        /// <param name="resources">Resource library</param>
        /// <param name="avatarFactory">Avatar factory</param>
        /// <param name="controllerFactories">Controller factories</param>
        /// <param name="customControllers">Additional controllers and overrides</param>
        /// <param name="position">Initial position</param>
        /// <param name="direction">Initial direction</param>
        /// <param name="velocity">Initial velocity</param>
        [SuppressMessage("Microsoft.Usage", "CA2214", Justification = "Call to virtuals should be safe.")]
        protected EntityBase(
            EntityDefinition definition,
            IResourceLibrary resources,
            IAvatarFactory avatarFactory,
            IControllerFactory[] controllerFactories,
            ControllerConfig[] customControllers,
            Vector3D position,
            Vector3D direction,
            Vector3D velocity)
        : base(
            resources.GetSerializedResource<AvatarDefinition>(definition.AvatarId),
            definition.Mass,
            controllerFactories,
            definition.Controllers,
            customControllers)
        {
#if LOG_VERBOSE
            Log.Trace("Creating entity '{0}' at {1}...", definition.Id, position);
#endif
            this.Avatar = avatarFactory.Create(this.AvatarDefinition);

            this.Position = position;
            this.Direction = direction;
            this.Velocity = velocity;

#if LOG_VERBOSE
            Log.Trace("Created '{0}' at {1}.", this.Avatar.Id, this.Avatar.Position);
#endif
        }

        /// <summary>Gets the avatar</summary>
        public IAvatar Avatar { get; private set; }

        /// <summary>Gets the clipping distance</summary>
        public float ClippingDistance { get { return this.Avatar.ClippingDistance; } }

        /// <summary>Gets or sets the position</summary>
        public Vector3D Position
        {
            get { return this.Avatar.Position; }
            set { this.Avatar.Position = value; }
        }

        /// <summary>Gets or sets the heading</summary>
        public Vector3D Direction
        {
            get { return this.Avatar.Direction; }
            set { this.Avatar.Direction = value; }
        }

        /// <summary>Gets or sets the speed (in units/second)</summary>
        public Vector3D Velocity { get; set; }

        /// <summary>Gets or sets the state</summary>
        public string State
        {
            get
            {
                return this.Avatar.CurrentState.Name;
            }

            set
            {
                if (!this.Avatar.States.ContainsKey(value))
                {
                    var msg = "Invalid state '{0}'. Valid states are: {1}"
                        .FormatInvariant(value, string.Join(", ", this.Avatar.States.Keys.ToArray()));
                    Log.Error(msg);
                    throw new ArgumentOutOfRangeException("value", msg);
                }

                this.Avatar.CurrentState = this.Avatar.States[value];
            }
        }

        /// <summary>Dispose of native/managed resources</summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>Gets a string representation of the object</summary>
        /// <returns>A string representation of the object</returns>
        public override string ToString()
        {
            return "{0} - p={1} v={2}".FormatInvariant(base.ToString(), this.Position, this.Velocity);
        }

        /// <summary>Dispose of native/managed resources</summary>
        /// <param name="disposing">Whether the object is being disposed</param>
        protected virtual void Dispose(bool disposing)
        {
            if (this.Avatar != null)
            {
                this.Avatar.Dispose();
                this.Avatar = null;
            }
        }
    }
}
