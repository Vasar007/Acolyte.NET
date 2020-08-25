using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Acolyte.Assertions;
using Acolyte.Common;

namespace Acolyte.Collections
{
    /// <summary>
    /// Contains useful methods to work with enumerable items.
    /// </summary>
    public static class EnumerableExtensions
    {
        #region Is Null Or Empty

        /// <summary>
        /// Checks if enumerable is <c>null</c> or empty without throwing exception.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements of <paramref name="source" />.
        /// </typeparam>
        /// <param name="source">The <see cref="IEnumerable{TSource}" /> to check.</param>
        /// <returns>
        /// Returns <c>true</c> in case the enumerable is <c>null</c> or empty, <c>false</c> 
        /// otherwise.
        /// </returns>
        public static bool IsNullOrEmpty<TSource>(this IEnumerable<TSource>? source)
        {
            return source is null || !source.Any();
        }

        #endregion

        #region First Or Default

        /// <summary>
        /// Returns the first element of a sequence, or a specified default value if the sequence
        /// contains no elements.
        /// </summary>
        ///<typeparam name="TSource">
        /// The type of the elements of <paramref name="source" />.
        /// </typeparam>
        /// <param name="source">
        /// The <see cref="IEnumerable{TSource}" /> to return the first element of or default.
        /// </param>
        /// <param name="defaultValue">
        /// The specified value to return if the sequence contains no elements.
        /// </param>
        /// <returns>
        /// <paramref name="defaultValue" /> if source is empty; otherwise, the first element in
        /// source.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source" /> is <c>null</c>.
        /// </exception>
        public static TSource FirstOrDefault<TSource>(this IEnumerable<TSource> source,
            TSource defaultValue)
        {
            source.ThrowIfNull(nameof(source));

            if (source is IList<TSource> list)
            {
                if (list.Count > 0) return list[0];
            }
            else
            {
                using IEnumerator<TSource> enumerator = source.GetEnumerator();

                if (enumerator.MoveNext()) return enumerator.Current;
            }

            return defaultValue;
        }

        /// <summary>
        /// Returns the first element of a sequence that satisfies the condition, or a specified 
        /// default value if the sequence contains no elements or no elements satisfy the condition.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements of <paramref name="source" />.
        /// </typeparam>
        /// <param name="source">
        /// The <see cref="IEnumerable{T}" /> to return the first element of or default.
        /// </param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <param name="defaultValue">
        /// The specified value to return if the sequence contains no elements that satisfies the 
        /// condition.
        /// </param>
        /// <returns>
        /// <paramref name="defaultValue" /> if <paramref name="source" /> is empty or if no element
        /// passes the test specified by predicate; otherwise, the first element in
        /// <paramref name="source" /> that passes the test specified by
        /// <paramref name="predicate" />.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source" /> is <c>null</c>. -or-
        /// <paramref name="predicate" /> is <c>null</c>.
        /// </exception>
        public static TSource FirstOrDefault<TSource>(this IEnumerable<TSource> source,
            Func<TSource, bool> predicate, TSource defaultValue)
        {
            source.ThrowIfNull(nameof(source));
            predicate.ThrowIfNull(nameof(predicate));

            foreach (TSource item in source)
            {
                if (predicate(item)) return item;
            }

            return defaultValue;
        }

        #endregion

        #region Last Or Default

        /// <summary>
        /// Returns the last element of a sequence, or a specified default value if the sequence
        /// contains no elements.
        /// </summary>
        ///<typeparam name="TSource">
        /// The type of the elements of <paramref name="source" />.
        /// </typeparam>
        /// <param name="source">
        /// The <see cref="IEnumerable{TSource}" /> to return the last element of or default.
        /// </param>
        /// <param name="defaultValue">
        /// The specified value to return if the sequence contains no elements.
        /// </param>
        /// <returns>
        /// <paramref name="defaultValue" /> if source is empty; otherwise, the last element in
        /// source.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source" /> is <c>null</c>.
        /// </exception>
        public static TSource LastOrDefault<TSource>(this IEnumerable<TSource> source,
            TSource defaultValue)
        {
            source.ThrowIfNull(nameof(source));

            if (source is IList<TSource> list)
            {
                int count = list.Count;
                if (count > 0) return list[count - 1];
            }
            else
            {
                using IEnumerator<TSource> enumerator = source.GetEnumerator();

                if (enumerator.MoveNext())
                {
                    TSource result;
                    do
                    {
                        result = enumerator.Current;
                    } while (enumerator.MoveNext());

                    return result;
                }
            }

            return defaultValue;
        }

        /// <summary>
        /// Returns the last element of a sequence that satisfies the condition, or a specified 
        /// default value if the sequence contains no elements or no elements satisfy the condition.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements of <paramref name="source" />.
        /// </typeparam>
        /// <param name="source">
        /// The <see cref="IEnumerable{T}" /> to return the last element of or default.
        /// </param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <param name="defaultValue">
        /// The specified value to return if the sequence contains no elements that satisfies the 
        /// condition.
        /// </param>
        /// <returns>
        /// <paramref name="defaultValue" /> if <paramref name="source" /> is empty or if no element
        /// passes the test specified by predicate; otherwise, the last element in
        /// <paramref name="source" /> that passes the test specified by
        /// <paramref name="predicate" />.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source" /> is <c>null</c>. -or-
        /// <paramref name="predicate" /> is <c>null</c>.
        /// </exception>
        public static TSource LastOrDefault<TSource>(this IEnumerable<TSource> source,
            Func<TSource, bool> predicate, TSource defaultValue)
        {
            source.ThrowIfNull(nameof(source));
            predicate.ThrowIfNull(nameof(predicate));

            TSource result = defaultValue;
            foreach (TSource element in source)
            {
                if (predicate(element))
                {
                    result = element;
                }
            }

            return result;
        }

        #endregion

        #region Single Or Default

        /// <summary>
        /// Returns the first element of a sequence, or a specified default value if the sequence
        /// contains no elements; this method throws an exception if sequence contains more than
        /// one element.
        /// </summary>
        ///<typeparam name="TSource">
        /// The type of the elements of <paramref name="source" />.
        /// </typeparam>
        /// <param name="source">
        /// The <see cref="IEnumerable{TSource}" /> to return the single element from or default.
        /// </param>
        /// <param name="defaultValue">
        /// The specified value to return if the sequence contains no elements.
        /// </param>
        /// <returns>
        /// The single element of the input sequence, or <paramref name="defaultValue" /> if the
        /// sequence contains no elements.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source" /> is <c>null</c>.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// The input sequence contains more than one element.
        /// </exception>
        public static TSource SingleOrDefault<TSource>(this IEnumerable<TSource> source,
            TSource defaultValue)
        {
            source.ThrowIfNull(nameof(source));

            if (source is IList<TSource> list)
            {
                switch (list.Count)
                {
                    case 0: return defaultValue;
                    case 1: return list[0];
                }
            }
            else
            {
                using IEnumerator<TSource> enumerator = source.GetEnumerator();

                if (!enumerator.MoveNext()) return defaultValue;
                TSource result = enumerator.Current;
                if (!enumerator.MoveNext()) return result;
            }

            throw Error.MoreThanOneElement();
        }

        /// <summary>
        /// Returns the only element of a sequence that satisfies a specified condition or
        /// a default value if no such element exists; this method throws an exception if
        /// more than one element satisfies the condition.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements of <paramref name="source" />.
        /// </typeparam>
        /// <param name="source">
        /// The <see cref="IEnumerable{T}" /> to return the single element from or default.
        /// </param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <param name="defaultValue">
        /// The specified value to return if the sequence contains no elements that satisfies the 
        /// condition.
        /// </param>
        /// <returns>
        /// The single element of the input sequence that satisfies the condition, or
        /// <paramref name="defaultValue" /> if no such element is found.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source" /> is <c>null</c>. -or-
        /// <paramref name="predicate" /> is <c>null</c>.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// More than one element satisfies the condition in predicate.
        /// </exception>
        public static TSource SingleOrDefault<TSource>(this IEnumerable<TSource> source,
            Func<TSource, bool> predicate, TSource defaultValue)
        {
            source.ThrowIfNull(nameof(source));
            predicate.ThrowIfNull(nameof(predicate));

            TSource result = defaultValue;
            int count = 0;
            foreach (TSource element in source)
            {
                if (predicate(element))
                {
                    result = element;
                    ++count;

                    // If there are more than one element satisfies the condition in predicate,
                    // skip the last part of sequence.
                    if (count > 1) break;
                }
            }

            switch (count)
            {
                case 0:
                case 1:
                    return result;

                default:
                    throw Error.MoreThanOneMatch();
            };
        }

