//-----------------------------------------------------------------------
// <copyright file="TileDefinition.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System.Runtime.Serialization;
using Core;
using Core.Data;
using Core.Resources;

namespace Game.Data
{
    /// <summary>Definition for tiles</summary>
    [DataContract(Namespace = "")]
    public class TileDefinition : SerializedResource<TileDefinition>
    {
        /// <summary>Gets or sets the avatar</summary>
        [DataMember]
        public string AvatarId { get; set; }

        /// <summary>Gets or sets the material</summary>
        [DataMember]
        public string MaterialId { get; set; }

        /// <summary>Gets or sets the volume</summary>
        [DataMember]
        public float Volume { get; set; }

        /// <summary>Gets or sets the direction (in euler angles)</summary>
        [DataMember]
        public Vector3 Direction { get; set; }
    }
}
