using System;
using System.Collections.Generic;
using System.Text;

namespace Resizer.Gui.Infrastructure.Extensions
{
    /// <summary>
    /// Defines a class that contains all the extensions for <see cref="string"/>
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Checks if the current string contains the content of another string
        /// </summary>
        /// <param name="source"><see cref="string"/> that will be checked to see if it contains the content of another string</param>
        /// <param name="other"><see cref="string"/> containing the filter to be checked for</param>
        /// <param name="comparison"><see cref="StringComparison"/> that dictates the culture, casing and sort rule aaplied during the contains check</param>
        /// <returns>Returns <see langword="true"/> if the source contains the filter, otherwise returns <see langword="false"/></returns>
        public static bool Contains(this string source, string other, StringComparison comparison)
        {
            return source?.IndexOf(other, comparison) >= 0;
        }
    }
}
