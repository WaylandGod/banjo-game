//-----------------------------------------------------------------------
// <copyright file="TestControllerA.cs" company="Benjamin Woodall">
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
    [Controller("controller.testA")]
    public class TestControllerA : TestControllerBase
    {
        /// <summary>Initializes a new instance of the TestControllerA class</summary>
        /// <param name="target">Target entity to be controlled</param>
        /// <param name="config">Controller configuration</param>
        public TestControllerA(IEntity target, IConfig config) : base(target, config) { }
    }
}
