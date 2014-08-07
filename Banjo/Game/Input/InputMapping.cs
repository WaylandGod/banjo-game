//-----------------------------------------------------------------------
// <copyright file="InputMapping.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;

namespace Game.Input
{
    /// <summary>Generic input mapping base class</summary>
    /// <typeparam>Type of the input values</typeparam>
    public abstract class InputMapping<TValue> : IInputMapping
    {
        /// <summary>Whether or not the mapped input is currently active</summary>
        private bool active;

        /// <summary>Initializes a new instance of the InputMapping class</summary>
        /// <param name="mappingName">Name of the mapping</param>
        /// <param name="inputName">Name of the input checked</param>
        protected InputMapping(string mappingName, string inputName)
        {
            this.Name = mappingName;
            this.InputName = inputName;
        }

        /// <summary>Gets the mapping name</summary>
        public string Name { get; private set; }

        /// <summary>Gets the name of the input</summary>
        protected string InputName { get; private set; }

        /// <summary>Gets or sets the last value</summary>
        protected TValue LastValue { get; set; }

        protected bool WasActive { get { return !this.LastValue.Equals(default(TValue)); } }

        /// <summary>Checks the input</summary>
        /// <param name="source">The input source to be checked.</param>
        /// <param name="value">The current value, if any; otherwise null.</param>
        /// <returns>The current InputPhase, if any; Otherwise, InputPhase.Unknown.</returns>
        InputPhase IInputMapping.CheckInput(IInputSource source, out object value)
        {
            TValue typedValue;
            var phase = this.CheckInput(source, out typedValue);
            value = (object)typedValue;
            return phase;
        }

        /// <summary>Checks the raw input and gets the mapped input event (if any)</summary>
        /// <param name="source">The input source to be checked.</param>
        /// <param name="value">The input value, if any. Otherwise, the default for the type.</param>
        /// <returns>The current InputPhase, if the input has a value; otherwise, InputPhase.Unknown.</returns>
        public InputPhase CheckInput(IInputSource source, out TValue value)
        {
            if (!this.TryGetInputValue(source, out value)) return InputPhase.Unknown;
            this.active = !value.Equals(default(TValue));
            var phase =
                !this.WasActive && this.active ? InputPhase.Begin :
                this.WasActive && this.active ? InputPhase.Continue :
                this.WasActive && !this.active ? InputPhase.End :
                InputPhase.Unknown;
            this.LastValue = value;
            return phase;
        }

        /// <summary>Use the hashcode of the InputMapping.Name</summary>
        public override int GetHashCode()
        {
            return this.Name.GetHashCode();
        }

        /// <summary>Gets a string representation of the current InputMapping</summary>
        public override string ToString()
        {
            return string.Format("[InputMapping: Name={0}, InputName={1}, Active={2}]", this.Name, this.InputName, this.active);
        }

        /// <summary>Overriden in derived classes to get an input value</summary>
        /// <param name="value">The input value, if any; Otherwise, the type's default value.</param>
        /// <returns>True if the input has a value; otherwise, false.</returns>
        protected abstract bool TryGetInputValue(IInputSource source, out TValue value);
    }
}
