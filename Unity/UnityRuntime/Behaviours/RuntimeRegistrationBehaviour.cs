//-----------------------------------------------------------------------
// <copyright file="RuntimeRegistrationBehaviour.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using Core;
using Core.DependencyInjection;
using UnityEngine;

namespace Unity.Runtime.Behaviours
{
    /// <summary>Behaviour for Avatar Prefab Resources</summary>
    [AddComponentMenu("Banjo/Runtime/Runtime Registration")]
    public class RuntimeRegistrationBehaviour : MonoBehaviour
    {
        /// <summary>Initializes a new instance of the RuntimeRegistrationBehaviour class</summary>
        public RuntimeRegistrationBehaviour()
        {
            GlobalContainer.Reset();
            UnityDependencyContainerManager.Register();
            Log.Trace("Unity Runtime Dependencies Registered");
        }
    }
}
