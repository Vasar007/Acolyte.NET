using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Acolyte.Assertions
{
    public static partial class ThrowsExtensions
    {
        /// <summary>
        /// Checks if collection is <see langword="null" /> or empty.
        /// </summary>
        /// <typeparam name="TCollection">A type of <paramref name="collection" />.</typeparam>
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
        public static TCollection ThrowIfNullOrEmpty<TCollection>(
            [NotNull] this TCollection? collection, string paramName, bool assertOnPureValueTypes)
            where TCollection : IEnumerable
        {
            collection.ThrowIfNullValue(paramName, assertOnPureValueTypes);
            paramName.ThrowIfNull(nameof(paramName));

            // Try to get "Count" property.
            // This trick will work for almost all collections except the ones inherited only
            // from "IReadOnlyCollection<T>".
            if (collection is ICollection castedCollection)
            {
                if (castedCollection.Count == 0)
                {
                    throw CreateNoElementsException(paramName);
                }
            }

            if (!collection.AnyNonGeneric())
            {
                throw CreateNoElementsException(paramName);
            }

            return collection;

            static ArgumentException CreateNoElementsException(string paramName)
            {
                return new ArgumentException($"{paramName} contains no elements.", paramName);
            }
        }

        /// <inheritdoc cref="ThrowIfNullOrEmpty{T}(T, string)" />
        [return: NotNull]
        public static TCollection ThrowIfNullOrEmpty<TCollection>(
            [NotNull] this TCollection? collection, string paramName)
            where TCollection : IEnumerable
        {
            return collection.ThrowIfNullOrEmpty(paramName, DefaultAssertOnPureValueTypes);
        }

        /// <summary>
        /// Checks if collection has any <see langword="null" /> items.
        /// </summary>
        /// <typeparam name="TCollection">A type of <paramref name="collection" />.</typeparam>
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
        public static TCollection ThrowIfContainsNullValue<TCollection>(
            [NotNull] this TCollection? collection, string paramName, bool assertOnPureValueTypes)
            where TCollection : IEnumerable
        {
            collection.ThrowIfNullValue(paramName, assertOnPureValueTypes);
            paramName.ThrowIfNull(nameof(paramName));

            // We prefer "Where" over "Contains" to save index too.
            var index = -1;
            var hasNullItem = collection
                .WhereNonGeneric((item, i) =>
                {
                    if (item is not null) return false;

                    index = i;
                    return true;
                })
                .AnyNonGeneric();
            if (hasNullItem)
            {
                string itemName = $"{paramName}[{index.ToString()}]";
                string message = $"{paramName} contains null item: {itemName}.";
                throw new ArgumentException(message, paramName);
            }

            return collection;
        }

        /// <exception cref="ArgumentNullException">
        /// <paramref name="collection" /> is <see langword="null" />. -or-
        /// <paramref name="paramName" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="collection" /> contains at least one <see langword="null" /> item.
        /// </exception>
        /// <remarks>
        /// This method calls <see cref="ThrowIfContainsNullValue{T}(T, string, bool)" />
        /// with parameter assertOnPureValueTypes = <see langword="false" />.
        /// </remarks>
        /// <inheritdoc cref="ThrowIfContainsNullValue{T}(T, string, bool)" path="//summary|//typeparam|//param|//returns" />
        [return: NotNull]
        public static TCollection ThrowIfContainsNullValue<TCollection>(
            [NotNull] this TCollection? collection, string paramName)
            where TCollection : IEnumerable
        {
            return collection.ThrowIfContainsNullValue(paramName, DefaultAssertOnPureValueTypes);
        }

        /// <inheritdoc cref="ThrowIfContainsNullValue{T}(T, string)" />
        [return: NotNull]
        public static IReadOnlyDictionary<TKey, TValue> ThrowIfContainsNullValue<TKey, TValue>(
            [NotNull] this IReadOnlyDictionary<TKey, TValue?>? collection, string paramName,
            bool assertOnPureValueTypes)
            where TKey : notnull
        {
            var values = collection.ThrowIfNull(nameof(collection)).Values;
            values.ThrowIfContainsNullValue(paramName, assertOnPureValueTypes);

            return collection!;
        }

        /// <inheritdoc cref="ThrowIfContainsNullValue{T}(T, string)" />
        [return: NotNull]
        public static IReadOnlyDictionary<TKey, TValue> ThrowIfContainsNullValue<TKey, TValue>(
            [NotNull] this IReadOnlyDictionary<TKey, TValue?>? collection, string paramName)
            where TKey : notnull
        {
            return collection.ThrowIfContainsNullValue(paramName, DefaultAssertOnPureValueTypes);
        }

        /// <inheritdoc cref="ThrowIfContainsNullValue{T}(T, string)" path="//*[name() != 'remarks']" />
        [return: NotNull]
        public static TCollection ThrowIfContainsNull<TCollection>(
            [NotNull] this TCollection? collection, string paramName)
            where TCollection : class, IEnumerable
        {
            return collection.ThrowIfContainsNullValue(paramName);
        }

        /// <inheritdoc cref="ThrowIfContainsNullValue{T}(T, string)" />
        [return: NotNull]
        public static IReadOnlyDictionary<TKey, TValue> ThrowIfContainsNull<TKey, TValue>(
            [NotNull] this IReadOnlyDictionary<TKey, TValue?>? collection, string paramName)
            where TKey : notnull
            where TValue : class?
        {
            return collection.ThrowIfContainsNullValue(paramName, DefaultAssertOnPureValueTypes);
        }

        private static bool AnyNonGeneric(this IEnumerable source)
        {
            var enumerator = source.GetEnumerator();
            return enumerator.MoveNext();
        }

        private static IEnumerable WhereNonGeneric(this IEnumerable source,
            Func<object?, int, bool> predicate)
        {
            int index = -1;
            foreach (var element in source)
            {
                checked
                {
                    ++index;
                }

                if (predicate(element, index))
                {
                    yield return element;
                }
            }
        }
    }
}
