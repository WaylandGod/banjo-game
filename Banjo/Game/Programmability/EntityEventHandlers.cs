//-----------------------------------------------------------------------
// <copyright file="EntityEventHandlers.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using Core.Input;
using Core.Programmability;

namespace Game.Programmability
{
    /// <summary>Entity controller event handler names</summary>
    public static class EntityEventHandlers
    {
        /// <summary>OnUpdate event handler name</summary>
        public const string OnUpdate = "OnUpdate";

        /// <summary>OnInput event handler name</summary>
        public const string OnInput = "OnInput";

        /// <summary>OnCollisionEnter event handler name</summary>
        public const string OnCollisionEnter = "OnCollisionEnter";

        /// <summary>OnCollisionContinue event handler name</summary>
        public const string OnCollisionContinue = "OnCollisionContinue";

        /// <summary>OnCollisionExit event handler name</summary>
        public const string OnCollisionExit = "OnCollisionExit";
    }
}