        #endregion

        #region Index Of

        /// <summary>
        /// Searches for an element that satisfies the condition and returns the zero-based index of
        /// the first occurrence within the entire sequence.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements of <paramref name="source" />.
        /// </typeparam>
        /// <param name="source">
        /// The <see cref="IEnumerable{TSource}" /> to find element index.
        /// </param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>
        /// The zero-based index of the first occurrence of an element that satisfies 
        /// <paramref name="predicate" />, if found; otherwise it will return
        /// <see cref="Constants.NotFoundIndex" /> (it's equal to -1).
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source" /> is <c>null</c>. -or-
        /// <paramref name="predicate" /> is <c>null</c>.
        /// </exception>
        public static int IndexOf<TSource>(this IEnumerable<TSource> source,
            Func<TSource, bool> predicate)
        {
            // Null check for "source" parameter is provided by Enumerable.Select method.
            predicate.ThrowIfNull(nameof(predicate));

            int foundIndex = source
                .Select((value, index) => (value, index))
                .Where(x => predicate(x.value))
                .Select(x => x.index)
                .FirstOrDefault(Constants.NotFoundIndex);

            return foundIndex;
        }

        /// <summary>
        /// Searches for an element that equals to the specifed element and returns the zero-based
        /// index of the first occurrence within the entire sequence. The values are compared by
        /// using a specified <paramref name="comparer" />.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements of <paramref name="source" />.
        /// </typeparam>
        /// <param name="source">
        /// The <see cref="IEnumerable{TSource}" /> to find element index.
        /// </param>
        /// <param name="comparer">
        /// An <see cref="IEqualityComparer{TSource}" /> to compare values with.
        /// </param>
        /// <returns>
        /// The zero-based index of the first occurrence of an element that equals to the 
        /// <paramref name="value" />, if found; otherwise it will return
        /// <see cref="Constants.NotFoundIndex" /> (it's equal to -1).
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source" /> is <c>null</c>. -or-
        /// <paramref name="comparer" /> is <c>null</c>.
        /// </exception>
        public static int IndexOf<TSource>(this IEnumerable<TSource> source, TSource value,
            IEqualityComparer<TSource> comparer)
        {
            comparer.ThrowIfNull(nameof(comparer));

            return source.IndexOf(item => comparer.Equals(item, value));
        }

        /// <summary>
        /// Searches for an element that equals to the specifed element and returns the zero-based
        /// index of the first occurrence within the entire sequence. The values are compared by
        /// using a default equality comparer.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements of <paramref name="source" />.
        /// </typeparam>
        /// <param name="source">
        /// The <see cref="IEnumerable{TSource}" /> to find element index.
        /// </param>
        /// <returns>
        /// The zero-based index of the first occurrence of an element that equals to the 
        /// <paramref name="value" />, if found; otherwise it will return
        /// <see cref="Constants.NotFoundIndex" /> (it's equal to -1).
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source" /> is <c>null</c>.
        /// </exception>
        public static int IndexOf<TSource>(this IEnumerable<TSource> source, TSource value)
        {
            return source.IndexOf(value, EqualityComparer<TSource>.Default);
        }

        #endregion

        #region To Read-Only Collections

        /// <summary>
        /// Creates a <see cref="IReadOnlyDictionary{TKey, TValue}" /> from an
        /// <see cref="IEnumerable{T}" /> according to a specified key selector function.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements of <paramref name="source" />.
        /// </typeparam>
        /// <typeparam name="TKey">
        /// The type of the key returned by <paramref name="keySelector" />.
        /// </typeparam>
        /// <param name="source">
        /// An <see cref="IEnumerable{T}" /> to create
        /// <see cref="IReadOnlyDictionary{TKey, TValue}" /> from.
        /// </param>
        /// <param name="keySelector">A function to extract a key from each element.</param>
        /// <returns>
        /// A <see cref="IReadOnlyDictionary{TKey, TValue}" /> that contains keys and values.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source" /> is <c>null</c>. -or-
        /// <paramref name="keySelector" /> is <c>null</c>. -or-
        /// <paramref name="keySelector" /> produces a key that is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="keySelector" /> produces duplicate keys for two elements.
        /// </exception>
        public static IReadOnlyDictionary<TKey, TSource>
            ToReadOnlyDictionary<TKey, TSource>(
                this IEnumerable<TSource> source,
                Func<TSource, TKey> keySelector)
            where TKey: notnull
        {
            // Null checks for parameters are provided by Enumerable.ToDictionary method.
            return new ReadOnlyDictionary<TKey, TSource>(source.ToDictionary(keySelector));
        }

        /// <summary>
        /// Creates a <see cref="IReadOnlyDictionary{TKey, TValue}" /> from an
        /// <see cref="IEnumerable{T}" /> according to a specified key selector function and key
        /// comparer.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements of <paramref name="source" />.
        /// </typeparam>
        /// <typeparam name="TKey">
        /// The type of the key returned by <paramref name="keySelector" />.
        /// </typeparam>
        /// <param name="source">
        /// An <see cref="IEnumerable{T}" /> to create
        /// <see cref="IReadOnlyDictionary{TKey, TValue}" /> from.
        /// </param>
        /// <param name="keySelector">A function to extract a key from each element.</param>
        /// <param name="comparer">An <see cref="IEqualityComparer{T}" /> to compare keys.</param>
        /// <returns>
        /// A <see cref="IReadOnlyDictionary{TKey, TValue}" /> that contains keys and values.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source" /> is <c>null</c>. -or-
        /// <paramref name="keySelector" /> is <c>null</c>. -or-
        /// <paramref name="keySelector" /> produces a key that is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="keySelector" /> produces duplicate keys for two elements.
        /// </exception>
        public static IReadOnlyDictionary<TKey, TSource>
            ToReadOnlyDictionary<TKey, TSource>(
                this IEnumerable<TSource> source,
                Func<TSource, TKey> keySelector,
                IEqualityComparer<TKey>? comparer)
            where TKey : notnull
        {
            // Null checks for parameters are provided by Enumerable.ToDictionary method.
            return new ReadOnlyDictionary<TKey, TSource>(
                source.ToDictionary(keySelector, comparer)
            );
        }

        /// <summary>
        /// Creates a <see cref="IReadOnlyDictionary{TKey, TValue}" /> from an
        /// <see cref="IEnumerable{T}" /> according to specified key selector and element selector
        /// functions.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements of <paramref name="source" />.
        /// </typeparam>
        /// <typeparam name="TKey">
        /// The type of the key returned by <paramref name="keySelector" />.
        /// </typeparam>
        /// <typeparam name="TElement">
        /// The type of the value returned by <paramref name="elementSelector" />.
        /// </typeparam>
        /// <param name="source">
        /// An <see cref="IEnumerable{T}" /> to create
        /// <see cref="IReadOnlyDictionary{TKey, TValue}" /> from.
        /// </param>
        /// <param name="keySelector">A function to extract a key from each element.</param>
        /// <param name="elementSelector">
        /// A transform function to produce a result element value from each element.
        /// </param>
        /// <returns>
        /// A <see cref="IReadOnlyDictionary{TKey, TValue}" /> that contains values of type
        /// <typeparamref name="TElement" /> selected from the input sequence.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source" /> is <c>null</c>. -or-
        /// <paramref name="keySelector" /> is <c>null</c>. -or-
        /// <paramref name="elementSelector" /> is <c>null</c>. -or-
        /// <paramref name="keySelector" /> produces a key that is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="keySelector" /> produces duplicate keys for two elements.
        /// </exception>
        public static IReadOnlyDictionary<TKey, TElement>
            ToReadOnlyDictionary<TSource, TKey, TElement>(
                this IEnumerable<TSource> source,
                Func<TSource, TKey> keySelector,
                Func<TSource, TElement> elementSelector)
            where TKey : notnull
        {
            // Null checks for parameters are provided by Enumerable.ToDictionary method.
            return new ReadOnlyDictionary<TKey, TElement>(
                source.ToDictionary(keySelector, elementSelector)
            );
        }

