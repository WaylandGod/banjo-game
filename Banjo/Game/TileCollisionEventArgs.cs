//-----------------------------------------------------------------------
// <copyright file="TileCollisionEventArgs.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using Game.Data;

namespace Game
{
    /// <summary>Event data for tile collisions</summary>
    public class TileCollisionEventArgs : CollisionEventArgs
    {
        /// <summary>Collider tile instance</summary>
        public readonly TileInstance ColliderTileInstance;

        /// <summary>Initializes a new instance of the TileCollisionEventArgs class</summary>
        /// <param name="target">Target entity</param>
        /// <param name="collider">Collider tile instance</param>
        public TileCollisionEventArgs(IEntity target, TileInstance collider)
            : base(target, collider)
        {
            this.ColliderTileInstance = collider;
        }
        
        /// <summary>Gets the collider tile (if any)</summary>
        public ITile ColliderTile
        {
            get { return this.ColliderTileInstance != null ? this.ColliderTileInstance.Tile : null; }
        }
    }
}
