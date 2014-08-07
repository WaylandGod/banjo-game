//-----------------------------------------------------------------------
// <copyright file="WorldBase.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using Core.Data;
using Core.Factories;
using Core.Programmability;
using Core.Resources.Management;
using Game;
using Game.Data;
using Game.Factories;
using Game.Input;
using Game.Programmability;

namespace Game
{
    /// <summary>Base class for IWorld implementations</summary>
    /// <remarks>Includes game state determinations and controller event dispatching</remarks>
    public abstract class WorldBase : ControllerTarget<IWorldController>, IWorld, IDisposable
    {
        /// <summary>Backing field for ControllerManager</summary>
        private readonly IControllerManager _controllerManager;

        /// <summary>Backing field for Entities</summary>
        private readonly IList<IEntity> _entities;

        /// <summary>Initializes a new instance of the WorldBase class</summary>
        public WorldBase(
            LevelDefinition definition,
            IResourceLibrary resources,
            IControllerManager controllerManager,
            IControllerFactory[] controllerFactories,
            IEntityFactory entityFactory)
        : base(definition.Id, controllerFactories, definition.Controllers)
        {
            this._controllerManager = controllerManager;
            this._entities = definition.Entities
                .Select(entity =>
                    entityFactory.Create(
                        resources.GetSerializedResource<EntityDefinition>(entity.EntityId),
                        entity.Controllers,
                        entity.Position,
                        entity.Direction,
                        entity.Velocity))
                .ToList();
            this.Summary = definition.Summary;
        }

        /// <summary>Gets the world's world summary</summary>
        public LevelSummary Summary { get; private set; }

        /// <summary>Gets all entities in the world</summary>
        public IEnumerable<IEntity> Entities { get { return this._entities; } }

        /// <summary>Gets all entity controllers in the world</summary>
        public IEnumerable<IEntityController> EntityControllers
        {
            get { return this.Entities.SelectMany(e => e.Controllers); }
        }

        /// <summary>Gets the player information</summary>
        public IPlayer Player { get; private set; }

        /// <summary>Gets the current state of the objectives</summary>
        public IEnumerable<IObjective> Objectives { get { return this.Controllers.SelectMany(c => c.Objectives); } }

        /// <summary>Gets the current state of the required objectives</summary>
        public IEnumerable<IObjective> RequiredObjectives { get { return this.Objectives.Where(o => o.Required); } }

        /// <summary>Gets the current state of the optional objectives</summary>
        public IEnumerable<IObjective> OptionalObjectives { get { return this.Objectives.Where(o => !o.Required); } }

        /// <summary>Gets the controller manager</summary>
        protected IControllerManager ControllerManager { get { return this._controllerManager; } }

        /// <summary>Send the OnStart event to all world and entity controllers</summary>
        public virtual void OnStart(EventArgs e)
        {
            this.ControllerManager.SendEvent<EventArgs>(WorldEventHandlers.OnStart, e);
            this.ControllerManager.SendEvent<EventArgs>(EntityEventHandlers.OnStart, e);
        }

        /// <summary>Send the OnUpdate event to all world and entity controllers</summary>
        public virtual void OnUpdate(FrameEventArgs e)
        {
            this.ControllerManager.SendEvent<FrameEventArgs>(WorldEventHandlers.OnUpdate, e);
            this.ControllerManager.SendEvent<FrameEventArgs>(EntityEventHandlers.OnUpdate, e);
        }

        /// <summary>Send the OnDrawUI event to all world and entity controllers</summary>
        public virtual void OnDrawUI(EventArgs e)
        {
            this.ControllerManager.SendEvent<EventArgs>(WorldEventHandlers.OnDrawUI, e);
            this.ControllerManager.SendEvent<EventArgs>(EntityEventHandlers.OnDrawUI, e);
        }

        /// <summary>Gets a string representation of the world</summary>
        /// <returns>A string representation of the world</returns>
        public override string ToString()
        {
            return "{0} - Entities: {1}"
                .FormatInvariant(this.Id, (this.Entities != null) ? this.Entities.Count() : 0);
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
                // Dispose of the entities
                while (this._entities.Count > 0)
                {
                    var entity = this.Entities.FirstOrDefault();
                    if (entity != null)
                    {
                        try
                        {
                            this.Entities.First().Dispose();
                        }
                        catch (Exception e)
                        {
                            Log.Error("Error disposing entity '{0}': {1}", entity.Id, e);
                        }
                    }

                    this._entities.RemoveAt(0);
                }
            }
        }
    }
}
