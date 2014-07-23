//-----------------------------------------------------------------------
// <copyright file="TestAvatar.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System.Collections.Generic;
using Core;
using Core.Resources;
using Game;

namespace TestUtilities.Game
{
    /// <summary>Test IAvatar implementation</summary>
    public sealed class TestAvatar : AvatarBase, IAvatar
    {
        /// <summary>Initializes a new instance of the TestAvatar class</summary>
        /// <param name="id">Avatar identifier</param>
        /// <param name="resource">Avatar resource</param>
        /// <param name="states">List of available states</param>
        /// <param name="defaultState">Default state</param>
        public TestAvatar(string id, IResource resource, AvatarState[] states, string defaultState)
            : base(id, resource, states)
        {
            this.CurrentState = this.States[defaultState];
            this.Visible = true;
        }
    
        /// <summary>Gets the clipping distance</summary>
        public override float ClippingDistance
        {
            get { throw new System.NotImplementedException(); }
        }
    }
}
