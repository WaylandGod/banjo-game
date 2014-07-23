//-----------------------------------------------------------------------
// <copyright file="ResourceLibraryBehaviour.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;
using Core;
using Core.Resources;
using Core.Resources.Management;
using Unity.Resources;
using Unity.Resources.Behaviours;
using UnityEditor;
using UnityEngine;

namespace Unity.Editors.Resources
{
    /// <summary>Behaviour for Avatar Prefab Resources</summary>
    [ExecuteInEditMode]
    [AddComponentMenu("Banjo/Resources/Resource Library")]
    [SuppressMessage("Microsoft.StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "MonoBehaviour")]
    public class ResourceLibraryBehaviour : MonoBehaviour
    {
        /// <summary>Resource Library XML</summary>
        public TextAsset LibraryXml;

        /// <summary>Asset resources</summary>
        public UnityEngine.Object[] Assets;

        /// <summary>Refresh library in editor</summary>
        public void Awake()
        {
            if (Application.isEditor)
            {
                this.Refresh();
            }
        }

        /// <summary>Refresh library in editor</summary>
        public void Update()
        {
            if (Application.isEditor)
            {
                this.Refresh();
            }
        }

        /// <summary>Build the resource library from the assets</summary>
        private void Refresh()
        {            
            var resources = new ResourceLibrary();
            foreach (var asset in this.Assets.Where(a => a != null))
            {
                var uri = "asset:{0}".FormatInvariant(AssetDatabase.GetAssetPath(asset.GetInstanceID()));
                var resource = asset as IResource ?? new GenericAssetResource<UnityEngine.Object>(asset);
                resources.AddResource(uri, resource);
            }

            // Update the library XML
            if (this.LibraryXml != null)
            {
                File.WriteAllText(
                    AssetDatabase.GetAssetPath(this.LibraryXml.GetInstanceID()),
                    resources.ToString());
            }
        }
    }
}