//-----------------------------------------------------------------------
// <copyright file="ControllerManager.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using Core.Input;

namespace Core.Programmability
{
    /// <summary>Manages all controllers</summary>
    public class ControllerManager : IControllerManager
    {
        /// <summary>Table of event controller set invocations</summary>
        /// <remarks>
        /// Keys must match the method being invoked. They are used in finding
        /// implemented event handlers in the controllers as they are added.
        /// </remarks>
        private readonly IDictionary<string, ControllerEventHandlerSet> eventHandlers = new Dictionary<string, ControllerEventHandlerSet>();

        /// <summary>Adds an event handler which will later be searched for in IController implementations</summary>
        /// <typeparam name="TController">Type of the declaring controller</typeparam>
        /// <typeparam name="TEventArgs">Type of the event</typeparam>
        /// <param name="handlerName">Handler name (must match the controller method name)</param>
        /// <param name="invocation">Action to invoke the event</param>
        public void AddEventHandler<TController, TEventArgs>(string handlerName, Action<TController, TEventArgs> invocation)
            where TController : IController
            where TEventArgs : EventArgs
        {
            //// Log.Trace("ControllerManager.AddEventHandler<{0}, {1}>('{2}')", typeof(TController).FullName, typeof(TEventArgs).FullName, handlerName);
            this.eventHandlers[handlerName] = ControllerEventHandlerSet.Create<TController, TEventArgs>(invocation);
        }

        /// <summary>Adds a controller to the applicable controller event handler sets</summary>
        /// <param name="controller">The controller</param>
        public void AddController(IController controller)
        {
            var type = controller.GetType();
            //// Log.Trace("Adding controller {0} for '{1}'...", type.FullName, controller.Target.Id);
            foreach (var set in this.eventHandlers)
            {
                var method = type.GetMethod(set.Key, new[] { set.Value.EventType });
                if (method != null && method.GetCustomAttributes(typeof(EventHandlerAttribute), true).Length > 0)
                {
                    //// Log.Trace("Adding controller event handler: {0}::{1}", type.FullName, method.Name);
                    
                    // TODO: Move into ControllerEventHandlerSet.AddController
                    if (!set.Value.Controllers.ContainsKey(controller.Target.Id))
                    {
                        set.Value.Controllers.Add(controller.Target.Id, new List<IController>());
                    }

                    set.Value.Controllers[controller.Target.Id].Add(controller);
                }
            }            
        }

        /// <summary>Removes a controller the applicable event handler sets</summary>
        /// <param name="controller">The controller</param>
        public void RemoveController(IController controller)
        {
            var type = controller.GetType();
            foreach (var set in this.eventHandlers)
            {
                // TODO: Move into ControllerEventHandlerSet.RemoveController
                var method = type.GetMethod(set.Key.ToString(), new[] { set.Value.EventType });
                if (method != null && method.DeclaringType == type &&
                    set.Value.Controllers.ContainsKey(controller.Target.Id))
                {
                    set.Value.Controllers[controller.Target.Id].Remove(controller);
                }
            }
        }

        /// <summary>Removes controllers for a target from all event handler sets</summary>
        /// <param name="targetId">Runtime identifier of the controller target</param>
        public void RemoveController(RuntimeId targetId)
        {
            foreach (var set in this.eventHandlers)
            {
                // TODO: Move into ControllerEventHandlerSet.RemoveTarget
                set.Value.Controllers.Remove(targetId);
            }
        }

        /// <summary>Sends the event to all controllers implementing the named event handler</summary>
        /// <typeparam name="TEventArgs">Type of the EventArgs</typeparam>
        /// <param name="eventHandler">The event handler</param>
        /// <param name="e">Data for the event</param>
        public void SendEvent<TEventArgs>(string eventHandler, TEventArgs e) where TEventArgs : EventArgs
        {
            this.FindControllerSet(eventHandler).SendEvent(e);
        }

        /// <summary>Sends the event to controllers implementing the named event handler for the specified target</summary>
        /// <typeparam name="TEventArgs">Type of the EventArgs</typeparam>
        /// <param name="eventHandler">The event handler</param>
        /// <param name="targetId">Runtime identifier of the controller target</param>
        /// <param name="e">Data for the event</param>
        public void SendEvent<TEventArgs>(string eventHandler, RuntimeId targetId, TEventArgs e) where TEventArgs : EventArgs
        {
            this.FindControllerSet(eventHandler).SendEvent(targetId, e);
        }

