//-----------------------------------------------------------------------
// <copyright file="ObjectiveAttribute.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;

namespace Game
{
    /// <summary>Attribute for Objectives</summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class ObjectiveAttribute : Attribute
    {
        /// <summary>Prevents initialization of default instances of the ObjectiveAttribute class</summary>
        private ObjectiveAttribute() { }

        /// <summary>Initializes a new instance of the <see cref="Game.ObjectiveAttribute"/> class.</summary>
        /// <param name="name">Objective id</param>
        public ObjectiveAttribute(string id)
        {
            this.Id = id;
        }

        /// <summary>Gets the identifier</summary>
        public string Id { get; private set; }
    }
}