//-----------------------------------------------------------------------
// <copyright file="SafeECall.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Security;
using Core;

namespace Core.Unity
{
    /// <summary>Wrapper for safely calling native Unity ECall methods</summary>
    /// <remarks>
    /// Calling native methods outside Unity will throw SecurityExceptions.
    /// This wrapper catches those exceptions and logs warnings instead.
    /// </remarks>
    public static class SafeECall
    {
        /// <summary>Safely invokes a function with no parameters and returns the result</summary>
        /// <remarks>
        /// If input values are required, func should be a lambda and input should be provided in the lambda scope.
        /// </remarks>
        /// <typeparam name="TResult">Type of the result</typeparam>
        /// <param name="func">Dangerous ECall function to invoke</param>
        /// <returns>The result</returns>
        public static TResult Invoke<TResult>(Func<TResult> func)
        {
            TResult result = default(TResult);
            Invoke((Action)(() => result = func()));
            return result;
        }

        /// <summary>Safely invokes an action with no parameters and no return</summary>
        /// <param name="action">Dangerous ECall action to invoke</param>
        public static void Invoke(Action action)
        {
#if SAFE_ECALLS
            try
            {
#endif
            action();
#if SAFE_ECALLS
            }
            catch (SecurityException e)
            {
                if (!e.Message.Contains("ECall"))
                {
                    throw;
                }

                Log.Error("Unable to set WorldBehaviour.Instance.World outside of Unity: {0}", e);
            }
#endif
        }
    }
}
