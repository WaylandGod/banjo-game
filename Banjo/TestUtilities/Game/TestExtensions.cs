//-----------------------------------------------------------------------
// <copyright file="TestExtensions.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using Game;

namespace TestUtilities.Game
{
    /// <summary>Extensions for core testing</summary>
    public static class TestExtensions
    {
        /// <summary>Returns a random Vector3</summary>
        /// <param name="random">System.Random instance</param>
        /// <returns>The random Vector3</returns>
        public static Vector3D NextVector3(this global::System.Random random)
        {
            return new Vector3D(
                (random.Next() - random.Next()) * (float)random.NextDouble(),
                (random.Next() - random.Next()) * (float)random.NextDouble(),
                (random.Next() - random.Next()) * (float)random.NextDouble());
        }
    }
}