        /// <summary>
        /// Creates a <see cref="IReadOnlyDictionary{TKey, TValue}" /> from an
        /// <see cref="IEnumerable{T}" /> to a specified key selector function, a comparer, and an
        /// element selector function.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements of <paramref name="source" />.
        /// </typeparam>
        /// <typeparam name="TKey">
        /// The type of the key returned by <paramref name="keySelector" />.
        /// </typeparam>
        /// <typeparam name="TElement">
        /// The type of the value returned by <paramref name="elementSelector" />.
        /// </typeparam>
        /// <param name="source">
        /// An <see cref="IEnumerable{T}" /> to create
        /// <see cref="IReadOnlyDictionary{TKey, TValue}" /> from.
        /// </param>
        /// <param name="keySelector">A function to extract a key from each element.</param>
        /// <param name="elementSelector">
        /// A transform function to produce a result element value from each element.
        /// </param>
        /// <param name="comparer">An <see cref="IEqualityComparer{T}" /> to compare keys.</param>
        /// <returns>
        /// A <see cref="IReadOnlyDictionary{TKey, TValue}" /> that contains values of type
        /// <typeparamref name="TElement" /> selected from the input sequence.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source" /> is <c>null</c>. -or-
        /// <paramref name="keySelector" /> is <c>null</c>. -or-
        /// <paramref name="elementSelector" /> is <c>null</c>. -or-
        /// <paramref name="keySelector" /> produces a key that is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="keySelector" /> produces duplicate keys for two elements.
        /// </exception>
        public static IReadOnlyDictionary<TKey, TElement>
            ToReadOnlyDictionary<TSource, TKey, TElement>(
                this IEnumerable<TSource> source,
                Func<TSource, TKey> keySelector,
                Func<TSource, TElement> elementSelector,
                IEqualityComparer<TKey>? comparer)
            where TKey : notnull
        {
            // Null checks for parameters are provided by Enumerable.ToDictionary method.
            return new ReadOnlyDictionary<TKey, TElement>(
                source.ToDictionary(keySelector, elementSelector, comparer)
            );
        }

        /// <summary>
        /// Creates a <see cref="IReadOnlyList{T}" /> from an <see cref="IEnumerable{T}" />.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <param name="source">
        /// The <see cref="IEnumerable{T}" /> to create a <see cref="IReadOnlyList{T}" /> from.
        /// </param>
        /// <returns>
        /// A <see cref="IReadOnlyList{T}" /> that contains elements from the input sequence.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source" /> is <c>null</c>.
        /// </exception>
        public static IReadOnlyList<TSource> ToReadOnlyList<TSource>(
            this IEnumerable<TSource> source)
        {
            // Null check for "source" parameter is provided by Enumerable.ToList method.
            return source.ToList().AsReadOnly();
        }

        /// <summary>
        /// Creates a <see cref="IReadOnlyCollection{T}" /> from an <see cref="IEnumerable{T}" />.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements of <paramref name="source" />.
        /// </typeparam>
        /// <param name="source">
        /// The <see cref="IEnumerable{T}" /> to create a <see cref="IReadOnlyCollection{T}" />
        /// from.
        /// </param>
        /// <returns>
        /// A <see cref="IReadOnlyCollection{T}" /> that contains elements from the input sequence.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source" /> is <c>null</c>.
        /// </exception>
        public static IReadOnlyCollection<TSource> ToReadOnlyCollection<TSource>(
            this IEnumerable<TSource> source)
        {
            // Null check for "source" parameter is provided by Enumerable.ToList method.
            return source.ToList().AsReadOnly();
        }

        #endregion

        #region Min/Max For Generic Types With Comparer

        /// <summary>
        /// Returns the minimum value in a generic sequence with proivided comparer.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements of <paramref name="source" />.
        /// </typeparam>
        /// <param name="source">
        /// A sequence of values to determine the minimum value of.
        /// </param>
        /// <param name="comparer">An element comparer.</param>
        /// <returns>The minimum value in the sequence.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source" /> is <c>null</c>. -or-
        /// <paramref name="comparer" /> is <c>null</c>.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="source" /> contains no elements.
        /// </exception>
        [return: MaybeNull]
        public static TSource Min<TSource>(
            this IEnumerable<TSource> source,
            IComparer<TSource> comparer)
        {
            source.ThrowIfNull(nameof(source));
            comparer.ThrowIfNull(nameof(comparer));

            TSource minValue = default;
            if (minValue is null)
            {
                foreach (TSource x in source)
                {
                    if (!(x is null) && (minValue is null || comparer.Compare(x, minValue) < 0)) minValue = x;
                }

                return minValue;
            }
            else
            {
                bool hasValue = false;
                foreach (TSource x in source)
                {
                    if (hasValue)
                    {
                        if (comparer.Compare(x, minValue) < 0) minValue = x;
                    }
                    else
                    {
                        minValue = x;
                        hasValue = true;
                    }
                }

                if (hasValue) return minValue;

                throw Error.NoElements();
            }
        }

        /// <summary>
        /// Returns the maximum value in a generic sequence with proivided comparer.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements of <paramref name="source" />.
        /// </typeparam>
        /// <param name="source">
        /// A sequence of values to determine the maximum value of.
        /// </param>
        /// <param name="comparer">An element comparer.</param>
        /// <returns>The maximum value in the sequence.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source" /> is <c>null</c>. -or-
        /// <paramref name="comparer" /> is <c>null</c>.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="source" /> contains no elements.
        /// </exception>
        [return: MaybeNull]
        public static TSource Max<TSource>(
            this IEnumerable<TSource> source,
            IComparer<TSource> comparer)
        {
            return source.Min(new InverseComparer<TSource>(comparer));
        }

        #endregion

        #region MinMax

        #region MinMax Overloads Without Selector

        #region MinMax For Int32

        /// <summary>
        /// Returns the minimum and maximum values in a sequence of <see cref="int" /> values.
        /// </summary>
        /// <param name="source">
        /// A sequence of <see cref="int" /> values to determine the minimum and maximum values
        /// of.
        /// </param>
        /// <returns>The minimum and maximum values in the sequence.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source" /> is <c>null</c>.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="source" /> contains no elements.
        /// </exception>
        public static (int minValue, int maxValue) MinMax(this IEnumerable<int> source)
        {
            source.ThrowIfNull(nameof(source));

            int minValue = 0;
            int maxValue = 0;
            bool hasValue = false;
            foreach (int x in source)
            {
                if (hasValue)
                {
                    if (x < minValue) minValue = x;
                    if (x > maxValue) maxValue = x;
                }
                else
                {
                    minValue = x;
                    maxValue = x;
                    hasValue = true;
                }
            }

            if (hasValue) return (minValue, maxValue);

            throw Error.NoElements();
        }

        /// <summary>
        /// Returns the minimum and maximum values in a sequence of nullable <see cref="int" />
        /// values.
        /// </summary>
        /// <param name="source">
        /// A sequence of nullable <see cref="int" /> values to determine the minimum and maximum
        /// values of.
        /// </param>
        /// <returns>
        /// A value of type <see cref="Nullable{Int32}" /> that corresponds to the minimum and
        /// maximum values in the sequence.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source" /> is <c>null</c>.
        /// </exception>
        public static (int? minValue, int? maxValue) MinMax(this IEnumerable<int?> source)
        {
            source.ThrowIfNull(nameof(source));

            int? minValue = null;
            int? maxValue = null;
            foreach (int? x in source)
            {
                if (minValue is null || x < minValue) minValue = x;
                if (maxValue is null || x > maxValue) maxValue = x;
            }

            return (minValue, maxValue);
        }

