//-----------------------------------------------------------------------
// <copyright file="AvatarFactoryBase.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using Core.Factories;
using Core.Resources.Management;
using Game.Data;

namespace Game.Factories
{
    /// <summary>Creates implementations of IAvatar</summary>
    public abstract class AvatarFactoryBase : ResourceFactoryBase<IAvatar>, IAvatarFactory
    {
        /// <summary>Initializes a new instance of the AvatarFactoryBase class</summary>
        /// <param name="resources">Resource library</param>
        public AvatarFactoryBase(IResourceLibrary resources) : base(resources) { }

        /// <summary>Creates an instance of IAvatar</summary>
        /// <param name="definition">Avatar definition</param>
        /// <returns>The created IAvatar instance</returns>
        protected abstract IAvatar Create(AvatarDefinition definition);
    }
}
