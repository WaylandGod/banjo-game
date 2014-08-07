//-----------------------------------------------------------------------
// <copyright file="EntityBehaviour.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Linq;
using Core;
using Game.Programmability;
using Game.Unity;
using UnityEngine;

namespace Game.Unity.Behaviours
{
    /// <summary>Behaviour for capturing Unity collision events, etc</summary>
    public class EntityBehaviour : ObjectBehaviour
    {
        /// <summary>Gets or sets the UnityEntity instance</summary>
        internal UnityEntity Entity { get; set; }

        /// <summary>Called when a Collider enters the Entity's trigger</summary>
        /// <param name="collider">The collider</param>
        public void OnTriggerEnter(Collider collider)
        {
            this.SendCollisionEvent(collider, EntityEventHandlers.OnCollisionEnter);
        }

        /// <summary>Called when a Collider stays within the Entity's trigger</summary>
        /// <param name="collider">The collider</param>
        public void OnTriggerStay(Collider collider)
        {
            this.SendCollisionEvent(collider, EntityEventHandlers.OnCollisionContinue);
        }

        /// <summary>Called when a Collider exits the Entity's trigger</summary>
        /// <param name="collider">The collider</param>
        public void OnTriggerExit(Collider collider)
        {
            this.SendCollisionEvent(collider, EntityEventHandlers.OnCollisionExit);
        }

        /// <summary>Sends a collision event to the Entity via the ControllerManager</summary>
        /// <param name="collider">Collider object</param>
        /// <param name="eventHandler">Event handler for the collision event</param>
        private void SendCollisionEvent(Collider collider, string eventHandler)
        {
            var objectBehaviour = collider.GetComponent<ObjectBehaviour>(true);
            if (objectBehaviour == null)
            {
                // Ignore colliders that do not have an ObjectBehaviour component
                return;
            }

            var entityBehaviour = objectBehaviour as EntityBehaviour;
            if (entityBehaviour != null)
            {
                var e = new EntityCollisionEventArgs(this.Entity, entityBehaviour.Entity);
                ControllerManager.SendEvent<EntityCollisionEventArgs>(eventHandler, this.Entity.Id, e);
                return;
            }

            Log.Error(
                "Collider '{0}' has unhandled ObjectBehaviour '{1}'",
                objectBehaviour.name,
                objectBehaviour.GetType().FullName);
        }
    }
}