        #endregion

        #region MinMax For Int64

        /// <summary>
        /// Returns the minimum and maximum values in a sequence of <see cref="long" /> values.
        /// </summary>
        /// <param name="source">
        /// A sequence of <see cref="long" /> values to determine the minimum and maximum values of.
        /// </param>
        /// <returns>The minimum and maximum values in the sequence.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source" /> is <c>null</c>.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="source" /> contains no elements.
        /// </exception>
        public static (long minValue, long maxValue) MinMax(this IEnumerable<long> source)
        {
            source.ThrowIfNull(nameof(source));

            long minValue = 0;
            long maxValue = 0;
            bool hasValue = false;
            foreach (long x in source)
            {
                if (hasValue)
                {
                    if (x < minValue) minValue = x;
                    if (x > maxValue) maxValue = x;
                }
                else
                {
                    minValue = x;
                    maxValue = x;
                    hasValue = true;
                }
            }

            if (hasValue) return (minValue, maxValue);

            throw Error.NoElements();
        }

        /// <summary>
        /// Returns the minimum and maximum values in a sequence of nullable <see cref="long" />
        /// values.
        /// </summary>
        /// <param name="source">
        /// A sequence of nullable <see cref="long" /> values to determine the minimum and maximum
        /// values of.
        /// </param>
        /// <returns>
        /// A value of type <see cref="Nullable{Int64}" /> that corresponds to the minimum and
        /// maximum values in the sequence.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source" /> is <c>null</c>.
        /// </exception>
        public static (long? minValue, long? maxValue) MinMax(this IEnumerable<long?> source)
        {
            source.ThrowIfNull(nameof(source));

            long? minValue = null;
            long? maxValue = null;
            foreach (long? x in source)
            {
                if (minValue is null || x < minValue) minValue = x;
                if (maxValue is null || x > maxValue) maxValue = x;
            }

            return (minValue, maxValue);
        }

        #endregion

        #region MinMax For Single

        /// <summary>
        /// Returns the minimum and maximum values in a sequence of <see cref="float" /> values.
        /// </summary>
        /// <param name="source">
        /// A sequence of <see cref="float" /> values to determine the minimum and maximum values
        /// of.
        /// </param>
        /// <returns>The minimum and maximum values in the sequence.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source" /> is <c>null</c>.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="source" /> contains no elements.
        /// </exception>
        /// <remarks>
        /// Normally NaN &lt; anything is false, as is anything &lt; NaN
        /// However, this leads to some irksome outcomes in Min and Max.
        /// If we use those semantics then Min(NaN, 5.0) is NaN, but
        /// Min(5.0, NaN) is 5.0! To fix this, we impose a total
        /// ordering where NaN is smaller/bigger than every value, including infinity.
        /// </remarks>
        public static (float minValue, float maxValue) MinMax(this IEnumerable<float> source)
        {
            source.ThrowIfNull(nameof(source));

            float minValue = 0;
            float maxValue = 0;
            bool hasValue = false;
            foreach (float x in source)
            {
                if (hasValue)
                {
                    // Normally NaN < anything is false, as is anything < NaN
                    // However, this leads to some irksome outcomes in Min and Max.
                    // If we use those semantics then Min(NaN, 5.0) is NaN, but
                    // Min(5.0, NaN) is 5.0! To fix this, we impose a total
                    // ordering where NaN is smaller/bigger than every value, including infinity.
                    if (x < minValue || float.IsNaN(x)) minValue = x;
                    if (x > maxValue || float.IsNaN(x)) maxValue = x;
                }
                else
                {
                    minValue = x;
                    maxValue = x;
                    hasValue = true;
                }
            }

            if (hasValue) return (minValue, maxValue);

            throw Error.NoElements();
        }

        /// <summary>
        /// Returns the minimum and maximum values in a sequence of nullable <see cref="float" />
        /// values.
        /// </summary>
        /// <param name="source">
        /// A sequence of nullable <see cref="float" /> values to determine the minimum and maximum
        /// values of.
        /// </param>
        /// <returns>
        /// A value of type <see cref="Nullable{Single}" /> that corresponds to the minimum and
        /// maximum values in the sequence.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source" /> is <c>null</c>.
        /// </exception>
        /// <remarks>
        /// Normally NaN &lt; anything is false, as is anything &lt; NaN
        /// However, this leads to some irksome outcomes in Min and Max.
        /// If we use those semantics then Min(NaN, 5.0) is NaN, but
        /// Min(5.0, NaN) is 5.0! To fix this, we impose a total
        /// ordering where NaN is smaller/bigger than every value, including infinity.
        /// </remarks>
        public static (float? minValue, float? maxValue) MinMax(this IEnumerable<float?> source)
        {
            source.ThrowIfNull(nameof(source));

            float? minValue = null;
            float? maxValue = null;
            foreach (float? x in source)
            {
                if (x is null) continue;
                if (minValue is null || x < minValue || float.IsNaN((float) x)) minValue = x;
                if (maxValue is null || x > maxValue || float.IsNaN((float) x)) maxValue = x;
            }

            return (minValue, maxValue);
        }

        #endregion

        #region MinMax For Double

        /// <summary>
        /// Returns the minimum and maximum values in a sequence of <see cref="double" /> values.
        /// </summary>
        /// <param name="source">
        /// A sequence of <see cref="double" /> values to determine the minimum and maximum values
        /// of.
        /// </param>
        /// <returns>The minimum and maximum values in the sequence.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source" /> is <c>null</c>.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="source" /> contains no elements.
        /// </exception>
        /// <remarks>
        /// Normally NaN &lt; anything is false, as is anything &lt; NaN
        /// However, this leads to some irksome outcomes in Min and Max.
        /// If we use those semantics then Min(NaN, 5.0) is NaN, but
        /// Min(5.0, NaN) is 5.0! To fix this, we impose a total
        /// ordering where NaN is smaller/bigger than every value, including infinity.
        /// </remarks>
        public static (double minValue, double maxValue) MinMax(this IEnumerable<double> source)
        {
            source.ThrowIfNull(nameof(source));

            double minValue = 0;
            double maxValue = 0;
            bool hasValue = false;
            foreach (double x in source)
            {
                if (hasValue)
                {
                    if (x < minValue || double.IsNaN(x)) minValue = x;
                    if (x > maxValue || double.IsNaN(x)) maxValue = x;
                }
                else
                {
                    minValue = x;
                    maxValue = x;
                    hasValue = true;
                }
            }

            if (hasValue) return (minValue, maxValue);

            throw Error.NoElements();
        }

        /// <summary>
        /// Returns the minimum and maximum values in a sequence of nullable <see cref="double" />
        /// values.
        /// </summary>
        /// <param name="source">
        /// A sequence of nullable <see cref="double" /> values to determine the minimum and maximum
        /// values of.
        /// </param>
        /// <returns>
        /// A value of type <see cref="Nullable{Double}" /> that corresponds to the minimum and
        /// maximum values in the sequence.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source" /> is <c>null</c>.
        /// </exception>
        /// <remarks>
        /// Normally NaN &lt; anything is false, as is anything &lt; NaN
        /// However, this leads to some irksome outcomes in Min and Max.
        /// If we use those semantics then Min(NaN, 5.0) is NaN, but
        /// Min(5.0, NaN) is 5.0! To fix this, we impose a total
        /// ordering where NaN is smaller/bigger than every value, including infinity.
        /// </remarks>
        public static (double? minValue, double? maxValue) MinMax(this IEnumerable<double?> source)
        {
            source.ThrowIfNull(nameof(source));

            double? minValue = null;
            double? maxValue = null;
            foreach (double? x in source)
            {
                if (x is null) continue;
                if (minValue is null || x < minValue || double.IsNaN((double) x)) minValue = x;
                if (maxValue is null || x > maxValue || double.IsNaN((double) x)) maxValue = x;
            }

            return (minValue, maxValue);
        }

