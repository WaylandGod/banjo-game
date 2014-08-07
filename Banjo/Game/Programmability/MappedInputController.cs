//-----------------------------------------------------------------------
// <copyright file="MappedInputController.cs" company="Benjamin Woodall">
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
    /// <summary>Base class for controllers with input mappings</summary>
    public abstract class MappedInputController : Controller
    {
        /// <summary>Input manager for registering input mappings</summary>
        private static IInputManager inputManager;

        /// <summary>Initializes a new instance of the MappedInputController class</summary>
        public MappedInputController(IControllerTarget target, IConfig config) : base(target, config)
        {
            foreach (var mapping in this.InputMappings)
            {
                InputManager.AddMappedInputHandler(mapping.Key, this.Id, mapping.Value);
            }
        }

        /// <summary>Gets the controller's input mappings</summary>
        public virtual IDictionary<IInputMapping, MappedInputHandler> InputMappings
        {
            get { return new Dictionary<IInputMapping, MappedInputHandler>(0); }
        }

        /// <summary>Gets the input manager</summary>
        private IInputManager InputManager
        {
            get { return inputManager ?? (inputManager = GlobalContainer.Resolve<IInputManager>()); }
        }

        /// <summary>Dispose of native/managed resources</summary>
        /// <param name="disposing">Whether the object is being disposed</param>
        protected override void Dispose(bool disposing)
        {
            foreach (var mapping in this.InputMappings)
            {
                InputManager.RemoveMappedInputHandler(mapping.Key, this.Id);
            }

            base.Dispose(disposing);
        }
    }
}
