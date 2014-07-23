//-----------------------------------------------------------------------
// <copyright file="DictionaryConfig.cs" company="Benjamin Woodall">
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

using System.Collections.Generic;
using System.Linq;

namespace Core
{
    /// <summary>Dictionary backed configuration</summary>
    public class DictionaryConfig : Config, IConfig
    {
        /// <summary>Values dictionary</summary>
        private readonly IDictionary<string, string> values;

        /// <summary>Initializes a new instance of the DictionaryConfig class</summary>
        /// <param name="values">Initial values</param>
        public DictionaryConfig(IDictionary<string, string> values)
        {
            this.values = values != null ?
                new Dictionary<string, string>(values) :
                new Dictionary<string, string>();
        }

        /// <summary>Gets the keys</summary>
        public override IEnumerable<string> Keys { get { return this.values.Keys; } }

        /// <summary>Merges two configs to create a new config</summary>
        /// <remarks>
        /// Values in the provided config will override
        /// any existing values in the current config.
        /// </remarks>
        /// <param name="config">Config to merge</param>
        /// <returns>The new, merged config</returns>
        public override IConfig Merge(IConfig config)
        {
            var merged = new DictionaryConfig(this.values);
            foreach (var key in config.Keys.ToArray())
            {
                merged.values[key] = config.GetValue(key);
            }

            return merged;
        }

        /// <summary>Checks whether a value exists</summary>
        /// <param name="key">Value key</param>
        /// <returns>True if the value exists; otherwise, false</returns>
        public override bool HasValue(string key)
        {
            return this.values.ContainsKey(key);
        }

        /// <summary>Gets a value</summary>
        /// <param name="key">Value key</param>
        /// <returns>The value, if it exists; otherwise, null</returns>
        public override string GetValue(string key)
        {
            return this.HasValue(key) ? this.values[key] : null;
        }

        /// <summary>Gets a string representation of the config</summary>
        /// <returns>A string representation of the config</returns>
        public override string ToString()
        {
            return string.Join("; ", this.values.Select(kvp => "{0} = '{1}'".FormatInvariant(kvp.Key, kvp.Value)).ToArray());
        }
    }
}
