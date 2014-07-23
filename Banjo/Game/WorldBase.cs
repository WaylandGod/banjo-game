//-----------------------------------------------------------------------
// <copyright file="WorldBase.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using Core.Input;
using Core.Programmability;
using Core.Resources.Management;
using Game;
using Game.Data;
using Game.Factories;
using Game.Programmability;

namespace Game.Unity
{
    /// <summary>Base class for IWorld implementations</summary>
    public abstract class WorldBase : IWorld, IDisposable
    {
        /// <summary>Backing field for Entities</summary>
        private readonly IList<IEntity> entities;

        /// <summary>Backing field for Entities</summary>
        private readonly ITile[] tiles;

        /// <summary>Initializes a new instance of the WorldBase class</summary>
        /// <param name="definition">World definition</param>
        /// <param name="resources">Resource library</param>
        /// <param name="controllerManager">Controller manager</param>
        /// <param name="entityFactory">Entity factory</param>
        /// <param name="tileFactory">Tile factory</param>
        public WorldBase(LevelDefinition definition, IResourceLibrary resources, IControllerManager controllerManager, IEntityFactory entityFactory, ITileFactory tileFactory)
        {
            this.Id = new RuntimeId(definition.Id);
            this.Summary = definition.Summary;
            this.ControllerManager = controllerManager;
            this.entities = definition.Entities
                .Select(entity =>
                    {
                        //// Log.Trace("Loading entity: '{0}'", entity.EntityId);
                        return entityFactory.Create(
                            resources.GetSerializedResource<EntityDefinition>(entity.EntityId),
                            entity.Controllers,
                            entity.Position,
                            entity.Direction,
                            entity.Velocity);
                    })
                .ToList();

            if (definition.Tiles != null && definition.Map != null)
            {
                this.tiles = definition.Tiles
                    .Select(tileId => tileFactory.Create(
                        resources.GetSerializedResource<TileDefinition>(tileId)))
                    .ToArray();

                Vector3 position;
                float x = 0f, z = 0f;
                var map = definition.Map;
                foreach (var tile in map.Tiles)
                {
                    position = new Vector3(x, 0, z) * map.TileSpacing; // TODO: Add Y for layer
                    this.tiles[tile].AddInstance(position);

                    if (++x == map.Breadth)
                    {
                        x = 0;
                        z++;
                    }
                }
            }
        }

        /// <summary>Gets the runtime identifier</summary>
        public RuntimeId Id { get; private set; }

        /// <summary>Gets the world's world summary</summary>
        public LevelSummary Summary { get; private set; }

        /// <summary>Gets all entities in the world</summary>
        public IEnumerable<IEntity> Entities { get { return this.entities; } }

        /// <summary>Gets all tiles in the world</summary>
        public IEnumerable<ITile> Tiles { get { return this.tiles; } }
        
        /// <summary>Gets all entity controllers in the world</summary>
        public IEnumerable<IEntityController> EntityControllers
        {
            get { return this.Entities.SelectMany(e => e.Controllers); }
        }

        /// <summary>Gets the controller manager</summary>
        protected IControllerManager ControllerManager { get; private set; }

        /// <summary>Called on each frame update</summary>
        /// <param name="e">Frame event args</param>
        public virtual void OnUpdate(FrameEventArgs e)
        {
            this.ControllerManager.SendEvent<FrameEventArgs>("OnUpdate", e);
        }

        /// <summary>Gets a string representation of the world</summary>
        /// <returns>A string representation of the world</returns>
        public override string ToString()
        {
            return "{0} - Tiles: {1}; Entities: {2}"
                .FormatInvariant(this.Id, (this.tiles != null) ? this.tiles.Length : 0, (this.entities != null) ? this.entities.Count : 0);
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
                while (this.entities.Count > 0)
                {
                    var entity = this.entities.FirstOrDefault();
                    if (entity != null)
                    {
                        try
                        {
                            this.entities.First().Dispose();
                        }
                        catch (Exception e)
                        {
                            Log.Error("Error disposing entity '{0}': {1}", entity.Id, e);
                        }
                    }

                    this.entities.RemoveAt(0);
                }

                // Dispose of the tiles
                if (this.tiles != null)
                {
                    for (int i = 0; i < this.tiles.Length; i++)
                    {
                        if (this.tiles[i] != null)
                        {
                            try
                            {
                                this.tiles[i].Dispose();
                                this.tiles[i] = null;
                            }
                            catch (Exception e)
                            {
                                Log.Error("Error disposing tile '{0}': {1}", this.tiles[i].Id, e);
                            }
                        }
                    }
                }
            }
        }
    }
}
