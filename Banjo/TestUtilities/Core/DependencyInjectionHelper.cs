//-----------------------------------------------------------------------
// <copyright file="DependencyInjectionHelper.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using Core.DependencyInjection;

namespace TestUtilities.Core
{
    /// <summary>Test helpers for dependency injection</summary>
    public static class DependencyInjectionHelper
    {
        /// <summary>
        /// Resets the global container by setting its internal instance null
        /// </summary>
        /// <returns>False if the internal instance was already null; otherwise, True</returns>
        public static bool ResetGlobalContainer()
        {
            if (GlobalContainer.Instance == null)
            {
                return false;
            }

            GlobalContainer.Instance = null;
            System.GC.Collect();
            return true;
        }
    }
}
