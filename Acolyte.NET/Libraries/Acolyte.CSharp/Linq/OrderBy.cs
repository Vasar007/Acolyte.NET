using System;
using System.Collections.Generic;
using System.Linq;
using Acolyte.Assertions;
using Acolyte.Functions;

namespace Acolyte.Linq
{
    public static partial class EnumerableExtensions
    {
        #region Order By Sequence Overloads Without Comparer

        /// <summary>
        /// Sorts the elements of a sequence in order according to a specified ordinal sequence.
        /// </summary>
        /// <remarks>
        /// Implementation uses
        /// <see cref="Enumerable.Join{TOuter, TInner, TKey, TResult}(IEnumerable{TOuter}, IEnumerable{TInner}, Func{TOuter, TKey}, Func{TInner, TKey}, Func{TOuter, TInner, TResult}, IEqualityComparer{TKey})" />
        /// method that performs inner join of two sequences.
        /// </remarks>
        /// <typeparam name="TSource">
        /// The type of the elements of <paramref name="source" />.
        /// </typeparam>
        /// <param name="source">A sequence of values to order.</param>
        /// <param name="order">A sequence of ordinal values.</param>
        /// <returns>An <see cref="IEnumerable{TSource}" /> that has elements of type
        /// <typeparamref name="TSource" /> that are obtained by performing an inner join on two
        /// sequences.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source" /> is <see langword="null" />. -or-
        /// <paramref name="order" /> is <see langword="null" />.
        /// </exception>
        public static IEnumerable<TSource> OrderBySequence<TSource>(
            this IEnumerable<TSource> source, IEnumerable<TSource> order)
        {
            return source.OrderBySequence(
                order,
                IdentityFunction<TSource>.Instance,
                IdentityFunction<TSource>.Instance,
                (sourceItem, orderItem) => sourceItem,
                EqualityComparer<TSource>.Default
            );
        }

        /// <summary>
        /// Sorts the elements of a sequence in order according to a specified ordinal sequence.
        /// </summary>
        /// <remarks>
        /// Implementation uses
        /// <see cref="Enumerable.Join{TOuter, TInner, TKey, TResult}(IEnumerable{TOuter}, IEnumerable{TInner}, Func{TOuter, TKey}, Func{TInner, TKey}, Func{TOuter, TInner, TResult}, IEqualityComparer{TKey})" />
        /// method that performs inner join of two sequences.
        /// </remarks>
        /// <typeparam name="TSource">
        /// The type of the elements of <paramref name="source" />.
        /// </typeparam>
        /// <typeparam name="TOrder">
        /// The type of the elements of <paramref name="order" />.
        /// </typeparam>
        /// <param name="source">A sequence of values to order.</param>
        /// <param name="order">A sequence of ordinal values.</param>
        /// <param name="sourceKeySelector">
        /// A function to extract a key from each element of <paramref name="source" />.
        /// </param>
        /// <returns>An <see cref="IEnumerable{TSource}" /> that has elements of type
        /// <typeparamref name="TSource" /> that are obtained by performing an inner join on two
        /// sequences.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source" /> is <see langword="null" />. -or-
        /// <paramref name="order" /> is <see langword="null" />. -or-
        /// <paramref name="sourceKeySelector" /> is <see langword="null" />.
        /// </exception>
        public static IEnumerable<TSource> OrderBySequence<TSource, TOrder>(
            this IEnumerable<TSource> source, IEnumerable<TOrder> order,
            Func<TSource, TOrder> sourceKeySelector)
        {
            return source.OrderBySequence(
                order,
                sourceKeySelector,
                IdentityFunction<TOrder>.Instance,
                (sourceItem, orderItem) => sourceItem,
                EqualityComparer<TOrder>.Default
            );
        }