        #endregion

        #region MinMax For Decimal

        /// <summary>
        /// Returns the minimum and maximum values in a sequence of <see cref="decimal" /> values.
        /// </summary>
        /// <param name="source">
        /// A sequence of <see cref="decimal" /> values to determine the minimum and maximum values
        /// of.
        /// </param>
        /// <returns>The minimum and maximum values in the sequence.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source" /> is <c>null</c>.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="source" /> contains no elements.
        /// </exception>
        public static (decimal minValue, decimal maxValue) MinMax(this IEnumerable<decimal> source)
        {
            source.ThrowIfNull(nameof(source));

            decimal minValue = 0;
            decimal maxValue = 0;
            bool hasValue = false;
            foreach (decimal x in source)
            {
                if (hasValue)
                {
                    if (x < minValue) minValue = x;
                    if (x > maxValue) maxValue = x;
                }
                else
                {
                    minValue = x;
                    maxValue = x;
                    hasValue = true;
                }
            }

            if (hasValue) return (minValue, maxValue);

            throw Error.NoElements();
        }

        /// <summary>
        /// Returns the minimum and maximum values in a sequence of nullable <see cref="decimal" />
        /// values.
        /// </summary>
        /// <param name="source">
        /// A sequence of nullable <see cref="decimal" /> values to determine the minimum and
        /// maximum values of.
        /// </param>
        /// <returns>
        /// A value of type <see cref="Nullable{Decimal}" /> that corresponds to the minimum and
        /// maximum values in the sequence.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source" /> is <c>null</c>.
        /// </exception>
        public static (decimal? minValue, decimal? maxValue) MinMax(
            this IEnumerable<decimal?> source)
        {
            source.ThrowIfNull(nameof(source));

            decimal? minValue = null;
            decimal? maxValue = null;
            foreach (decimal? x in source)
            {
                if (minValue is null || x < minValue) minValue = x;
                if (maxValue is null || x > maxValue) maxValue = x;
            }

            return (minValue, maxValue);
        }

        #endregion

        #region MinMax For Generic Types

        /// <summary>
        /// Returns the minimum and maximum values in a generic sequence with proivided comparer.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements of <paramref name="source" />.
        /// </typeparam>
        /// <param name="source">
        /// A sequence of values to determine the minimum and maximum values of.
        /// </param>
        /// <param name="comparer">An element comparer.</param>
        /// <returns>The minimum and maximum values in the sequence.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source" /> is <c>null</c>. -or-
        /// <paramref name="comparer" /> is <c>null</c>.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="source" /> contains no elements.
        /// </exception>
        public static (TSource minValue, TSource maxValue) MinMax<TSource>(
            this IEnumerable<TSource> source,
            IComparer<TSource> comparer)
        {
            source.ThrowIfNull(nameof(source));
            comparer.ThrowIfNull(nameof(comparer));

#pragma warning disable CS8653 // A default expression introduces a null value for a type parameter.
            TSource minValue = default;
            TSource maxValue = default;
#pragma warning restore CS8653 // A default expression introduces a null value for a type parameter.

            // Second part of check is redundant but otherwise compiler shows warning about
            // null reference assignment of maxValue variable in the else block.
            if (minValue is null || maxValue is null)
            {
                foreach (TSource x in source)
                {
                    if (!(x is null) && (minValue is null || comparer.Compare(x, minValue) < 0)) minValue = x;
                    if (!(x is null) && (maxValue is null || comparer.Compare(x, maxValue) > 0)) maxValue = x;
                }

                return (minValue, maxValue);
            }
            else
            {
                bool hasValue = false;
                foreach (TSource x in source)
                {
                    if (hasValue)
                    {
                        if (comparer.Compare(x, minValue) < 0) minValue = x;
                        if (comparer.Compare(x, maxValue) > 0) maxValue = x;
                    }
                    else
                    {
                        minValue = x;
                        maxValue = x;
                        hasValue = true;
                    }
                }

                if (hasValue) return (minValue, maxValue);

                throw Error.NoElements();
            }
        }

        /// <summary>
        /// Returns the minimum and maximum values in a generic sequence with default comparer of
        /// type <typeparamref name="TSource" />.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements of <paramref name="source" />.
        /// </typeparam>
        /// <param name="source">
        /// A sequence of values to determine the minimum and maximum values of.
        /// </param>
        /// <returns>The minimum and maximum values in the sequence.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source" /> is <c>null</c>.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="source" /> contains no elements.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// No object in <paramref name="source" /> implements the <see cref="IComparable" /> or
        /// <see cref="IComparable{TSource}" /> interface.
        /// </exception>
        public static (TSource minValue, TSource maxValue) MinMax<TSource>(
            this IEnumerable<TSource> source)
        {
            return source.MinMax(Comparer<TSource>.Default);
        }

        #endregion

        #endregion

        #region MinMax Overloads With Selector

        #region MinMax For Int32

        /// <summary>
        /// Invokes a transform function on each element of a sequence and returns the minimum and
        /// maximum <see cref="int" /> values.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements of <paramref name="source" />.
        /// </typeparam>
        /// <param name="source">
        /// A sequence of values to determine the minimum and maximum values of.
        /// </param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>The minimum and maximum values in the sequence.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source" /> is <c>null</c>.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="source" /> contains no elements.
        /// </exception>
        public static (int minValue, int maxValue) MinMax<TSource>(
            this IEnumerable<TSource> source,
            Func<TSource, int> selector)
        {
            return source
                  .Select(selector)
                  .MinMax();
        }

        /// <summary>
        /// Invokes a transform function on each element of a sequence and returns the minimum and
        /// maximum nullable <see cref="int" /> values.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements of <paramref name="source" />.
        /// </typeparam>
        /// <param name="source">
        /// A sequence of values to determine the minimum and maximum values of.
        /// </param>
        /// <param name="selector">A transform function to apply to each element.</param>
        ///  <returns>
        /// A value of type <see cref="Nullable{Int32}" /> that corresponds to the minimum and
        /// maximum values in the sequence.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source" /> is <c>null</c>.
        /// </exception>
        public static (int? minValue, int? maxValue) MinMax<TSource>(
            this IEnumerable<TSource> source,
            Func<TSource, int?> selector)
        {
            return source
                .Select(selector)
                .MinMax();
        }

        #endregion

        #region MinMax For Int64

        /// <summary>
        /// Invokes a transform function on each element of a sequence and returns the minimum and
        /// maximum <see cref="long" /> values.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements of <paramref name="source" />.
        /// </typeparam>
        /// <param name="source">
        /// A sequence of values to determine the minimum and maximum values of.
        /// </param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>The minimum and maximum values in the sequence.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source" /> is <c>null</c>.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="source" /> contains no elements.
        /// </exception>
        public static (long minValue, long maxValue) MinMax<TSource>(
            this IEnumerable<TSource> source,
            Func<TSource, long> selector)
        {
            return source
                .Select(selector)
                .MinMax();
        }

        /// <summary>
        /// Invokes a transform function on each element of a sequence and returns the minimum and
        /// maximum nullable <see cref="long" /> values.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements of <paramref name="source" />.
        /// </typeparam>
        /// <param name="source">
        /// A sequence of values to determine the minimum and maximum values of.
        /// </param>
        /// <param name="selector">A transform function to apply to each element.</param>
        ///  <returns>
        /// A value of type <see cref="Nullable{Int64}" /> that corresponds to the minimum and
        /// maximum values in the sequence.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source" /> is <c>null</c>.
        /// </exception>
        public static (long? minValue, long? maxValue) MinMax<TSource>(
            this IEnumerable<TSource> source,
            Func<TSource, long?> selector)
        {
            return source
                .Select(selector)
                .MinMax();
        }

