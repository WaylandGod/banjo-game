//-----------------------------------------------------------------------
// <copyright file="InputPhase.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Core.Input
{
    /// <summary>Something yadda yadda</summary>
    public enum InputPhase
    {
        /// <summary>Unknown phase</summary>
        Unknown = 0x0,

        /// <summary>Input begins</summary>
        Begin = 0x1,

        /// <summary>Input ends</summary>
        End = 0x2,

        /// <summary>Input continues</summary>
        Continue = 0x3,
    }
}
