//-----------------------------------------------------------------------
// <copyright file="TileInstanceBehaviour.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using Game;
using Game.Programmability;
using UnityEngine;

namespace Game.Unity.Behaviours
{
    /// <summary>Behaviour for capturing Unity collision events, etc</summary>
    public class TileInstanceBehaviour : ObjectBehaviour
    {
        /// <summary>Gets or sets the tile instance</summary>
        public TileInstance TileInstance { get; set; }

        /// <summary>Gets the tile</summary>
        public ITile Tile { get { return this.TileInstance != null ? this.TileInstance.Tile : null; } }
    }
}
