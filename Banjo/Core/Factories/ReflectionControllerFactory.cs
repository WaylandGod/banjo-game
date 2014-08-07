//-----------------------------------------------------------------------
// <copyright file="ReflectionControllerFactory.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Core;
using Core.Data;
using Core.DependencyInjection;
using Core.Programmability;
using Core.Resources.Management;

namespace Core.Factories
{
    /// <summary>Creates implementations of IController</summary>
    /// <remarks>
    /// Reflects on all assemblies in the execution context of the current AppDomain to find valid IController implementations.
    /// Valid controllers must
    ///   A) Implement IController
    ///   B) Have the Controller attribute (with a unique identifier)
    ///   C) Have a constructor taking a parameter each of types IControllerTarget and IConfig
    ///   D) Additional parameters must be IResourceLibrary or registered with the global dependency container
    /// </remarks>
    public class ReflectionControllerFactory<TControllerTarget> : FactoryBase<IController>, IControllerFactory
        where TControllerTarget : IControllerTarget
    {
        /// <summary>Default constructor parameter types</summary>
        private static readonly Type[] DefaultConstructorParameterTypes = new[] { typeof(IControllerTarget), typeof(IConfig), typeof(IResourceLibrary) };

        /// <summary>Dictionary of entity controller types</summary>
        private readonly IDictionary<string, ConstructorInfo> constructors;

        /// <summary>Controller manager</summary>
        private readonly IControllerManager controllerManager;

        /// <summary>Resource library</summary>
        private readonly IResourceLibrary resources;

        /// <summary>Initializes a new instance of the ReflectionControllerFactory class</summary>
        /// <param name="controllerManager">Controller manager</param>
        /// <param name="resourceLibrary">Resource library</param>
        public ReflectionControllerFactory(IControllerManager controllerManager, IResourceLibrary resourceLibrary)
        {
            this.controllerManager = controllerManager;
            this.resources = resourceLibrary;

            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            //// Log.Trace("ControllerFactory - Searching for controllers in {0} assemblies: {1}", assemblies.Length, string.Join(", ", assemblies.Select(asm => asm.FullName).ToArray()));

            var types = assemblies
                .SelectMany(asm => asm.TryGetTypes())
                .Where(t =>
                    t != null && !t.IsInterface && !t.IsAbstract &&
                    typeof(IController).IsAssignableFrom(t))
                .ToArray();

            this.constructors = new Dictionary<string, ConstructorInfo>();
            foreach (var type in types)
            {
                //// Log.Trace("ControllerFactory - Indexing type '{0}'...", type.FullName);

                var attr = type.GetCustomAttributes(typeof(ControllerAttribute), false)
                    .FirstOrDefault() as ControllerAttribute;

                if (attr == null)
                {
                    Log.Error(
                        "Skipping '{0}': IController implementation missing required attribute '[Controller(\"controller.identifier\")]'.",
                        type.FullName);
                    continue;
                }

                var ctor = type.GetConstructors()
                    .FirstOrDefault(c => c.GetParameters()
                        .Select(p => p.ParameterType)
                        .All(t => DefaultConstructorParameterTypes.Any(ct => ct.IsAssignableFrom(t)) || GlobalContainer.CanResolve(t)));
                if (ctor == null)
                {
                    Log.Error(
                        "Skipping '{0}': IController implementation did not contain any acceptable constructors.",
                        type.FullName,
                        type.Name,
                        string.Join(", ", DefaultConstructorParameterTypes.Select(t => t.FullName).ToArray()));
                    continue;
                }

                this.constructors.Add(attr.Id, ctor);
            }

#if TRACE
            Log.Trace(
                "ControllerFactory - Indexed {0} controller types:\n\t{1}",
                this.constructors.Count,
                string.Join("\n\t", this.constructors.Select(kvp => "{0} ({1})".FormatInvariant(kvp.Key, kvp.Value.DeclaringType.FullName)).ToArray()));
#endif
        }

        /// <summary>Gets the type of the controller targets</summary>
        public Type TargetType { get { return typeof(TControllerTarget); } }

        /// <summary>Creates an entity controller instance</summary>
        /// <param name="controllerId">Controller identifier</param>
        /// <param name="settings">Controller configuration</param>
        /// <param name="target">Controller target</param>
        /// <returns>The created IGame instance</returns>
        public virtual IController Create(string controllerId, IConfig settings, IControllerTarget target)
        {
#if DEBUG
            if (!this.constructors.ContainsKey(controllerId))
            {
                throw new ArgumentOutOfRangeException(
                    "controllerConfig", "No controller found with id '{0}'.".FormatInvariant(controllerId));
            }
#endif

            var ctor = this.constructors[controllerId];

            //// Log.Trace("Creating controller '{0}' ({1}) with target '{2}'\nSettings: {3}", controllerId, ctor.DeclaringType.FullName, target.Id, settings);

            var defaultParameters = new Dictionary<Type, object>
            {
                { typeof(IResourceLibrary), this.resources },
                { typeof(TControllerTarget), target },
                { typeof(IConfig), settings },
            };
            var parameters = ctor.GetParameters().Select(p => p.ParameterType)
                .Select(t =>
                    defaultParameters.ContainsKey(t)  ? defaultParameters[t] :
                    t.IsArray ? GlobalContainer.ResolveAll(t) :
                    GlobalContainer.Resolve(t))
                .ToArray();

            var controller = (IController)ctor.Invoke(parameters);
            this.controllerManager.AddController(controller);
            return controller;
        }
    }
}
