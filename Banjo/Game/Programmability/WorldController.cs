//-----------------------------------------------------------------------
// <copyright file="WorldController.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using Core.DependencyInjection;
using Core.Programmability;
using Game.Input;

namespace Game.Programmability
{
    /// <summary>Base class for world controllers</summary>
    public abstract class WorldController : MappedInputController, IWorldController
    {
        /// <summary>Initializes static members of the WorldController class</summary>
        static WorldController()
        {
            ControllerManager.AddEventHandler<IWorldController, EventArgs>(WorldEventHandlers.OnStart, "OnStart");
            ControllerManager.AddEventHandler<IWorldController, FrameEventArgs>(WorldEventHandlers.OnUpdate, "OnUpdate");
            ControllerManager.AddEventHandler<IWorldController, EventArgs>(WorldEventHandlers.OnDrawUI, "OnDrawUI");
        }

        public static void Test<TEventArgs>(System.Linq.Expressions.Expression<Action<TEventArgs>> handler) where TEventArgs : EventArgs
        {
            Console.Out.WriteLine(handler.GetType().FullName);
        }

        /// <summary>Initializes a new instance of the WorldController class</summary>
        public WorldController(IWorld target, IConfig config) : base(target, config) { }

        /// <summary>Gets the IWorld target</summary>
        public new IWorld Target { get { return (IWorld)base.Target; } }

        /// <summary>Gets the controller's objectives (if any)</summary>
        public virtual IEnumerable<IObjective> Objectives { get { return new IObjective[0]; } }

        public virtual void OnStart(EventArgs e) { }

        /// <summary>Runs during main update</summary>
        /// <param name="e">Frame event args</param>
        public virtual void OnUpdate(FrameEventArgs e) { }

        /// <summary>Called when it is time to draw user interface elements</summary>
        public virtual void OnDrawUI(EventArgs e) { }
    }
}
