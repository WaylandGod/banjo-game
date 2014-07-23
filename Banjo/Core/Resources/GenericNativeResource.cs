//-----------------------------------------------------------------------
// <copyright file="GenericNativeResource.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Core.Resources
{
    /// <summary>Generic resource wrapper</summary>
    /// <typeparam name="TNativeResource">Type of the wrapped resource</typeparam>
    public class GenericNativeResource<TNativeResource> : INativeResource
        where TNativeResource : class
    {
        /// <summary>List of known text types</summary>
        /// <remarks>IResource.IsText will return true for types derived from any of these</remarks>
        internal static readonly Type[] KnownTextTypes = new[] { typeof(string), typeof(System.Text.StringBuilder), typeof(System.IO.StringWriter) };

        /// <summary>Initializes a new instance of the GenericNativeResource class.</summary>
        /// <param name="nativeResource">The native resource</param>
        public GenericNativeResource(TNativeResource nativeResource)
            : this(Guid.NewGuid().ToString(), nativeResource) { }

        /// <summary>Initializes a new instance of the GenericNativeResource class.</summary>
        /// <param name="id">Resource identifier</param>
        /// <param name="nativeResource">The native resource</param>
        [SuppressMessage("Microsoft.Usage", "CA2214", Justification = "Virtual Id setters should be safe")]
        public GenericNativeResource(string id, TNativeResource nativeResource)
        {
            this.Id = id;
            this.NativeResource = nativeResource;
        }

        /// <summary>Gets the type of the native resource</summary>
        public Type NativeType { get { return typeof(TNativeResource); } }

        /// <summary>Gets or sets the identifier</summary>
        public virtual string Id { get; set; }

        /// <summary>Gets a value indicating whether the resource is text</summary>
        public virtual bool IsText
        {
            get { return KnownTextTypes.Any(type => type.IsInstanceOfType(this.NativeResource)); }
        }

        /// <summary>Gets the native resource</summary>
        public TNativeResource NativeResource { get; private set; }

        /// <summary>Gets the native resource</summary>
        object INativeResource.NativeResource { get { return this.NativeResource; } }

        /// <summary>Release the native resource</summary>
        public void Dispose()
        {
            this.Dispose(true);
            this.NativeResource = null;
            GC.SuppressFinalize(this);
        }

        /// <summary>Gets a string representation of the object</summary>
        /// <returns>String representation</returns>
        public override string ToString()
        {
            return this.IsText ? this.NativeResource.ToString() : base.ToString();
        }

        /// <summary>Release the native resources in a derived class</summary>
        /// <param name="disposing">Whether the object is being disposed</param>
        protected virtual void Dispose(bool disposing) { }
    }
}
