//-----------------------------------------------------------------------
// <copyright file="Extensions.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using Core;
using Core.Unity;
using Game.Unity.Behaviours;
using UnityEngine;

namespace Game.Unity
{
    /// <summary>Unity game extensions</summary>
    public static class Extensions
    {
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
