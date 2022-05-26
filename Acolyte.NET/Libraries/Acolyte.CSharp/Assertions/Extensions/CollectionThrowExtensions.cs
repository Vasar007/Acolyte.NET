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
            collection.ThrowIfNull(paramName);

            if (!collection.Any())
            {
                throw new ArgumentException($"{paramName} contains no elements.", paramName);
            }

            return collection!;
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

            return collection;
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

        /// <summary>
        /// Checks if collection has any <see langword="null" /> items.
        /// </summary>
        /// <typeparam name="T">Internal type of <paramref name="collection" />.</typeparam>
        /// <param name="collection">Collection to check.</param>
        /// <param name="paramName">
        /// Name of the parameter for error message. Use operator <see langword="nameof" /> to get
        /// proper parameter name.
        /// </param>
        /// <returns>
        /// Returns <paramref name="collection" /> without any changes if collection does not
        /// contain any <see langword="null" /> items; otherwise throws exception.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="collection" /> is <see langword="null" />. -or-
        /// <paramref name="paramName" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="collection" /> contains at least one <see langword="null" /> item.
        /// </exception>
        public static IEnumerable<T> ThrowIfContainsNull<T>(this IEnumerable<T>? collection,
            string paramName)
        {
            paramName.ThrowIfNull(nameof(paramName));
            collection.ThrowIfNull(paramName);

            // We prefer "Where" over "Contains" to save index too.
            var index = -1;
            var hasNullItem = collection
                .Where((item, i) =>
                {
                    if (item is not null) return false;

                    index = i;
                    return true;
                })
                .Any();
            if (hasNullItem)
            {
                string itemName = $"{paramName}[{index.ToString()}]";
                string message = $"{paramName} contains null item: {itemName}.";
                throw new ArgumentException(message, paramName);
            }

            return collection!;
        }

        /// <inheritdoc cref="ThrowIfContainsNull{T}(IEnumerable{T}?, string)" />
        public static IReadOnlyCollection<T> ThrowIfContainsNull<T>(
            this IReadOnlyCollection<T>? collection, string paramName)
        {
            var cast = collection?.AsEnumerable();
            cast.ThrowIfContainsNull(paramName);

            return collection!;
        }

        /// <inheritdoc cref="ThrowIfContainsNull{T}(IEnumerable{T}?, string)" />
        public static IReadOnlyList<T> ThrowIfContainsNull<T>(
           this IReadOnlyList<T>? collection, string paramName)
        {
            var cast = (IReadOnlyCollection<T>?) collection;
            cast.ThrowIfContainsNull(paramName);

            return collection!;
        }

        /// <inheritdoc cref="ThrowIfContainsNull{T}(IEnumerable{T}?, string)" />
        public static IReadOnlyDictionary<TKey, TValue> ThrowIfContainsNull<TKey, TValue>(
           this IReadOnlyDictionary<TKey, TValue>? collection, string paramName)
        {
            var cast = (IReadOnlyCollection<KeyValuePair<TKey, TValue>>?) collection;
            cast.ThrowIfContainsNull(paramName);

            return collection!;
        }
    }
}
