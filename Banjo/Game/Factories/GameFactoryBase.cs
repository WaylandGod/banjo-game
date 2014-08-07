//-----------------------------------------------------------------------
// <copyright file="GameFactoryBase.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using Core.Factories;
using Core.Resources.Management;
using Game.Data;
using Game.Factories;
using Game.Input;

namespace Game.Factories
{
    /// <summary>Creates implementations of IGame</summary>
    public abstract class GameFactoryBase : ResourceFactoryBase<IGame>, IGameFactory
    {
        /// <summary>World factory</summary>
        protected readonly IWorldFactory WorldFactory;

        /// <summary>Input manager</summary>
        protected readonly IInputManager InputManager;

        /// <summary>Initializes a new instance of the GameFactoryBase class</summary>
        /// <param name="resources">Resource library</param>
        /// <param name="worldFactory">World factory</param>
        /// <param name="inputManager">Input manager</param>
        protected GameFactoryBase(IResourceLibrary resources, IWorldFactory worldFactory, IInputManager inputManager)
            : base(resources)
        {
            this.WorldFactory = worldFactory;
            this.InputManager = inputManager;
        }

        /// <summary>Creates an instance of IGame</summary>
        /// <param name="definition">Game definition</param>
        /// <returns>The created IGame instance</returns>
        protected abstract IGame Create(GameDefinition definition);
    }
}