        /// <summary>Finds the controller set for the specified event handler name</summary>
        /// <param name="eventHandler">The event handler</param>
        /// <returns>The controller set</returns>
        /// <exception cref="IndexOutOfRangeException">If no controller set exists for the given event handler name</exception>
        private ControllerEventHandlerSet FindControllerSet(string eventHandler)
        {
#if DEBUG
            if (!this.eventHandlers.ContainsKey(eventHandler))
            {
                var msg = "Invalid event handler '{0}'\nValid handlers: {1}"
                    .FormatInvariant(eventHandler, string.Join(", ", this.eventHandlers.Keys.Select(k => k.ToString()).ToArray()));
                throw new ArgumentOutOfRangeException("eventHandler", msg);
            }
#endif
            return this.eventHandlers[eventHandler];
        }

        /// <summary>A set of controllers on which an event may be run</summary>
        private class ControllerEventHandlerSet
        {
            /// <summary>Invocation for the controllers</summary>
            private readonly Action<IController, EventArgs> invocation;

            /// <summary>Initializes a new instance of the ControllerEventHandlerSet class</summary>
            /// <param name="eventType">Type of event for the controller set</param>
            /// <param name="invocation">Controller event handler invocation</param>
            private ControllerEventHandlerSet(Type eventType, Action<IController, EventArgs> invocation)
            {
                this.Controllers = new Dictionary<RuntimeId, IList<IController>>();
                this.EventType = eventType;
                this.invocation = invocation;
            }

            /// <summary>Gets the type of the event for the controller set</summary>
            public Type EventType { get; private set; }

            /// <summary>Gets the list of controllers</summary>
            public IDictionary<RuntimeId, IList<IController>> Controllers { get; private set; }

            /// <summary>Creates an event controller set</summary>
            /// <typeparam name="TController">Type of the declaring controller</typeparam>
            /// <typeparam name="TEventArgs">Type of the event</typeparam>
            /// <param name="invocation">Action to invoke the event</param>
            /// <returns>The created event controller set</returns>
            public static ControllerEventHandlerSet Create<TController, TEventArgs>(Action<TController, TEventArgs> invocation)
                where TController : IController
                where TEventArgs : EventArgs
            {
                return new ControllerEventHandlerSet(typeof(TEventArgs), (c, e) => invocation((TController)c, (TEventArgs)e));
            }

            /// <summary>Send an event to all controllers in the set</summary>
            /// <param name="e">Event data</param>
            public void SendEvent(EventArgs e)
            {
#if DEBUG
                if (!this.EventType.IsAssignableFrom(e.GetType()))
                {
                    throw new ArgumentException("Incorrect event type. Was: {0}, Expecting: {1}".FormatInvariant(e.GetType().FullName, this.EventType.FullName), "e");
                }
#endif

                foreach (var controllers in this.Controllers.Values)
                {
                    foreach (var controller in controllers)
                    {
                        this.invocation(controller, e);
                    }
                }
            }

            /// <summary>Send an event to a specific controller</summary>
            /// <param name="targetId">Runtime identifier of the controller's target</param>
            /// <param name="e">Event data</param>
            public void SendEvent(RuntimeId targetId, EventArgs e)
            {
#if DEBUG
                if (!this.EventType.IsAssignableFrom(e.GetType()))
                {
                    throw new ArgumentException("Incorrect event type. Was: {0}, Expecting: {1}".FormatInvariant(e.GetType().FullName, this.EventType.FullName), "e");
                }
#endif
                if (!this.Controllers.ContainsKey(targetId))
                {
                    /*
                    Log.Trace(
                        "SendEvent<{0}> - Controller not found: '{1}'\nControllers:\n",
                        this.EventType.FullName,
                        targetId,
                        this.Controllers.Keys);
                     */
                    return;
                }

                foreach (var controller in this.Controllers[targetId])
                {
                    this.invocation(controller, e);
                }
            }
        }
    }
}
