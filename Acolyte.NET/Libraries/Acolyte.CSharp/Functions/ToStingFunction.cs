using System;

namespace Acolyte.Functions
{
    /// <summary>
    /// Helper class that provides static selector functions for string conversion.
    /// </summary>
    /// <typeparam name="TElement">
    /// The type of the elements to convert to <see cref="string" />.
    /// </typeparam>
    public static class ToStingFunction<TElement>
    {
        /// <summary>
        /// Simple selector function for objects.
        /// </summary>
        /// <returns>
        /// Function to convert value of type <typeparamref name="TElement" /> to string.
        /// If value of element is <see langword="null" />, <see cref="string.Empty" /> will be
        /// returned. Otherwise, result of call <see cref="object.ToString" /> will be returned.
        /// </returns>
        public static Func<TElement, string> Simple { get; }
            = x => x is null ? string.Empty : x.ToString();

        /// <summary>
        /// Selector function that add single quotes to result of string conversion.
        /// </summary>
        /// <returns>
        /// Function to convert value of type <typeparamref name="TElement" /> to string.
        /// If value of element is <see langword="null" />, <see cref="string.Empty" /> will be
        /// returned. Otherwise, quoted result of call <see cref="object.ToString" /> will be
        /// returned.
        /// </returns>
        public static Func<TElement, string> WithSingleQuotes { get; }
            = x => x is null ? string.Empty : $"'{x.ToString()}'";

        /// <summary>
        /// Selector function that add double quotes to result of string conversion.
        /// </summary>
        /// <returns>
        /// Function to convert value of type <typeparamref name="TElement" /> to string.
        /// If value of element is <see langword="null" />, <see cref="string.Empty" /> will be
        /// returned. Otherwise, quoted result of call <see cref="object.ToString" /> will be
        /// returned.
        /// </returns>
        public static Func<TElement, string> WithDoubleQuotes { get; }
            = x => x is null ? string.Empty : $"\"{x.ToString()}\"";
    }
}
