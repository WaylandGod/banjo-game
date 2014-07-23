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
using Core.Programmability;
using Game.Data;
using Game.Factories;
using Game.Programmability;

namespace Game.Factories
{
    /// <summary>Creates implementations of IController</summary>
    /// <remarks>
    /// Reflects on all assemblies in the execution context of the current AppDomain to find valid IController implementations.
    /// Valid controllers must
    ///   A) Implement IController
    ///   B) Have the Controller attribute (with a unique identifier)
    ///   C) Have a constructor taking parameters (IEntity, IConfig)
    /// </remarks>
    public class ReflectionControllerFactory : FactoryBase<IEntityController>, IControllerFactory
    {
        /// <summary>Required constructor parameter types</summary>
        private static readonly Type[] RequiredConstructorTypes = new[] { typeof(IEntity), typeof(IConfig) };

        /// <summary>Dictionary of entity controller types</summary>
        private readonly IDictionary<string, ConstructorInfo> constructors;

        /// <summary>Controller manager</summary>
        private readonly IControllerManager controllerManager;

        /// <summary>Initializes a new instance of the ReflectionControllerFactory class</summary>
        /// <param name="controllerManager">Controller manager</param>
        public ReflectionControllerFactory(IControllerManager controllerManager)
        {
            this.controllerManager = controllerManager;

            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            //// Log.Trace("ControllerFactory - Searching for controllers in {0} assemblies: {1}", assemblies.Length, string.Join(", ", assemblies.Select(asm => asm.FullName).ToArray()));

            var types = assemblies
                .SelectMany(asm => asm.TryGetTypes())
                .Where(t =>
                    t != null && !t.IsInterface && !t.IsAbstract &&
                    typeof(IEntityController).IsAssignableFrom(t))
                .ToArray();

            this.constructors = new Dictionary<string, ConstructorInfo>();
            foreach (var type in types)
            {
                //// Log.Trace("ControllerFactory - Indexing type '{0}'...", type.FullName);

                var attr = type.GetCustomAttributes(typeof(ControllerAttribute), false)
                    .FirstOrDefault() as ControllerAttribute;

                if (attr == null)
                {
                    Log.Warning(
                        "IController implementation '{0}' missing required attribute '[Controller(\"controller.identifier\")]' and will not be available.",
                        type.FullName);
                    continue;
                }

                var ctor = type.GetConstructor(RequiredConstructorTypes);
                if (ctor == null)
                {
                    Log.Warning(
                        "IController implementation '{0}' missing required constructor 'public {1}({2})' and will not be available",
                        type.FullName,
                        type.Name,
                        string.Join(", ", RequiredConstructorTypes.Select(t => t.FullName).ToArray()));
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

        /// <summary>Creates an entity controller instance</summary>
        /// <param name="controllerId">Controller identifier</param>
        /// <param name="settings">Controller configuration</param>
        /// <param name="target">Controller target</param>
        /// <returns>The created IGame instance</returns>
        protected virtual IEntityController Create(string controllerId, IConfig settings, IEntity target)
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

            var controller = (IEntityController)ctor.Invoke(new object[] { target, settings });
            this.controllerManager.AddController(controller);
            return controller;
        }
    }
}
