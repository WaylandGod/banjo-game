//-----------------------------------------------------------------------
// <copyright file="InputManager.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using Core.Programmability;
using Game.Programmability;

namespace Game.Input
{
    public delegate void MappedInputHandler(string name, InputPhase phase, object value);

    /// <summary>Input manager</summary>
    /// <remarks>Monitors the input sources' inputs and send input events.</remarks>
    public class InputManager : IInputManager
    {
        /// <summary>List of input mappings</summary>
        private readonly IDictionary<IInputMapping, IDictionary<RuntimeId, MappedInputHandler>> inputMappings =
            new Dictionary<IInputMapping, IDictionary<RuntimeId, MappedInputHandler>>();

        /// <summary>Input sources</summary>
        private readonly IInputSource[] inputSources;

        /// <summary>Initializes a new instance of the InputManager class</summary>
        /// <param name="controllerManager">Controller manager</param>
        public InputManager(IInputSource[] inputSources)
        {
            this.inputSources = inputSources;
        }

        /// <summary>Adds a mapped input handler</summary>
        /// <param name="mapping">Input mapping.</param>
        /// <param name="id">Identifier (used to later remove).</param>
        /// <param name="handler">Handler to add.</param>
        public void AddMappedInputHandler(IInputMapping mapping, RuntimeId id, MappedInputHandler handler)
        {
            lock (this.inputMappings)
            {
                if (!this.inputMappings.ContainsKey(mapping))
                {
                    this.inputMappings.Add(mapping, new Dictionary<RuntimeId, MappedInputHandler>());
                }

                this.inputMappings[mapping][id] = handler;
            }
        }

        /// <summary>Removes a mapped input handler</summary>
        /// <param name="mapping">Input mapping.</param>
        /// <param name="id">Identifier of the handler to remove.</param>
        /// <returns>True if the input was found and removed; otherwise, false.</returns>
        public bool RemoveMappedInputHandler(IInputMapping mapping, RuntimeId id)
        {
            lock (this.inputMappings)
            {
                if (!this.inputMappings.ContainsKey(mapping))
                {
                    return false;
                }

                return this.inputMappings[mapping].Remove(id);
            }
        }

        /// <summary>Updates all mapped inputs</summary>
        public void Update()
        {
            foreach (var mapping in this.inputMappings)
            {
                foreach (var source in this.inputSources)
                {
                    object value;
                    var phase = mapping.Key.CheckInput(source, out value);
                    if (phase == InputPhase.Unknown) continue;
                    foreach (var handler in mapping.Value.Values)
                    {
                        handler(mapping.Key.Name, phase, value);
                    }
                }
            }
        }
    }
}
