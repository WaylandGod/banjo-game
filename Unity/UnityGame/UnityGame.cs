//-----------------------------------------------------------------------
// <copyright file="UnityGame.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using Core;
using Core.Resources.Management;
using Core.Unity;
using Game;
using Game.Data;
using Game.Factories;
using Game.Input;
using Game.Unity.Behaviours;
using UnityEngine;

namespace Game.Unity
{
    /// <summary>Unity IGame implementation</summary>
    public class UnityGame : GameBase, IGame
    {
        /// <summary>Initializes a new instance of the UnityGame class</summary>
        /// <param name="definition">Game definition</param>
        /// <param name="resources">Resource library</param>
        /// <param name="worldFactory">World factory</param>
        /// <param name="inputManager">Input manager</param>
        public UnityGame(GameDefinition definition, IResourceLibrary resources, IWorldFactory worldFactory, IInputManager inputManager)
            : base(definition, resources, worldFactory, inputManager) { }

        /// <summary>Gets the game transform</summary>
        public static Transform Transform { get { return GameBehaviour.Instance.transform; } }
    }
}
