//-----------------------------------------------------------------------
// <copyright file="GlobalContainer.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

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
            return Instance.ResolveImpl<TType>(label);
        }

        /// <summary>Resolve all registrations for a type</summary>
        /// <typeparam name="TType">Type to resolve</typeparam>
        /// <returns>Instances of the resolved types</returns>
        public static IEnumerable<TType> ResolveAll<TType>()
        {
            return Instance.ResolveAllImpl<TType>();
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

        /// <summary>Resolve a registered type</summary>
        /// <typeparam name="TType">Type to resolve</typeparam>
        /// <param name="label">Registration label (null for default)</param>
        /// <returns>The resolved type instance</returns>
        private TType ResolveImpl<TType>(string label)
        {
            var instance = this.containers
                .Where(c => c.CanResolve<TType>(label))
                .Select(c => c.Resolve<TType>(label))
                .SingleOrDefault();
            if (instance == null)
            {
                throw new DependencyInjectionException(
                    "No registration found for type '{0}'{1} in any of the currently loaded containers.",
                    typeof(TType).FullName,
                    label == null ? string.Empty : " with label '{0}'".FormatInvariant(label));
            }

            return instance;
        }

        /// <summary>Resolve all registrations for a type</summary>
        /// <typeparam name="TType">Type to resolve</typeparam>
        /// <returns>Instances of the resolved types</returns>
        private IEnumerable<TType> ResolveAllImpl<TType>()
        {
            return this.containers
                .Where(c => c.CanResolve<TType>())
                .SelectMany(c => c.ResolveAll<TType>());
        }

        /// <summary>Add a container</summary>
        /// <param name="container">Dependency container</param>
        private void AddContainerImpl(DependencyContainer container)
        {
            this.containers.Add(container);
        }
    }
}
