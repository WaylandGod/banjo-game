//-----------------------------------------------------------------------
// <copyright file="GlobalContainer.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.DependencyInjection
{
    /// <summary>Provides global access to loaded dependency containers</summary>
    public sealed class GlobalContainer
    {
        /// <summary>Backing field for Instance</summary>
        private static GlobalContainer instance;

        /// <summary>Collection of dependency containers</summary>
        private readonly ICollection<DependencyContainer> containers = new List<DependencyContainer>();

        /// <summary>Prevents a default instance of the GlobalContainer class from being created.</summary>
        private GlobalContainer() { }

        /// <summary>Gets all registered containers</summary>
        internal static IEnumerable<DependencyContainer> AllContainers
        {
            get { return Instance.containers; }
        }

        /// <summary>Gets or sets the singleton instance</summary>
        internal static GlobalContainer Instance
        {
            get { return instance ?? (instance = new GlobalContainer()); }
            set { instance = value; }
        }

        /// <summary>Determines whether a type is registered</summary>
        /// <typeparam name="TType">Type to resolve</typeparam>
        /// <returns>True if the type can be resolved</returns>
        public static bool CanResolve<TType>()
        {
            return CanResolve(typeof(TType));
        }

        /// <summary>Determines whether a type is registered</summary>
        /// <typeparam name="TType">Type to resolve</typeparam>
        /// <param name="label">Registration label (null for default)</param>
        /// <returns>True if the type can be resolved</returns>
        public static bool CanResolve<TType>(string label)
        {
            return CanResolve(typeof(TType), label);
        }

        /// <summary>Determines whether a type is registered</summary>
        /// <param name="type">Type to resolve</param>
        /// <returns>True if the type can be resolved</returns>
        public static bool CanResolve(Type type)
        {
            return CanResolve(type, null);
        }

        /// <summary>Determines whether a type is registered</summary>
        /// <param name="type">Type to resolve</param>
        /// <param name="label">Registration label (null for default)</param>
        /// <returns>True if the type can be resolved</returns>
        public static bool CanResolve(Type type, string label)
        {
            return Instance.CanResolveImpl(type, label);
        }

        /// <summary>Resolve a registered type</summary>
        /// <typeparam name="TType">Type to resolve</typeparam>
        /// <returns>The resolved type instance</returns>
        public static TType Resolve<TType>()
        {
            return Resolve<TType>(null);
        }

        /// <summary>Resolve a registered type</summary>
        /// <typeparam name="TType">Type to resolve</typeparam>
        /// <param name="label">Registration label (null for default)</param>
        /// <returns>The resolved type instance</returns>
        public static TType Resolve<TType>(string label)
        {
            return (TType)Resolve(typeof(TType), label);
        }

        /// <summary>Resolve a registered type</summary>
        /// <param name="type">Type to resolve</param>
        /// <returns>The resolved type instance</returns>
        public static object Resolve(Type type)
        {
            return Resolve(type, null);
        }

        /// <summary>Resolve a registered type</summary>
        /// <param name="type">Type to resolve</param>
        /// <param name="label">Registration label (null for default)</param>
        /// <returns>The resolved type instance</returns>
        public static object Resolve(Type type, string label)
        {
            return Instance.ResolveImpl(type, label);
        }

        /// <summary>Resolve all registrations for a type</summary>
        /// <typeparam name="TType">Type to resolve</typeparam>
        /// <returns>Instances of the resolved types</returns>
        public static IEnumerable<TType> ResolveAll<TType>()
        {
            return ResolveAll(typeof(TType)).Cast<TType>();
        }

        /// <summary>Resolve all registrations for a type</summary>
        /// <param name="type">Type to resolve</param>
        /// <returns>Instances of the resolved types</returns>
        public static IEnumerable<object> ResolveAll(Type type)
        {
            return Instance.ResolveAllImpl(type);
        }

        /// <summary>Resets the GlobalContainer</summary>
        public static void Reset()
        {
            instance = null;
        }

        /// <summary>Add a container</summary>
        /// <param name="container">Dependency container</param>
        internal static void AddContainer(DependencyContainer container)
        {
            Instance.AddContainerImpl(container);
        }

        /// <summary>Determines whether a type is registered</summary>
        /// <param name="type">Type to resolve</param>
        /// <param name="label">Registration label (null for default)</param>
        /// <returns>True if the type can be resolved</returns>
        private bool CanResolveImpl(Type type, string label)
        {
            return this.containers.Any(c => c.CanResolve(type, label));
        }

        /// <summary>Resolve a registered type</summary>
        /// <param name="type">Type to resolve</param>
        /// <param name="label">Registration label (null for default)</param>
        /// <returns>The resolved type instance</returns>
        private object ResolveImpl(Type type, string label)
        {
            var instance = this.containers
                .Where(c => c.CanResolve(type, label))
                .Select(c => c.Resolve(type, label))
                .SingleOrDefault();
            if (instance == null)
            {
                throw new DependencyInjectionException(
                    "No registration found for type '{0}'(\"{1}\") in any of the currently loaded containers.",
                    type.FullName,
                    label == null ? string.Empty : " with label '{0}'".FormatInvariant(label));
            }

            return instance;
        }

        /// <summary>Resolve all registrations for a type</summary>
        /// <param name="type">Type to resolve</param>
        /// <returns>Instances of the resolved types</returns>
        private IEnumerable<object> ResolveAllImpl(Type type)
        {
            return this.containers
                .Where(c => c.CanResolve(type))
                .SelectMany(c => c.ResolveAll(type));
        }

        /// <summary>Add a container</summary>
        /// <param name="container">Dependency container</param>
        private void AddContainerImpl(DependencyContainer container)
        {
            this.containers.Add(container);
        }
    }
}
