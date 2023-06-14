using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Acolyte.Assertions
{
    public static partial class ThrowsExtensions
    {
        /// <summary>
        /// Checks if the string is <see langword="null" /> or empty.
        /// </summary>
        /// <param name="str">String to check.</param>
        /// <param name="paramName">
        /// Name of the parameter for error message. Use operator <see langword="nameof" /> to get
        /// proper parameter name.
        /// </param>
        /// <returns>The original string.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="str" /> is <see langword="null" />. -or-
        /// <paramref name="paramName" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="str" /> presents empty string.
        /// </exception>
        [return: NotNull]
        public static string ThrowIfNullOrEmpty([NotNull] this string? str, string paramName)
        {
            paramName.ThrowIfNull(nameof(paramName));

            if (str is null)
            {
                throw new ArgumentNullException(paramName);
            }
            if (str.Length == 0)
            {
                throw new ArgumentException($"{paramName} presents empty string.", paramName);
            }

            return str;
        }

        /// <summary>
        /// Checks if the string is <see langword="null" />, empty or contains only whitespaces.
        /// </summary>
        /// <param name="str">String to check.</param>
        /// <param name="paramName">
        /// Name of the parameter for error message. Use operator <see langword="nameof" /> to get
        /// proper parameter name.
        /// </param>
        /// <returns>The original string.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="str" /> is <see langword="null" />. -or-
        /// <paramref name="paramName" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="str" /> presents empty string or contains only whitespaces.
        /// </exception>
        [return: NotNull]
        public static string ThrowIfNullOrWhiteSpace([NotNull] this string? str, string paramName)
        {
            paramName.ThrowIfNull(nameof(paramName));

            if (str is null)
            {
                throw new ArgumentNullException(paramName);
            }
            if (str.Length == 0)
            {
                throw new ArgumentException($"{paramName} presents empty string.", paramName);
            }
            if (str.All(char.IsWhiteSpace))
            {
                throw new ArgumentException($"{paramName} contains only whitespaces.", paramName);
            }

            return str;
        }
    }
}
