//-----------------------------------------------------------------------
// <copyright file="UnityGameFactory.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using Core.Resources.Management;
using Game;
using Game.Data;
using Game.Factories;
using Game.Input;
using Game.Unity;

namespace Game.Unity.Factories
{
    /// <summary>Creates implementations of IGame</summary>
    public class UnityGameFactory : GameFactoryBase
    {
        /// <summary>Initializes a new instance of the UnityGameFactory class</summary>
        /// <param name="resources">Resource library</param>
        /// <param name="worldFactory">World factory</param>
        /// <param name="inputManager">Input manager</param>
        public UnityGameFactory(
            IResourceLibrary resources,
            IWorldFactory worldFactory,
            IInputManager inputManager)
            : base(resources, worldFactory, inputManager) { }

        /// <summary>Creates an instance of IGame</summary>
        /// <param name="definition">Game definition</param>
        /// <returns>The created IGame instance</returns>
        protected override IGame Create(GameDefinition definition)
        {
            return new UnityGame(
                definition,
                this.Resources,
                this.WorldFactory,
                this.InputManager);
        }
    }
}
