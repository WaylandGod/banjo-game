//-----------------------------------------------------------------------
// <copyright file="RuntimeId.cs" company="Benjamin Woodall">
//  Copyright 2013-2014 Benjamin Woodall
//
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
//
//       http://www.apache.org/licenses/LICENSE-2.0
//
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License.
// </copyright>
//-----------------------------------------------------------------------

using System;
using Core.Resources;

namespace Core
{
    /// <summary>Runtime identifier for runtime objects</summary>
    /// <remarks>For performance, only the RuntimeId.Guid is used for comparisons</remarks>
    public struct RuntimeId
    {
        /// <summary>Runtime globally unique identifier</summary>
        public readonly Guid Guid;

        /// <summary>Runtime type name</summary>
        public readonly string TypeName;

        /// <summary>Resource identifier</summary>
        public readonly string ResourceId;

        /// <summary>Gets the runtime identifier as a string</summary>
        public readonly string AsString;

        /// <summary>Initializes a new instance of the RuntimeId struct</summary>
        /// <param name="resource">Resource from which the runtime instance was created</param>
        public RuntimeId(IResource resource) : this(resource.Id, resource.GetType().FullName) { }

        /// <summary>Initializes a new instance of the RuntimeId struct</summary>
        /// <param name="resourceId">Resource identifier</param>
        public RuntimeId(string resourceId) : this(resourceId, new System.Diagnostics.StackFrame(1).GetMethod().DeclaringType.FullName) { }

        /// <summary>Initializes a new instance of the RuntimeId struct</summary>
        /// <param name="resourceId">Resource identifier</param>
        /// <param name="runtimeType">Runtime type</param>
        public RuntimeId(string resourceId, Type runtimeType) : this(resourceId, runtimeType.FullName) { }

        /// <summary>Initializes a new instance of the RuntimeId struct</summary>
        /// <param name="resourceId">Resource identifier</param>
        /// <param name="typeName">Runtime type name</param>
        private RuntimeId(string resourceId, string typeName)
        {
            this.Guid = System.Guid.NewGuid();
            this.ResourceId = resourceId;
            this.TypeName = typeName;
            this.AsString = "{0}({1})[{2}]".FormatInvariant(this.TypeName, this.ResourceId, this.Guid.ToString("N"));
        }

        /// <summary>Determines whether two RuntimeIds are equal</summary>
        /// <param name="idA">First RuntimeId</param>
        /// <param name="idB">Second RuntimeId</param>
        /// <returns>Whether the RuntimeIds are equal</returns>
        public static bool operator ==(RuntimeId idA, RuntimeId idB) { return idA.Guid == idB.Guid; }

        /// <summary>Determines whether two RuntimeIds are inequal</summary>
        /// <param name="idA">First RuntimeId</param>
        /// <param name="idB">Second RuntimeId</param>
        /// <returns>Whether the RuntimeIds are inequal</returns>
        public static bool operator !=(RuntimeId idA, RuntimeId idB) { return idA.Guid != idB.Guid; }

        /// <summary>Determines whether the specified object is equal to the current RuntimeId</summary>
        /// <param name="obj">Object to compare with the current RuntimeId</param>
        /// <returns>True if the specified object is equal to the current RuntimeId; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            return obj != null && obj.GetType() == typeof(RuntimeId) && ((RuntimeId)obj).Guid == this.Guid;
        }

        /// <summary>Serves as a hash function for the RuntimeId type</summary>
        /// <returns>A hash code for the current RuntimeId</returns>
        public override int GetHashCode()
        {
            return this.Guid.GetHashCode();
        }

        /// <summary>Gets a string representation of the runtime identifier</summary>
        /// <returns>The string representation</returns>
        public override string ToString()
        {
            return this.AsString;
        }
    }
}
