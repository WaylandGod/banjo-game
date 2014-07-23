//-----------------------------------------------------------------------
// <copyright file="Extensions.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using Core.Unity;
using Game.Unity.Behaviours;

namespace Game.Unity
{
    /// <summary>Unity game extensions</summary>
    public static class Extensions
    {
        /// <summary>Creates a UnityEngine.Vector3 from a Game.Vector3</summary>
        /// <param name="vector">Vector to convert</param>
        /// <returns>The UnityEngine.Vector3</returns>
        public static UnityEngine.Vector3 ToUnityVector3(this Core.Vector3 vector)
        {
            return new UnityEngine.Vector3((float)vector.X, (float)vector.Y, (float)vector.Z);
        }

        /// <summary>Creates a Game.Vector3 from a UnityEngine.Vector3</summary>
        /// <param name="vector">Vector to convert</param>
        /// <returns>The Game.Vector3</returns>
        public static Core.Vector3 ToGameVector3(this UnityEngine.Vector3 vector)
        {
            return new Core.Vector3(vector.x, vector.y, vector.z);
        }

        /// <summary>Adds an ObjectBehaviour to the UnityAvatar</summary>
        /// <typeparam name="TBehaviour">ObjectBehaviour type</typeparam>
        /// <param name="avatar">Unity avatar</param>
        /// <returns>The added behaviour</returns>
        public static TBehaviour AddObjectBehaviour<TBehaviour>(this UnityAvatar avatar)
            where TBehaviour : ObjectBehaviour
        {
            return SafeECall.Invoke<TBehaviour>(() => avatar.Collider.gameObject.AddComponent<TBehaviour>());
        }

        /// <summary>Gets the component, including checking children if specified</summary>
        /// <typeparam name="TComponent">Component type</typeparam>
        /// <param name="this">The UnityEngine GameObject</param>
        /// <param name="includeChildren">Whether to check children if the component is not found on the GameObject</param>
        /// <returns>The component, if found; otherwise, null.</returns>
        public static TComponent GetComponent<TComponent>(this UnityEngine.GameObject @this, bool includeChildren)
            where TComponent : UnityEngine.Component
        {
            var component = @this.GetComponent<TComponent>();
            if (component == null)
            {
                component = @this.GetComponentInChildren<TComponent>();
            }

            return component;
        }

        /// <summary>Gets the component, including checking children if specified</summary>
        /// <typeparam name="TComponent">Component type</typeparam>
        /// <param name="this">The UnityEngine Component</param>
        /// <param name="includeChildren">Whether to check children if the component is not found on the Component</param>
        /// <returns>The component, if found; otherwise, null.</returns>
        public static TComponent GetComponent<TComponent>(this UnityEngine.Component @this, bool includeChildren)
            where TComponent : UnityEngine.Component
        {
            var component = @this.GetComponent<TComponent>();
            if (component == null)
            {
                component = @this.GetComponentInChildren<TComponent>();
            }

            return component;
        }
    }
}
