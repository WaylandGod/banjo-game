//-----------------------------------------------------------------------
// <copyright file="TextAssetResource.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using Core.Resources;

namespace Unity.Resources
{
    /// <summary>Unity resource base class</summary>
    public class TextAssetResource : GenericAssetResource<UnityEngine.TextAsset>, ITextResource
    {
        /// <summary>Initializes a new instance of the TextAssetResource class</summary>
        /// <param name="asset">Unity TextAsset</param>
        public TextAssetResource(UnityEngine.TextAsset asset) : base(asset) { }

        /// <summary>Gets the resource's text</summary>
        public string Text { get { return this.NativeResource.text; } }
    }
}
