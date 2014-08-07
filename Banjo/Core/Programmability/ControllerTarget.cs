using System;
using System.Linq;
using Core.Data;
using Core.Factories;

namespace Core.Programmability
{
    /// <summary>Base class for controller targets</summary>
    /// <typeparam name="TController">Type of targetting controller</typeparam>
    public abstract class ControllerTarget<TController> : IControllerTarget
        where TController : IController
    {
        /// <summary>Initializes a new instance of the ControllerTarget class</summary>
        /// <param name="id">Controller target id</param>
        /// <param name="controllerFactories">Controller factories</param>
        /// <param name="controllers">Primary controller configurations</param>
        /// <param name="customControllers">Additional controller configurations/setting overrides</param>
        protected ControllerTarget(
            string id,
            IControllerFactory[] controllerFactories,
            ControllerConfig[] controllers,
            ControllerConfig[] customControllers = null)
        {            
            this.Id = new RuntimeId(id, this.GetType());
            this.Controllers = CreateControllers(controllerFactories, controllers, customControllers ?? new ControllerConfig[0], this);
        }

        /// <summary>Gets the runtime identifier</summary>
        public RuntimeId Id { get; private set; }

        /// <summary>Gets the object's controllers</summary>
        public TController[] Controllers { get; private set; }

        /// <summary>Creates controllers for the object</summary>
        /// <param name="controllerFactories">Controller factories</param>
        /// <param name="controllers">Primary controller configurations</param>
        /// <param name="customControllers">Additional controller configurations/setting overrides</param>
        protected static TController[] CreateControllers(
            IControllerFactory[] controllerFactories,
            ControllerConfig[] controllers,
            ControllerConfig[] customControllers,
            IControllerTarget target)
        {
            if (controllers == null)
                throw new ArgumentNullException("controllers");
            if (customControllers == null)
                throw new ArgumentNullException("customControllers");

            // Find the additional controllers that are actually
            // overrides for built-in controllers' settings
            var overrides = customControllers
                .Where(cc =>
                    controllers.Any(bicc => cc.ControllerId == bicc.ControllerId))
                .ToDictionary(
                                cc => cc.ControllerId,
                                cc => new DictionaryConfig(cc.Settings));

            // Create a dictionary of controller configs keyed by their ids
            // Start with the built-in controllers, then the non-overrides.
            // Next, merge in settings overrides where available.
            var controllersToCreate = controllers
                .ToDictionary(
                                          cc => cc.ControllerId,
                                          cc => new DictionaryConfig(cc.Settings))
                .Concat(customControllers
                    .Where(cc => !overrides.ContainsKey(cc.ControllerId))
                    .ToDictionary(
                                              cc => cc.ControllerId,
                                              cc => new DictionaryConfig(cc.Settings)))
                .ToDictionary(
                                          kvp => kvp.Key,
                                          kvp => overrides.ContainsKey(kvp.Key) ?
                        kvp.Value.Merge(overrides[kvp.Key]) : kvp.Value);

            var controllerFactory = controllerFactories.FirstOrDefault(f => target.GetType().GetInterfaces().Any(t => t == f.TargetType));
            if (controllerFactory == null) {
                var message = "No factories found for controllers with targets of type {0} (factories: {1})".FormatInvariant(target.GetType().FullName, string.Join(", ", controllerFactories.Select(f => f.TargetType.FullName).ToArray()));
                throw new ArgumentException(message, "controllerFactories");
            }

            return controllersToCreate
                .Select(kvp =>
                    controllerFactory.Create(kvp.Key, kvp.Value, target))
                .Cast<TController>()
                .ToArray();
        }
    }
}

