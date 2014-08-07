//-----------------------------------------------------------------------
// <copyright file="Vector2.cs" company="Benjamin Woodall">
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

using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Core;

namespace Game
{
    /// <summary>2-dimensional vector using doubles</summary>
    public struct Vector2D
    {
        /// <summary>Initializes static members of the Vector2 struct</summary>
        static Vector2D()
        {
            Config.AddValueDeserializer<Vector2D>(s => Vector2D.Parse(s));
        }

        /// <summary>Initializes a new instance of the Vector2 struct</summary>
        public Vector2D(float x, float y) : this()
        {
            this.X = x;
            this.Y = y;
        }

        /// <summary>Gets a zero vector</summary>
        public static Vector2D Zero
        {
            get { return new Vector2D(0, 0); }
        }

        /// <summary>Gets an identity vector</summary>
        public static Vector2D Identity
        {
            get { return new Vector2D(1, 1); }
        }

        /// <summary>Gets or sets the horizontal value</summary>
        public float X { get; set; }

        /// <summary>Gets or sets the vertical value</summary>
        public float Y { get; set; }

        /// <summary>Parses a Vector2 from a comma separated string</summary>
        /// <param name="text">Vector string</param>
        /// <returns>The parsed vector</returns>
        public static Vector2D Parse(string text)
        {
            var values = text.Split(',').Select(v => v.Trim('(', ')', ' ')).Select(v => double.Parse(v)).ToArray();
            if (values.Length != 2)
            {
                throw new System.FormatException("'{0}' is not a valid Vector2D".FormatInvariant(text));
            }

            return new Vector2D((float)values[0], (float)values[1]);
        }

        /// <summary>Multiplies a vector</summary>
        /// <param name="vector">The vector</param>
        /// <param name="factor">The factor</param>
        /// <returns>The scaled vector</returns>
        public static Vector2D operator *(Vector2D vector, float factor)
        {
            return Vector2D.Multiply(vector, factor);
        }

        /// <summary>Divides a vector</summary>
        /// <param name="vector">The vector</param>
        /// <param name="divisor">The divisor</param>
        /// <returns>The divided vector</returns>
        public static Vector2D operator /(Vector2D vector, float divisor)
        {
            return Vector2D.Divide(vector, divisor);
        }

        /// <summary>Gets a Vector3 representation of the Vector2</summary>
        /// <param name="vector">The 2-dimensional vector</param>
        /// <returns>The 3-dimensional vector</returns>
        public static implicit operator Vector3D(Vector2D vector)
        {
            return vector.ToVector3D();
        }

        /// <summary>Multiplies a vector</summary>
        /// <param name="vector">The vector</param>
        /// <param name="factor">The factor</param>
        /// <returns>The scaled vector</returns>
        public static Vector2D Multiply(Vector2D vector, float factor)
        {
            return new Vector2D(vector.X * factor, vector.Y * factor);
        }

        /// <summary>Divides a vector</summary>
        /// <param name="vector">The vector</param>
        /// <param name="divisor">The divisor</param>
        /// <returns>The divided vector</returns>
        public static Vector2D Divide(Vector2D vector, float divisor)
        {
            return new Vector2D(vector.X / divisor, vector.Y / divisor);
        }

        /// <summary>Gets a Vector3 representation of this Vector2</summary>
        /// <returns>The 3-dimensional vector</returns>
        public Vector3D ToVector3D()
        {
            return new Vector3D(this.X, this.Y, 0f);
        }

        /// <summary>Gets a string representation of the Vector2</summary>
        /// <returns>The string vector</returns>
        public override string ToString()
        {
            return "({0},{1})".FormatInvariant(this.X, this.Y);
        }
    }
}
