//-----------------------------------------------------------------------
// <copyright file="Vector3.cs" company="Benjamin Woodall">
//  Copyright 2013-2014 Benjamin Woodall
//
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
//
//       http://www.apache.org/licenses/LICENSE-2.0
//
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.InteropServices;

namespace Core
{
    /// <summary>3-dimensional vector</summary>
    public struct Vector3
    {
        /// <summary>Factor for converting radians to angles</summary>
        private const double RadiansToAngle = 180f / Math.PI;

        /// <summary>Factor for converting angles to radians</summary>
        private const double AngleToRadians = Math.PI / 180f;

        /// <summary>Initializes static members of the Vector3 struct</summary>
        [SuppressMessage("Microsoft.Usage", "CA2207", Justification = "Need to self-register parse method with Config")]
        static Vector3()
        {
            Config.AddValueDeserializer<Vector3>(s => Vector3.Parse(s));
        }

        /// <summary>Initializes a new instance of the Vector3 struct</summary>
        /// <param name="source">Source vector</param>
        public Vector3(Vector3 source) : this()
        {
            this.X = source.X;
            this.Y = source.Y;
            this.Z = source.Z;
        }

        /// <summary>Initializes a new instance of the Vector3 struct</summary>
        /// <param name="x">X value</param>
        /// <param name="y">Y value</param>
        /// <param name="z">Z value</param>
        [SuppressMessage("Microsoft.Naming", "CA1704", Justification = "Names are meaningful")]
        public Vector3(double x, double y, double z) : this()
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        /// <summary>Gets a zero vector</summary>
        public static Vector3 Zero
        {
            get { return new Vector3(0, 0, 0); }
        }

        /// <summary>Gets an identity vector</summary>
        public static Vector3 Identity
        {
            get { return new Vector3(1, 1, 1); }
        }

        /// <summary>Gets or sets the horizontal value</summary>
        [SuppressMessage("Microsoft.Naming", "CA1704", Justification = "Name is meaningful")]
        public double X { get; set; }

        /// <summary>Gets or sets the vertical value</summary>
        [SuppressMessage("Microsoft.Naming", "CA1704", Justification = "Name is meaningful")]
        public double Y { get; set; }

        /// <summary>Gets or sets the horizontal depth value</summary>
        [SuppressMessage("Microsoft.Naming", "CA1704", Justification = "Name is meaningful")]
        public double Z { get; set; }

        /// <summary>Gets the magnitude of the vector</summary>
        public double Magnitude
        {
            get { return Math.Sqrt((this.X * this.X) + (this.Y * this.Y) + (this.Z * this.Z)); }
        }

        /// <summary>Gets the unit vector</summary>
        /// <remarks>Vector with the same direction and magnitude 1</remarks>
        public Vector3 UnitVector { get { return this / this.Magnitude; } }

        /// <summary>Gets the flat (X, Z) angle of the vector (in degrees)</summary>
        /// <remarks>Calculated as atan2(Z, X) * 180 / PI</remarks>
        public double AngleXZ { get { return Math.Atan2(this.Z, this.X) * RadiansToAngle; } }

        /// <summary>Gets the flat (X, Y) angle of the vector (in degrees)</summary>
        /// <remarks>Calculated as atan2(Y, X) * 180 / PI</remarks>
        public double AngleXY { get { return Math.Atan2(this.Y, this.X) * RadiansToAngle; } }

        /// <summary>Gets the flat (X, Z) magnitude of the vector</summary>
        public double MagnitudeXZ { get { return Math.Sqrt((this.X * this.X) + (this.Z * this.Z)); } }

        /// <summary>Gets the flat (X, Y) magnitude of the vector</summary>
        public double MagnitudeXY { get { return Math.Sqrt((this.X * this.X) + (this.Y * this.Y)); } }

        /// <summary>Multiplies a vector</summary>
        /// <param name="vector">The vector</param>
        /// <param name="factor">The factor</param>
        /// <returns>The scaled vector</returns>
        public static Vector3 operator *(Vector3 vector, double factor)
        {
            return Vector3.Multiply(vector, factor);
        }

        /// <summary>Multiplies a vector</summary>
        /// <param name="factor">The factor</param>
        /// <param name="vector">The vector</param>
        /// <returns>The scaled vector</returns>
        public static Vector3 operator *(double factor, Vector3 vector)
        {
            return Vector3.Multiply(vector, factor);
        }

        /// <summary>Divides a vector</summary>
        /// <param name="vector">The vector</param>
        /// <param name="divisor">The divisor</param>
        /// <returns>The divided vector</returns>
        public static Vector3 operator /(Vector3 vector, double divisor)
        {
            return Vector3.Divide(vector, divisor);
        }

        /// <summary>Adds to a vector</summary>
        /// <param name="vector">The vector</param>
        /// <param name="delta">The delta</param>
        /// <returns>The resulting vector</returns>
        public static Vector3 operator +(Vector3 vector, Vector3 delta)
        {
            return Vector3.Add(vector, delta);
        }

        /// <summary>Subtracts from a vector</summary>
        /// <param name="vector">The vector</param>
        /// <param name="delta">The delta</param>
        /// <returns>The resulting vector</returns>
        public static Vector3 operator -(Vector3 vector, Vector3 delta)
        {
            return Vector3.Subtract(vector, delta);
        }

        /// <summary>Negates a vector</summary>
        /// <param name="vector">The vector</param>
        /// <returns>The negated vector</returns>
        public static Vector3 operator -(Vector3 vector)
        {
            return Vector3.Negate(vector);
        }

