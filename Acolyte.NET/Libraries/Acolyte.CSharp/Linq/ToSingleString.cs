using System;
using System.Collections.Generic;
using System.Linq;
using Acolyte.Assertions;
using Acolyte.Common;
using Acolyte.Functions;

namespace Acolyte.Linq
{
    public static partial class EnumerableExtensions
    {
        /// <summary>
        /// Transforms sequence to the string.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements of <paramref name="source" />.
        /// </typeparam>
        /// <param name="source">A sequence of values to convert to string.</param>
        /// <returns>
        /// The string which represent converted value of sequence or special message if 
        /// <paramref name="source" /> is <see langword="null" /> or contains no elements.
        /// </returns>
        public static string ToSingleString<TSource>(this IEnumerable<TSource>? source)
        {
            return source.ToSingleString(
                emptyCollectionMessage: Strings.DefaultEmptyCollectionMessage,
                separator: Strings.DefaultItemSeparator,
                selector: item => ToStingFunction<TSource>.WithSingleQuotes(item)
            );
        }

        /// <summary>
        /// Transforms sequence to the string or returns message when sequence contains no elements.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements of <paramref name="source" />.
        /// </typeparam>
        /// <param name="source">A sequence of values to convert to string.</param>
        /// <param name="emptyCollectionMessage">
        /// The string to return if <paramref name="source" /> is <see langword="null" /> or
        /// contains no elements.
        /// </param>
        /// <returns>
        /// The string which represent converted value of sequence or special message if 
        /// <paramref name="source" /> is <see langword="null" /> or contains no elements.
        /// </returns>
        public static string ToSingleString<TSource>(this IEnumerable<TSource>? source,
            string? emptyCollectionMessage)
        {
            return source.ToSingleString(
                emptyCollectionMessage: emptyCollectionMessage,
                separator: Strings.DefaultItemSeparator,
                selector: item => ToStingFunction<TSource>.WithSingleQuotes(item)
            );
        }

        /// <summary>
        /// Transforms sequence to the string or returns message when sequence contains no elements.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements of <paramref name="source" />
        /// .</typeparam>
        /// <param name="source">A sequence of values to convert to string.</param>
        /// <param name="emptyCollectionMessage">
        /// The string to return if <paramref name="source" /> is <see langword="null" /> or
        /// contains no elements.
        /// </param>
        /// <param name="separator">
        /// The string to use as a separator. <paramref name="separator" /> is included in the
        /// returned string only if values has more than one element.
        /// </param>
        /// <returns>
        /// The string which represent converted value of sequence or special message if
        /// <paramref name="source" /> is <see langword="null" /> or contains no elements.
        /// </returns>
        public static string ToSingleString<TSource>(this IEnumerable<TSource>? source,
            string? emptyCollectionMessage, string? separator)
        {
            return source.ToSingleString(
                emptyCollectionMessage: emptyCollectionMessage,
                separator: separator,
                selector: item => ToStingFunction<TSource>.WithSingleQuotes(item)
            );
        }

        /// <summary>
        /// Invokes a transform function on each element of a sequence and transforms sequence to
        /// the string.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements of <paramref name="source" />.
        /// </typeparam>
        /// <param name="source">A sequence of values to convert to string.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>
        /// The string which represent converted value of sequence or special message if 
        /// <paramref name="source" /> is <see langword="null" /> or contains no elements.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="selector" /> is <see langword="null" />.
        /// </exception>
        public static string ToSingleString<TSource>(this IEnumerable<TSource>? source,
            Func<TSource, string> selector)
        {
            return source.ToSingleString(
                emptyCollectionMessage: Strings.DefaultEmptyCollectionMessage,
                separator: Strings.DefaultItemSeparator,
                selector: selector
            );
        }

        /// <summary>
        /// Invokes a transform function on each element of a sequence and transforms sequence to
        /// the string or returns message when sequence contains no elements.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements of <paramref name="source" />.
        /// </typeparam>
        /// <param name="source">A sequence of values to convert to string.</param>
        /// <param name="emptyCollectionMessage">
        /// The string to return if <paramref name="source" /> is <see langword="null" /> or
        /// contains no elements.
        /// </param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>
        /// The string which represent converted value of sequence or special message if 
        /// <paramref name="source" /> is <see langword="null" /> or contains no elements.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="selector" /> is <see langword="null" />.
        /// </exception>
        public static string ToSingleString<TSource>(this IEnumerable<TSource>? source,
            string? emptyCollectionMessage, Func<TSource, string> selector)
        {
            return source.ToSingleString(
                emptyCollectionMessage: emptyCollectionMessage,
                separator: Strings.DefaultItemSeparator,
                selector: selector
            );
        }

        /// <summary>
        /// Invokes a transform function on each element of a sequence and transforms sequence to
        /// the string with provided separator or returns message when sequence contains no
        /// elements.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements of <paramref name="source" />.
        /// </typeparam>
        /// <param name="source">A sequence of values to convert to string.</param>
        /// <param name="emptyCollectionMessage">
        /// The string to return if <paramref name="source" /> is <see langword="null" /> or
        /// contains no elements.
        /// </param>
        /// <param name="separator">
        /// The string to use as a separator. <paramref name="separator" /> is included in the
        /// returned string only if values has more than one element.
        /// </param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>
        /// The string which represent converted value of sequence or specified message if 
        /// <paramref name="source" /> is <see langword="null" /> or contains no elements.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="selector" /> is <see langword="null" />.
        /// </exception>
        public static string ToSingleString<TSource>(this IEnumerable<TSource>? source,
            string? emptyCollectionMessage, string? separator, Func<TSource, string> selector)
        {
            selector.ThrowIfNull(nameof(selector));

            if (source is null)
            {
                return emptyCollectionMessage ?? Strings.DefaultEmptyCollectionMessage;
            }

            IReadOnlyList<TSource> enumerated = source.ToArray();
            if (enumerated.Count == 0)
            {
                return emptyCollectionMessage ?? Strings.DefaultEmptyCollectionMessage;
            }

            IEnumerable<string> transformedSource = enumerated.Select(selector);

            return string.Join(separator, transformedSource);
        }
    }
}
