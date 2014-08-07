//-----------------------------------------------------------------------
// <copyright file="IControllerManager.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace Core.Programmability
{
    /// <summary>Interface for the manager of all controllers</summary>
    public interface IControllerManager
    {
        /// <summary>Adds an event handler which will later be searched for in IController implementations</summary>
        /// <typeparam name="TController">Type of the declaring controller</typeparam>
        /// <typeparam name="TEventArgs">Type of the event</typeparam>
        /// <param name="eventName">Named used to send events to this handler</param>
        /// <param name="handlerName">Name of the TController method to invoke</param>
        void AddEventHandler<TController, TEventArgs>(string eventName, string handlerName)
            where TController : IController
            where TEventArgs : EventArgs;
        
        /// <summary>Adds a controller</summary>
        /// <param name="controller">The controller</param>
        void AddController(IController controller);

        /// <summary>Sends the event to all controllers implementing the named event handler</summary>
        /// <typeparam name="TEventArgs">Type of the EventArgs</typeparam>
        /// <param name="eventHandler">Name of the event</param>
        /// <param name="e">Data for the event</param>
        void SendEvent<TEventArgs>(string eventName, TEventArgs e) where TEventArgs : EventArgs;

        /// <summary>Sends the event to controllers implementing the named event handler for the specified target</summary>
        /// <typeparam name="TEventArgs">Type of the EventArgs</typeparam>
        /// <param name="eventHandler">Name of the event</param>
        /// <param name="targetId">Runtime identifier of the controller target</param>
        /// <param name="e">Data for the event</param>
        void SendEvent<TEventArgs>(string eventName, RuntimeId targetId, TEventArgs e) where TEventArgs : EventArgs;
    }
}