        #endregion

        #region MinMax For Single

        /// <summary>
        /// Invokes a transform function on each element of a sequence and returns the minimum and
        /// maximum <see cref="float" /> values.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements of <paramref name="source" />.
        /// </typeparam>
        /// <param name="source">
        /// A sequence of values to determine the minimum and maximum values of.
        /// </param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>The minimum and maximum values in the sequence.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source" /> is <c>null</c>.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="source" /> contains no elements.
        /// </exception>
        /// <remarks>
        /// Normally NaN &lt; anything is false, as is anything &lt; NaN
        /// However, this leads to some irksome outcomes in Min and Max.
        /// If we use those semantics then Min(NaN, 5.0) is NaN, but
        /// Min(5.0, NaN) is 5.0! To fix this, we impose a total
        /// ordering where NaN is smaller/bigger than every value, including infinity.
        /// </remarks>
        public static (float minValue, float maxValue) MinMax<TSource>(
            this IEnumerable<TSource> source,
            Func<TSource, float> selector)
        {
            return source
                 .Select(selector)
                 .MinMax();
        }

        /// <summary>
        /// Invokes a transform function on each element of a sequence and returns the minimum and
        /// maximum nullable <see cref="float" /> values.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements of <paramref name="source" />.
        /// </typeparam>
        /// <param name="source">
        /// A sequence of values to determine the minimum and maximum values of.
        /// </param>
        /// <param name="selector">A transform function to apply to each element.</param>
        ///  <returns>
        /// A value of type <see cref="Nullable{Single}" /> that corresponds to the minimum and
        /// maximum values in the sequence.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source" /> is <c>null</c>.
        /// </exception>
        /// <remarks>
        /// Normally NaN &lt; anything is false, as is anything &lt; NaN
        /// However, this leads to some irksome outcomes in Min and Max.
        /// If we use those semantics then Min(NaN, 5.0) is NaN, but
        /// Min(5.0, NaN) is 5.0! To fix this, we impose a total
        /// ordering where NaN is smaller/bigger than every value, including infinity.
        /// </remarks>
        public static (float? minValue, float? maxValue) MinMax<TSource>(
            this IEnumerable<TSource> source,
            Func<TSource, float?> selector)
        {
            return source
                 .Select(selector)
                 .MinMax();
        }

        #endregion

        #region MinMax For Double

        /// <summary>
        /// Invokes a transform function on each element of a sequence and returns the minimum and
        /// maximum <see cref="double" /> values.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements of <paramref name="source" />.
        /// </typeparam>
        /// <param name="source">
        /// A sequence of values to determine the minimum and maximum values of.
        /// </param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>The minimum and maximum values in the sequence.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source" /> is <c>null</c>.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="source" /> contains no elements.
        /// </exception>
        /// <remarks>
        /// Normally NaN &lt; anything is false, as is anything &lt; NaN
        /// However, this leads to some irksome outcomes in Min and Max.
        /// If we use those semantics then Min(NaN, 5.0) is NaN, but
        /// Min(5.0, NaN) is 5.0! To fix this, we impose a total
        /// ordering where NaN is smaller/bigger than every value, including infinity.
        /// </remarks>
        public static (double minValue, double maxValue) MinMax<TSource>(
            this IEnumerable<TSource> source,
            Func<TSource, double> selector)
        {
            return source
                .Select(selector)
                .MinMax();
        }

        /// <summary>
        /// Invokes a transform function on each element of a sequence and returns the minimum and
        /// maximum nullable <see cref="double" /> values.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements of <paramref name="source" />.
        /// </typeparam>
        /// <param name="source">
        /// A sequence of values to determine the minimum and maximum values of.
        /// </param>
        /// <param name="selector">A transform function to apply to each element.</param>
        ///  <returns>
        /// A value of type <see cref="Nullable{Double}" /> that corresponds to the minimum and
        /// maximum values in the sequence.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source" /> is <c>null</c>.
        /// </exception>
        /// <remarks>
        /// Normally NaN &lt; anything is false, as is anything &lt; NaN
        /// However, this leads to some irksome outcomes in Min and Max.
        /// If we use those semantics then Min(NaN, 5.0) is NaN, but
        /// Min(5.0, NaN) is 5.0! To fix this, we impose a total
        /// ordering where NaN is smaller/bigger than every value, including infinity.
        /// </remarks>
        public static (double? minValue, double? maxValue) MinMax<TSource>(
            this IEnumerable<TSource> source,
            Func<TSource, double?> selector)
        {
            return source
                 .Select(selector)
                 .MinMax();
        }

        #endregion

        #region MinMax For Decimal

        /// <summary>
        /// Invokes a transform function on each element of a sequence and returns the minimum and
        /// maximum <see cref="decimal" /> values.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements of <paramref name="source" />.
        /// </typeparam>
        /// <param name="source">
        /// A sequence of values to determine the minimum and maximum values of.
        /// </param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>The minimum and maximum values in the sequence.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source" /> is <c>null</c>.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="source" /> contains no elements.
        /// </exception>
        public static (decimal minValue, decimal maxValue) MinMax<TSource>(
            this IEnumerable<TSource> source,
            Func<TSource, decimal> selector)
        {
            return source
                 .Select(selector)
                 .MinMax();
        }

        /// <summary>
        /// Invokes a transform function on each element of a sequence and returns the minimum and
        /// maximum nullable <see cref="decimal" /> values.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements of <paramref name="source" />.
        /// </typeparam>
        /// <param name="source">
        /// A sequence of values to determine the minimum and maximum values of.
        /// </param>
        /// <param name="selector">A transform function to apply to each element.</param>
        ///  <returns>
        /// A value of type <see cref="Nullable{Decimal}" /> that corresponds to the minimum and
        /// maximum values in the sequence.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source" /> is <c>null</c>.
        /// </exception>
        public static (decimal? minValue, decimal? maxValue) MinMax<TSource>(
            this IEnumerable<TSource> source,
            Func<TSource, decimal?> selector)
        {
            return source
                .Select(selector)
                .MinMax();
        }

        #endregion

        #region MinMax For Generic Types

        /// <summary>
        /// Invokes a transform function on each element of a generic sequence and returns the
        /// minimum and maximum resulting values with provided comparer.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements of <paramref name="source"/>.
        /// </typeparam>
        /// <typeparam name="TResult">
        /// The type of the value returned by <paramref name="selector"/>.
        /// </typeparam>
        /// <param name="source">
        /// A sequence of values to determine the minimum and maximum values of.
        /// </param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <param name="comparer">An element comparer.</param>
        /// <returns>The minimum and maximum values in the sequence.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source" /> is <c>null</c> -or-
        /// <paramref name="comparer" /> is <c>null</c>.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="source" /> contains no elements.
        /// </exception>
        public static (TResult minValue, TResult maxValue) MinMax<TSource, TResult>(
            this IEnumerable<TSource> source,
            Func<TSource, TResult> selector,
            IComparer<TResult> comparer)
        {
            return source
                 .Select(selector)
                 .MinMax(comparer);
        }

        /// <summary>
        /// Invokes a transform function on each element of a generic sequence and returns the
        /// minimum and maximum resulting values with default comparer of type
        /// <typeparamref name="TResult" />.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements of <paramref name="source"/>.
        /// </typeparam>
        /// <typeparam name="TResult">
        /// The type of the value returned by <paramref name="selector"/>.
        /// </typeparam>
        /// <param name="source">
        /// A sequence of values to determine the minimum and maximum values of.
        /// </param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>The minimum and maximum values in the sequence.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source" /> is <c>null</c>.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="source" /> contains no elements.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// No object in <paramref name="source" /> implements the <see cref="IComparable" /> or
        /// <see cref="IComparable{TResult}" /> interface.
        /// </exception>
        public static (TResult minValue, TResult maxValue) MinMax<TSource, TResult>(
            this IEnumerable<TSource> source,
            Func<TSource, TResult> selector)
        {
            return source.MinMax(selector, Comparer<TResult>.Default);
        }

