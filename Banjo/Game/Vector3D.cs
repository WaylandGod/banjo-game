//-----------------------------------------------------------------------
// <copyright file="Vector3D.cs" company="Benjamin Woodall">
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
using Core;

namespace Game
{
    /// <summary>3-dimensional vector using doubles</summary>
    public struct Vector3D
    {
        /// <summary>Factor for converting radians to angles</summary>
        private const double RadiansToAngle = 180.0 / Math.PI;

        /// <summary>Factor for converting angles to radians</summary>
        private const double AngleToRadians = Math.PI / 180.0;

        /// <summary>Initializes static members of the Vector3D struct</summary>
        static Vector3D()
        {
            Config.AddValueDeserializer<Vector3D>(s => Vector3D.Parse(s));
        }

        /// <summary>Initializes a new instance of the Vector3D struct</summary>
        /// <param name="source">Source vector</param>
        public Vector3D(Vector3D source) : this(source.X, source.Y, source.Z)
        {
        }

        /// <summary>Initializes a new instance of the Vector3D struct</summary>
        /// <param name="source">Source vector</param>
        public Vector3D(UnityEngine.Vector3 source) : this(source.x, source.y, source.z)
        {
        }

        /// <summary>Initializes a new instance of the Vector3D struct</summary>
        public Vector3D(double x, double y, double z) : this()
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        /// <summary>Gets a zero vector</summary>
        public static Vector3D Zero
        {
            get { return new Vector3D(0, 0, 0); }
        }

        /// <summary>Gets an identity vector</summary>
        public static Vector3D Identity
        {
            get { return new Vector3D(1, 1, 1); }
        }

        /// <summary>Gets or sets the horizontal value</summary>
        public double X { get; private set; }

        /// <summary>Gets or sets the vertical value</summary>
        public double Y { get; private set; }

        /// <summary>Gets or sets the horizontal depth value</summary>
        public double Z { get; private set; }

        /// <summary>Gets the magnitude of the vector</summary>
        public double Magnitude
        {
            get { return Math.Sqrt((this.X * this.X) + (this.Y * this.Y) + (this.Z * this.Z)); }
        }

        /// <summary>Gets the squared magnitude of the vector</summary>
        /// <remarks>For simple distance comparisons. Saves the expense of taking the square root.</remarks>
        public double MagnitudeSquared
        {
            get { return (this.X * this.X) + (this.Y * this.Y) + (this.Z * this.Z); }
        }

        /// <summary>Gets the normalized value</summary>
        /// <remarks>Vector with the same direction and magnitude 1</remarks>
        public Vector3D Normalized { get { return this / this.Magnitude; } }

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
        public static Vector3D operator *(Vector3D vector, double factor)
        {
            return Vector3D.Multiply(vector, factor);
        }

        /// <summary>Multiplies a vector</summary>
        /// <param name="factor">The factor</param>
        /// <param name="vector">The vector</param>
        /// <returns>The scaled vector</returns>
        public static Vector3D operator *(double factor, Vector3D vector)
        {
            return Vector3D.Multiply(vector, factor);
        }

        /// <summary>Divides a vector</summary>
        /// <param name="vector">The vector</param>
        /// <param name="divisor">The divisor</param>
        /// <returns>The divided vector</returns>
        public static Vector3D operator /(Vector3D vector, double divisor)
        {
            return Vector3D.Divide(vector, divisor);
        }

        /// <summary>Adds to a vector</summary>
        /// <param name="vector">The vector</param>
        /// <param name="delta">The delta</param>
        /// <returns>The resulting vector</returns>
        public static Vector3D operator +(Vector3D vector, Vector3D delta)
        {
            return Vector3D.Add(vector, delta);
        }

        /// <summary>Subtracts from a vector</summary>
        /// <param name="vector">The vector</param>
        /// <param name="delta">The delta</param>
        /// <returns>The resulting vector</returns>
        public static Vector3D operator -(Vector3D vector, Vector3D delta)
        {
            return Vector3D.Subtract(vector, delta);
        }

        /// <summary>Negates a vector</summary>
        /// <param name="vector">The vector</param>
        /// <returns>The negated vector</returns>
        public static Vector3D operator -(Vector3D vector)
        {
            return Vector3D.Negate(vector);
        }

        /// <summary>Determines whether two Vector3D values are equal</summary>
        /// <param name="vectorA">First vector</param>
        /// <param name="vectorB">Second vector</param>
        /// <returns>True if all axis are of equal value; otherwise, false</returns>
        public static bool operator ==(Vector3D vectorA, Vector3D vectorB) { return Vector3D.Equal(vectorA, vectorB); }

        /// <summary>Determines whether two Vector3D values are inequal</summary>
        /// <param name="vectorA">First vector</param>
        /// <param name="vectorB">Second vector</param>
        /// <returns>True if any axis are of inequal value; otherwise, false</returns>
        public static bool operator !=(Vector3D vectorA, Vector3D vectorB) { return !Vector3D.Equal(vectorA, vectorB); }

        /// <summary>Gets a Vector2D representation of the Vector3D</summary>
        public static implicit operator Vector2D(Vector3D vector)
        {
            return vector.ToVector2D();
        }

