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
        private static WorldBehaviour _instance;

        /// <summary>Gets the WorldBehaviour singleton</summary>
        public static WorldBehaviour Instance
        {
            get { return _instance ?? (_instance = CreateInstance()); }
        }
        
        /// <summary>Gets or sets the UnityWorld instance</summary>
        internal UnityWorld World { get; set; }

        public void Start()
        {
            if (this.World != null) this.World.OnStart(new EventArgs());
        }

        public void Update()
        {
            if (this.World != null) this.World.OnUpdate(new FrameEventArgs { TimeElapsed = Time.deltaTime });
        }

        public void OnGUI()
        {
            if (this.World != null) this.World.OnDrawUI(new EventArgs());
        }

        public void OnDestroy()
        {
            _instance = null;
        }

        private static WorldBehaviour CreateInstance()
        {
            var go = new GameObject("World", typeof(WorldBehaviour));
            go.transform.parent = UnityGame.Transform;
            return go.GetComponent<WorldBehaviour>(true);
        }
    }
}
