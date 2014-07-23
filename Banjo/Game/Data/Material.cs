//-----------------------------------------------------------------------
// <copyright file="Material.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System.Runtime.Serialization;
using Core.Data;
using Core.Resources;

namespace Game.Data
{
    /// <summary>Represents a type of material</summary>
    /// <remarks>A material is used to determine the properties of objects</remarks>
    [DataContract(Namespace = "")]
    public class Material : SerializedResource<Material>
    {
        /// <summary>Gets or sets the element</summary>
        [DataMember]
        public Element Element { get; set; }

        /// <summary>Gets or sets the friction</summary>
        /// <remarks>Valid values are between 0.0f and 1.0f</remarks>
        [DataMember]
        public float Friction { get; set; }

        /// <summary>Gets or sets the mass</summary>
        [DataMember]
        public float Mass { get; set; }

        /// <summary>Gets or sets a value indicating whether it is round</summary>
        /// <remarks>
        /// Round objects can roll over some things and go around corners.
        /// They also get less resistance from friction.
        /// </remarks>
        [DataMember]
        public bool Round { get; set; }

        /// <summary>Gets or sets a value indicating whether it is solid</summary>
        /// <remarks>Solid objects block motion</remarks>
        [DataMember]
        public bool Solid { get; set; }
    }
}