        /// <summary>Gets a UnityEngine.Vector3 representation of the Game.Vector3D</summary>
        public static implicit operator UnityEngine.Vector3(Vector3D vector)
        {
            return new UnityEngine.Vector3((float)vector.X, (float)vector.Y, (float)vector.Z);
        }

        /// <summary>Gets a Game.Vector3D representation of the UnityEngine.Vector3</summary>
        public static implicit operator Vector3D(UnityEngine.Vector3 vector)
        {
            return new Vector3D(vector);
        }

        /// <summary>Multiplies a vector</summary>
        /// <param name="vector">The vector</param>
        /// <param name="factor">The factor</param>
        /// <returns>The scaled vector</returns>
        public static Vector3D Multiply(Vector3D vector, double factor)
        {
            return new Vector3D(vector.X * factor, vector.Y * factor, vector.Z * factor);
        }

        /// <summary>Divides a vector</summary>
        /// <param name="vector">The vector</param>
        /// <param name="divisor">The divisor</param>
        /// <returns>The divided vector</returns>
        public static Vector3D Divide(Vector3D vector, double divisor)
        {
            return new Vector3D(vector.X / divisor, vector.Y / divisor, vector.Z / divisor);
        }

        /// <summary>Adds to a vector</summary>
        /// <param name="vector">The vector</param>
        /// <param name="delta">The delta</param>
        /// <returns>The resulting vector</returns>
        public static Vector3D Add(Vector3D vector, Vector3D delta)
        {
            return new Vector3D(vector.X + delta.X, vector.Y + delta.Y, vector.Z + delta.Z);
        }

        /// <summary>Subtracts from a vector</summary>
        /// <param name="vector">The vector</param>
        /// <param name="delta">The delta</param>
        /// <returns>The resulting vector</returns>
        public static Vector3D Subtract(Vector3D vector, Vector3D delta)
        {
            return new Vector3D(vector.X - delta.X, vector.Y - delta.Y, vector.Z - delta.Z);
        }

        /// <summary>Negates a vector</summary>
        /// <param name="vector">The vector</param>
        /// <returns>The negated vector</returns>
        public static Vector3D Negate(Vector3D vector)
        {
            return Vector3D.Subtract(Vector3D.Zero, vector);
        }

        /// <summary>Determines whether two Vector3D values are equal</summary>
        /// <param name="vectorA">First vector</param>
        /// <param name="vectorB">Second vector</param>
        /// <returns>True if all axis are of equal value; otherwise, false</returns>
        public static bool Equal(Vector3D vectorA, Vector3D vectorB)
        {
            return vectorA.X == vectorB.X && vectorA.Y == vectorB.Y && vectorA.Z == vectorB.Z;
        }

        /// <summary>Creates a Vector3D from a horizontal (X, Z) angle and magnitude</summary>
        /// <param name="angleXZ">Horizontal (X, Z) angle</param>
        /// <param name="magnitude">Horizontal magnitude</param>
        /// <returns>The created vector</returns>
        public static Vector3D FromAngleXZAndMagnitude(double angleXZ, double magnitude)
        {
            var r = angleXZ * AngleToRadians;
            return new Vector3D(magnitude * Math.Sin(r), 0f, magnitude * Math.Cos(r));
        }

        /// <summary>Parses a Vector3D from a comma separated string</summary>
        /// <param name="text">Vector string</param>
        /// <returns>The parsed vector</returns>
        public static Vector3D Parse(string text)
        {
            var values = text.Split(',').Select(v => v.Trim('(', ')', ' ')).Select(v => double.Parse(v)).ToArray();
            if (values.Length != 3)
            {
                throw new System.FormatException("'{0}' is not a valid Vector3D".FormatInvariant(text));
            }

            return new Vector3D(values[0], values[1], values[2]);
        }

        /// <summary>Gets a Vector2 representation of the Vector3D</summary>
        /// <returns>The 2-dimensional vector</returns>
        public Vector2D ToVector2D()
        {
            return new Vector2D((float)this.X, (float)this.Y);
        }

        /// <summary>Sets the X value and returns the vector</summary>
        public Vector3D SetX(float x) { this.X = x; return this; }

        /// <summary>Sets the Y value and returns the vector</summary>
        public Vector3D SetY(float y) { this.Y = y; return this; }

        /// <summary>Sets the Z value and returns the vector</summary>
        public Vector3D SetZ(float z) { this.Z = z; return this; }

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
            if (type != typeof(Vector3D) && type != typeof(Vector2D))
            {
                return false;
            }

            return Vector3D.Equal(this, (Vector3D)obj);
        }

        /// <summary>Gets a hashcode for the Vector3D</summary>
        /// <returns>Hashcode representing the Vector3D</returns>
        public override int GetHashCode()
        {
            return this.X.GetHashCode() ^ this.Y.GetHashCode() ^ this.Z.GetHashCode();
        }

        /// <summary>Gets a string representation of the Vector3D</summary>
        /// <returns>The string vector</returns>
        public override string ToString()
        {
            return "({0},{1},{2})".FormatInvariant(this.X, this.Y, this.Z);
        }
    }
}
