//-----------------------------------------------------------------------
// <copyright file="AvatarPrefabResourceBehaviour.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace Unity.Resources.Behaviours
{
    /// <summary>Behaviour for Avatar Prefab Resources</summary>
    [AddComponentMenu("Banjo/Resources/Avatar Prefab Resource")]
    [RequireComponent(typeof(Animator))]
    [SuppressMessage("Microsoft.StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "MonoBehaviour")]
    public class AvatarPrefabResourceBehaviour : PrefabResourceBehaviour, IResourceBehaviour<PrefabResource>
    {
        /// <summary>Backing field for Resource</summary>
        private PrefabResource resource;
        
        /// <summary>Gets the avatar prefab resource</summary>
        public new PrefabResource Resource
        {
            get { return this.resource ?? (this.resource = new PrefabResource(this.gameObject)); }
        }
    }
}
