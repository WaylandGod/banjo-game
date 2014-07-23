//-----------------------------------------------------------------------
// <copyright file="CompassPoints.cs" company="Benjamin Woodall">
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

namespace Core
{
    /// <summary>Compass points</summary>
    /// <remarks>Only includes cardinal and ordinal points</remarks>
    [Flags]
    public enum CompassPoints
    {
        /// <summary>Unknown (center)</summary>
        Unknown = 0x0,

        /// <summary>North (tramontana)</summary>
        /// <remarks>bits: 1000</remarks>
        North = 0x8,

        /// <summary>Northeast (greco)</summary>
        /// <remarks>bits: 1100</remarks>
        Northeast = North | East,

        /// <summary>East (levante)</summary>
        /// <remarks>bits: 0100</remarks>
        East = 0x4,

        /// <summary>Southeast (Scirocco)</summary>
        /// <remarks>bits: 0110</remarks>
        Southeast = South | East,

        /// <summary>South (ostro)</summary>
        /// <remarks>bits: 0010</remarks>
        South = 0x2,

        /// <summary>Southwest (libeccio)</summary>
        /// <remarks>bits: 0011</remarks>
        Southwest = South | West,

        /// <summary>West (ponente)</summary>
        /// <remarks>bits: 0001</remarks>
        West = 0x1,

        /// <summary>Northwest (maestro)</summary>
        /// <remarks>bits: 1001</remarks>
        Northwest = North | West,
    }
}
