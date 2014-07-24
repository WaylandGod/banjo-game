//-----------------------------------------------------------------------
// <copyright file="PrefabAssetResourceLoader.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Unity.Resources.Management
{
    /// <summary>Loader for Unity Prefab (GameObject) resources</summary>
    internal sealed class PrefabAssetResourceLoader : GenericAssetResourceLoader<PrefabResource, UnityEngine.GameObject>
    {
        /// <summary>Supported asset resource extensions</summary>
        public static readonly string[] PrefabExtension = new[] { "prefab" };

        /// <summary>Initializes a new instance of the PrefabAssetResourceLoader class</summary>
        public PrefabAssetResourceLoader() : base(PrefabExtension) { }

        /// <summary>Creates an instance of TAssetResource from a loaded TAsset</summary>
        /// <param name="asset">Loaded Unity asset</param>
        /// <returns>Created asset resource</returns>
        protected override PrefabResource CreateAssetResource(UnityEngine.GameObject asset)
        {
            return new PrefabResource(asset);
        }
    }
}
