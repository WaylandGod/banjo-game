//-----------------------------------------------------------------------
// <copyright file="AvatarBase.cs" company="Benjamin Woodall">
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
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Core;
using Core.Resources;
using Game;

namespace Game
{
    /// <summary>Base class for IAvatar implementations</summary>
    public abstract class AvatarBase : IAvatar, IDisposable
    {
        /// <summary>Initializes a new instance of the AvatarBase class</summary>
        /// <param name="id">Avatar identifier</param>
        /// <param name="resource">Underlying resource</param>
        /// <param name="states">List of available states</param>
        public AvatarBase(string id, IResource resource, AvatarState[] states)
        {
            this.Id = new RuntimeId(id);
            this.Resource = resource;
            this.States = states.ToDictionary(state => state.Name, state => state);
        }
        
        /// <summary>Gets the id</summary>
        public RuntimeId Id { get; private set; }

        /// <summary>Gets the underlying resource</summary>
        public IResource Resource { get; private set; }

        /// <summary>Gets the avatar's possible states</summary>
        public IDictionary<string, AvatarState> States { get; private set; }

        /// <summary>Gets or sets the current state</summary>
        public virtual AvatarState CurrentState { get; set; }

        /// <summary>Gets or sets a value indicating whether the object is visible</summary>
        public virtual bool Visible { get; set; }

        /// <summary>Gets or sets the position</summary>
        public virtual Vector3D Position { get; set; }

        /// <summary>Gets or sets the direction</summary>
        public virtual Vector3D Direction { get; set; }

        /// <summary>Gets the clipping distance</summary>
        public abstract float ClippingDistance { get; }

        /// <summary>Dispose of native/managed resources</summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>Gets a string representation of the avatar</summary>
        /// <returns>The string representation</returns>
        public override string ToString()
        {
            return this.Id.ToString();
        }

        /// <summary>Dispose of native/managed resources</summary>
        /// <param name="disposing">Whether the object is being disposed</param>
        protected virtual void Dispose(bool disposing)
        {
            if (this.Resource != null)
            {
                if (this.Resource is IDisposable)
                {
                    ((IDisposable)this.Resource).Dispose();
                }

                this.Resource = null;
            }
        }
    }
}
