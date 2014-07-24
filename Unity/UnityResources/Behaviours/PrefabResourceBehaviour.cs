//-----------------------------------------------------------------------
// <copyright file="PrefabResourceBehaviour.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace Unity.Resources.Behaviours
{
    /// <summary>Behaviour for Prefab Resources</summary>
    [AddComponentMenu("Banjo/Resources/Prefab Resource")]
    [SuppressMessage("Microsoft.StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "MonoBehaviour")]
    public class PrefabResourceBehaviour : MonoBehaviour, IResourceBehaviour<PrefabResource>
    {
        /// <summary>Id (for testing)</summary>
        public string Id;

        /// <summary>Backing field for Resource</summary>
        private PrefabResource resource;
        
        /// <summary>Gets the avatar prefab resource</summary>
        public PrefabResource Resource
        {
            get { return this.resource ?? (this.resource = new PrefabResource(this.gameObject)); }
        }
    }
}
