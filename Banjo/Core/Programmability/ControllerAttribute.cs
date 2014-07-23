//-----------------------------------------------------------------------
// <copyright file="ControllerAttribute.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;

namespace Core.Programmability
{
    /// <summary>Attribute for Controllers</summary>
    /// <remarks>Used to specify controller ids, etc</remarks>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class ControllerAttribute : Attribute
    {
        /// <summary>Initializes a new instance of the ControllerAttribute class</summary>
        /// <param name="id">Controller identifier</param>
        public ControllerAttribute(string id)
        {
            this.Id = id;
        }

        /// <summary>Gets the controller identifier</summary>
        public string Id { get; private set; }
    }
}
