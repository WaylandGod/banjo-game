//-----------------------------------------------------------------------
// <copyright file="UnityWorld.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System.Collections.Generic;
using Core.Data;
using Core.Factories;
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
        public UnityWorld(
            LevelDefinition definition,
            IResourceLibrary resources,
            IControllerManager controllerManager,
            IControllerFactory[] controllerFactories,
            IEntityFactory entityFactory)
        : base(definition, resources, controllerManager, controllerFactories, entityFactory)
        {
            SafeECall.Invoke(() => WorldBehaviour.Instance.World = this);
        }

        /// <summary>Gets the world transform</summary>
        public static Transform Transform { get { return WorldBehaviour.Instance.transform; } }
    }
}
