//-----------------------------------------------------------------------
// <copyright file="IControllerTarget.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Core.Programmability
{
    /// <summary>Represents a target for the controllers</summary>
    /// <typeparam name="TController">Type of targetting controller</typeparam>
    public interface IControllerTarget
    {
        /// <summary>Gets the runtime identifier</summary>
        RuntimeId Id { get; }
    }
}
