//-----------------------------------------------------------------------
// <copyright file="TileInstance.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using Core;
using Game.Data;

namespace Game
{
    /// <summary>Represents an instance of a tile</summary>
    public class TileInstance : ICollidable, IDisposable
    {
        /// <summary>Initializes a new instance of the TileInstance class</summary>
        /// <param name="tile">Master tile</param>
        /// <param name="avatar">Avatar instance</param>
        /// <param name="position">Tile position</param>
        public TileInstance(ITile tile, IAvatar avatar, Vector3 position)
        {
            this.Tile = tile;
            this.Avatar = avatar;
            this.Position = position;
        }

        /// <summary>Gets the tile</summary>
        public ITile Tile { get; private set; }

        /// <summary>Gets the avatar</summary>
        public IAvatar Avatar { get; private set; }

        /// <summary>Gets or sets the position</summary>
        /// <remarks>Pass-through to the avatar's position</remarks>
        public Vector3 Position
        {
            get { return this.Avatar.Position; }
            set { this.Avatar.Position = value; }
        }

        /// <summary>Gets the material</summary>
        public Material Material { get { return this.Tile.Material; } }

        /// <summary>Gets the mass</summary>
        public float Mass { get { return this.Tile.Mass; } }

        /// <summary>Gets the clipping distance</summary>
        public float ClippingDistance { get { return this.Avatar.ClippingDistance; } }

        /// <summary>Gets the velocity (always zero for tiles)</summary>
        public Vector3 Velocity { get { return Vector3.Zero; } }

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
            return "{0} - {1}".FormatInvariant(this.Tile.Id, this.Position);
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
