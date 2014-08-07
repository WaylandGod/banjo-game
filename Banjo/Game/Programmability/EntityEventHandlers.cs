//-----------------------------------------------------------------------
// <copyright file="EntityEventHandlers.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Game.Programmability
{
    /// <summary>Entity controller event handler names</summary>
    public static class EntityEventHandlers
    {
        public const string OnStart = "Entity.OnStart";
        public const string OnUpdate = "Entity.OnUpdate";
        public const string OnInput = "Entity.OnInput";
        public const string OnCollisionEnter = "Entity.OnCollisionEnter";
        public const string OnCollisionContinue = "Entity.OnCollisionContinue";
        public const string OnCollisionExit = "Entity.OnCollisionExit";
        public const string OnDrawUI = "Entity.OnDrawUI";
    }
}
