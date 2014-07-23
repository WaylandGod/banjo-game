//-----------------------------------------------------------------------
// <copyright file="ObjectBase.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Core;
using Game.Data;
using Game.Factories;
using Game.Programmability;

namespace Game
{
    /// <summary>Base class for IObject implementations</summary>
    public abstract class ObjectBase : IObject
    {
        /// <summary>Backing field for Mass</summary>
        private float? mass;

        /// <summary>Initializes a new instance of the ObjectBase class</summary>
        /// <param name="avatarDefinition">Definition of the avatar that will represent the object</param>
        /// <param name="material">Material of the object</param>
        /// <param name="volume">Volume of the object</param>
        [SuppressMessage("Microsoft.Usage", "CA2214", Justification = "Call to virtuals should be safe.")]
        protected ObjectBase(AvatarDefinition avatarDefinition, Material material, float volume)
        {
            this.Id = new RuntimeId(avatarDefinition.Id, this.GetType());
            this.AvatarDefinition = avatarDefinition;
            this.Material = material;
            this.Volume = volume;
        }

        /// <summary>Gets the runtime identifier</summary>
        public RuntimeId Id { get; private set; }

        /// <summary>Gets the definition of the avatar representing the tile</summary>
        public virtual AvatarDefinition AvatarDefinition { get; private set; }

        /// <summary>Gets the material of the tile</summary>
        public virtual Material Material { get; private set; }

        /// <summary>Gets the volume</summary>
        public virtual float Volume { get; private set; }

        /// <summary>Gets the mass of the object</summary>
        public float Mass
        {
            get { return (this.mass ?? (this.mass = this.Volume * this.Material.Mass)).Value; }
        }

        /// <summary>Gets a string representation of the object</summary>
        /// <returns>A string representation of the object</returns>
        public override string ToString()
        {
            return this.Id.ToString();
        }
    }
}
