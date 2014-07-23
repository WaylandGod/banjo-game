//-----------------------------------------------------------------------
// <copyright file="IInputManager.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Core.Input
{
    /// <summary>Describes an input manager</summary>
    /// <remarks>Input managers monitor the platform's inputs and dispatch input events.</remarks>
    public interface IInputManager
    {
        /// <summary>Updates with the latest input states</summary>
        void Update();
    }
}
