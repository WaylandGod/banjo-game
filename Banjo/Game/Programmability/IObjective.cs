//-----------------------------------------------------------------------
// <copyright file="IObjective.cs" company="Benjamin Woodall">
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

namespace Game.Programmability
{
    /// <summary>Describes the level objective and current state</summary>
    public interface IObjective
    {
        /// <summary>Gets the identifier</summary>
        string Id { get; }

        /// <summary>Gets a value indicating whether the objective is required</summary>
        bool Required { get; }

        /// <summary>Gets a value indicating whether the objective has been completed</summary>
        /// <remarks>When true the game should exit play and enter level results UI</remarks>
        bool Completed { get; }

        /// <summary>Gets a value indicating whether the objective has failed</summary>
        /// <remarks>When true the game should reset the level</remarks>
        bool Failed { get; }
    }
}