        #endregion

        #endregion

        #endregion

        #region Min/Max By Key

        #region Min By Key

        /// <summary>
        /// Retrieves minimum element by key in a generic sequence.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements of <paramref name="source" />.
        /// </typeparam>
        /// <typeparam name="TKey">Type of key item to compare.</typeparam>
        /// <param name="source">
        /// A sequence of values to determine the minimum by key element of.
        /// </param>
        /// <param name="keySelector">Selector that transform source item to key item.</param>
        /// <param name="comparer">Key item comparer.</param>
        /// <returns>The minimum by key element in the sequence.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source" /> is <c>null</c>. -or-
        /// <paramref name="keySelector" /> is <c>null</c>. -or-
        /// <paramref name="comparer" /> is <c>null</c>.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="source" /> contains no elements.
        /// </exception>
        [return: MaybeNull]
        public static TSource MinBy<TSource, TKey>(
            this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector,
            IComparer<TKey> comparer)
        {
            source.ThrowIfNull(nameof(source));
            keySelector.ThrowIfNull(nameof(keySelector));
            comparer.ThrowIfNull(nameof(comparer));

#pragma warning disable CS8653 // A default expression introduces a null value for a type parameter.
            TSource minValue = default;
#pragma warning restore CS8653 // A default expression introduces a null value for a type parameter.

            using (IEnumerator<TSource> enumerator = source.GetEnumerator())
            {
                if (!enumerator.MoveNext())
                {
                    if (minValue is null) return minValue;

                    throw Error.NoElements();
                }

                minValue = enumerator.Current;
                TKey minElementKey = keySelector(minValue);

                while (enumerator.MoveNext())
                {
                    TSource element = enumerator.Current;
                    TKey elementKey = keySelector(element);

                    if (comparer.Compare(elementKey, minElementKey) < 0)
                    {
                        minValue = element;
                        minElementKey = elementKey;
                    }
                }
            }

            return minValue;
        }

        /// <summary>
        /// Retrieves minimum element by key in a generic sequence with default key comparer of type
        /// <typeparamref name="TKey" />.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements of <paramref name="source" />.
        /// </typeparam>
        /// <typeparam name="TKey">Type of key item to compare.</typeparam>
        /// <param name="source">
        /// A sequence of values to determine the minimum by key element of.
        /// </param>
        /// <param name="keySelector">Selector that transform source item to key item.</param>
        /// <returns>The minimum by key element in the sequence.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source" /> is <c>null</c>. -or-
        /// <paramref name="keySelector" /> is <c>null</c>. -or-
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="source" /> contains no elements.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// No object in <paramref name="source" /> implements the <see cref="IComparable" /> or
        /// <see cref="IComparable{TKey}" /> interface.
        /// </exception>
        [return: MaybeNull]
        public static TSource MinBy<TSource, TKey>(
            this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector)
        {
            return source.MinBy(keySelector, Comparer<TKey>.Default);
        }

        #endregion

        #region Max By Key

        /// <summary>
        /// Retrieves maximum element by key in a generic sequence.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements of <paramref name="source" />.
        /// </typeparam>
        /// <typeparam name="TKey">Type of key item to compare.</typeparam>
        /// <param name="source">
        /// A sequence of values to determine the maximum by key element of.
        /// </param>
        /// <param name="keySelector">Selector that transform source item to key item.</param>
        /// <param name="comparer">Key item comparer.</param>
        /// <returns>The maximum by key element in the sequence.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source" /> is <c>null</c>. -or-
        /// <paramref name="keySelector" /> is <c>null</c>. -or-
        /// <paramref name="comparer" /> is <c>null</c>.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="source" /> contains no elements.
        /// </exception>
        [return: MaybeNull]
        public static TSource MaxBy<TSource, TKey>(
            this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector,
            IComparer<TKey> comparer)
        {
            return source.MinBy(keySelector, new InverseComparer<TKey>(comparer));
        }

        /// <summary>
        /// Retrieves maximum element by key in a generic sequence with default key comparer of type
        /// <typeparamref name="TKey" />.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements of <paramref name="source" />.
        /// </typeparam>
        /// <typeparam name="TKey">Type of key item to compare.</typeparam>
        /// <param name="source">
        /// A sequence of values to determine the maximum by key element of.
        /// </param>
        /// <param name="keySelector">Selector that transform source item to key item.</param>
        /// <returns>The maximum by key element in the sequence.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source" /> is <c>null</c>. -or-
        /// <paramref name="keySelector" /> is <c>null</c>. -or-
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="source" /> contains no elements.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// No object in <paramref name="source" /> implements the <see cref="IComparable" /> or
        /// <see cref="IComparable{TKey}" /> interface.
        /// </exception>
        [return: MaybeNull]
        public static TSource MaxBy<TSource, TKey>(
            this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector)
        {
            return source.MinBy(keySelector, new InverseComparer<TKey>(Comparer<TKey>.Default));
        }

        #endregion

        #endregion

        #region MinMax By Key

        /// <summary>
        /// Retrieves minimum and maximum by key elements in a generic sequence.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements of <paramref name="source" />.
        /// </typeparam>
        /// <typeparam name="TKey">Type of key item to compare.</typeparam>
        /// <param name="source">
        /// A sequence of values to determine the minimum and maximum by key elements of.
        /// </param>
        /// <param name="keySelector">Selector that transform source item to key item.</param>
        /// <param name="comparer">Key item comparer.</param>
        /// <returns>The minimum and maximum by key elements in the sequence.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source" /> is <c>null</c>. -or-
        /// <paramref name="keySelector" /> is <c>null</c>. -or-
        /// <paramref name="comparer" /> is <c>null</c>.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="source" /> contains no elements.
        /// </exception>
        public static (TSource minValue, TSource maxValue) MinMaxBy<TSource, TKey>(
            this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector,
            IComparer<TKey> comparer)
        {
            source.ThrowIfNull(nameof(source));
            keySelector.ThrowIfNull(nameof(keySelector));
            comparer.ThrowIfNull(nameof(comparer));

#pragma warning disable CS8653 // A default expression introduces a null value for a type parameter.
            TSource minValue = default;
            TSource maxValue = default;
#pragma warning restore CS8653 // A default expression introduces a null value for a type parameter.

            using (IEnumerator<TSource> enumerator = source.GetEnumerator())
            {
                if (!enumerator.MoveNext())
                {
                    if (minValue is null) return (minValue, maxValue);

                    throw Error.NoElements();
                }

                minValue = enumerator.Current;
                maxValue = enumerator.Current;
                TKey minElementKey = keySelector(minValue);
                TKey maxElementKey = keySelector(maxValue);

                while (enumerator.MoveNext())
                {
                    TSource element = enumerator.Current;
                    TKey elementKey = keySelector(element);

                    if (comparer.Compare(elementKey, minElementKey) < 0)
                    {
                        minValue = element;
                        minElementKey = elementKey;
                    }
                }
            }

            return (minValue, maxValue);
        }

        /// <summary>
        /// Retrieves minimum element by key in a generic sequence with default key comparer of
        /// type <typeparamref name="TKey" />.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements of <paramref name="source" />.
        /// </typeparam>
        /// <typeparam name="TKey">Type of key item to compare.</typeparam>
        /// <param name="source">
        /// A sequence of values to determine the minimum by key element of.
        /// </param>
        /// <param name="keySelector">Selector that transform source item to key item.</param>
        /// <returns>The minimum by key element in the sequence.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source" /> is <c>null</c>. -or-
        /// <paramref name="keySelector" /> is <c>null</c>. -or-
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="source" /> contains no elements.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// No object in <paramref name="source" /> implements the <see cref="IComparable" /> or
        /// <see cref="IComparable{TKey}" /> interface.
        /// </exception>
        public static (TSource minValue, TSource maxValue) MinMaxBy<TSource, TKey>(
            this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector)
        {
            return source.MinMaxBy(keySelector, Comparer<TKey>.Default);
        }

