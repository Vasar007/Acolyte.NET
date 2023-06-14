using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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
        [return: NotNull]
        public static IEnumerable<T> ThrowIfNullOrEmpty<T>(
            [NotNull] this IEnumerable<T>? collection, string paramName)
        {
            collection.ThrowIfNull(paramName);

            if (!collection.Any())
            {
                throw new ArgumentException($"{paramName} contains no elements.", paramName);
            }

            return collection;
        }

        /// <inheritdoc cref="ThrowIfNullOrEmpty{T}(IEnumerable{T}?, string)" />
        [return: NotNull]
        public static IReadOnlyCollection<T> ThrowIfNullOrEmpty<T>(
            [NotNull] this IReadOnlyCollection<T>? collection, string paramName)
        {
            (collection?.AsEnumerable()).ThrowIfNull(paramName);

            if (collection.Count == 0)
            {
                throw new ArgumentException($"{paramName} contains no elements.", paramName);
            }

            return collection;
        }

        /// <inheritdoc cref="ThrowIfNullOrEmpty{T}(IEnumerable{T}?, string)" />
        [return: NotNull]
        public static IReadOnlyList<T> ThrowIfNullOrEmpty<T>(
           [NotNull] this IReadOnlyList<T>? collection, string paramName)
        {
            ((IReadOnlyCollection<T>?) collection).ThrowIfNullOrEmpty(paramName);

            return collection;
        }

        /// <inheritdoc cref="ThrowIfNullOrEmpty{T}(IEnumerable{T}?, string)" />
        [return: NotNull]
        public static IReadOnlyDictionary<TKey, TValue> ThrowIfNullOrEmpty<TKey, TValue>(
           [NotNull] this IReadOnlyDictionary<TKey, TValue>? collection, string paramName)
        {
            ((IReadOnlyCollection<KeyValuePair<TKey, TValue>>?) collection).ThrowIfNullOrEmpty(paramName);

            return collection;
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
        /// <param name="assertOnPureValueTypes">
        /// Allows to throw exception if <paramref name="collection" /> item's type is pure value
        /// type ("pure" means that it is non-nullable type). It can be useful when you do not want
        /// to check "pure" value types. So, using this parameter you can catch assertion during
        /// debug and eliminate redundant checks (because non-nullable value types do not have null
        /// value at all).
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
        /// <paramref name="collection" /> contains at least one <see langword="null" /> item. -or-
        /// <paramref name="assertOnPureValueTypes"/> is <see langword="true" /> and
        /// type of <paramref name="collection" /> item's type is pure value type.
        /// </exception>
        /// <inheritdoc cref="ThrowIfNullValue{T}(T, string, bool)" path="//remarks" />
        [return: NotNull]
        public static IEnumerable<T> ThrowIfContainsNullValue<T>(
            [NotNull] this IEnumerable<T?>? collection, string paramName, bool assertOnPureValueTypes)
        {
            paramName.ThrowIfNull(nameof(paramName));
            collection.ThrowIfNull(paramName);
            AssertOnPureValueTypes<T>(paramName, assertOnPureValueTypes);

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

        /// <exception cref="ArgumentNullException">
        /// <paramref name="collection" /> is <see langword="null" />. -or-
        /// <paramref name="paramName" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="collection" /> contains at least one <see langword="null" /> item.
        /// </exception>
        /// <remarks>
        /// This method calls <see cref="ThrowIfContainsNullValue{T}(IEnumerable{T}, string, bool)" />
        /// with parameter assertOnPureValueTypes = <see langword="false" />.
        /// </remarks>
        /// <inheritdoc cref="ThrowIfContainsNullValue{T}(IEnumerable{T}, string, bool)" path="//summary|//typeparam|//param|//returns" />
        [return: NotNull]
        public static IEnumerable<T> ThrowIfContainsNullValue<T>(
            [NotNull] this IEnumerable<T?>? collection, string paramName)
        {
            return collection.ThrowIfContainsNullValue(paramName, assertOnPureValueTypes: false);
        }

        /// <inheritdoc cref="ThrowIfContainsNullValue{T}(IEnumerable{T}?, string)" />
        [return: NotNull]
        public static IReadOnlyCollection<T> ThrowIfContainsNullValue<T>(
            [NotNull] this IReadOnlyCollection<T?>? collection, string paramName)
        {
            (collection?.AsEnumerable()).ThrowIfContainsNullValue(paramName);

            return collection!;
        }

        /// <inheritdoc cref="ThrowIfContainsNullValue{T}(IEnumerable{T}?, string)" />
        public static IReadOnlyList<T> ThrowIfContainsNullValue<T>(
           [NotNull] this IReadOnlyList<T?>? collection, string paramName)
        {
            ((IReadOnlyCollection<T?>?) collection).ThrowIfContainsNullValue(paramName);

            return collection!;
        }

        /// <inheritdoc cref="ThrowIfContainsNullValue{T}(IEnumerable{T}?, string)" />
        [return: NotNull]
        public static IReadOnlyDictionary<TKey, TValue> ThrowIfContainsNullValue<TKey, TValue>(
            [NotNull] this IReadOnlyDictionary<TKey, TValue?>? collection, string paramName)
            where TKey : notnull
        {
            var values = collection.ThrowIfNull(nameof(collection)).Values;
            values.ThrowIfContainsNullValue(paramName);

            return collection!;
        }

        /// <inheritdoc cref="ThrowIfContainsNullValue{T}(IEnumerable{T}?, string)" path="//*[name() != 'remarks']" />
        [return: NotNull]
        public static IEnumerable<T> ThrowIfContainsNull<T>(
            [NotNull] this IEnumerable<T?>? collection, string paramName)
            where T : class?
        {
            return collection.ThrowIfContainsNullValue(paramName);
        }

        /// <inheritdoc cref="ThrowIfContainsNull{T}(IEnumerable{T}?, string)" />
        [return: NotNull]
        public static IReadOnlyCollection<T> ThrowIfContainsNull<T>(
            [NotNull] this IReadOnlyCollection<T?>? collection, string paramName)
            where T : class?
        {
            (collection?.AsEnumerable()).ThrowIfContainsNullValue(paramName);

            return collection!;
        }

        /// <inheritdoc cref="ThrowIfContainsNull{T}(IEnumerable{T}?, string)" />
        [return: NotNull]
        public static IReadOnlyList<T> ThrowIfContainsNull<T>(
            [NotNull] this IReadOnlyList<T?>? collection, string paramName)
            where T : class?
        {
            ((IReadOnlyCollection<T?>?) collection).ThrowIfContainsNull(paramName);

            return collection!;
        }

        /// <inheritdoc cref="ThrowIfContainsNullValue{T}(IEnumerable{T}?, string)" />
        [return: NotNull]
        public static IReadOnlyDictionary<TKey, TValue> ThrowIfContainsNull<TKey, TValue>(
            [NotNull] this IReadOnlyDictionary<TKey, TValue?>? collection, string paramName)
            where TKey : notnull
            where TValue : class?
        {
            var values = collection.ThrowIfNull(nameof(collection)).Values;
            values.ThrowIfContainsNull(paramName);

            return collection!;
        }
    }
}
