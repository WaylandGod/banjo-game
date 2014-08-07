//-----------------------------------------------------------------------
// <copyright file="ObjectBase.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Core;
using Core.Data;
using Core.Factories;
using Core.Programmability;
using Game.Data;
using Game.Factories;
using Game.Programmability;

namespace Game
{
    /// <summary>Base class for IObject implementations</summary>
    public abstract class ObjectBase<TController> : ControllerTarget<TController>, IObject
        where TController : IController
    {
        /// <summary>Initializes a new instance of the ObjectBase class</summary>
        /// <param name="avatarDefinition">Definition of the avatar that will represent the object</param>
        /// <param name="mass">Mass of the object</param>
        /// <param name="controllerFactories">Controller factories</param>
        /// <param name="controllers">Controller configurations</param>
        /// <param name="customControllers">Additional controllers and overrides</param>
        [SuppressMessage("Microsoft.Usage", "CA2214", Justification = "Call to virtuals should be safe.")]
        protected ObjectBase(
            AvatarDefinition avatarDefinition,
            double mass,
            IControllerFactory[] controllerFactories,
            ControllerConfig[] builtInControllers,
            ControllerConfig[] customControllers)
        : base(avatarDefinition.Id, controllerFactories, builtInControllers, customControllers)
        {
            this.AvatarDefinition = avatarDefinition;
            this.Mass = mass;
        }

        /// <summary>Gets the definition of the avatar representing the tile</summary>
        public virtual AvatarDefinition AvatarDefinition { get; private set; }

        /// <summary>Gets or sets the mass</summary>
        public virtual double Mass { get; set; }

        /// <summary>Gets a string representation of the object</summary>
        /// <returns>A string representation of the object</returns>
        public override string ToString()
        {
            return this.Id.ToString();
        }
    }
}