        #endregion

        #region To Single String

        /// <summary>
        /// Transforms sequence to the string.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements of <paramref name="source" />.
        /// </typeparam>
        /// <param name="source">A sequence of values to convert to string.</param>
        /// <returns>
        /// The string which represent converted value of sequence or special message if 
        /// <paramref name="source" /> is <c>null</c> or contains no elements.
        /// </returns>
        public static string ToSingleString<TSource>(this IEnumerable<TSource>? source)
        {
            return source.ToSingleString(
                separator: ", ",
                emptyCollectionMessage: "None",
                selector: item => item is null ? string.Empty : $"'{item.ToString()}'"
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
        /// The string to return if <paramref name="source" /> is <c>null</c> or contains no
        /// elements.
        /// </param>
        /// <returns>
        /// The string which represent converted value of sequence or special message if 
        /// <paramref name="source" /> is <c>null</c> or contains no elements.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="emptyCollectionMessage" /> is <c>null</c>.
        /// </exception>
        public static string ToSingleString<TSource>(this IEnumerable<TSource>? source,
            string emptyCollectionMessage)
        {
            return source.ToSingleString(
                separator: ", ",
                emptyCollectionMessage: emptyCollectionMessage,
                selector: item => item is null ? string.Empty : $"'{item.ToString()}'"
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
        /// The string to return if <paramref name="source" /> is <c>null</c> or contains no
        /// elements.
        /// </param>
        /// <param name="separator">
        /// The string to use as a separator. <paramref name="separator" /> is included in the
        /// returned string only if values has more than one element.
        /// </param>
        /// <returns>
        /// The string which represent converted value of sequence or special message if
        /// <paramref name="source" /> is <c>null</c> or contains no elements.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="emptyCollectionMessage" /> is <c>null</c>.
        /// </exception>
        public static string ToSingleString<TSource>(this IEnumerable<TSource>? source,
            string emptyCollectionMessage, string? separator)
        {
            return source.ToSingleString(
                emptyCollectionMessage: emptyCollectionMessage,
                separator: separator,
                selector: item => item is null ? string.Empty : $"'{item.ToString()}'"
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
        /// <paramref name="source" /> is <c>null</c> or contains no elements.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="selector" /> is <c>null</c>.
        /// </exception>
        public static string ToSingleString<TSource>(this IEnumerable<TSource>? source,
            Func<TSource, string> selector)
        {
            return source.ToSingleString(
                emptyCollectionMessage: "None",
                separator: ", ",
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
        /// The string to return if <paramref name="source" /> is <c>null</c> or contains no
        /// elements.
        /// </param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>
        /// The string which represent converted value of sequence or special message if 
        /// <paramref name="source" /> is <c>null</c> or contains no elements.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="emptyCollectionMessage" /> is <c>null</c>. -or-
        /// <paramref name="selector" /> is <c>null</c>.
        /// </exception>
        public static string ToSingleString<TSource>(this IEnumerable<TSource>? source,
            string emptyCollectionMessage, Func<TSource, string> selector)
        {
            return source.ToSingleString(
                emptyCollectionMessage: emptyCollectionMessage,
                separator: ", ",
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
        /// The string to return if <paramref name="source" /> is <c>null</c> or contains no
        /// elements.
        /// </param>
        /// <param name="separator">
        /// The string to use as a separator. <paramref name="separator" /> is included in the
        /// returned string only if values has more than one element.
        /// </param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>
        /// The string which represent converted value of sequence or specified message if 
        /// <paramref name="source" /> is <c>null</c> or contains no elements.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="emptyCollectionMessage" /> is <c>null</c>. -or-
        /// <paramref name="selector" /> is <c>null</c>.
        /// </exception>
        public static string ToSingleString<TSource>(this IEnumerable<TSource>? source,
            string emptyCollectionMessage, string? separator, Func<TSource, string> selector)
        {
            // Null check for "selector" parameter is provided by Enumerable.Select method.
            emptyCollectionMessage.ThrowIfNull(nameof(emptyCollectionMessage));

            if (source.IsNullOrEmpty()) return emptyCollectionMessage;

            IEnumerable<string> transformedSource = source.Select(selector);

            return string.Join(separator, transformedSource);
        }

        #endregion

        #region For Each

        public static void ForEach<TSource>(
            this IEnumerable<TSource> source,
            Action<TSource> action)
        {
            source.ThrowIfNull(nameof(source));

            foreach (TSource item in source)
            {
                action(item);
            }
        }

        public static async Task ForEachAsync<TSource>(
            this IEnumerable<TSource> source,
            Func<TSource, Task> action,
            CancellationToken cancellationToken)
        {
            source.ThrowIfNull(nameof(source));
            action.ThrowIfNull(nameof(action));

            foreach (TSource item in source)
            {
                cancellationToken.ThrowIfCancellationRequested();
                await action(item);
            }
        }

        public static Task ForEachAsync<TSource>(
            this IEnumerable<TSource> source,
            Func<TSource, Task> action)
        {
            return source.ForEachAsync(action, cancellationToken: CancellationToken.None);
        }

        #endregion

        #region Distinct By

        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(
            this IEnumerable<TSource> source,
            Func<TSource, TKey> selector)
        {
            source.ThrowIfNull(nameof(source));
            selector.ThrowIfNull(nameof(selector));

            var seenKeys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                if (seenKeys.Add(selector(element))) yield return element;
            }
        }

        #endregion

        #region Slicing

        public static IEnumerable<TSource> SliceIfRequired<TSource>(
            this IEnumerable<TSource> source,
            int? startIndex,
            int? count)
        {
            source.ThrowIfNull(nameof(source));

            if (!startIndex.HasValue && !count.HasValue) return source;

            if (startIndex.HasValue && count.HasValue)
            {
                return source
                    .Skip(startIndex.Value)
                    .Take(count.Value);
            }

            int startIndexValue = startIndex.GetValueOrDefault();

            IEnumerable<TSource> result = source.Skip(startIndexValue);
            if (count.HasValue)
            {
                result = result.Take(count.Value);
            }

            return result;
        }

        #endregion

        #region Order By Sequence

        public static IEnumerable<TSource> OrderBySequence<TSource>(
            this IEnumerable<TSource> source,
            IEnumerable<TSource> order)
        {
            return source.OrderBySequence(
                order,
                sourceItem => sourceItem,
                orderItem => orderItem,
                (sourceItem, orderItem) => sourceItem
            );
        }

        public static IEnumerable<TSource> OrderBySequence<TSource, TOrder>(
            this IEnumerable<TSource> source,
            IEnumerable<TOrder> order,
            Func<TSource, TOrder> sourceKeySelector)
        {
            return source.OrderBySequence(
                order,
                sourceKeySelector,
                orderItem => orderItem,
                (sourceItem, orderItem) => sourceItem
            );
        }

        public static IEnumerable<TSource> OrderBySequence<TSource, TOrder, TKey>(
            this IEnumerable<TSource> source,
            IEnumerable<TOrder> order,
            Func<TSource, TKey> sourceKeySelector,
            Func<TOrder, TKey> orderKeySelector)
        {
            return source.OrderBySequence(
                order,
                sourceKeySelector,
                orderKeySelector,
                (sourceItem, orderItem) => sourceItem
            );
        }

        public static IEnumerable<TResult> OrderBySequence<TSource, TOrder, TKey, TResult>(
            this IEnumerable<TSource> source,
            IEnumerable<TOrder> order,
            Func<TSource, TKey> sourceKeySelector,
            Func<TOrder, TKey> orderKeySelector,
            Func<TSource, TOrder, TResult> resultSelector)
        {
            // Join method keeps order of the first collection, e.g. "order" in our case.
            return order.Join(
                source,
                orderKeySelector,
                sourceKeySelector,
                (orderItem, sourceItem) => resultSelector(sourceItem, orderItem)
            );
        }

        #endregion
    }
}
