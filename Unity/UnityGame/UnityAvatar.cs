//-----------------------------------------------------------------------
// <copyright file="UnityAvatar.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using Core;
using Core.Resources;
using Core.Unity;
using Game;
using Game.Programmability;
using Unity.Resources;
using UnityEngine;

namespace Game.Unity
{
    /// <summary>Unity IAvatar implementation</summary>
    /// <remarks>
    /// WARNING: This class is unsafe to use outside Unity.
    /// Use TestUtilities.Game.Factories.TestAvatarFactory to create mocks for unit testing.
    /// </remarks>
    public sealed class UnityAvatar : AvatarBase, IAvatar
    {
        /// <summary>Instantiated prefab GameObject</summary>
        public readonly GameObject GameObject;

        /// <summary>Collider component</summary>
        public readonly Collider Collider;

        /// <summary>Gets the physics rigidbody</summary>
        private readonly Rigidbody rigidbody;

        /// <summary>Instantiated prefab Animator</summary>
        private readonly Animator animator;

        /// <summary>Backing field for ClippingDistance</summary>
        private float? clippingDistance;

        /// <summary>Initializes a new instance of the UnityAvatar class</summary>
        /// <param name="id">Avatar identifier</param>
        /// <param name="resource">Unity prefab resource</param>
        /// <param name="states">List of available states</param>
        /// <param name="defaultState">Default state</param>
        public UnityAvatar(string id, PrefabResource resource, AvatarState[] states, string defaultState)
            : base(id, resource, states)
        {
            this.GameObject = (GameObject)UnityEngine.Object.Instantiate(this.Prefab.NativeResource);
            if (UnityAvatar.Parent != null)
            {
                this.GameObject.transform.parent = UnityAvatar.Parent;
            }
            else
            {
                Log.Warning("UnityAvatar.Parent has not been set. All avatars will be created in the root of the scene.");
            }

            this.animator = this.GameObject.GetComponent<Animator>(true);
            this.CurrentState = this.States[defaultState];

            if ((this.Collider = this.GameObject.GetComponent<Collider>(true)) != null)
            {
                this.Collider.isTrigger = true;
            }
            else
            {
                Log.Error("Avatar prefab '{0}' missing required Collider component!", resource.Id);
            }
            
            if ((this.rigidbody = this.GameObject.GetComponent<Rigidbody>(true)) != null ||
                (this.rigidbody = this.GameObject.AddComponent<Rigidbody>()) != null)
            {
                this.rigidbody.useGravity = false;
                this.rigidbody.isKinematic = true;
            }
            else
            {
                Log.Error("Avatar prefab '{0}' missing required Rigidbody component!", resource.Id);
            }
        }

        /// <summary>Gets the parent for all avatars</summary>
        public static Transform Parent { get { return UnityWorld.Transform; } }

        /// <summary>Gets or sets a value indicating whether the object is visible</summary>
        public override bool Visible
        {
            get { return this.GameObject.activeSelf; }
            set { this.GameObject.SetActive(value); }
        }

        /// <summary>Gets or sets the current state</summary>
        public override AvatarState CurrentState
        {
            get
            {
                if (this.animator == null)
                {
                    this.States.Values.FirstOrDefault();
                }

                return this.States
                    .First(s => s.Value.Id == this.animator.GetInteger("State"))
                    .Value;
            }

            set
            {
                if (this.animator == null)
                {
                    return;
                }

                this.animator.SetInteger("State", value.Id);
            }
        }

        /// <summary>Gets or sets the position</summary>
        public override Vector3D Position
        {
            get { return this.GameObject.transform.position; }
            set { this.GameObject.transform.localPosition = value; }
        }

        /// <summary>Gets or sets the direction</summary>
        public override Vector3D Direction
        {
            get { return this.GameObject.transform.rotation.eulerAngles; }
            set { this.GameObject.transform.rotation = Quaternion.Euler(value); }
        }

        /// <summary>Gets or sets the avatar's Unity layer</summary>
        public string Layer
        {
            get
            {
                return SafeECall.Invoke<string>(() =>
                {
                    return UnityEngine.LayerMask.LayerToName(this.GameObject.layer);
                });
            }

            set
            {
                SafeECall.Invoke(() =>
                {
                    var layer = UnityEngine.LayerMask.NameToLayer(value);
#if DEBUG
                    if (layer < 0)
                    {
                        Log.Warning("Unable to set avatar layer: The layer '{0}' does not exist. \nPlease add a layer named '{0}' in the Unity LayerManager.", value);
                        return;
                    }
#endif
                    this.GameObject.layer = layer;
                });
            }
        }

        /// <summary>Gets the clipping distance</summary>
        public override float ClippingDistance
        {
            get
            {
                if (!this.clippingDistance.HasValue)
                {
                    var extents = this.Collider.bounds.extents;
                    this.clippingDistance = Mathf.Max(extents.x, extents.z) + 0.00005f;
                }

                return this.clippingDistance.Value;
            }
        }

        /// <summary>Gets the underlying resource as an AvatarPrefabResource</summary>
        private PrefabResource Prefab { get { return (PrefabResource)this.Resource; } }
    }
}
