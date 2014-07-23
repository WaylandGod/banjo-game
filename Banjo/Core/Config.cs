//-----------------------------------------------------------------------
// <copyright file="Config.cs" company="Benjamin Woodall">
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
using Core.Data;

namespace Core
{
    /// <summary>Base class for IConfig implementations</summary>
    public abstract class Config : IConfig
    {
        /// <summary>Built-in value deserializers</summary>
        private static readonly IDictionary<Type, Func<string, object>> ValueDeserializers =
            new Dictionary<Type, Func<string, object>>
            {
                { typeof(string), value => value },
                { typeof(short), value => Convert.ToInt16(value) },
                { typeof(int), value => Convert.ToInt32(value) },
                { typeof(long), value => Convert.ToInt64(value) },
                { typeof(float), value => (float)Convert.ToDouble(value) },
                { typeof(double), value => Convert.ToDouble(value) },
                { typeof(TimeSpan), value => TimeSpan.Parse(value) },
                { typeof(Guid), value => new Guid(value) },
            };

        /// <summary>Gets the keys</summary>
        public abstract IEnumerable<string> Keys { get; }

        /// <summary>Merges two configs to create a new config</summary>
        /// <remarks>
        /// Values in the provided config will override
        /// any existing values in the current config.
        /// </remarks>
        /// <param name="config">Config to merge</param>
        /// <returns>Merged config</returns>
        public abstract IConfig Merge(IConfig config);

        /// <summary>Checks whether a value exists</summary>
        /// <param name="key">Value key</param>
        /// <returns>True if the value exists; otherwise, false</returns>
        public abstract bool HasValue(string key);

        /// <summary>Gets a value</summary>
        /// <param name="key">Value key</param>
        /// <returns>The value, if it exists; otherwise, null</returns>
        public abstract string GetValue(string key);

        /// <summary>Gets a value</summary>
        /// <typeparam name="TValue">Value type</typeparam>
        /// <param name="key">Value key</param>
        /// <returns>The value, if it exists; otherwise, the type's default value</returns>
        public TValue GetValue<TValue>(string key)
        {
            return this.GetValue<TValue>(key, default(TValue));
        }

        /// <summary>Gets a value or the default if it doesn't exist</summary>
        /// <typeparam name="TValue">Value type</typeparam>
        /// <param name="key">Value key</param>
        /// <param name="defaultValue">Default value</param>
        /// <returns>The value, if it exists; otherwise, the provided default</returns>
        public TValue GetValue<TValue>(string key, TValue defaultValue)
        {
            if (!this.HasValue(key))
            {
                return default(TValue);
            }

            var value = this.GetValue(key);
            var type = typeof(TValue);
            
            if (type.IsEnum)
            {
                return (TValue)Enum.Parse(type, value, true);
            }

            if (ValueDeserializers.ContainsKey(type))
            {
                return this.GetValue<TValue>(key, s => (TValue)ValueDeserializers[type](s));
            }

            return default(TValue);
        }

        /// <summary>Gets a value or the default if it doesn't exist</summary>
        /// <typeparam name="TValue">Value type</typeparam>
        /// <param name="key">Value key</param>
        /// <param name="deserializer">Function used to deserialize the value</param>
        /// <returns>The value, if it exists; otherwise, the type's default value</returns>
        public TValue GetValue<TValue>(string key, Func<string, TValue> deserializer)
        {
            return this.HasValue(key) ?
                deserializer(this.GetValue(key)) :
                default(TValue);
        }

        /// <summary>Adds a value deserializer</summary>
        /// <typeparam name="TValue">Value type</typeparam>
        /// <param name="deserializer">Deserializer function</param>
        internal static void AddValueDeserializer<TValue>(Func<string, object> deserializer)
        {
            ValueDeserializers.Add(typeof(TValue), deserializer);
        }
    }
}