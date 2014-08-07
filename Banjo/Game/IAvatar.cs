//-----------------------------------------------------------------------
// <copyright file="IAvatar.cs" company="Benjamin Woodall">
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
using Core;
using Core.Resources;

namespace Game
{
    /// <summary>Visual representation of an entity</summary>
    /// <remarks>Avatars encapsulate a displayable object with animations, etc.</remarks>
    public interface IAvatar : IDisposable
    {
        /// <summary>Gets the runtime identifier</summary>
        RuntimeId Id { get; }

        /// <summary>Gets the underlying resource</summary>
        IResource Resource { get; }

        /// <summary>Gets the avatar's possible states</summary>
        IDictionary<string, AvatarState> States { get; }

        /// <summary>Gets or sets the current state</summary>
        AvatarState CurrentState { get; set; }

        /// <summary>Gets or sets a value indicating whether the object is visible</summary>
        bool Visible { get; set; }

        /// <summary>Gets or sets the position</summary>
        Vector3D Position { get; set; }

        /// <summary>Gets or sets the direction as a vector of euler angles</summary>
        Vector3D Direction { get; set; }

        /// <summary>Gets the clipping distance</summary>
        float ClippingDistance { get; }
    }
}
