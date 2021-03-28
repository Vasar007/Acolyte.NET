using System;
using System.Collections.Generic;
using System.Linq;
using Acolyte.Assertions;
using Acolyte.Common;

namespace Acolyte.Linq
{
    public static partial class EnumerableExtensions
    {
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
        /// <paramref name="source" /> is <see langword="null" />.
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
        /// <paramref name="source" /> is <see langword="null" />.
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
        /// <paramref name="source" /> is <see langword="null" />.
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
        /// <paramref name="source" /> is <see langword="null" />.
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
        /// <paramref name="source" /> is <see langword="null" />.
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
        /// <paramref name="source" /> is <see langword="null" />.
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
        /// <paramref name="source" /> is <see langword="null" />.
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
        /// <paramref name="source" /> is <see langword="null" />.
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
        /// <paramref name="source" /> is <see langword="null" />.
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
        /// <paramref name="source" /> is <see langword="null" />.
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
        /// Returns the minimum and maximum values in a generic sequence with provided comparer.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements of <paramref name="source" />.
        /// </typeparam>
        /// <param name="source">
        /// A sequence of values to determine the minimum and maximum values of.
        /// </param>
        /// <param name="comparer">
        /// The <see cref="IComparer{TSource}" /> implementation to use when
        /// comparing values, or <see langword="null" /> to use the default
        /// <see cref="Comparer{TSource}" /> implementation for the
        /// <typeparamref name="TSource" /> type.
        /// </param>
        /// <returns>
        /// The minimum and maximum values in the sequence.
        /// If <paramref name="source" /> contains no elements and <typeparamref name="TSource" />
        /// is reference type, <see langword="null" /> values will be return.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="source" /> contains no elements and <typeparamref name="TSource" /> is
        /// value type.
        /// </exception>
        public static (TSource? minValue, TSource? maxValue) MinMax<TSource>(
            this IEnumerable<TSource> source,
            IComparer<TSource>? comparer)
        {
            source.ThrowIfNull(nameof(source));
            comparer ??= Comparer<TSource>.Default;

            TSource? minValue = default;
            TSource? maxValue = default;

            // Second part of check is redundant but otherwise compiler shows warning about
            // null reference assignment of maxValue variable in the else block.
            if (minValue is null || maxValue is null)
            {
                foreach (TSource x in source)
                {
                    if (x is not null && (minValue is null || comparer.Compare(x, minValue) < 0)) minValue = x;
                    if (x is not null && (maxValue is null || comparer.Compare(x, maxValue) > 0)) maxValue = x;
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
        /// <returns>
        /// The minimum and maximum values in the sequence.
        /// If <paramref name="source" /> contains no elements and <typeparamref name="TSource" />
        /// is reference type, <see langword="null" /> values will be return.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="source" /> contains no elements and <typeparamref name="TSource" /> is
        /// value type.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// No object in <paramref name="source" /> implements the <see cref="IComparable" /> or
        /// <see cref="IComparable{TSource}" /> interface.
        /// </exception>
        public static (TSource? minValue, TSource? maxValue) MinMax<TSource>(
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
        /// <paramref name="source" /> is <see langword="null" />.
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
        /// <paramref name="source" /> is <see langword="null" />.
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
        /// <paramref name="source" /> is <see langword="null" />.
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
        /// <paramref name="source" /> is <see langword="null" />.
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
        /// <paramref name="source" /> is <see langword="null" />.
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
        /// <paramref name="source" /> is <see langword="null" />.
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
        /// <paramref name="source" /> is <see langword="null" />.
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
        /// <paramref name="source" /> is <see langword="null" />.
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
        /// <paramref name="source" /> is <see langword="null" />.
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
        /// <paramref name="source" /> is <see langword="null" />.
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
        /// <param name="comparer">
        /// The <see cref="IComparer{TResult}" /> implementation to use when
        /// comparing values, or <see langword="null" /> to use the default
        /// <see cref="Comparer{TResult}" /> implementation for the
        /// <typeparamref name="TResult" /> type.
        /// </param>
        /// <returns>
        /// The minimum and maximum values in the sequence.
        /// If <paramref name="source" /> contains no elements and <typeparamref name="TSource" />
        /// is reference type, <see langword="null" /> values will be return.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="source" /> contains no elements and <typeparamref name="TResult" />  is
        /// value type.
        /// </exception>
        public static (TResult? minValue, TResult? maxValue) MinMax<TSource, TResult>(
            this IEnumerable<TSource> source,
            Func<TSource, TResult> selector,
            IComparer<TResult>? comparer)
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
        /// <returns>
        /// The minimum and maximum values in the sequence.
        /// If <paramref name="source" /> contains no elements and <typeparamref name="TSource" />
        /// is reference type, <see langword="null" /> values will be return.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="source" /> contains no elements and <typeparamref name="TResult" /> is
        /// value type.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// No object in <paramref name="source" /> implements the <see cref="IComparable" /> or
        /// <see cref="IComparable{TResult}" /> interface.
        /// </exception>
        public static (TResult? minValue, TResult? maxValue) MinMax<TSource, TResult>(
            this IEnumerable<TSource> source,
            Func<TSource, TResult> selector)
        {
            return source.MinMax(selector, Comparer<TResult>.Default);
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
        /// <param name="comparer">
        /// The <see cref="IComparer{TKey}" /> implementation to use when
        /// comparing keys, or <see langword="null" /> to use the default
        /// <see cref="Comparer{TKey}" /> implementation for the
        /// <typeparamref name="TKey" /> type.
        /// </param>
        /// <returns>
        /// The minimum and maximum by key element in sequence in the sequence.
        /// If <paramref name="source" /> contains no elements and <typeparamref name="TSource" />
        /// is reference type, <see langword="null" /> values will be return.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source" /> is <see langword="null" />. -or-
        /// <paramref name="keySelector" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="source" /> contains no elements and <typeparamref name="TSource" /> is
        /// value type.
        /// </exception>
        public static (TSource? minValue, TSource? maxValue) MinMaxBy<TSource, TKey>(
            this IEnumerable<TSource> source, Func<TSource, TKey> keySelector,
            IComparer<TKey>? comparer)
        {
            source.ThrowIfNull(nameof(source));
            keySelector.ThrowIfNull(nameof(keySelector));
            comparer ??= Comparer<TKey>.Default;

            TSource? minValue = default;
            TSource? maxValue = default;

            using (IEnumerator<TSource> enumerator = source.GetEnumerator())
            {
                if (!enumerator.MoveNext())
                {
                    if (minValue is null) return (minValue, maxValue);

                    throw Error.NoElements();
                }

                minValue = maxValue = enumerator.Current;
                TKey minElementKey = keySelector(minValue);
                TKey maxElementKey = minElementKey;

                while (enumerator.MoveNext())
                {
                    TSource element = enumerator.Current;
                    TKey elementKey = keySelector(element);

                    if (comparer.Compare(elementKey, minElementKey) < 0)
                    {
                        minValue = element;
                        minElementKey = elementKey;
                    }
                    if (comparer.Compare(elementKey, maxElementKey) > 0)
                    {
                        maxValue = element;
                        maxElementKey = elementKey;
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
        /// <returns>
        /// The minimum and maximum by key element in the sequence.
        /// If <paramref name="source" /> contains no elements and <typeparamref name="TSource" />
        /// is reference type, <see langword="null" /> values will be return.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source" /> is <see langword="null" />. -or-
        /// <paramref name="keySelector" /> is <see langword="null" />. -or-
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="source" /> contains no elements and <typeparamref name="TSource" /> is
        /// value type.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// No object in <paramref name="source" /> implements the <see cref="IComparable" /> or
        /// <see cref="IComparable{TKey}" /> interface.
        /// </exception>
        public static (TSource? minValue, TSource? maxValue) MinMaxBy<TSource, TKey>(
            this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            return source.MinMaxBy(keySelector, Comparer<TKey>.Default);
        }

        #endregion
    }
}
