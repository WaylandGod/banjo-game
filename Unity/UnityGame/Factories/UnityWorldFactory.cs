//-----------------------------------------------------------------------
// <copyright file="UnityWorldFactory.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using Core.Factories;
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
        public UnityWorldFactory(
            IResourceLibrary resources,
            IControllerManager controllerManager,
            IControllerFactory[] controllerFactories,
            IEntityFactory entityFactory)
        : base(resources, controllerManager, controllerFactories, entityFactory) { }

        /// <summary>Creates an instance of IWorld</summary>
        /// <param name="definition">World definition</param>
        /// <returns>The created IWorld instance</returns>
        public override IWorld Create(LevelDefinition definition)
        {
            return new UnityWorld(
                definition,
                this.Resources,
                this.ControllerManager,
                this.ControllerFactories,
                this.EntityFactory);
        }
    }
}
