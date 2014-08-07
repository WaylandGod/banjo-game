//-----------------------------------------------------------------------
// <copyright file="EntityDefinition.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Core.Data;
using Core.Resources;

namespace Game.Data
{
    /// <summary>Definition for entities</summary>
    [DataContract(Namespace = "")]
    public class EntityDefinition : SerializedResource<EntityDefinition>
    {
        /// <summary>Initializes a new instance of the <see cref="Game.Data.EntityDefinition"/> class</summary>
        public EntityDefinition()
        {
            this.Controllers = new ControllerConfig[0];
        }

        /// <summary>Gets or sets the avatar</summary>
        [DataMember(IsRequired = true)]
        public string AvatarId { get; set; }

        /// <summary>Gets or sets the mass</summary>
        [DataMember(IsRequired = true)]
        public double Mass { get; set; }

        /// <summary>Gets or sets the controller configurations</summary>
        [DataMember(IsRequired = true, Order = 0)]
        public ControllerConfig[] Controllers { get; set; }
    }
}
