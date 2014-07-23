//-----------------------------------------------------------------------
// <copyright file="InputManager.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using Core.Programmability;

namespace Core.Input
{
    /// <summary>Input manager</summary>
    /// <remarks>Monitors the input sources' inputs and send input events.</remarks>
    public class InputManager : IInputManager
    {
        /// <summary>List of input mappings</summary>
        private static readonly IList<InputMapping> InputMappings = new List<InputMapping>();

        /// <summary>Controller manager</summary>
        private readonly IControllerManager controllerManager;

        /// <summary>Input sources</summary>
        private readonly IInputSource[] inputSources;

        /// <summary>Initializes a new instance of the InputManager class</summary>
        /// <param name="controllerManager">Controller manager</param>
        public InputManager(IControllerManager controllerManager)
        {
            this.controllerManager = controllerManager;

            //// TODO: Add array/IEnumerable ctor arguments to DependencyContainer resolver
            this.inputSources = Core.DependencyInjection.GlobalContainer.ResolveAll<IInputSource>().ToArray();
        }

        /// <summary>Updates with the latest input states</summary>
        public void Update()
        {
            // Raw input
            var rawInput = new RawInputEventArgs(this.inputSources);
            this.controllerManager.SendEvent<InputEventArgs>("OnInput", rawInput);

            // Mapped input
            var mappedInputs = InputMappings
                .Select(m => m.CheckInput(rawInput))
                .Where(e => e != null);
            foreach (var mappedInput in mappedInputs)
            {
                this.controllerManager.SendEvent<InputEventArgs>("OnInput", mappedInput);
            }
        }
    }
}