        /// <summary>Determines whether two Vector3 values are equal</summary>
        /// <param name="vectorA">First vector</param>
        /// <param name="vectorB">Second vector</param>
        /// <returns>True if all axis are of equal value; otherwise, false</returns>
        public static bool operator ==(Vector3 vectorA, Vector3 vectorB) { return Vector3.Equal(vectorA, vectorB); }

        /// <summary>Determines whether two Vector3 values are inequal</summary>
        /// <param name="vectorA">First vector</param>
        /// <param name="vectorB">Second vector</param>
        /// <returns>True if any axis are of inequal value; otherwise, false</returns>
        public static bool operator !=(Vector3 vectorA, Vector3 vectorB) { return !Vector3.Equal(vectorA, vectorB); }

        /// <summary>Gets a Vector2 representation of the Vector3</summary>
        /// <param name="vector">The 3-dimensional vector</param>
        /// <returns>The 2-dimensional vector</returns>
        public static implicit operator Vector2(Vector3 vector)
        {
            return vector.ToVector2();
        }

        /// <summary>Multiplies a vector</summary>
        /// <param name="vector">The vector</param>
        /// <param name="factor">The factor</param>
        /// <returns>The scaled vector</returns>
        public static Vector3 Multiply(Vector3 vector, double factor)
        {
            return new Vector3(vector.X * factor, vector.Y * factor, vector.Z * factor);
        }

        /// <summary>Divides a vector</summary>
        /// <param name="vector">The vector</param>
        /// <param name="divisor">The divisor</param>
        /// <returns>The divided vector</returns>
        public static Vector3 Divide(Vector3 vector, double divisor)
        {
            return new Vector3(vector.X / divisor, vector.Y / divisor, vector.Z / divisor);
        }

        /// <summary>Adds to a vector</summary>
        /// <param name="vector">The vector</param>
        /// <param name="delta">The delta</param>
        /// <returns>The resulting vector</returns>
        public static Vector3 Add(Vector3 vector, Vector3 delta)
        {
            return new Vector3(vector.X + delta.X, vector.Y + delta.Y, vector.Z + delta.Z);
        }

        /// <summary>Subtracts from a vector</summary>
        /// <param name="vector">The vector</param>
        /// <param name="delta">The delta</param>
        /// <returns>The resulting vector</returns>
        public static Vector3 Subtract(Vector3 vector, Vector3 delta)
        {
            return new Vector3(vector.X - delta.X, vector.Y - delta.Y, vector.Z - delta.Z);
        }

        /// <summary>Negates a vector</summary>
        /// <param name="vector">The vector</param>
        /// <returns>The negated vector</returns>
        public static Vector3 Negate(Vector3 vector)
        {
            return Vector3.Subtract(Vector3.Zero, vector);
        }

        /// <summary>Determines whether two Vector3 values are equal</summary>
        /// <param name="vectorA">First vector</param>
        /// <param name="vectorB">Second vector</param>
        /// <returns>True if all axis are of equal value; otherwise, false</returns>
        public static bool Equal(Vector3 vectorA, Vector3 vectorB)
        {
            return vectorA.X == vectorB.X && vectorA.Y == vectorB.Y && vectorA.Z == vectorB.Z;
        }

        /// <summary>Creates a Vector3 from a horizontal (X, Z) angle and magnitude</summary>
        /// <param name="angleXZ">Horizontal (X, Z) angle</param>
        /// <param name="magnitude">Horizontal magnitude</param>
        /// <returns>The created vector</returns>
        public static Vector3 FromAngleXZAndMagnitude(double angleXZ, double magnitude)
        {
            var r = angleXZ * AngleToRadians;
            return new Vector3(magnitude * Math.Sin(r), 0f, magnitude * Math.Cos(r));
        }

        /// <summary>Parses a Vector3 from a comma separated string</summary>
        /// <param name="text">Vector string</param>
        /// <returns>The parsed vector</returns>
        public static Vector3 Parse(string text)
        {
            var values = text.Split(',').Select(v => v.Trim('(', ')', ' ')).Select(v => double.Parse(v)).ToArray();
            if (values.Length != 3)
            {
                throw new System.FormatException("'{0}' is not a valid Vector3".FormatInvariant(text));
            }

            return new Vector3(values[0], values[1], values[2]);
        }

        /// <summary>Gets a Vector2 representation of the Vector3</summary>
        /// <returns>The 2-dimensional vector</returns>
        public Vector2 ToVector2()
        {
            return new Vector2 { X = (float)this.X, Y = (float)this.Y };
        }

        /// <summary>Determines whether the specified object is equal to the current RuntimeId</summary>
        /// <param name="obj">Object to compare with the current RuntimeId</param>
        /// <returns>True if the specified object is equal to the current RuntimeId; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            var type = obj.GetType();
            if (type != typeof(Vector3) && type != typeof(Vector2))
            {
                return false;
            }

            return Vector3.Equal(this, (Vector3)obj);
        }

        /// <summary>Gets a hashcode for the Vector3</summary>
        /// <returns>Hashcode representing the Vector3</returns>
        public override int GetHashCode()
        {
            return (int)(this.X + this.Y + this.Z);
        }

        /// <summary>Gets a string representation of the Vector3</summary>
        /// <returns>The string vector</returns>
        public override string ToString()
        {
            return "({0},{1},{2})".FormatInvariant(this.X, this.Y, this.Z);
        }
    }
}