        /// <summary>
        /// Sorts the elements of a sequence in order according to a specified ordinal sequence.
        /// </summary>
        /// <remarks>
        /// Implementation uses
        /// <see cref="Enumerable.Join{TOuter, TInner, TKey, TResult}(IEnumerable{TOuter}, IEnumerable{TInner}, Func{TOuter, TKey}, Func{TInner, TKey}, Func{TOuter, TInner, TResult}, IEqualityComparer{TKey})" />
        /// method that performs inner join of two sequences.
        /// </remarks>
        /// <typeparam name="TSource">
        /// The type of the elements of <paramref name="source" />.
        /// </typeparam>
        /// <typeparam name="TOrder">
        /// The type of the elements of <paramref name="order" />.
        /// </typeparam>
        /// <typeparam name="TKey">
        /// The type of the keys returned by the key selector functions.
        /// </typeparam>
        /// <param name="source">A sequence of values to order.</param>
        /// <param name="order">A sequence of ordinal values.</param>
        /// <param name="sourceKeySelector">
        /// A function to extract a key from each element of <paramref name="source" />.
        /// </param>
        /// <param name="orderKeySelector">
        /// A function to extract a key from each element of <paramref name="order" />.
        /// </param>
        /// <returns>An <see cref="IEnumerable{TSource}" /> that has elements of type
        /// <typeparamref name="TSource" /> that are obtained by performing an inner join on two
        /// sequences.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source" /> is <see langword="null" />. -or-
        /// <paramref name="order" /> is <see langword="null" />. -or-
        /// <paramref name="sourceKeySelector" /> is <see langword="null" />. -or-
        /// <paramref name="orderKeySelector" /> is <see langword="null" />.
        /// </exception>
        public static IEnumerable<TSource> OrderBySequence<TSource, TOrder, TKey>(
            this IEnumerable<TSource> source, IEnumerable<TOrder> order,
            Func<TSource, TKey> sourceKeySelector, Func<TOrder, TKey> orderKeySelector)
        {
            return source.OrderBySequence(
                order,
                sourceKeySelector,
                orderKeySelector,
                (sourceItem, orderItem) => sourceItem,
                EqualityComparer<TKey>.Default
            );
        }

        /// <summary>
        /// Sorts the elements of a sequence in order according to a specified ordinal sequence.
        /// </summary>
        /// <remarks>
        /// Implementation uses
        /// <see cref="Enumerable.Join{TOuter, TInner, TKey, TResult}(IEnumerable{TOuter}, IEnumerable{TInner}, Func{TOuter, TKey}, Func{TInner, TKey}, Func{TOuter, TInner, TResult}, IEqualityComparer{TKey})" />
        /// method that performs inner join of two sequences.
        /// </remarks>
        /// <typeparam name="TSource">
        /// The type of the elements of <paramref name="source" />.
        /// </typeparam>
        /// <typeparam name="TOrder">
        /// The type of the elements of <paramref name="order" />.
        /// </typeparam>
        /// <typeparam name="TKey">
        /// The type of the keys returned by the key selector functions.
        /// </typeparam>
        /// <typeparam name="TResult">The type of the result elements.</typeparam>
        /// <param name="source">A sequence of values to order.</param>
        /// <param name="order">A sequence of ordinal values.</param>
        /// <param name="sourceKeySelector">
        /// A function to extract a key from each element of <paramref name="source" />.
        /// </param>
        /// <param name="orderKeySelector">
        /// A function to extract a key from each element of <paramref name="order" />.
        /// </param>
        /// <param name="resultSelector">
        /// A function to create a result element from two matching elements.
        /// </param>
        /// <returns>An <see cref="IEnumerable{TResult}" /> that has elements of type
        /// <typeparamref name="TResult" /> that are obtained by performing an inner join on two
        /// sequences.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source" /> is <see langword="null" />. -or-
        /// <paramref name="order" /> is <see langword="null" />. -or-
        /// <paramref name="sourceKeySelector" /> is <see langword="null" />. -or-
        /// <paramref name="orderKeySelector" /> is <see langword="null" />. -or-
        /// <paramref name="resultSelector" /> is <see langword="null" />.
        /// </exception>
        public static IEnumerable<TResult> OrderBySequence<TSource, TOrder, TKey, TResult>(
            this IEnumerable<TSource> source, IEnumerable<TOrder> order,
            Func<TSource, TKey> sourceKeySelector, Func<TOrder, TKey> orderKeySelector,
            Func<TSource, TOrder, TResult> resultSelector)
        {
            return source.OrderBySequence(
                order,
                sourceKeySelector,
                orderKeySelector,
                resultSelector,
                EqualityComparer<TKey>.Default
            );
        }

