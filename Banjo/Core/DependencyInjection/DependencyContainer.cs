//-----------------------------------------------------------------------
// <copyright file="DependencyContainer.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Core.DependencyInjection
{
    /// <summary>Dependency injection container</summary>
    public class DependencyContainer
    {
        /// <summary>List of type registrations</summary>
        private readonly IDictionary<Type, IDictionary<string, TypeRegistration>> typeRegistry =
            new Dictionary<Type, IDictionary<string, TypeRegistration>>();

        /// <summary>Initializes a new instance of the DependencyContainer class</summary>
        public DependencyContainer()
        {
            GlobalContainer.AddContainer(this);
        }

        /// <summary>Register a type mapping</summary>
        /// <typeparam name="TFrom">Type to be resolved</typeparam>
        /// <typeparam name="TTo">Type to be returned</typeparam>
        /// <returns>The container</returns>
        public DependencyContainer RegisterType<TFrom, TTo>()
            where TTo : TFrom
        {
            return this.RegisterType<TFrom, TTo>(null);
        }

        /// <summary>Register a type mapping</summary>
        /// <typeparam name="TFrom">Type to be resolved</typeparam>
        /// <typeparam name="TTo">Type to be returned</typeparam>
        /// <param name="label">Registration label (null for default)</param>
        /// <returns>The container</returns>
        public DependencyContainer RegisterType<TFrom, TTo>(
            string label)
            where TTo : TFrom
        {
            var registration = new TypeRegistration(
                typeof(TFrom), typeof(TTo), label);
            this.AddRegistration(registration);
            return this;
        }

        /// <summary>Register a singleton mapping</summary>
        /// <typeparam name="TFrom">Type to be resolved</typeparam>
        /// <typeparam name="TTo">Type to be returned</typeparam>
        /// <returns>The container</returns>
        public DependencyContainer RegisterSingleton<TFrom, TTo>()
            where TTo : TFrom
        {
            return this.RegisterSingleton<TFrom, TTo>(null);
        }

        /// <summary>Register a singleton mapping</summary>
        /// <typeparam name="TFrom">Type to be resolved</typeparam>
        /// <typeparam name="TTo">Type to be returned</typeparam>
        /// <param name="label">Registration label (null for default)</param>
        /// <returns>The container</returns>
        public DependencyContainer RegisterSingleton<TFrom, TTo>(
            string label)
            where TTo : TFrom
        {
            var registration = new TypeRegistration(
                typeof(TFrom), typeof(TTo), true, label);
            this.AddRegistration(registration);
            return this;
        }

        /// <summary>Register a singleton mapping</summary>
        /// <typeparam name="TFrom">Type to be resolved</typeparam>
        /// <param name="singletonInstance">Singleton instance</param>
        /// <returns>The container</returns>
        public DependencyContainer RegisterSingleton<TFrom>(
            TFrom singletonInstance)
        {
            return this.RegisterSingleton<TFrom>(singletonInstance, null);
        }

        /// <summary>Register a singleton mapping</summary>
        /// <typeparam name="TFrom">Type to be resolved</typeparam>
        /// <param name="singletonInstance">Singleton instance</param>
        /// <param name="label">Registration label (null for default)</param>
        /// <returns>The container</returns>
        public DependencyContainer RegisterSingleton<TFrom>(
            TFrom singletonInstance,
            string label)
        {
            var registration = new TypeRegistration(
                typeof(TFrom), null, singletonInstance, label);
            this.AddRegistration(registration);
            return this;
        }

        /// <summary>Checks if a type can be resolved</summary>
        /// <typeparam name="TType">Type to be resolved</typeparam>
        /// <returns>True if the type can be resolved; otherwise, false.</returns>
        public bool CanResolve<TType>()
        {
            return this.CanResolve<TType>(null);
        }

        /// <summary>Checks if a type can be resolved</summary>
        /// <typeparam name="TType">Type to be resolved</typeparam>
        /// <param name="label">Registration label (null for default)</param>
        /// <returns>True if the type can be resolved; otherwise, false.</returns>
        public bool CanResolve<TType>(string label)
        {
            return this.CanResolve(typeof(TType), label);
        }

        /// <summary>Checks if a type can be resolved</summary>
        /// <param name="type">Type to be resolved</param>
        /// <returns>True if the type can be resolved; otherwise, false.</returns>
        public bool CanResolve(Type type)
        {
            return this.CanResolve(type, null);
        }

        /// <summary>Checks if a type can be resolved</summary>
        /// <param name="type">Type to be resolved</param>
        /// <param name="label">Registration label (null for default)</param>
        /// <returns>True if the type can be resolved; otherwise, false.</returns>
        public bool CanResolve(Type type, string label)
        {
            return label == null ?
                this.GetRegistrationsForType(type) != null :
                this.GetRegistration(type, label) != null;
        }

        /// <summary>Resolve a registered type</summary>
        /// <typeparam name="TType">Type to resolve</typeparam>
        /// <returns>The resolved type instance</returns>
        public TType Resolve<TType>()
        {
            return this.Resolve<TType>(null);
        }

        /// <summary>Resolve a registered type</summary>
        /// <typeparam name="TType">Type to resolve</typeparam>
        /// <param name="label">Registration label (null for default)</param>
        /// <returns>The resolved type instance</returns>
        public TType Resolve<TType>(string label)
        {
            return (TType)this.Resolve(typeof(TType), label);
        }

        /// <summary>Resolve a registered type</summary>
        /// <param name="type">Type to resolve</param>
        /// <returns>The resolved type instance</returns>
        public object Resolve(Type type)
        {
            return this.Resolve(type, null);
        }

        /// <summary>Resolve a registered type</summary>
        /// <param name="type">Type to resolve</param>
        /// <param name="label">Registration label (null for default)</param>
        /// <returns>The resolved type instance</returns>
        public object Resolve(Type type, string label)
        {
            var registration = this.GetRegistration(type, label);
            if (registration == null)
            {
                throw new DependencyInjectionException(
                    "No registration found for type '{0}'{1}",
                    type.FullName,
                    label == null ? string.Empty : " with label '{0}'".FormatInvariant(label));
            }

            return registration.RealizeInstance();
        }

        /// <summary>Resolve all registrations for a type</summary>
        /// <typeparam name="TType">Type to resolve</typeparam>
        /// <returns>Instances of the resolved types</returns>
        public IEnumerable<TType> ResolveAll<TType>()
        {
            return this.ResolveAll(typeof(TType)).Cast<TType>();
        }
            
        /// <summary>Resolve all registrations for a type</summary>
        /// <param name="type">Type to resolve</param>
        /// <returns>Instances of the resolved types</returns>
        public IEnumerable<object> ResolveAll(Type type)
        {
            var registrations =
                this.GetRegistrationsForType(type) ??
                new Dictionary<string, TypeRegistration>(0);
            return registrations.Values
                .Select(r => r.RealizeInstance());
        }

        /// <summary>Adds a registration to the type registry</summary>
        /// <param name="registration">The registration to add</param>
        private void AddRegistration(TypeRegistration registration)
        {
            lock (this.typeRegistry)
            {
                if (!this.typeRegistry.ContainsKey(registration.FromType))
                {
                    this.typeRegistry.Add(
                        registration.FromType,
                        new Dictionary<string, TypeRegistration>());
                }

                this.typeRegistry[registration.FromType][registration.Label] = registration;
            }
        }

        /// <summary>Gets the registration for a type and label</summary>
        /// <param name="type">The type</param>
        /// <param name="label">The label</param>
        /// <returns>The registration if it exists; otherwise, null</returns>
        private TypeRegistration GetRegistration(Type type, string label)
        {
            var registrations = this.GetRegistrationsForType(type);
            return
                registrations == null ? null :
                string.IsNullOrEmpty(label) ? registrations.Values.FirstOrDefault() :
                registrations.ContainsKey(label) ? registrations[label] :
                null;
        }

        /// <summary>Gets the registrations for a type</summary>
        /// <param name="type">The type</param>
        /// <returns>The registrations if they exist; otherwise, null</returns>
        private IDictionary<string, TypeRegistration> GetRegistrationsForType(Type type)
        {
            return this.typeRegistry.ContainsKey(type) ?
                this.typeRegistry[type] : null;
        }

        /// <summary>Used for defining type registrations</summary>
        private class TypeRegistration
        {
            /// <summary>Registered type</summary>
            public readonly Type FromType;

            /// <summary>Resolved type</summary>
            public readonly Type ToType;

            /// <summary>Registration label (null for default)</summary>
            public readonly string Label;

            /// <summary>
            /// Whether this registration is a singleton
            /// </summary>
            public readonly bool IsSingleton;

            /// <summary>Instance for singleton registrations</summary>
            private object singletonInstance;

            /// <summary>Constructor used to create instance of the type</summary>
            private ConstructorInfo constructor;

            /// <summary>Initializes a new instance of the TypeRegistration class</summary>
            /// <param name="fromType">Registered type</param>
            /// <param name="toType">Resolved type</param>
            /// <param name="singleton">Whether registration is for a singleton</param>
            /// <param name="label">Registration label (null for default)</param>
            internal TypeRegistration(
                Type fromType,
                Type toType,
                bool singleton,
                string label)
            {
                this.FromType = fromType;
                this.ToType = toType;
                this.IsSingleton = singleton;
                this.Label = label ?? string.Empty;
            }

            /// <summary>Initializes a new instance of the TypeRegistration class</summary>
            /// <param name="fromType">Registered type</param>
            /// <param name="toType">Resolved type</param>
            /// <param name="label">Registration label (null for default)</param>
            internal TypeRegistration(
                Type fromType,
                Type toType,
                string label)
                : this(fromType, toType, false, label)
            {
            }

            /// <summary>Initializes a new instance of the TypeRegistration class</summary>
            /// <param name="fromType">Registered type</param>
            /// <param name="toType">Resolved type</param>
            /// <param name="singletonInstance">Singleton instance</param>
            /// <param name="label">Registration label (null for default)</param>
            internal TypeRegistration(
                Type fromType,
                Type toType,
                object singletonInstance,
                string label)
                : this(fromType, toType, true, label)
            {
                this.singletonInstance = singletonInstance;
            }

            /// <summary>Realizes an instance of the registered type using the provider</summary>
            /// <returns>The realized type instance</returns>
            internal object RealizeInstance()
            {
                lock (this)
                {
                    // If a singleton already exists, return it
                    if (this.singletonInstance != null)
                    {
                        return this.singletonInstance;
                    }

                    // Find a valid constructor using currently registered types
                    this.constructor = this.constructor ??
                        this.ToType.GetConstructors()
                        .Where(c =>
                            !c.GetParameters().Any(p => p.ParameterType == this.FromType) &&
                            c.GetParameters().All(p => GlobalContainer.AllContainers.Any(dc => dc.typeRegistry.ContainsKey(p.ParameterType))))
                        .OrderByDescending(c => c.GetParameters().Length)
                        .FirstOrDefault();
                    if (this.constructor == null)
                    {
                        throw new DependencyInjectionException(
                            "Unable to realize '{0}': No constructor found for '{0}' using currently registered types.",
                            this.FromType.FullName,
                            this.ToType.FullName);
                    }
                    
                    // Resolve the parameters and invoke the constructor
                    var parameters = this.constructor.GetParameters()
                        .Select(p => GlobalContainer.AllContainers.First(dc => dc.CanResolve(p.ParameterType)).Resolve(p.ParameterType))
                        .Cast<object>()
                        .ToArray();
                    var instance = this.constructor.Invoke(parameters);

                    // If registration is for a singleton, set it
                    if (this.IsSingleton)
                    {
                        this.singletonInstance = instance;
                    }

                    // Return the realized instance
                    return instance;
                }
            }
        }
    }
}
