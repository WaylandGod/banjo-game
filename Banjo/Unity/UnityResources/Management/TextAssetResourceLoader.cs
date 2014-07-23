//-----------------------------------------------------------------------
// <copyright file="TextAssetResourceLoader.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Unity.Resources.Management
{
    /// <summary>Loader for Unity TextAsset resources</summary>
    internal sealed class TextAssetResourceLoader : GenericAssetResourceLoader<TextAssetResource, UnityEngine.TextAsset>
    {
        /// <summary>Supported asset resource extensions</summary>
        public static readonly string[] TextExtensions = new[] { "txt", "xml", "json" };

        /// <summary>Initializes a new instance of the TextAssetResourceLoader class</summary>
        public TextAssetResourceLoader() : base(TextExtensions) { }

        /// <summary>Creates an instance of TAssetResource from a loaded TAsset</summary>
        /// <param name="asset">Loaded Unity asset</param>
        /// <returns>Created asset resource</returns>
        protected override TextAssetResource CreateAssetResource(UnityEngine.TextAsset asset)
        {
            return new TextAssetResource(asset);
        }
    }
}
