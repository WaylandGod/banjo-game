//-----------------------------------------------------------------------
// <copyright file="ObjectBehaviour.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using Core.DependencyInjection;
using Core.Programmability;
using Game;
using UnityEngine;

namespace Game.Unity.Behaviours
{
    /// <summary>Base behaviour for entity and tile instance game objects</summary>
    public abstract class ObjectBehaviour : MonoBehaviour
    {
        /// <summary>Backing field for ControllerManager</summary>
        private static IControllerManager controllerManager;

        /// <summary>Gets or sets the IObject corresponding to the GameObject</summary>
        public IObject Object { get; protected set; }

        /// <summary>Gets the controller manager</summary>
        protected static IControllerManager ControllerManager
        {
            get
            {
                return controllerManager ?? (controllerManager = GlobalContainer.Resolve<IControllerManager>());
            }
        }
    }
}
