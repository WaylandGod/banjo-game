//-----------------------------------------------------------------------
// <copyright file="IConfig.cs" company="Benjamin Woodall">
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

namespace Core
{
    /// <summary>Interface for configuration classes</summary>
    public interface IConfig
    {
        /// <summary>Gets the keys</summary>
        IEnumerable<string> Keys { get; }

        /// <summary>Merges two configs to create a new config</summary>
        /// <remarks>
        /// Values in the provided config will override
        /// any existing values in the current config.
        /// </remarks>
        /// <param name="config">Config to merge</param>
        /// <returns>Merged config</returns>
        IConfig Merge(IConfig config);

        /// <summary>Checks whether a value exists</summary>
        /// <param name="key">Value key</param>
        /// <returns>True if the value exists; otherwise, false</returns>
        bool HasValue(string key);

        /// <summary>Gets a value</summary>
        /// <param name="key">Value key</param>
        /// <returns>The value, if it exists; otherwise, null</returns>
        string GetValue(string key);

        /// <summary>Gets a value</summary>
        /// <typeparam name="TValue">Value type</typeparam>
        /// <param name="key">Value key</param>
        /// <returns>The value, if it exists; otherwise, the type's default value</returns>
        TValue GetValue<TValue>(string key);

        /// <summary>Gets a value or the default if it doesn't exist</summary>
        /// <typeparam name="TValue">Value type</typeparam>
        /// <param name="key">Value key</param>
        /// <param name="defaultValue">Default value</param>
        /// <returns>The value, if it exists; otherwise, the provided default</returns>
        TValue GetValue<TValue>(string key, TValue defaultValue);

        /// <summary>Gets a value or the default if it doesn't exist</summary>
        /// <typeparam name="TValue">Value type</typeparam>
        /// <param name="key">Value key</param>
        /// <param name="deserializer">Function used to deserialize the value</param>
        /// <returns>The value, if it exists; otherwise, the type's default value</returns>
        TValue GetValue<TValue>(string key, Func<string, TValue> deserializer);
    }
}
