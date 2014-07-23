//-----------------------------------------------------------------------
// <copyright file="TestControllerBase.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using Core;
using Core.Programmability;
using Game;
using Game.Programmability;

namespace TestUtilities.Game.Programmability
{
    /// <summary>Base class for dummy entity controllers</summary>
    public class TestControllerBase : EntityController
    {
        /// <summary>Initializes a new instance of the TestControllerBase class</summary>
        /// <param name="target">Target entity to be controlled</param>
        /// <param name="config">Controller configuration</param>
        public TestControllerBase(IEntity target, IConfig config) : base(target, config) { }
    }
}
