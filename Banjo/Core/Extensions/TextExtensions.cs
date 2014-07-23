//-----------------------------------------------------------------------
// <copyright file="TextExtensions.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System.Globalization;
using System.Text;

/// <summary>Extensions for working with text</summary>
public static class TextExtensions
{
    /// <summary>Returns a string formatted using the invariant culture</summary>
    /// <param name="format">Composite format string</param>
    /// <param name="args">Objects to format</param>
    /// <returns>The formatted string</returns>
    public static string FormatInvariant(this string format, params object[] args)
    {
        return string.Format(CultureInfo.InvariantCulture, format, args);
    }
}
