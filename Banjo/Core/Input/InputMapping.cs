//-----------------------------------------------------------------------
// <copyright file="InputMapping.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;

namespace Core.Input
{
    /// <summary>Mapping for raw input events to mapped input events</summary>
    public class InputMapping
    {
        /// <summary>Input event mapping name</summary>
        public readonly string Name;

        /// <summary>Predecate which check whether the mapped input is active</summary>
        public readonly Func<RawInputEventArgs, bool> Predicate;

        /// <summary>Function which translates raw input events</summary>
        public readonly Func<RawInputEventArgs, MappedInputEventArgs> Mapping;

        /// <summary>Whether or not the mapped input is currently active</summary>
        private bool active;

        /// <summary>Initializes a new instance of the InputMapping class</summary>
        /// <param name="name">Mapping name</param>
        /// <param name="predicate">Raw input check predicate</param>
        /// <param name="mapping">Raw to mapped input mapping</param>
        private InputMapping(
            string name,
            Func<RawInputEventArgs, bool> predicate,
            Func<RawInputEventArgs, MappedInputEventArgs> mapping)
        {
            this.Name = name;
            this.Predicate = predicate;
            this.Mapping = mapping;
        }

        /// <summary>Creates a new instance of the InputMapping class</summary>
        /// <typeparam name="TMappedInputEventArgs">Type of the mapped input event</typeparam>
        /// <param name="name">Mapping name</param>
        /// <param name="predicate">Raw input check predicate</param>
        /// <param name="mapping">Raw to mapped input mapping</param>
        /// <returns>Created InputMapping</returns>
        public static InputMapping Create<TMappedInputEventArgs>(
            string name,
            Func<RawInputEventArgs, bool> predicate,
            Func<RawInputEventArgs, TMappedInputEventArgs> mapping)
            where TMappedInputEventArgs : MappedInputEventArgs
        {
            return new InputMapping(name, predicate, e => mapping(e));
        }

        /// <summary>Checks the raw input and gets the mapped input event (if any)</summary>
        /// <param name="e">Raw input event args</param>
        /// <returns>Mapped input event, if any; otherwise, false</returns>
        public MappedInputEventArgs CheckInput(RawInputEventArgs e)
        {
            var wasActive = this.active;
            this.active = this.Predicate(e);

            if (!this.active)
            {
                return null;
            }

            var mappedInputEventArgs = this.Mapping(e);
            mappedInputEventArgs.Phase =
                !wasActive && this.active ? InputPhase.Begin :
                wasActive && this.active ? InputPhase.Continue :
                wasActive && !this.active ? InputPhase.End :
                InputPhase.Unknown;
            return mappedInputEventArgs;
        }
    }
}
