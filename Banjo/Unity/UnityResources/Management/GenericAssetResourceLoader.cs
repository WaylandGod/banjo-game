//-----------------------------------------------------------------------
// <copyright file="GenericAssetResourceLoader.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using Core;
using Core.Resources;
using Core.Resources.Management;

namespace Unity.Resources.Management
{
    /// <summary>Loader for Unity Asset resources</summary>
    /// <typeparam name="TAssetResource">Type of unity asset resources loaded</typeparam>
    /// <typeparam name="TAsset">Type of unity assets loaded</typeparam>
    internal abstract class GenericAssetResourceLoader<TAssetResource, TAsset> : GenericResourceLoader<TAssetResource>
        where TAssetResource : GenericAssetResource<TAsset>
        where TAsset : UnityEngine.Object
    {
        /// <summary>Scheme for unity asset resources</summary>
        public const string AssetScheme = "asset";

        /// <summary>Initializes a new instance of the GenericAssetResourceLoader class</summary>
        /// <param name="extensions">Resource extensions handled by the loader</param>
        protected GenericAssetResourceLoader(string[] extensions)
            : base(AssetScheme, extensions)
        {
        }

        /// <summary>Prevents a default instance of the GenericAssetResourceLoader class from being created</summary>
        private GenericAssetResourceLoader() : this(null) { }

        /// <summary>Creates an instance of TAssetResource from a loaded TAsset</summary>
        /// <param name="asset">Loaded Unity asset</param>
        /// <returns>Created asset resource</returns>
        protected abstract TAssetResource CreateAssetResource(TAsset asset);

        /// <summary>Loads the resource for the identifier</summary>
        /// <param name="identifier">Resource identifier</param>
        /// <returns>The loaded resource</returns>
        protected override TAssetResource LoadGenericResource(string identifier)
        {
            var resourcePath = identifier.Substring(0, identifier.LastIndexOf('.'));
            var asset = UnityEngine.Resources.Load(resourcePath, typeof(TAsset));
            if (asset == null)
            {
                Log.Error("Unable to load resource '{0}' ({1})", resourcePath, typeof(TAsset).FullName);
            }
            else
            {
                Log.Trace("Loaded {0} '{1}' as {2}", asset.GetType().FullName, identifier, typeof(TAssetResource).FullName);
            }

            return this.CreateAssetResource(asset as TAsset);
        }
    }
}
