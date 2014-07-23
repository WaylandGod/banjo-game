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

namespace Core
{
    /// <summary>2-dimensional vector</summary>
    public struct Vector2
    {
        /// <summary>Initializes static members of the Vector2 struct</summary>
        [SuppressMessage("Microsoft.Usage", "CA2207", Justification = "Need to self-register parse method with Config")]
        static Vector2()
        {
            Config.AddValueDeserializer<Vector2>(s => Vector2.Parse(s));
        }

        /// <summary>Initializes a new instance of the Vector2 struct</summary>
        /// <param name="x">X value</param>
        /// <param name="y">Y value</param>
        [SuppressMessage("Microsoft.Naming", "CA1704", Justification = "Names are meaningful")]
        public Vector2(float x, float y) : this()
        {
            this.X = x;
            this.Y = y;
        }

        /// <summary>Gets a zero vector</summary>
        public static Vector2 Zero
        {
            get { return new Vector2(0, 0); }
        }

        /// <summary>Gets an identity vector</summary>
        public static Vector2 Identity
        {
            get { return new Vector2(1, 1); }
        }

        /// <summary>Gets or sets the horizontal value</summary>
        [SuppressMessage("Microsoft.Naming", "CA1704", Justification = "Name is meaningful")]
        public float X { get; set; }

        /// <summary>Gets or sets the vertical value</summary>
        [SuppressMessage("Microsoft.Naming", "CA1704", Justification = "Name is meaningful")]
        public float Y { get; set; }

        /// <summary>Parses a Vector2 from a comma separated string</summary>
        /// <param name="text">Vector string</param>
        /// <returns>The parsed vector</returns>
        public static Vector2 Parse(string text)
        {
            var values = text.Split(',').Select(v => v.Trim('(', ')', ' ')).Select(v => float.Parse(v)).ToArray();
            if (values.Length != 2)
            {
                throw new System.FormatException("'{0}' is not a valid Vector2".FormatInvariant(text));
            }

            return new Vector3(values[0], values[1], values[2]);
        }

        /// <summary>Multiplies a vector</summary>
        /// <param name="vector">The vector</param>
        /// <param name="factor">The factor</param>
        /// <returns>The scaled vector</returns>
        public static Vector2 operator *(Vector2 vector, float factor)
        {
            return Vector2.Multiply(vector, factor);
        }

        /// <summary>Divides a vector</summary>
        /// <param name="vector">The vector</param>
        /// <param name="divisor">The divisor</param>
        /// <returns>The divided vector</returns>
        public static Vector2 operator /(Vector2 vector, float divisor)
        {
            return Vector2.Divide(vector, divisor);
        }

        /// <summary>Gets a Vector3 representation of the Vector2</summary>
        /// <param name="vector">The 2-dimensional vector</param>
        /// <returns>The 3-dimensional vector</returns>
        public static implicit operator Vector3(Vector2 vector)
        {
            return vector.ToVector3();
        }

        /// <summary>Multiplies a vector</summary>
        /// <param name="vector">The vector</param>
        /// <param name="factor">The factor</param>
        /// <returns>The scaled vector</returns>
        public static Vector2 Multiply(Vector2 vector, float factor)
        {
            return new Vector2(vector.X * factor, vector.Y * factor);
        }

        /// <summary>Divides a vector</summary>
        /// <param name="vector">The vector</param>
        /// <param name="divisor">The divisor</param>
        /// <returns>The divided vector</returns>
        public static Vector2 Divide(Vector2 vector, float divisor)
        {
            return new Vector2(vector.X / divisor, vector.Y / divisor);
        }

        /// <summary>Gets a Vector3 representation of this Vector2</summary>
        /// <returns>The 3-dimensional vector</returns>
        public Vector3 ToVector3()
        {
            return new Vector3 { X = this.X, Y = this.Y, Z = 0f };
        }

        /// <summary>Gets a string representation of the Vector2</summary>
        /// <returns>The string vector</returns>
        public override string ToString()
        {
            return "({0},{1})".FormatInvariant(this.X, this.Y);
        }
    }
}
