//-----------------------------------------------------------------------
// <copyright file="Objective.cs" company="Benjamin Woodall">
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
using System.Linq;
using Core;

namespace Game.Programmability
{
    /// <summary>Base class for objectives</summary>
    public abstract class Objective : IObjective, IDisposable
    {
        /// <summary>Initializes a new instance of the <see cref="Game.Objective"/> class.</summary>
        protected Objective(IWorld world, IConfig config)
        {
            this.Id = this.GetType().GetCustomAttributes(typeof(ObjectiveAttribute), false)
                    .Cast<ObjectiveAttribute>()
                    .Select(attr => attr.Id)
                    .FirstOrDefault();
            if (this.Id == null)
            {
                Log.Error("Missing required Objective attribute");
                this.Id = string.Empty;
            }

            this.World = world;
            this.Config = config;
        }

        /// <summary>Gets the identifier</summary>
        public string Id { get; private set; }

        /// <summary>Gets a value indicating whether the objective is required</summary>
        public abstract bool Required { get; }

        /// <summary>Gets a value indicating whether the objective has been completed</summary>
        /// <remarks>When true the game should exit play and enter level results UI</remarks>
        public abstract bool Completed { get; }

        /// <summary>Gets a value indicating whether the objective has failed</summary>
        /// <remarks>When true the game should reset the level</remarks>
        public abstract bool Failed { get; }

        /// <summary>Gets the world instance</summary>
        protected IWorld World { get; private set; }

        /// <summary>Gets the configuration</summary>
        protected IConfig Config { get; private set; }

        /// <summary>Dispose of native/managed resources</summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>Gets a string representation of the controller</summary>
        /// <returns>A string representation of the controller</returns>
        public override string ToString()
        {
            return this.Id.ToString();
        }

        /// <summary>Dispose of native/managed resources</summary>
        /// <param name="disposing">Whether the object is being disposed</param>
        protected virtual void Dispose(bool disposing) { }
    }
}

