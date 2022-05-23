using System;
using System.Collections.Generic;
using System.Linq;

namespace Acolyte.Assertions
{
    public static partial class ThrowsExtensions
    {
        /// <summary>
        /// Checks if collection is <see langword="null" /> or empty.
        /// </summary>
        /// <typeparam name="T">Internal type of <paramref name="collection" />.</typeparam>
        /// <param name="collection">Collection to check.</param>
        /// <param name="paramName">
        /// Name of the parameter for error message. Use operator <see langword="nameof" /> to get
        /// proper parameter name.
        /// </param>
        /// <returns>
        /// Returns <paramref name="collection" /> without any changes if collection is not
        /// <see langword="null" /> or empty; otherwise throws exception.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="collection" /> is <see langword="null" />. -or-
        /// <paramref name="paramName" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="collection" /> contains no elements.
        /// </exception>
        public static IEnumerable<T> ThrowIfNullOrEmpty<T>(this IEnumerable<T>? collection,
            string paramName)
        {
            paramName.ThrowIfNull(nameof(paramName));

            if (collection is null)
            {
                throw new ArgumentNullException(paramName);
            }
            if (!collection.Any())
            {
                throw new ArgumentException($"{paramName} contains no elements.", paramName);
            }

            return collection;
        }


        /// <inheritdoc cref="ThrowIfNullOrEmpty{T}(IEnumerable{T}?, string)" />
        public static IReadOnlyCollection<T> ThrowIfNullOrEmpty<T>(
            this IReadOnlyCollection<T>? collection, string paramName)
        {
            var cast = collection?.AsEnumerable();
            cast.ThrowIfNull(paramName);

            if (collection!.Count == 0)
            {
                throw new ArgumentException($"{paramName} contains no elements.", paramName);
            }

            return collection!;
        }

        /// <inheritdoc cref="ThrowIfNullOrEmpty{T}(IEnumerable{T}?, string)" />
        public static IReadOnlyList<T> ThrowIfNullOrEmpty<T>(
           this IReadOnlyList<T>? collection, string paramName)
        {
            var cast = (IReadOnlyCollection<T>?) collection;
            cast.ThrowIfNullOrEmpty(paramName);

            return collection!;
        }

        /// <inheritdoc cref="ThrowIfNullOrEmpty{T}(IEnumerable{T}?, string)" />
        public static IReadOnlyDictionary<TKey, TValue> ThrowIfNullOrEmpty<TKey, TValue>(
           this IReadOnlyDictionary<TKey, TValue>? collection, string paramName)
        {
            var cast = (IReadOnlyCollection<KeyValuePair<TKey, TValue>>?) collection;
            cast.ThrowIfNullOrEmpty(paramName);

            return collection!;
        }
    }
}
