//-----------------------------------------------------------------------
// <copyright file="ControllerManager.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Core.Programmability
{
    /// <summary>Manages all controllers</summary>
    public class ControllerManager : IControllerManager
    {
        /// <summary>Table of controller event handler sets</summary>
        private readonly IDictionary<string, IControllerEventHandlerSet> eventHandlers = new Dictionary<string, IControllerEventHandlerSet>();

        /// <summary>Adds an event handler mapping for the specified event name to the handler method</summary>
        /// <typeparam name="TController">Type of the declaring controller</typeparam>
        /// <typeparam name="TEventArgs">Type of the event</typeparam>
        /// <param name="eventName">Named used to send events to this handler</param>
        /// <param name="handlerName">Name of the TController method to invoke</param>
        public void AddEventHandler<TController, TEventArgs>(string eventName, string handlerName)
            where TController : IController
            where TEventArgs : EventArgs
        {
            var handler = typeof(TController).GetMethod(handlerName);
            this.eventHandlers[eventName] = new ControllerEventHandlerSet<TController, TEventArgs>(handler);
        }

        /// <summary>Adds a controller to the applicable controller event handler sets</summary>
        /// <param name="controller">The controller</param>
        public void AddController(IController controller)
        {
            foreach (var handlerSet in this.eventHandlers)
            {
                if (handlerSet.Value.AddController(controller))
                {
#if DEBUG
                    ////Log.Trace("Added controller {0} to handlerSet {1}", controller.Id, handlerSet.Key);
#endif
                }
            }            
        }

        /// <summary>Removes controllers for a target from all event handler sets</summary>
        /// <param name="targetId">Runtime identifier of the controller target</param>
        public void RemoveControllerTarget(RuntimeId targetId)
        {
            foreach (var handlerSet in this.eventHandlers.Values)
            {
                handlerSet.RemoveControllerTarget(targetId);
            }
        }

        /// <summary>Sends the event to all controllers implementing the named event handler</summary>
        /// <typeparam name="TEventArgs">Type of the EventArgs</typeparam>
        /// <param name="eventHandler">The event handler</param>
        /// <param name="e">Data for the event</param>
        public void SendEvent<TEventArgs>(string eventHandler, TEventArgs e) where TEventArgs : EventArgs
        {
            var controllerSet = this.FindControllerSet(eventHandler);
            if (controllerSet != null) controllerSet.SendEvent(e);
        }

        /// <summary>Sends the event to controllers implementing the named event handler for the specified target</summary>
        /// <typeparam name="TEventArgs">Type of the EventArgs</typeparam>
        /// <param name="eventHandler">The event handler</param>
        /// <param name="targetId">Runtime identifier of the controller target</param>
        /// <param name="e">Data for the event</param>
        public void SendEvent<TEventArgs>(string eventHandler, RuntimeId targetId, TEventArgs e) where TEventArgs : EventArgs
        {
            var controllerSet = this.FindControllerSet(eventHandler);
            if (controllerSet != null) controllerSet.SendEvent(targetId, e);
        }

        /// <summary>Finds the controller set for the specified event handler name</summary>
        /// <param name="eventHandler">The event handler</param>
        /// <returns>The controller set</returns>
        /// <exception cref="IndexOutOfRangeException">If no controller set exists for the given event handler name</exception>
        private IControllerEventHandlerSet FindControllerSet(string eventHandler)
        {
#if DEBUG
            if (!this.eventHandlers.ContainsKey(eventHandler))
            {
                Log.Warning("Invalid event handler '{0}'\nValid handlers: {1}", eventHandler, string.Join(", ", this.eventHandlers.Keys.Select(k => k.ToString()).ToArray()));
                return null;
            }
#endif
            return this.eventHandlers[eventHandler];
        }

        private interface IControllerEventHandlerSet
        {
            bool AddController(IController controller);
            bool RemoveControllerTarget(RuntimeId targetId);
            void SendEvent(EventArgs e);
            void SendEvent(RuntimeId targetId, EventArgs e);
        }

        /// <summary>A set of controllers on which an event may be run</summary>
        private class ControllerEventHandlerSet<TController, TEventArgs> : IControllerEventHandlerSet
            where TController : IController
            where TEventArgs : EventArgs
        {
            private const BindingFlags EventHandlerBinding = BindingFlags.Instance | BindingFlags.Public; // | BindingFlags.DeclaredOnly;

            private delegate void ControllerEventHandler(TEventArgs e);

            /// <summary>Table of handlers by target ids</summary>
            private readonly IDictionary<RuntimeId, IList<ControllerEventHandler>> handlers;

            private readonly string methodName;
            private readonly Type controllerType;
            private readonly Type eventType;

            /// <summary>Initializes a new instance of the ControllerEventHandlerSet class</summary>
            /// <param name="eventType">Type of event for the controller set</param>
            /// <param name="invocation">Controller event handler invocation</param>
            public ControllerEventHandlerSet(MethodInfo method)
            {
                // TODO: invocation validation?
                this.handlers = new Dictionary<RuntimeId, IList<ControllerEventHandler>>();
                this.controllerType = typeof(TController);
                this.eventType = typeof(TEventArgs);
                this.methodName = method.Name;

                var methodParameters = method.GetParameters();
                if (methodParameters.Length != 1)
                {
                    Log.Error("Handler does not take an event parameter");
                }

                if (methodParameters[0].ParameterType != this.eventType)
                {
                    Log.Error("Incorrect event type '{0}' (expected {1})", methodParameters[0].ParameterType.FullName, this.eventType.FullName);
                }
            }

            public bool AddController(IController controller)
            {
                // Check the controller's target type
                if (!this.controllerType.IsAssignableFrom(controller.GetType()))
                {
                    return false;
                }

                // Find if the controller implements this event
                var controllerType = controller.GetType();
                MethodInfo controllerMethod;
                try
                {
                    controllerMethod = controllerType.GetMethod(this.methodName, new[] { typeof(TEventArgs) });
                }
                catch (Exception e)
                {
                    // TODO: Add details
                    Log.Error("Error retrieving handler method for controller: {0}", e);
                    return false;
                }

                // Controller does not implement a handler for this event
                if (controllerMethod == null) return false;

                // Don't add handlers for methods that have not been overridden
                if (controllerMethod == controllerMethod.GetBaseDefinition())
                {
                    return false;
                }

                // Create a delegate for invoking the event on this controller instance
                ControllerEventHandler handler;
                try
                {
                    handler = (ControllerEventHandler)Delegate.CreateDelegate(typeof(ControllerEventHandler), controller, controllerMethod);
                }
                catch (Exception e)
                {
                    Log.Error("Error binding controller handler: {0}", e);
                    return false;
                }

                // Add a handler list for this controller if not already present
                var targetId = controller.Target.Id;
                if (!this.handlers.ContainsKey(targetId))
                {
                    this.handlers.Add(targetId, new List<ControllerEventHandler>());
                }

                // Add the handler to the list
                this.handlers[targetId].Add(handler);
                return true;
            }

            /// <summary>Removes all handlers for the specified controller target.</summary>
            /// <returns>True, if controller was removed; otherwise, false.</returns>
            public bool RemoveControllerTarget(RuntimeId targetId)
            {
                return this.handlers.Remove(targetId);
            }

            /// <summary>Send an event to all controllers in the set</summary>
            /// <param name="e">Event data</param>
            public void SendEvent(EventArgs e)
            {
                if (e == null) throw new ArgumentNullException("e", "EventArgs must not be null");

#if DEBUG
                if (!this.eventType.IsAssignableFrom(e.GetType()))
                {
                    throw new ArgumentException("Incorrect event type. Was: {0}, Expecting: {1}".FormatInvariant(e.GetType().FullName, this.eventType.FullName), "e");
                }
#endif
                var eventArgs = (TEventArgs)e;
                foreach (var controllers in this.handlers.Values)
                {
                    foreach (var handler in controllers)
                    {
                        handler(eventArgs);
                    }
                }
            }

            /// <summary>Send an event to a specific controller</summary>
            /// <param name="targetId">Runtime identifier of the controller's target</param>
            /// <param name="e">Event data</param>
            public void SendEvent(RuntimeId targetId, EventArgs e)
            {
#if DEBUG
                if (!this.eventType.IsAssignableFrom(e.GetType()))
                {
                    throw new ArgumentException("Incorrect event type. Was: {0}, Expecting: {1}".FormatInvariant(e.GetType().FullName, this.eventType.FullName), "e");
                }
#endif
                if (!this.handlers.ContainsKey(targetId))
                {
#if LOG_VERBOSE
                                    /*
                    Log.Trace(
                        "SendEvent<{0}> - Controller not found: '{1}'\nControllers:\n",
                        this.EventType.FullName,
                        targetId,
                        this.Controllers.Keys);
                     */
#endif
                     return;
                }

                var eventArgs = (TEventArgs)e;
                foreach (var handler in this.handlers[targetId])
                {
                    handler(eventArgs);
                }
            }
        }
    }
}
