//-----------------------------------------------------------------------
// <copyright file="GenericAssetResource.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using Core.Resources;

namespace Unity.Resources
{
    /// <summary>Unity resource base class</summary>
    /// <typeparam name="TAsset">Type of the Unity asset</typeparam>
    public class GenericAssetResource<TAsset> : GenericNativeResource<TAsset>
        where TAsset : UnityEngine.Object
    {
        /// <summary>Backing field for Id</summary>
        private string id;

        /// <summary>Initializes a new instance of the GenericAssetResource class.</summary>
        /// <param name="asset">The Unity Asset (loaded using UnityEngine.Resources.Load)</param>
        /// <seealso cref="UnityEngine.Resources"/>
        public GenericAssetResource(TAsset asset) : base(asset)
        {
            this.id = asset.name;
        }

        /// <summary>Gets the asset's name</summary>
        public override string Id { get { return this.id; } }

        /// <summary>Releases native Unity resources</summary>
        /// <param name="disposing">Whether the object is being disposed</param>
        protected override void Dispose(bool disposing)
        {
            UnityEngine.Object.DestroyImmediate(this.NativeResource);
        }
    }
}
