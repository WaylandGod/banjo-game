//-----------------------------------------------------------------------
// <copyright file="Extensions.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Core.Resources.Management
{
    /// <summary>Resource management extensions</summary>
    public static class Extensions
    {
        /// <summary>Gets the text representation of a resource</summary>
        /// <typeparam name="TResource">Serialized resource type</typeparam>
        /// <param name="this">The resource library</param>
        /// <param name="id">Id for the resource</param>
        /// <returns>Text representation of the resource</returns>
        public static TResource GetSerializedResource<TResource>(this IResourceLibrary @this, string id)
            where TResource : SerializedResource<TResource>
        {
            return SerializedResource<TResource>.FromString(@this.GetTextResource(id));
        }

        /// <summary>Gets the text representation of a resource</summary>
        /// <param name="this">The resource library</param>
        /// <param name="id">Id for the resource</param>
        /// <returns>Text representation of the resource</returns>
        public static string GetTextResource(this IResourceLibrary @this, string id)
        {
            return @this.GetResource<ITextResource>(id).Text;
        }
    }
}
