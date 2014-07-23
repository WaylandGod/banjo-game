//-----------------------------------------------------------------------
// <copyright file="EntityStateController.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Linq;
using Core;
using Core.Programmability;
using Game;

namespace Game.Programmability
{
    /// <summary>Controllers the target's state using conditions provided by other controllers</summary>
    [Controller(EntityStateController.ControllerId)]
    public class EntityStateController : EntityController
    {
        /// <summary>Controller identifier</summary>
        public const string ControllerId = "controller.entitystates";

        /// <summary>Backing field for StateChecks</summary>
        private EntityStateCheck[] stateChecks;

        /// <summary>Initializes a new instance of the EntityStateController class</summary>
        /// <param name="target">Controller target</param>
        /// <param name="config">Controller config</param>
        public EntityStateController(IEntity target, IConfig config) : base(target, config) { }

        /// <summary>Gets the list of state checks from all state check providing controllers</summary>
        private EntityStateCheck[] StateChecks
        {
            get
            {
                return this.stateChecks ?? (this.stateChecks = this.GetStateChecks());
            }
        }

        /// <summary>Update each frame</summary>
        /// <param name="e">Frame event data</param>
        [EventHandler]
        public override void OnUpdate(FrameEventArgs e)
        {
            foreach (var check in this.StateChecks)
            {
                if (check.Condition(this.Target) == true)
                {
                    if (this.Target.State != check.State)
                    {
                        this.Target.State = check.State;
                        break;
                    }
                }
            }
        }

        /// <summary>Gets the state checks from the target controllers</summary>
        /// <returns>The state checks.</returns>
        private EntityStateCheck[] GetStateChecks()
        {
            var stateChecks = this.Target.Controllers
                    .OfType<IEntityStateCheckProvider>()
                    .OrderByDescending(p => p.EntityStatePriority)
                    .SelectMany(p => p.EntityStateChecks)
                    .ToArray();
#if DEBUG
            if (stateChecks.Length == 0)
            {
                var providers = this.Target.Controllers.OfType<IEntityStateCheckProvider>();
                Log.Warning(
                    "EntityStateController: No state checks found! EntityStateCheckProviders=[{0}]",
                    string.Join(", ", providers.Select(p => p.GetType().FullName).ToArray()));
                stateChecks = providers
                    .OrderByDescending(p => p.EntityStatePriority)
                    .SelectMany(p => p.EntityStateChecks)
                    .ToArray();
            }
#endif

            return this.stateChecks;
        }
    }
}
