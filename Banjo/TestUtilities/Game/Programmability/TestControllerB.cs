//-----------------------------------------------------------------------
// <copyright file="TestControllerB.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using Core;
using Core.Programmability;
using Game;
using Game.Programmability;

namespace TestUtilities.Game.Programmability
{
    /// <summary>Dummy entity controller for testing</summary>
    [Controller("controller.testB")]
    public class TestControllerB : TestControllerBase
    {
        /// <summary>Initializes a new instance of the TestControllerB class</summary>
        /// <param name="target">Target entity to be controlled</param>
        /// <param name="config">Controller configuration</param>
        public TestControllerB(IEntity target, IConfig config) : base(target, config) { }
    }
}