        #endregion

        #region Order By Sequence Overloads With Comparer

        /// <summary>
        /// Sorts the elements of a sequence in order according to a specified ordinal sequence.
        /// </summary>
        /// <remarks>
        /// Implementation uses
        /// <see cref="Enumerable.Join{TOuter, TInner, TKey, TResult}(IEnumerable{TOuter}, IEnumerable{TInner}, Func{TOuter, TKey}, Func{TInner, TKey}, Func{TOuter, TInner, TResult}, IEqualityComparer{TKey})" />
        /// method that performs inner join of two sequences.
        /// </remarks>
        /// <typeparam name="TSource">
        /// The type of the elements of <paramref name="source" />.
        /// </typeparam>
        /// <typeparam name="TOrder">
        /// The type of the elements of <paramref name="order" />.
        /// </typeparam>
        /// <typeparam name="TKey">
        /// The type of the keys returned by the key selector functions.
        /// </typeparam>
        /// <typeparam name="TResult">The type of the result elements.</typeparam>
        /// <param name="source">A sequence of values to order.</param>
        /// <param name="order">A sequence of ordinal values.</param>
        /// <param name="sourceKeySelector">
        /// A function to extract a key from each element of <paramref name="source" />.
        /// </param>
        /// <param name="orderKeySelector">
        /// A function to extract a key from each element of <paramref name="order" />.
        /// </param>
        /// <param name="resultSelector">
        /// A function to create a result element from two matching elements.
        /// </param>
        /// <param name="comparer">
        /// The <see cref="IEqualityComparer{TKey}" /> implementation to use when
        /// comparing keys, or <see langword="null" /> to use the default
        /// <see cref="EqualityComparer{T}" /> implementation for the <typeparamref name="TKey" />
        /// type.
        /// </param>
        /// <returns>An <see cref="IEnumerable{TResult}" /> that has elements of type
        /// <typeparamref name="TResult" /> that are obtained by performing an inner join on two
        /// sequences.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source" /> is <see langword="null" />. -or-
        /// <paramref name="order" /> is <see langword="null" />. -or-
        /// <paramref name="sourceKeySelector" /> is <see langword="null" />. -or-
        /// <paramref name="orderKeySelector" /> is <see langword="null" />. -or-
        /// <paramref name="resultSelector" /> is <see langword="null" />.
        /// </exception>
        public static IEnumerable<TResult> OrderBySequence<TSource, TOrder, TKey, TResult>(
            this IEnumerable<TSource> source, IEnumerable<TOrder> order,
            Func<TSource, TKey> sourceKeySelector, Func<TOrder, TKey> orderKeySelector,
            Func<TSource, TOrder, TResult> resultSelector, IEqualityComparer<TKey>? comparer)
        {
            // Null checks for "source", "order", "sourceKeySelector", "orderKeySelector",
            // parameters are provided by "Enumerable.Join" method.
            resultSelector.ThrowIfNull(nameof(resultSelector));

            // Join method keeps order of the first collection, e.g. "order" in our case.
            return order.Join(
                source,
                orderKeySelector,
                sourceKeySelector,
                (orderItem, sourceItem) => resultSelector(sourceItem, orderItem),
                comparer
            );
        }

        #endregion
    }
}
