//-----------------------------------------------------------------------
// <copyright file="UnityAvatarFactory.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using Core.Resources.Management;
using Game;
using Game.Data;
using Game.Factories;
using Unity.Resources;
using UnityEngine;

namespace Game.Unity.Factories
{
    /// <summary>Creates implementations of IAvatar</summary>
    public class UnityAvatarFactory : AvatarFactoryBase, IAvatarFactory
    {
        /// <summary>Initializes a new instance of the UnityAvatarFactory class</summary>
        /// <param name="resources">Resource library</param>
        public UnityAvatarFactory(IResourceLibrary resources) : base(resources) { }

        /// <summary>Creates an instance of IAvatar</summary>
        /// <param name="definition">Avatar definition</param>
        /// <returns>The created IAvatar instance</returns>
        protected override IAvatar Create(AvatarDefinition definition)
        {
            return new UnityAvatar(
                definition.Id,
                this.Resources.GetResource<PrefabResource>(definition.ResourceId),
                definition.States,
                definition.DefaultState);
        }
    }
}
