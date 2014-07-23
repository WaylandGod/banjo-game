//-----------------------------------------------------------------------
// <copyright file="TestAvatarFactory.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using Core.Resources;
using Core.Resources.Management;
using Game;
using Game.Data;
using Game.Factories;

namespace TestUtilities.Game.Factories
{
    /// <summary>Creates implementations of IAvatar</summary>
    public class TestAvatarFactory : AvatarFactoryBase, IAvatarFactory
    {
        /// <summary>Initializes a new instance of the TestAvatarFactory class</summary>
        /// <param name="resources">Resource library</param>
        public TestAvatarFactory(IResourceLibrary resources) : base(resources) { }

        /// <summary>Creates an instance of IAvatar</summary>
        /// <param name="definition">Avatar definition</param>
        /// <returns>The created IAvatar instance</returns>
        protected override IAvatar Create(AvatarDefinition definition)
        {
            return new TestAvatar(
                definition.Id,
                this.Resources.GetResource<IResource>(definition.ResourceId),
                definition.States,
                definition.DefaultState);
        }
    }
}
