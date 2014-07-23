//-----------------------------------------------------------------------
// <copyright file="TestExtensions.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using Core;

namespace TestUtilities.Core
{
    /// <summary>Extensions for core testing</summary>
    public static class TestExtensions
    {
        /// <summary>Returns a random Vector3</summary>
        /// <param name="random">System.Random instance</param>
        /// <returns>The random Vector3</returns>
        public static Vector3 NextVector3(this Random random)
        {
            return new Vector3(
                (random.Next() - random.Next()) * (float)random.NextDouble(),
                (random.Next() - random.Next()) * (float)random.NextDouble(),
                (random.Next() - random.Next()) * (float)random.NextDouble());
        }
    }
}
