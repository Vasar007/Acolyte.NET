using System;
using System.Collections.Generic;
using System.Linq;
using Acolyte.Assertions;

namespace Acolyte.Linq
{
    public static partial class EnumerableExtensions
    {
        /// <summary>
        /// Applies a specified function to the corresponding elements of two sequences,
        /// producing a sequence of the results coupled into value tuple.
        /// </summary>
        /// <typeparam name="TFirst">
        /// The type of the elements of the <paramref name="first" /> input sequence.
        /// </typeparam>
        /// <typeparam name="TSecond">
        /// The type of the elements of the <paramref name="second" /> input sequence.
        /// </typeparam>
        /// <param name="first">The first sequence to merge.</param>
        /// <param name="second">The second sequence to merge.</param>
        /// <returns>
        /// An <see cref="IEnumerable{T}" /> that contains merged elements of two input sequences.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="first" /> is <see langword="null" />. -or-
        /// <paramref name="second" /> is <see langword="null" />.
        /// </exception>
        public static IEnumerable<(TFirst, TSecond)> ZipTwo<TFirst, TSecond>(
            this IEnumerable<TFirst> first, IEnumerable<TSecond> second)
        {
            // Null checks for "first", "second" parameters is provided by "Enumerable.Zip" method.

            Func<TFirst, TSecond, (TFirst, TSecond)> resultSelector =
                (sourceItem, secondItem) => (sourceItem, secondItem);

            return first.Zip(
                second: second,
                resultSelector: resultSelector
            );
        }

        /// <summary>
        /// Applies a specified function to the corresponding elements of three sequences,
        /// producing a sequence of the results.
        /// </summary>
        /// <typeparam name="TFirst">
        /// The type of the elements of the <paramref name="first" /> input sequence.
        /// </typeparam>
        /// <typeparam name="TSecond">
        /// The type of the elements of the <paramref name="second" /> input sequence.
        /// </typeparam>
        /// <typeparam name="TThird">
        /// The type of the elements of the <paramref name="third" /> input sequence.
        /// </typeparam>
        /// <typeparam name="TResult">The type of the elements of the result sequence.</typeparam>
        /// <param name="first">The first sequence to merge.</param>
        /// <param name="second">The second sequence to merge.</param>
        /// <param name="third">The third sequence to merge.</param>
        /// <param name="resultSelector">
        /// A function that specifies how to merge the elements from the three sequences.
        /// </param>
        /// <returns>
        /// An <see cref="IEnumerable{T}" /> that contains merged elements of three input sequences.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="first" /> is <see langword="null" />. -or-
        /// <paramref name="second" /> is <see langword="null" />. -or-
        /// <paramref name="third" /> is <see langword="null" />. -or-
        /// <paramref name="resultSelector" /> is <see langword="null" />.
        /// </exception>
        public static IEnumerable<TResult> ZipThree<TFirst, TSecond, TThird, TResult>(
            this IEnumerable<TFirst> first, IEnumerable<TSecond> second, IEnumerable<TThird> third,
            Func<TFirst, TSecond, TThird, TResult> resultSelector)
        {
            first.ThrowIfNull(nameof(first));
            second.ThrowIfNull(nameof(second));
            third.ThrowIfNull(nameof(third));
            resultSelector.ThrowIfNull(nameof(resultSelector));

            using var e1 = first.GetEnumerator();
            using var e2 = second.GetEnumerator();
            using var e3 = third.GetEnumerator();

            while (e1.MoveNext() && e2.MoveNext() && e3.MoveNext())
            {
                yield return resultSelector(e1.Current, e2.Current, e3.Current);
            }
        }

        /// <summary>
        /// Applies a specified function to the corresponding elements of three sequences,
        /// producing a sequence of the results coupled into value tuple.
        /// </summary>
        /// <typeparam name="TFirst">
        /// The type of the elements of the <paramref name="first" /> input sequence.
        /// </typeparam>
        /// <typeparam name="TSecond">
        /// The type of the elements of the <paramref name="second" /> input sequence.
        /// </typeparam>
        /// <typeparam name="TThird">
        /// The type of the elements of the <paramref name="third" /> input sequence.
        /// </typeparam>
        /// <param name="first">The first sequence to merge.</param>
        /// <param name="second">The second sequence to merge.</param>
        /// <param name="third">The third sequence to merge.</param>
        /// <returns>
        /// An <see cref="IEnumerable{T}" /> that contains merged elements of three input sequences.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="first" /> is <see langword="null" />. -or-
        /// <paramref name="second" /> is <see langword="null" />. -or-
        /// <paramref name="third" /> is <see langword="null" />.
        /// </exception>
        public static IEnumerable<(TFirst, TSecond, TThird)> ZipThree<TFirst, TSecond, TThird>(
            this IEnumerable<TFirst> first, IEnumerable<TSecond> second, IEnumerable<TThird> third)
        {
            Func<TFirst, TSecond, TThird, (TFirst, TSecond, TThird)> resultSelector =
                (sourceItem, secondItem, thirdItem) => (sourceItem, secondItem, thirdItem);

            return first.ZipThree(
                second: second,
                third: third,
                resultSelector: resultSelector
            );
        }
    }
}
