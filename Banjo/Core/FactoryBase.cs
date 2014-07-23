//-----------------------------------------------------------------------
// <copyright file="FactoryBase.cs" company="Benjamin Woodall">
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
using System.Linq;
using System.Reflection;

namespace Core
{
    /// <summary>Base class for IFactory implementations</summary>
    /// <typeparam name="TType">Type created by the factory</typeparam>
    public abstract class FactoryBase<TType> : IFactory<TType>
    {
        /// <summary>Binding flags for the Create method</summary>
        private const BindingFlags CreateMethodBinding = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.InvokeMethod | BindingFlags.Instance;

        /// <summary>Create method implementation</summary>
        private readonly MethodInfo createMethod;

        /// <summary>Expected parameter types for the create method</summary>
        private readonly Type[] expectedParameterTypes;

        /// <summary>Initializes a new instance of the FactoryBase class</summary>
        protected FactoryBase()
        {
            var methods = this.GetType().GetMethods(CreateMethodBinding);
            this.createMethod = methods.FirstOrDefault(mi =>
                mi.Name == "Create" &&
                mi.ReturnType == typeof(TType) &&
                mi.DeclaringType != typeof(FactoryBase<TType>));
#if DEBUG
            if (this.createMethod == null)
            {
                var msg = "No Create method found with return type {0} for class {1}"
                    .FormatInvariant(typeof(TType).FullName, this.GetType().FullName);
                throw new NotImplementedException(msg);
            }
#endif

            this.expectedParameterTypes = this.createMethod.GetParameters()
                .Select(pi => pi.ParameterType)
                .ToArray();
        }

        /// <summary>Creates a new instance of TType</summary>
        /// <param name="parameters">Optional parameters</param>
        /// <returns>The created instance</returns>
        public TType Create(params object[] parameters)
        {
#if DEBUG
            this.CheckParameterTypes(parameters);
#endif
            return (TType)this.createMethod.Invoke(this, parameters);
        }

        /// <summary>Verifies arguments of the expected types have been provided</summary>
        /// <param name="parameters">Parameters to check</param>
        private void CheckParameterTypes(object[] parameters)
        {
            if (parameters.Length != this.expectedParameterTypes.Length)
            {
                var msg = "The number of parameters provided did not match those expected (expected: {0}, actual: {1})"
                    .FormatInvariant(this.expectedParameterTypes.Length, parameters.Length);
                throw new ArgumentException(msg, "parameters");
            }

            for (var i = 0; i < this.expectedParameterTypes.Length; i++)
            {
                var expected = this.expectedParameterTypes[i];
                var actual = parameters[i] != null ? parameters[i].GetType() : null;

                if (expected.IsValueType && actual == null)
                {
                    var msg = "Parameter {0} of {1}: Value type parameters cannot be null (expected: {2})"
                        .FormatInvariant(i, this.expectedParameterTypes.Length, expected.FullName);
                    throw new ArgumentException(msg, "parameters");
                }

                if (actual != null && !expected.IsAssignableFrom(actual))
                {
                    var msg = "Parameter {0} of {1}: Type mismatch (expected: {2}, actual {3})"
                        .FormatInvariant(i, this.expectedParameterTypes.Length, expected.FullName, actual.FullName);
                    throw new ArgumentException(msg, "parameters");
                }
            }
        }
    }
}
