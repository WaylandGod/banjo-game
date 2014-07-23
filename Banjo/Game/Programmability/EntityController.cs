//-----------------------------------------------------------------------
// <copyright file="EntityController.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using Core.Input;
using Core.Programmability;

namespace Game.Programmability
{
    /// <summary>Base class for entity controllers</summary>
    public abstract class EntityController : Controller, IEntityController
    {
        /// <summary>Initializes static members of the EntityController class</summary>
        static EntityController()
        {
            ControllerManager.AddEventHandler<IEntityController, FrameEventArgs>(EntityEventHandlers.OnUpdate, (c, e) => c.OnUpdate(e));
            ControllerManager.AddEventHandler<IEntityController, CollisionEventArgs>(EntityEventHandlers.OnCollisionEnter, (c, e) => c.OnCollisionEnter(e));
            ControllerManager.AddEventHandler<IEntityController, CollisionEventArgs>(EntityEventHandlers.OnCollisionContinue, (c, e) => c.OnCollisionContinue(e));
            ControllerManager.AddEventHandler<IEntityController, CollisionEventArgs>(EntityEventHandlers.OnCollisionExit, (c, e) => c.OnCollisionExit(e));
        }

        /// <summary>Initializes a new instance of the EntityController class</summary>
        /// <param name="target">Target to be controlled</param>
        /// <param name="config">Controller configuration</param>
        public EntityController(IEntity target, IConfig config)
            : base(target, config) { }

        /// <summary>Method signature for mapped input event handlers</summary>
        /// <param name="controller">Entity controller</param>
        /// <param name="e">Mapped input event args</param>
        public delegate void MappedInputEventHandler(IEntityController controller, MappedInputEventArgs e);

        /// <summary>Gets the IEntity target</summary>
        public new IEntity Target { get { return (IEntity)base.Target; } }

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
    }
}
