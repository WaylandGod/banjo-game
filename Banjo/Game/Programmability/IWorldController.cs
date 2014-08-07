//-----------------------------------------------------------------------
// <copyright file="IEntityController.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace Game.Programmability
{
    /// <summary>Represents a controller that can be applied to entities</summary>
    public interface IWorldController : IMappedInputController
    {
        /// <summary>Gets the target</summary>
        new IWorld Target { get; }

        /// <summary>Gets the controller's objectives (if any)</summary>
        IEnumerable<IObjective> Objectives { get; }

        /// <summary>Initializes the controller</summary>
        void OnStart(EventArgs e);

        /// <summary>Called on each frame update</summary>
        void OnUpdate(FrameEventArgs e);

        /// <summary>Called when it is time to draw user interface elements</summary>
        void OnDrawUI(EventArgs e);
    }
}
