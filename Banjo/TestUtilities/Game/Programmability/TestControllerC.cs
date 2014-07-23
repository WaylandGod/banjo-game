//-----------------------------------------------------------------------
// <copyright file="TestControllerC.cs" company="Benjamin Woodall">
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
    [Controller("controller.testC")]
    public class TestControllerC : TestControllerBase
    {
        /// <summary>Initializes a new instance of the TestControllerC class</summary>
        /// <param name="target">Target entity to be controlled</param>
        /// <param name="config">Controller configuration</param>
        public TestControllerC(IEntity target, IConfig config) : base(target, config) { }
    }
}
