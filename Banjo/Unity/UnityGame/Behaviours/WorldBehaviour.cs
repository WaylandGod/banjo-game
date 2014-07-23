//-----------------------------------------------------------------------
// <copyright file="WorldBehaviour.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using Game;
using Game.Unity;
using UnityEngine;

namespace Game.Unity.Behaviours
{
    /// <summary>Behaviour which serves as a bridge between Unity and the World</summary>
    public class WorldBehaviour : MonoBehaviour
    {
        /// <summary>Backing field for Instance</summary>
        private static WorldBehaviour instance;

        /// <summary>Gets the WorldBehaviour singleton</summary>
        public static WorldBehaviour Instance
        {
            get
            {
                if (instance == null)
                {
                    var go = new GameObject("World", typeof(WorldBehaviour));
                    go.transform.parent = UnityGame.Transform;
                    instance = go.GetComponent<WorldBehaviour>(true);
                }

                return instance;
            }
        }
        
        /// <summary>Gets or sets the UnityWorld instance</summary>
        internal UnityWorld World { get; set; }

        /// <summary>Called every frame</summary>
        public void LateUpdate()
        {
            if (this.World != null)
            {
                this.World.OnUpdate(new FrameEventArgs { TimeElapsed = Time.deltaTime });
            }
        }

        /// <summary>Called when the behaviour instance is being destroyed</summary>
        public void OnDestroy()
        {
            instance = null;
        }
    }
}
