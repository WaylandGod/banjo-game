//-----------------------------------------------------------------------
// <copyright file="IEntityController.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using Core;
using Game.Input;
using Core.Programmability;

namespace Game.Programmability
{
    /// <summary>Represents a controller that can be applied to entities</summary>
    public interface IEntityController : IController
    {
        /// <summary>Gets the target</summary>
        new IEntity Target { get; }

        /// <summary>Gets the controller's input mappings</summary>
        IDictionary<IInputMapping, MappedInputHandler> InputMappings { get; }

        /// <summary>Initializes the controller</summary>
        void OnStart(EventArgs e);

        /// <summary>Called on each frame update</summary>
        /// <param name="e">Frame event args</param>
        void OnUpdate(FrameEventArgs e);

        /// <summary>Called when the target first collides with an entity or tile</summary>
        /// <param name="e">Collision event args</param>
        void OnCollisionEnter(CollisionEventArgs e);

        /// <summary>Called when the target continues to collide with an entity or tile</summary>
        /// <param name="e">Collision event args</param>
        void OnCollisionContinue(CollisionEventArgs e);

        /// <summary>Called when the target stops colliding with an entity or tile</summary>
        /// <param name="e">Collision event args</param>
        void OnCollisionExit(CollisionEventArgs e);

        /// <summary>Called when it is time to draw user interface elements</summary>
        void OnDrawUI(EventArgs e);
    }
}
