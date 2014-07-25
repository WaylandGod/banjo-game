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
using Core.Resources.Management;
using Game.Data;
using Game.Factories;
using Game.Programmability;

namespace Game
{
    /// <summary>Base class for IEntity implementations</summary>
    /// <remarks>An entity is any interactive/dynamic object in a game</remarks>
    public abstract class EntityBase : ObjectBase, IEntity
    {
        /// <summary>Initializes a new instance of the EntityBase class</summary>
        /// <param name="definition">Entity definition</param>
        /// <param name="resources">Resource library</param>
        /// <param name="avatarFactory">Avatar factory</param>
        /// <param name="controllerFactory">Controller factory</param>
        /// <param name="controllers">Additional controllers</param>
        /// <param name="position">Initial position</param>
        /// <param name="direction">Initial direction</param>
        /// <param name="velocity">Initial velocity</param>
        [SuppressMessage("Microsoft.Usage", "CA2214", Justification = "Call to virtuals should be safe.")]
        protected EntityBase(
            EntityDefinition definition,
            IResourceLibrary resources,
            IAvatarFactory avatarFactory,
            IControllerFactory controllerFactory,
            ControllerConfig[] controllers,
            Vector3 position,
            Vector3 direction,
            Vector3 velocity)
            : base(
                resources.GetSerializedResource<AvatarDefinition>(definition.AvatarId),
                resources.GetSerializedResource<Material>(definition.MaterialId),
                definition.Volume)
        {
#if LOG_VERBOSE
            Log.Trace("Creating entity '{0}' at {1}...", definition.Id, position);
#endif
            this.Avatar = avatarFactory.Create(this.AvatarDefinition);

            this.Position = position;
            this.Direction = direction;
            this.Velocity = velocity;

            this.CreateControllers(definition.Controllers, controllers ?? new ControllerConfig[0], controllerFactory);

#if LOG_VERBOSE
            Log.Trace("Created '{0}' at {1}.", this.Avatar.Id, this.Avatar.Position);
#endif
        }

        /// <summary>Gets the avatar</summary>
        public IAvatar Avatar { get; private set; }

        /// <summary>Gets the clipping distance</summary>
        public float ClippingDistance { get { return this.Avatar.ClippingDistance; } }

        /// <summary>Gets or sets the position</summary>
        public Vector3 Position
        {
            get { return this.Avatar.Position; }
            set { this.Avatar.Position = value; }
        }

        /// <summary>Gets or sets the heading</summary>
        public Vector3 Direction
        {
            get { return this.Avatar.Direction; }
            set { this.Avatar.Direction = value; }
        }

        /// <summary>Gets or sets the speed (in units/second)</summary>
        public Vector3 Velocity { get; set; }

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

        /// <summary>Gets the object's controllers</summary>
        public IEntityController[] Controllers { get; private set; }

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

        /// <summary>Creates controllers for the object</summary>
        /// <param name="builtInControllers">Controller configurations from the definition</param>
        /// <param name="controllers">Additional controller configurations/setting overrides</param>
        /// <param name="controllerFactory">Controller factory</param>
        protected void CreateControllers(
            ControllerConfig[] builtInControllers,
            ControllerConfig[] controllers,
            IControllerFactory controllerFactory)
        {
            if (builtInControllers == null) throw new ArgumentNullException("builtInControllers");
            if (controllers == null) throw new ArgumentNullException("controllers");

            // Find the additional controllers that are actually
            // overrides for built-in controllers' settings
            var overrides = controllers
                .Where(cc =>
                    builtInControllers.Any(bicc => cc.ControllerId == bicc.ControllerId))
                .ToDictionary(
                    cc => cc.ControllerId,
                    cc => new DictionaryConfig(cc.Settings));

            // Create a dictionary of controller configs keyed by their ids
            // Start with the built-in controllers, then the non-overrides.
            // Next, merge in settings overrides where available.
            var controllersToCreate = builtInControllers
                .ToDictionary(
                    cc => cc.ControllerId,
                    cc => new DictionaryConfig(cc.Settings))
                .Concat(controllers
                    .Where(cc => !overrides.ContainsKey(cc.ControllerId))
                    .ToDictionary(
                        cc => cc.ControllerId,
                        cc => new DictionaryConfig(cc.Settings)))
                .ToDictionary(
                    kvp => kvp.Key,
                    kvp => overrides.ContainsKey(kvp.Key) ?
                        kvp.Value.Merge(overrides[kvp.Key]) : kvp.Value);

            //// TODO: Merge in a global config?

            // Create the controllers from the controller id/config pairs.
#if LOG_VERBOSE
            Log.Trace("Entity<{0}>[{1}].CreateControllers - Creating {2} controllers: [{3}]", this.GetType().FullName, this.Id, controllersToCreate.Count, string.Join(", ", controllersToCreate.Keys.ToArray()));
#endif
            this.Controllers = controllersToCreate
                .Select(kvp =>
                    controllerFactory.Create(kvp.Key, kvp.Value, this))
                .ToArray();
        }
    }
}
