//-----------------------------------------------------------------------
// <copyright file="UnityWorld.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System.Collections.Generic;
using Core.Programmability;
using Core.Resources.Management;
using Core.Unity;
using Game;
using Game.Data;
using Game.Factories;
using Game.Unity.Behaviours;
using UnityEngine;

namespace Game.Unity
{
    /// <summary>Unity IWorld implementation</summary>
    public class UnityWorld : WorldBase, IWorld
    {
        /// <summary>Initializes a new instance of the UnityWorld class</summary>
        /// <param name="definition">World definition</param>
        /// <param name="resources">Resource library</param>
        /// <param name="controllerManager">Controller manager</param>
        /// <param name="entityFactory">Entity factory</param>
        /// <param name="tileFactory">Tile factory</param>
        public UnityWorld(LevelDefinition definition, IResourceLibrary resources, IControllerManager controllerManager, IEntityFactory entityFactory, ITileFactory tileFactory)
            : base(definition, resources, controllerManager, entityFactory, tileFactory)
        {
            SafeECall.Invoke(() => WorldBehaviour.Instance.World = this);
        }

        /// <summary>Gets the world transform</summary>
        public static Transform Transform { get { return WorldBehaviour.Instance.transform; } }
    }
}
