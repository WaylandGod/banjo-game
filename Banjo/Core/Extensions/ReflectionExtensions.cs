//-----------------------------------------------------------------------
// <copyright file="ReflectionExtensions.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Reflection;

/// <summary>Reflection related extensions</summary>
public static class ReflectionExtensions
{
    /// <summary>Attempt to get all types from an assembly</summary>
    /// <param name="assembly">The assembly</param>
    /// <returns>The types, if successful; otherwise an empty array</returns>
    public static Type[] TryGetTypes(this Assembly assembly)
    {
        try
        {
            return assembly.GetTypes();
        }
        catch
        {
            return new Type[0];
        }
    }
}
