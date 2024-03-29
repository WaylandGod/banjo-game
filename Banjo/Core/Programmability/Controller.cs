﻿//-----------------------------------------------------------------------
// <copyright file="Controller.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using Core.DependencyInjection;

namespace Core.Programmability
{
    /// <summary>Base class for controllers</summary>
    public abstract class Controller : IController
    {
        /// <summary>Backing field for ControllerManager</summary>
        private static IControllerManager controllerManager;

        /// <summary>Initializes a new instance of the Controller class</summary>
        /// <param name="target">Target to be controlled</param>
        /// <param name="config">Controller configuration</param>
        public Controller(IControllerTarget target, IConfig config)
        {
            var controllerId =
                this.GetType().GetCustomAttributes(typeof(ControllerAttribute), false)
                    .Cast<ControllerAttribute>()
                    .Select(attr => attr.Id)
                    .FirstOrDefault();
            this.Id = new RuntimeId(controllerId);
            this.Target = target;
            this.Config = config;
        }
        
        /// <summary>Gets the runtime identifier</summary>
        public RuntimeId Id { get; private set; }

        /// <summary>Gets the target object</summary>
        public IControllerTarget Target { get; private set; }

        /// <summary>Gets the configuration</summary>
        public IConfig Config { get; private set; }

        /// <summary>Gets the ControllerManager singleton</summary>
        protected static IControllerManager ControllerManager
        {
            get
            {
                return Controller.controllerManager ??
                    (Controller.controllerManager = GlobalContainer.Resolve<IControllerManager>());
            }
        }

        /// <summary>Dispose of native/managed resources</summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>Gets a string representation of the controller</summary>
        /// <returns>A string representation of the controller</returns>
        public override string ToString()
        {
            return this.Id.ToString();
        }

        /// <summary>Dispose of native/managed resources</summary>
        /// <param name="disposing">Whether the object is being disposed</param>
        protected virtual void Dispose(bool disposing) { }
    }
}
