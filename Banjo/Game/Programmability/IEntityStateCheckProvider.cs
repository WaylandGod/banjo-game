//-----------------------------------------------------------------------
// <copyright file="IEntityStateCheckProvider.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using Core.Programmability;

namespace Game.Programmability
{
    /// <summary>Interface for providers of entity state checks</summary>
    /// <seealso cref="Game.Programmability.EntityStateController"/>
    public interface IEntityStateCheckProvider : IController
    {
        /// <summary>Gets the priority of the provided checks</summary>
        int EntityStatePriority { get; }

        /// <summary>Gets the list of entity state checks</summary>
        EntityStateCheck[] EntityStateChecks { get; }
    }
}
