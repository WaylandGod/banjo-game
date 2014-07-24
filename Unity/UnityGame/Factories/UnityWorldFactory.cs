//-----------------------------------------------------------------------
// <copyright file="UnityWorldFactory.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using Core.Programmability;
using Core.Resources.Management;
using Game;
using Game.Data;
using Game.Factories;
using Game.Unity;
using Game.Unity.Behaviours;
using UnityEngine;

namespace Game.Unity.Factories
{
    /// <summary>Creates implementations of IWorld</summary>
    public class UnityWorldFactory : WorldFactoryBase
    {
        /// <summary>Initializes a new instance of the UnityWorldFactory class</summary>
        /// <param name="resources">Resource library</param>
        /// <param name="controllerManager">Controller manager</param>
        /// <param name="entityFactory">Entity factory</param>
        /// <param name="tileFactory">Tile factory</param>
        public UnityWorldFactory(
            IResourceLibrary resources,
            IControllerManager controllerManager,
            IEntityFactory entityFactory,
            ITileFactory tileFactory)
            : base(resources, controllerManager, entityFactory, tileFactory) { }

        /// <summary>Creates an instance of IWorld</summary>
        /// <param name="definition">World definition</param>
        /// <returns>The created IWorld instance</returns>
        protected override IWorld Create(LevelDefinition definition)
        {
            return new UnityWorld(
                definition,
                this.Resources,
                this.ControllerManager,
                this.EntityFactory,
                this.TileFactory);
        }
    }
}
