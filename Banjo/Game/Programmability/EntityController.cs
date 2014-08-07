//-----------------------------------------------------------------------
// <copyright file="EntityController.cs" company="Benjamin Woodall">
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
    /// <summary>Base class for entity controllers</summary>
    public abstract class EntityController : MappedInputController, IEntityController
    {
        /// <summary>Initializes static members of the EntityController class</summary>
        static EntityController()
        {
            ControllerManager.AddEventHandler<IEntityController, EventArgs>(EntityEventHandlers.OnStart, "OnStart");
            ControllerManager.AddEventHandler<IEntityController, FrameEventArgs>(EntityEventHandlers.OnUpdate, "OnUpdate");
            ControllerManager.AddEventHandler<IEntityController, EventArgs>(EntityEventHandlers.OnDrawUI, "OnDrawUI");
            ControllerManager.AddEventHandler<IEntityController, CollisionEventArgs>(EntityEventHandlers.OnCollisionEnter, "OnCollisionEnter");
            ControllerManager.AddEventHandler<IEntityController, CollisionEventArgs>(EntityEventHandlers.OnCollisionContinue, "OnCollisionContinue");
            ControllerManager.AddEventHandler<IEntityController, CollisionEventArgs>(EntityEventHandlers.OnCollisionExit, "OnCollisionExit");
        }

        /// <summary>Initializes a new instance of the EntityController class</summary>
        public EntityController(IEntity target, IConfig config) : base(target, config) { }

        /// <summary>Gets the IEntity target</summary>
        public new IEntity Target { get { return (IEntity)base.Target; } }

        /// <summary>Initializes the controller</summary>
        public virtual void OnStart(EventArgs e) { }

        /// <summary>Runs during main update</summary>
        /// <param name="e">Frame event args</param>
        public virtual void OnUpdate(FrameEventArgs e) { }

        /// <summary>Called when the target first collides with an entity or tile</summary>
        /// <param name="e">Collision event args</param>
        public virtual void OnCollisionEnter(CollisionEventArgs e) { }

        /// <summary>Called when the target continues to collide with an entity or tile</summary>
        /// <param name="e">Collision event args</param>
        public virtual void OnCollisionContinue(CollisionEventArgs e) { }

        /// <summary>Called when the target stops colliding with an entity or tile</summary>
        /// <param name="e">Collision event args</param>
        public virtual void OnCollisionExit(CollisionEventArgs e) { }

        /// <summary>Called when it is time to draw user interface elements</summary>
        public virtual void OnDrawUI(EventArgs e) { }
    }
}
