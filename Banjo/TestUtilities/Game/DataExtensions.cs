//-----------------------------------------------------------------------
// <copyright file="DataExtensions.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using Core.Resources;
using Game.Data;

namespace TestUtilities.Game
{
    /// <summary>Game data related extensions</summary>
    public static class DataExtensions
    {
        /// <summary>Gets a generic string resource containing the text of the serialzied resource</summary>
        /// <typeparam name="TType">Type of resource serialized</typeparam>
        /// <param name="resource">Serializable resource</param>
        /// <returns>Generic string resource</returns>
        public static GenericNativeResource<string> GetTextResource<TType>(this SerializedResource<TType> resource)
            where TType : SerializedResource<TType>
        {
            return new GenericTextResource(resource.Id, resource.ToString());
        }

        /// <summary>Generic text resource</summary>
        public class GenericTextResource : GenericNativeResource<string>, ITextResource
        {
            /// <summary>Initializes a new instance of the GenericTextResource class.</summary>
            /// <param name="id">Resource identifier</param>
            /// <param name="text">Resource text</param>
            public GenericTextResource(string id, string text) : base(id, text) { }

            /// <summary>Gets the resource's text</summary>
            public string Text { get { return this.NativeResource; } }
        }
    }
}
