//-----------------------------------------------------------------------
// <copyright file="EventHandlerAttribute.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;

namespace Core.Programmability
{
    /// <summary>Attribute for Controllers</summary>
    /// <remarks>Used to specify controller ids, etc</remarks>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class EventHandlerAttribute : Attribute { }
}
