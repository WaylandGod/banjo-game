//-----------------------------------------------------------------------
// <copyright file="EntityStateCheck.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;

namespace Game.Programmability
{
    /// <summary>Used to check whether an entity should be set to a specified state</summary>
    public struct EntityStateCheck
    {
        /// <summary>The applicable state</summary>
        public readonly string State;

        /// <summary>Condition for when to apply the state</summary>
        public readonly Func<IEntity, bool> Condition;

        /// <summary>Initializes a new instance of the EntityStateCheck struct</summary>
        /// <param name="state">The state</param>
        /// <param name="condition">Condition for the state</param>
        public EntityStateCheck(string state, Func<IEntity, bool> condition)
        {
            this.State = state;
            this.Condition = condition;
        }
    }
}
