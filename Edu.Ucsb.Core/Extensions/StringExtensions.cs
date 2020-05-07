using System;

namespace Edu.Ucsb.Core.Extensions
{
    /// <summary>
    /// Extensions of <see cref="string"/>.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Returns <c>true</c> when the strings are equal without regard to cultural locales
        /// or casing.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="otherString"></param>
        public static bool EqualsInvariant(this string input, string otherString)
        {
            return input.EqualsInvariant(otherString, ignoreCase : true);
        }

        /// <summary>
        /// Returns <c>true</c> when the strings are equal without regard to cultural locales.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="otherString"></param>
        /// <param name="ignoreCase"></param>
        /// <returns></returns>
        public static bool EqualsInvariant(this string input, string otherString, bool ignoreCase)
        {
            return ignoreCase ?
                string.Equals(input, otherString, StringComparison.InvariantCultureIgnoreCase) :
                string.Equals(input, otherString, StringComparison.InvariantCulture);
        }
        /// <summary>
        /// Truncates the specified input to 16 characters.
        /// <param name="input">The input.</param>
        /// </summary>
        public static string Truncate(this string input)
        {
            return input.Truncate(length: 16, ellipsis: "…");
        }

        /// <summary>
        /// Truncates the specified input to 16 characters.
        /// <param name="input">The input.</param>
        /// <param name="length">The length.</param>
        /// </summary>
        public static string Truncate(this string input, int length)
        {
            return input.Truncate(length, ellipsis: "…");
        }

        /// <summary>
        /// Truncates the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="length">The length.</param>
        /// <param name="ellipsis"></param>
        public static string Truncate(this string input, int length, string ellipsis)
        {
            if (string.IsNullOrWhiteSpace(input)) return input;
            if (input.Length <= length) return input;
            if (length <= 0) length = 0;
            return string.Concat(input.Substring(0, length).TrimEnd(), ellipsis);
        }
    }
}