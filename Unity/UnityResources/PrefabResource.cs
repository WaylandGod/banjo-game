//-----------------------------------------------------------------------
// <copyright file="PrefabResource.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using Core.Resources;
using UnityEngine;

namespace Unity.Resources
{
    /// <summary>Prefab (GameObject) Resource</summary>
    public class PrefabResource : GenericAssetResource<UnityEngine.GameObject>
    {
        /// <summary>Initializes a new instance of the PrefabResource class</summary>
        /// <param name="gameObject">Prefab (GameObject)</param>
        public PrefabResource(UnityEngine.GameObject gameObject) : base(gameObject) { }

        /// <summary>Creates an instance of the prefab in the current scene</summary>
        /// <returns>The instantiated GameObject</returns>
        public GameObject Instantiate()
        {
            return this.Instantiate(Vector3.zero);
        }

        /// <summary>Creates an instance of the prefab in the current scene</summary>
        /// <param name="position">Initial position</param>
        /// <returns>The instantiated GameObject</returns>
        public GameObject Instantiate(Vector3 position)
        {
            return this.Instantiate(position, Quaternion.identity);
        }

        /// <summary>Creates an instance of the prefab in the current scene</summary>
        /// <param name="position">Initial position</param>
        /// <param name="direction">Initial direction</param>
        /// <returns>The instantiated GameObject</returns>
        public GameObject Instantiate(Vector3 position, Vector3 direction)
        {
            return this.Instantiate(position, Quaternion.Euler(direction));
        }

        /// <summary>Creates an instance of the prefab in the current scene</summary>
        /// <param name="position">Initial position</param>
        /// <param name="rotation">Initial rotation</param>
        /// <returns>The instantiated GameObject</returns>
        public GameObject Instantiate(Vector3 position, Quaternion rotation)
        {
            return (GameObject)UnityEngine.Object.Instantiate(this.NativeResource, position, rotation);
        }
    }
}
