//-----------------------------------------------------------------------
// <copyright file="Extensions.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game
{
    /// <summary>Extensions for working with Game types</summary>
    public static class Extensions
    {
        /// <summary>Computes the sum of a sequence of Vector3 values.</summary>
        /// <param name="source">A sequence of Vector3D values.</param>
        /// <returns>The sum of the values in the sequence.</returns>
        /// <exception cref="System.ArgumentNullException">Source is null.</exception>
        public static Vector3D Sum(this IEnumerable<Vector3D> source)
        {
            if (source == null) throw new ArgumentNullException("source");

            var sum = Vector3D.Zero;
            foreach (var vector in source) sum += vector;
            return sum;
        }
    }
}