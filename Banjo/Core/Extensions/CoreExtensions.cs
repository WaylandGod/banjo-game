//-----------------------------------------------------------------------
// <copyright file="CoreExtensions.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

namespace Core
{
    /// <summary>Extensions for working with core types</summary>
    public static class CoreExtensions
    {
        /// <summary>Lookup table for compass point angles</summary>
        internal static readonly IDictionary<CompassPoints, float> CompassPointAngles =
            new Dictionary<CompassPoints, float>
            {
                { CompassPoints.North, 0 },
                { CompassPoints.Northeast, 45 },
                { CompassPoints.East, 90 },
                { CompassPoints.Southeast, 135 },
                { CompassPoints.South, 180 },
                { CompassPoints.Southwest, 225 },
                { CompassPoints.West, 270 },
                { CompassPoints.Northwest, 315 },
                { CompassPoints.Unknown, 0 },
            };

        /// <summary>Gets the angle corresponding to the compass point</summary>
        /// <param name="point">Compass point</param>
        /// <returns>The angle</returns>
        public static float ToAngle(this CompassPoints point)
        {
            return CompassPointAngles[point];
        }

        /// <summary>Gets the compass point nearest to the angle</summary>
        /// <param name="angle">Angle (degrees)</param>
        /// <returns>The compass point</returns>
        public static CompassPoints ToCompassPoint(this float angle)
        {
            while (angle < 0)
            {
                angle += 360;
            }

            angle %= 360;
            if (angle > 337.5)
            {
                return CompassPoints.North;
            }

            return CompassPointAngles.First(kvp =>
                {
                    var delta = Math.Abs(kvp.Value - angle);
                    return delta <= 22.5;
                }).Key;
        }

        /// <summary>Computes the sum of a sequence of Core.Vector3 values.</summary>
        /// <param name="source">A sequence of Core.Vector3 values.</param>
        /// <returns>The sum of the values in the sequence.</returns>
        /// <exception cref="System.ArgumentNullException">Source is null.</exception>
        public static Vector3 Sum(this IEnumerable<Vector3> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            var sum = Vector3.Zero;
            foreach (var vector in source)
            {
                sum += vector;
            }

            return sum;
        }
    }
}