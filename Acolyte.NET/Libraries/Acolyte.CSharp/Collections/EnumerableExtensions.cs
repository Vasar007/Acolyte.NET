using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Acolyte.Common;

namespace Acolyte.Collections
{
    /// <inheritdoc cref="Linq.EnumerableExtensions" />
    [Obsolete("Use \"Acolyte.Linq.EnumerableExtensions\" instead. This class will be remove in next major version.")]
    public static class EnumerableExtensions
    {
        #region Is Null Or Empty

        /// <inheritdoc cref="Linq.EnumerableExtensions.IsNullOrEmpty{TSource}(IEnumerable{TSource}?)" />
        [Obsolete("Use \"Acolyte.Linq.EnumerableExtensions.IsNullOrEmpty\" instead. This method will be removed in next major version.")]
        public static bool IsNullOrEmpty<TSource>(this IEnumerable<TSource>? source)
        {
            return Linq.EnumerableExtensions.IsNullOrEmpty(source);
        }

        #endregion

        #region First Or Default

        /// <inheritdoc cref="Linq.EnumerableExtensions.FirstOrDefault{TSource}(IEnumerable{TSource}, TSource)" />
        [Obsolete("Use \"Acolyte.Linq.EnumerableExtensions.FirstOrDefault\" instead. This method will be removed in next major version.")]
        public static TSource FirstOrDefault<TSource>(this IEnumerable<TSource> source,
            TSource defaultValue)
        {
            return Linq.EnumerableExtensions.FirstOrDefault(source, defaultValue);
        }

        /// <inheritdoc cref="Linq.EnumerableExtensions.FirstOrDefault{TSource}(IEnumerable{TSource}, Func{TSource, bool}, TSource)" />
        [Obsolete("Use \"Acolyte.Linq.EnumerableExtensions.FirstOrDefault\" instead. This method will be removed in next major version.")]
        public static TSource FirstOrDefault<TSource>(this IEnumerable<TSource> source,
            Func<TSource, bool> predicate, TSource defaultValue)
        {
            return Linq.EnumerableExtensions.FirstOrDefault(source, predicate, defaultValue);
        }

        #endregion

        #region Last Or Default

        /// <inheritdoc cref="Linq.EnumerableExtensions.LastOrDefault{TSource}(IEnumerable{TSource}, TSource)" />
        [Obsolete("Use \"Acolyte.Linq.EnumerableExtensions.LastOrDefault\" instead. This method will be removed in next major version.")]
        public static TSource LastOrDefault<TSource>(this IEnumerable<TSource> source,
            TSource defaultValue)
        {
            return Linq.EnumerableExtensions.LastOrDefault(source, defaultValue);
        }

        /// <inheritdoc cref="Linq.EnumerableExtensions.LastOrDefault{TSource}(IEnumerable{TSource}, TSource)" />
        [Obsolete("Use \"Acolyte.Linq.EnumerableExtensions.LastOrDefault\" instead. This method will be removed in next major version.")]
        public static TSource LastOrDefault<TSource>(this IEnumerable<TSource> source,
            Func<TSource, bool> predicate, TSource defaultValue)
        {
            return Linq.EnumerableExtensions.LastOrDefault(source, predicate, defaultValue);
        }

        #endregion

        #region Single Or Default

        /// <inheritdoc cref="Linq.EnumerableExtensions.SingleOrDefault{TSource}(IEnumerable{TSource}, TSource)" />
        [Obsolete("Use \"Acolyte.Linq.EnumerableExtensions.SingleOrDefault\" instead. This method will be removed in next major version.")]
        public static TSource SingleOrDefault<TSource>(this IEnumerable<TSource> source,
            TSource defaultValue)
        {
            return Linq.EnumerableExtensions.SingleOrDefault(source, defaultValue);
        }

        /// <inheritdoc cref="Linq.EnumerableExtensions.SingleOrDefault{TSource}(IEnumerable{TSource}, Func{TSource, bool}, TSource)" />
        [Obsolete("Use \"Acolyte.Linq.EnumerableExtensions.SingleOrDefault\" instead. This method will be removed in next major version.")]
        public static TSource SingleOrDefault<TSource>(this IEnumerable<TSource> source,
            Func<TSource, bool> predicate, TSource defaultValue)
        {
            return Linq.EnumerableExtensions.SingleOrDefault(source, predicate, defaultValue);
        }

        #endregion

        #region Index Of

        /// <inheritdoc cref="Linq.EnumerableExtensions.IndexOf{TSource}(IEnumerable{TSource}, Func{TSource, bool})" />
        [Obsolete("Use \"Acolyte.Linq.EnumerableExtensions.IndexOf\" instead. This method will be removed in next major version.")]
        public static int IndexOf<TSource>(this IEnumerable<TSource> source,
            Func<TSource, bool> predicate)
        {
            return Linq.EnumerableExtensions.IndexOf(source, predicate);
        }

        /// <inheritdoc cref="Linq.EnumerableExtensions.IndexOf{TSource}(IEnumerable{TSource}, TSource, IEqualityComparer{TSource}?)" />
        [Obsolete("Use \"Acolyte.Linq.EnumerableExtensions.IndexOf\" instead. This method will be removed in next major version.")]
        public static int IndexOf<TSource>(this IEnumerable<TSource> source, TSource value,
            IEqualityComparer<TSource>? comparer)
        {
            return Linq.EnumerableExtensions.IndexOf(source, value, comparer);
        }

        /// <inheritdoc cref="Linq.EnumerableExtensions.IndexOf{TSource}(IEnumerable{TSource}, TSource)" />
        [Obsolete("Use \"Acolyte.Linq.EnumerableExtensions.IndexOf\" instead. This method will be removed in next major version.")]
        public static int IndexOf<TSource>(this IEnumerable<TSource> source, TSource value)
        {
            return Linq.EnumerableExtensions.IndexOf(source, value);
        }

        #endregion

        #region To Read-Only Collections

        #region To Read-Only Dictionary

        /// <inheritdoc cref="Linq.EnumerableExtensions.ToReadOnlyDictionary{TKey, TSource}(IEnumerable{TSource}, Func{TSource, TKey})" />
        [Obsolete("Use \"Acolyte.Linq.EnumerableExtensions.ToReadOnlyDictionary\" instead. This method will be removed in next major version.")]
        public static IReadOnlyDictionary<TKey, TSource>
            ToReadOnlyDictionary<TKey, TSource>(
                this IEnumerable<TSource> source,
                Func<TSource, TKey> keySelector)
            where TKey: notnull
        {
            return Linq.EnumerableExtensions.ToReadOnlyDictionary(source, keySelector);
        }

        /// <inheritdoc cref="Linq.EnumerableExtensions.ToReadOnlyDictionary{TKey, TSource}(IEnumerable{TSource}, Func{TSource, TKey}, IEqualityComparer{TKey}?)" />
        [Obsolete("Use \"Acolyte.Linq.EnumerableExtensions.ToReadOnlyDictionary\" instead. This method will be removed in next major version.")]
        public static IReadOnlyDictionary<TKey, TSource>
            ToReadOnlyDictionary<TKey, TSource>(
                this IEnumerable<TSource> source,
                Func<TSource, TKey> keySelector,
                IEqualityComparer<TKey>? comparer)
            where TKey : notnull
        {
            return Linq.EnumerableExtensions.ToReadOnlyDictionary(source, keySelector, comparer);
        }

        /// <inheritdoc cref="Linq.EnumerableExtensions.ToReadOnlyDictionary{TSource, TKey, TElement}(IEnumerable{TSource}, Func{TSource, TKey}, Func{TSource, TElement})" />
        [Obsolete("Use \"Acolyte.Linq.EnumerableExtensions.ToReadOnlyDictionary\" instead. This method will be removed in next major version.")]
        public static IReadOnlyDictionary<TKey, TElement>
            ToReadOnlyDictionary<TSource, TKey, TElement>(
                this IEnumerable<TSource> source,
                Func<TSource, TKey> keySelector,
                Func<TSource, TElement> elementSelector)
            where TKey : notnull
        {
            return Linq.EnumerableExtensions.ToReadOnlyDictionary(source, keySelector, elementSelector);
        }

        /// <inheritdoc cref="Linq.EnumerableExtensions.ToReadOnlyDictionary{TSource, TKey, TElement}(IEnumerable{TSource}, Func{TSource, TKey}, Func{TSource, TElement}, IEqualityComparer{TKey}?)" />
        [Obsolete("Use \"Acolyte.Linq.EnumerableExtensions.ToReadOnlyDictionary\" instead. This method will be removed in next major version.")]
        public static IReadOnlyDictionary<TKey, TElement>
            ToReadOnlyDictionary<TSource, TKey, TElement>(
                this IEnumerable<TSource> source,
                Func<TSource, TKey> keySelector,
                Func<TSource, TElement> elementSelector,
                IEqualityComparer<TKey>? comparer)
            where TKey : notnull
        {
            return Linq.EnumerableExtensions.ToReadOnlyDictionary(source, keySelector, elementSelector, comparer);
        }

        #endregion

        #region To Read-Only List

        /// <inheritdoc cref="Linq.EnumerableExtensions.ToReadOnlyList{TSource}(IEnumerable{TSource})" />
        [Obsolete("Use \"Acolyte.Linq.EnumerableExtensions.ToReadOnlyList\" instead. This method will be removed in next major version.")]
        public static IReadOnlyList<TSource> ToReadOnlyList<TSource>(
            this IEnumerable<TSource> source)
        {
            return Linq.EnumerableExtensions.ToReadOnlyList(source);
        }

        #endregion

        #region To Read-Only Collection

        /// <inheritdoc cref="Linq.EnumerableExtensions.ToReadOnlyCollection{TSource}(IEnumerable{TSource})" />
        [Obsolete("Use \"Acolyte.Linq.EnumerableExtensions.ToReadOnlyCollection\" instead. This method will be removed in next major version.")]
        public static IReadOnlyCollection<TSource> ToReadOnlyCollection<TSource>(
            this IEnumerable<TSource> source)
        {
            return Linq.EnumerableExtensions.ToReadOnlyCollection(source);
        }

        #endregion

        #endregion

        #region Min/Max For Generic Types With Comparer

        /// <inheritdoc cref="Linq.EnumerableExtensions.Min{TSource}(IEnumerable{TSource}, IComparer{TSource}?)" />
        [Obsolete("Use \"Acolyte.Linq.EnumerableExtensions.Min\" instead. This method will be removed in next major version.")]
        public static TSource? Min<TSource>(
            this IEnumerable<TSource> source,
            IComparer<TSource>? comparer)
        {
            return Linq.EnumerableExtensions.Min(source, comparer);
        }

        /// <inheritdoc cref="Linq.EnumerableExtensions.Max{TSource}(IEnumerable{TSource}, IComparer{TSource}?)" />
        [Obsolete("Use \"Acolyte.Linq.EnumerableExtensions.Max\" instead. This method will be removed in next major version.")]
        public static TSource? Max<TSource>(
            this IEnumerable<TSource> source,
            IComparer<TSource>? comparer)
        {
            return Linq.EnumerableExtensions.Max(source, comparer);
        }

        #endregion

        #region MinMax

        #region MinMax Overloads Without Selector

        #region MinMax For Int32

        /// <inheritdoc cref="Linq.EnumerableExtensions.MinMax(IEnumerable{int})" />
        [Obsolete("Use \"Acolyte.Linq.EnumerableExtensions.MinMax\" instead. This method will be removed in next major version.")]
        public static (int minValue, int maxValue) MinMax(this IEnumerable<int> source)
        {
            return Linq.EnumerableExtensions.MinMax(source);
        }

        /// <inheritdoc cref="Linq.EnumerableExtensions.MinMax(IEnumerable{int?})" />
        [Obsolete("Use \"Acolyte.Linq.EnumerableExtensions.IsNullOrEmpty\" instead. This method will be removed in next major version.")]
        public static (int? minValue, int? maxValue) MinMax(this IEnumerable<int?> source)
        {
            return Linq.EnumerableExtensions.MinMax(source);
        }

        #endregion

        #region MinMax For Int64

        /// <inheritdoc cref="Linq.EnumerableExtensions.MinMax(IEnumerable{long})" />
        [Obsolete("Use \"Acolyte.Linq.EnumerableExtensions.MinMax\" instead. This method will be removed in next major version.")]
        public static (long minValue, long maxValue) MinMax(this IEnumerable<long> source)
        {
            return Linq.EnumerableExtensions.MinMax(source);
        }

        /// <inheritdoc cref="Linq.EnumerableExtensions.MinMax(IEnumerable{long?})" />
        [Obsolete("Use \"Acolyte.Linq.EnumerableExtensions.MinMax\" instead. This method will be removed in next major version.")]
        public static (long? minValue, long? maxValue) MinMax(this IEnumerable<long?> source)
        {
            return Linq.EnumerableExtensions.MinMax(source);
        }

        #endregion

        #region MinMax For Single

        /// <inheritdoc cref="Linq.EnumerableExtensions.MinMax(IEnumerable{float})" />
        [Obsolete("Use \"Acolyte.Linq.EnumerableExtensions.MinMax\" instead. This method will be removed in next major version.")]
        public static (float minValue, float maxValue) MinMax(this IEnumerable<float> source)
        {
            return Linq.EnumerableExtensions.MinMax(source);
        }

        /// <inheritdoc cref="Linq.EnumerableExtensions.MinMax(IEnumerable{float?})" />
        [Obsolete("Use \"Acolyte.Linq.EnumerableExtensions.MinMax\" instead. This method will be removed in next major version.")]
        public static (float? minValue, float? maxValue) MinMax(this IEnumerable<float?> source)
        {
            return Linq.EnumerableExtensions.MinMax(source);
        }

        #endregion

        #region MinMax For Double

        /// <inheritdoc cref="Linq.EnumerableExtensions.MinMax(IEnumerable{double})" />
        [Obsolete("Use \"Acolyte.Linq.EnumerableExtensions.MinMax\" instead. This method will be removed in next major version.")]
        public static (double minValue, double maxValue) MinMax(this IEnumerable<double> source)
        {
            return Linq.EnumerableExtensions.MinMax(source);
        }

        /// <inheritdoc cref="Linq.EnumerableExtensions.MinMax(IEnumerable{double?})" />
        [Obsolete("Use \"Acolyte.Linq.EnumerableExtensions.MinMax\" instead. This method will be removed in next major version.")]
        public static (double? minValue, double? maxValue) MinMax(this IEnumerable<double?> source)
        {
            return Linq.EnumerableExtensions.MinMax(source);
        }

        #endregion

        #region MinMax For Decimal

        /// <inheritdoc cref="Linq.EnumerableExtensions.MinMax(IEnumerable{decimal})" />
        [Obsolete("Use \"Acolyte.Linq.EnumerableExtensions.MinMax\" instead. This method will be removed in next major version.")]
        public static (decimal minValue, decimal maxValue) MinMax(this IEnumerable<decimal> source)
        {
            return Linq.EnumerableExtensions.MinMax(source);
        }

        /// <inheritdoc cref="Linq.EnumerableExtensions.MinMax(IEnumerable{decimal?})" />
        [Obsolete("Use \"Acolyte.Linq.EnumerableExtensions.MinMax\" instead. This method will be removed in next major version.")]
        public static (decimal? minValue, decimal? maxValue) MinMax(
            this IEnumerable<decimal?> source)
        {
            return Linq.EnumerableExtensions.MinMax(source);
        }

        #endregion

        #region MinMax For Generic Types

        /// <inheritdoc cref="Linq.EnumerableExtensions.MinMax{TSource}(IEnumerable{TSource}, IComparer{TSource}?)" />
        [Obsolete("Use \"Acolyte.Linq.EnumerableExtensions.MinMax\" instead. This method will be removed in next major version.")]
        public static (TSource? minValue, TSource? maxValue) MinMax<TSource>(
            this IEnumerable<TSource> source,
            IComparer<TSource>? comparer)
        {
            return Linq.EnumerableExtensions.MinMax(source, comparer);
        }

        /// <inheritdoc cref="Linq.EnumerableExtensions.MinMax{TSource}(IEnumerable{TSource})" />
        [Obsolete("Use \"Acolyte.Linq.EnumerableExtensions.MinMax\" instead. This method will be removed in next major version.")]
        public static (TSource? minValue, TSource? maxValue) MinMax<TSource>(
            this IEnumerable<TSource> source)
        {
            return Linq.EnumerableExtensions.MinMax(source);
        }

        #endregion

        #endregion

        #region MinMax Overloads With Selector

        #region MinMax For Int32

        /// <inheritdoc cref="Linq.EnumerableExtensions.MinMax(IEnumerable{int})" />
        [Obsolete("Use \"Acolyte.Linq.EnumerableExtensions.MinMax\" instead. This method will be removed in next major version.")]
        public static (int minValue, int maxValue) MinMax<TSource>(
            this IEnumerable<TSource> source,
            Func<TSource, int> selector)
        {
            return Linq.EnumerableExtensions.MinMax(source, selector);
        }

        /// <inheritdoc cref="Linq.EnumerableExtensions.MinMax(IEnumerable{int?})" />
        [Obsolete("Use \"Acolyte.Linq.EnumerableExtensions.MinMax\" instead. This method will be removed in next major version.")]
        public static (int? minValue, int? maxValue) MinMax<TSource>(
            this IEnumerable<TSource> source,
            Func<TSource, int?> selector)
        {
            return Linq.EnumerableExtensions.MinMax(source, selector);
        }

        #endregion

        #region MinMax For Int64

        /// <inheritdoc cref="Linq.EnumerableExtensions.MinMax(IEnumerable{long})" />
        [Obsolete("Use \"Acolyte.Linq.EnumerableExtensions.MinMax\" instead. This method will be removed in next major version.")]
        public static (long minValue, long maxValue) MinMax<TSource>(
            this IEnumerable<TSource> source,
            Func<TSource, long> selector)
        {
            return Linq.EnumerableExtensions.MinMax(source, selector);
        }

        /// <inheritdoc cref="Linq.EnumerableExtensions.MinMax(IEnumerable{long?})" />
        [Obsolete("Use \"Acolyte.Linq.EnumerableExtensions.MinMax\" instead. This method will be removed in next major version.")]
        public static (long? minValue, long? maxValue) MinMax<TSource>(
            this IEnumerable<TSource> source,
            Func<TSource, long?> selector)
        {
            return Linq.EnumerableExtensions.MinMax(source, selector);
        }

        #endregion

        #region MinMax For Single

        /// <inheritdoc cref="Linq.EnumerableExtensions.MinMax(IEnumerable{float})" />
        [Obsolete("Use \"Acolyte.Linq.EnumerableExtensions.MinMax\" instead. This method will be removed in next major version.")]
        public static (float minValue, float maxValue) MinMax<TSource>(
            this IEnumerable<TSource> source,
            Func<TSource, float> selector)
        {
            return Linq.EnumerableExtensions.MinMax(source, selector);
        }

        /// <inheritdoc cref="Linq.EnumerableExtensions.MinMax(IEnumerable{float?})" />
        [Obsolete("Use \"Acolyte.Linq.EnumerableExtensions.MinMax\" instead. This method will be removed in next major version.")]
        public static (float? minValue, float? maxValue) MinMax<TSource>(
            this IEnumerable<TSource> source,
            Func<TSource, float?> selector)
        {
            return Linq.EnumerableExtensions.MinMax(source, selector);
        }

        #endregion

        #region MinMax For Double

        /// <inheritdoc cref="Linq.EnumerableExtensions.MinMax(IEnumerable{double})" />
        [Obsolete("Use \"Acolyte.Linq.EnumerableExtensions.MinMax\" instead. This method will be removed in next major version.")]
        public static (double minValue, double maxValue) MinMax<TSource>(
            this IEnumerable<TSource> source,
            Func<TSource, double> selector)
        {
            return Linq.EnumerableExtensions.MinMax(source, selector);
        }

        /// <inheritdoc cref="Linq.EnumerableExtensions.MinMax(IEnumerable{double?})" />
        [Obsolete("Use \"Acolyte.Linq.EnumerableExtensions.MinMax\" instead. This method will be removed in next major version.")]
        public static (double? minValue, double? maxValue) MinMax<TSource>(
            this IEnumerable<TSource> source,
            Func<TSource, double?> selector)
        {
            return Linq.EnumerableExtensions.MinMax(source, selector);
        }

        #endregion

        #region MinMax For Decimal

        /// <inheritdoc cref="Linq.EnumerableExtensions.MinMax(IEnumerable{decimal})" />
        [Obsolete("Use \"Acolyte.Linq.EnumerableExtensions.MinMax\" instead. This method will be removed in next major version.")]
        public static (decimal minValue, decimal maxValue) MinMax<TSource>(
            this IEnumerable<TSource> source,
            Func<TSource, decimal> selector)
        {
            return Linq.EnumerableExtensions.MinMax(source, selector);
        }

        /// <inheritdoc cref="Linq.EnumerableExtensions.MinMax(IEnumerable{decimal})" />
        [Obsolete("Use \"Acolyte.Linq.EnumerableExtensions.MinMax\" instead. This method will be removed in next major version.")]
        public static (decimal? minValue, decimal? maxValue) MinMax<TSource>(
            this IEnumerable<TSource> source,
            Func<TSource, decimal?> selector)
        {
            return Linq.EnumerableExtensions.MinMax(source, selector);
        }

        #endregion

        #region MinMax For Generic Types

        /// <inheritdoc cref="Linq.EnumerableExtensions.MinMax{TSource, TResult}(IEnumerable{TSource}, Func{TSource, TResult})" />
        [Obsolete("Use \"Acolyte.Linq.EnumerableExtensions.MinMax\" instead. This method will be removed in next major version.")]
        public static (TResult? minValue, TResult? maxValue) MinMax<TSource, TResult>(
            this IEnumerable<TSource> source,
            Func<TSource, TResult> selector,
            IComparer<TResult>? comparer)
        {
            return Linq.EnumerableExtensions.MinMax(source, selector, comparer);
        }

        /// <inheritdoc cref="Linq.EnumerableExtensions.MinMax{TSource, TResult}(IEnumerable{TSource}, Func{TSource, TResult})" />
        [Obsolete("Use \"Acolyte.Linq.EnumerableExtensions.MinMax\" instead. This method will be removed in next major version.")]
        public static (TResult? minValue, TResult? maxValue) MinMax<TSource, TResult>(
            this IEnumerable<TSource> source,
            Func<TSource, TResult> selector)
        {
            return Linq.EnumerableExtensions.MinMax(source, selector);
        }

        #endregion

        #endregion

        #endregion

        #region Min/Max By Key

        #region Min By Key

        /// <inheritdoc cref="Linq.EnumerableExtensions.MinBy{TSource, TKey}(IEnumerable{TSource}, Func{TSource, TKey}, IComparer{TKey}?)" />
        [Obsolete("Use \"Acolyte.Linq.EnumerableExtensions.MinBy\" instead. This method will be removed in next major version.")]
        public static TSource? MinBy<TSource, TKey>(
            this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector,
            IComparer<TKey>? comparer)
        {
            return Linq.EnumerableExtensions.MinBy(source, keySelector, comparer);
        }

        /// <inheritdoc cref="Linq.EnumerableExtensions.MinBy{TSource, TKey}(IEnumerable{TSource}, Func{TSource, TKey})" />
        [Obsolete("Use \"Acolyte.Linq.EnumerableExtensions.MinBy\" instead. This method will be removed in next major version.")]
        public static TSource? MinBy<TSource, TKey>(this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector)
        {
            return Linq.EnumerableExtensions.MinBy(source, keySelector);
        }

        #endregion

        #region Max By Key

        /// <inheritdoc cref="Linq.EnumerableExtensions.MaxBy{TSource, TKey}(IEnumerable{TSource}, Func{TSource, TKey}, IComparer{TKey}?)" />
        [Obsolete("Use \"Acolyte.Linq.EnumerableExtensions.MaxBy\" instead. This method will be removed in next major version.")]
        public static TSource? MaxBy<TSource, TKey>(this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector, IComparer<TKey>? comparer)
        {
            return Linq.EnumerableExtensions.MaxBy(source, keySelector, comparer);
        }

        /// <inheritdoc cref="Linq.EnumerableExtensions.MaxBy{TSource, TKey}(IEnumerable{TSource}, Func{TSource, TKey})" />
        [Obsolete("Use \"Acolyte.Linq.EnumerableExtensions.MaxBy\" instead. This method will be removed in next major version.")]
        public static TSource? MaxBy<TSource, TKey>(this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector)
        {
            return Linq.EnumerableExtensions.MaxBy(source, keySelector);
        }

        #endregion

        #endregion

        #region MinMax By Key

        /// <inheritdoc cref="Linq.EnumerableExtensions.IsNullOrEmpty{TSource}(IEnumerable{TSource}?)" />
        [Obsolete("Use \"Acolyte.Linq.EnumerableExtensions.IsNullOrEmpty\" instead. This method will be removed in next major version.")]
        public static (TSource? minValue, TSource? maxValue) MinMaxBy<TSource, TKey>(
            this IEnumerable<TSource> source, Func<TSource, TKey> keySelector,
            IComparer<TKey>? comparer)
        {
            return Linq.EnumerableExtensions.MinMaxBy(source, keySelector, comparer);
        }

        /// <inheritdoc cref="Linq.EnumerableExtensions.IsNullOrEmpty{TSource}(IEnumerable{TSource}?)" />
        [Obsolete("Use \"Acolyte.Linq.EnumerableExtensions.IsNullOrEmpty\" instead. This method will be removed in next major version.")]
        public static (TSource? minValue, TSource? maxValue) MinMaxBy<TSource, TKey>(
            this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            return Linq.EnumerableExtensions.MinMaxBy(source, keySelector);
        }

        #endregion

        #region To Single String

        /// <inheritdoc cref="Linq.EnumerableExtensions.ToSingleString{TSource}(IEnumerable{TSource}?)" />
        [Obsolete("Use \"Acolyte.Linq.EnumerableExtensions.ToSingleString\" instead. This method will be removed in next major version.")]
        public static string ToSingleString<TSource>(this IEnumerable<TSource>? source)
        {
            return Linq.EnumerableExtensions.ToSingleString(source);
        }

        /// <inheritdoc cref="Linq.EnumerableExtensions.ToSingleString{TSource}(IEnumerable{TSource}?, string?)" />
        [Obsolete("Use \"Acolyte.Linq.EnumerableExtensions.ToSingleString\" instead. This method will be removed in next major version.")]
        public static string ToSingleString<TSource>(this IEnumerable<TSource>? source,
            string? emptyCollectionMessage)
        {
            return Linq.EnumerableExtensions.ToSingleString(source, emptyCollectionMessage);
        }

        /// <inheritdoc cref="Linq.EnumerableExtensions.ToSingleString{TSource}(IEnumerable{TSource}?, string?, string?)" />
        [Obsolete("Use \"Acolyte.Linq.EnumerableExtensions.ToSingleString\" instead. This method will be removed in next major version.")]
        public static string ToSingleString<TSource>(this IEnumerable<TSource>? source,
            string? emptyCollectionMessage, string? separator)
        {
            return Linq.EnumerableExtensions.ToSingleString(
                source, emptyCollectionMessage, separator
            );
        }

        /// <inheritdoc cref="Linq.EnumerableExtensions.ToSingleString{TSource}(IEnumerable{TSource}?, Func{TSource, string})" />
        [Obsolete("Use \"Acolyte.Linq.EnumerableExtensions.ToSingleString\" instead. This method will be removed in next major version.")]
        public static string ToSingleString<TSource>(this IEnumerable<TSource>? source,
            Func<TSource, string> selector)
        {
            return Linq.EnumerableExtensions.ToSingleString(source, selector);
        }

        /// <inheritdoc cref="Linq.EnumerableExtensions.ToSingleString{TSource}(IEnumerable{TSource}?, string?, Func{TSource, string})" />
        [Obsolete("Use \"Acolyte.Linq.EnumerableExtensions.ToSingleString\" instead. This method will be removed in next major version.")]
        public static string ToSingleString<TSource>(this IEnumerable<TSource>? source,
            string? emptyCollectionMessage, Func<TSource, string> selector)
        {
            return Linq.EnumerableExtensions.ToSingleString(
                source, emptyCollectionMessage, selector
             );
        }

        /// <inheritdoc cref="Linq.EnumerableExtensions.ToSingleString{TSource}(IEnumerable{TSource}?, string?, string?, Func{TSource, string})" />
        [Obsolete("Use \"Acolyte.Linq.EnumerableExtensions.ToSingleString\" instead. This method will be removed in next major version.")]
        public static string ToSingleString<TSource>(this IEnumerable<TSource>? source,
            string? emptyCollectionMessage, string? separator, Func<TSource, string> selector)
        {
            return Linq.EnumerableExtensions.ToSingleString(
                source, emptyCollectionMessage, separator, selector
            );
        }

        #endregion

        #region For Each

        /// <inheritdoc cref="Linq.EnumerableExtensions.ForEach{TSource}(IEnumerable{TSource}, Action{TSource})" />
        [Obsolete("Use \"Acolyte.Linq.EnumerableExtensions.ForEach\" instead. This method will be removed in next major version.")]
        public static void ForEach<TSource>(this IEnumerable<TSource> source,
            Action<TSource> action)
        {
            Linq.EnumerableExtensions.ForEach(source, action);
        }

        /// <inheritdoc cref="Linq.EnumerableExtensions.ForEachAsync{TSource}(IEnumerable{TSource}, Func{TSource, Task}, CancellationToken)" />
        [Obsolete("Use \"Acolyte.Linq.EnumerableExtensions.ForEachAsync\" instead. This method will be removed in next major version.")]
        public static Task ForEachAsync<TSource>(this IEnumerable<TSource> source,
            Func<TSource, Task> action, CancellationToken cancellationToken)
        {
            return Linq.EnumerableExtensions.ForEachAsync(source, action, cancellationToken);
        }

        /// <inheritdoc cref="Linq.EnumerableExtensions.ForEachAsync{TSource}(IEnumerable{TSource}, Func{TSource, Task})" />
        [Obsolete("Use \"Acolyte.Linq.EnumerableExtensions.ForEachAsync\" instead. This method will be removed in next major version.")]
        public static Task ForEachAsync<TSource>(this IEnumerable<TSource> source,
            Func<TSource, Task> action)
        {
            return Linq.EnumerableExtensions.ForEachAsync(source, action);
        }

        /// <inheritdoc cref="Linq.EnumerableExtensions.ForEachAsync{TSource, TResult}(IAsyncEnumerable{TSource}, Func{TSource, Task{TResult}}, CancellationToken)" />
        [Obsolete("Use \"Acolyte.Linq.EnumerableExtensions.ForEachAsync\" instead. This method will be removed in next major version.")]
        public static Task<TResult[]> ForEachAsync<TSource, TResult>(
            this IEnumerable<TSource> source, Func<TSource, Task<TResult>> function,
            CancellationToken cancellationToken)
        {
            return Linq.EnumerableExtensions.ForEachAsync(source, function, cancellationToken);
        }

        /// <inheritdoc cref="Linq.EnumerableExtensions.ForEachAsync{TSource, TResult}(IEnumerable{TSource}, Func{TSource, Task{TResult}})" />
        [Obsolete("Use \"Acolyte.Linq.EnumerableExtensions.ForEachAsync\" instead. This method will be removed in next major version.")]
        public static Task<TResult[]> ForEachAsync<TSource, TResult>(
            this IEnumerable<TSource> source, Func<TSource, Task<TResult>> function)
        {
            return Linq.EnumerableExtensions.ForEachAsync(source, function);
        }

        /// <inheritdoc cref="Linq.EnumerableExtensions.ForEachSafeAsync{TSource}(IEnumerable{TSource}, Func{TSource, Task}, CancellationToken)" />
        [Obsolete("Use \"Acolyte.Linq.EnumerableExtensions.ForEachSafeAsync\" instead. This method will be removed in next major version.")]
        public static Task<Result<NoneResult, Exception>[]> ForEachSafeAsync<TSource>(
            this IEnumerable<TSource> source, Func<TSource, Task> action,
            CancellationToken cancellationToken)
        {
            return Linq.EnumerableExtensions.ForEachSafeAsync(source, action, cancellationToken);
        }

        /// <inheritdoc cref="Linq.EnumerableExtensions.ForEachSafeAsync{TSource}(IEnumerable{TSource}, Func{TSource, Task})" />
        [Obsolete("Use \"Acolyte.Linq.EnumerableExtensions.ForEachSafeAsync\" instead. This method will be removed in next major version.")]
        public static Task<Result<NoneResult, Exception>[]> ForEachSafeAsync<TSource>(
            this IEnumerable<TSource> source, Func<TSource, Task> action)
        {
            return Linq.EnumerableExtensions.ForEachSafeAsync(source, action);
        }

        /// <inheritdoc cref="Linq.EnumerableExtensions.ForEachSafeAsync{TSource, TResult}(IEnumerable{TSource}, Func{TSource, Task{TResult}}, CancellationToken)" />
        [Obsolete("Use \"Acolyte.Linq.EnumerableExtensions.ForEachSafeAsync\" instead. This method will be removed in next major version.")]
        public static Task<Result<TResult, Exception>[]> ForEachSafeAsync<TSource, TResult>(
            this IEnumerable<TSource> source, Func<TSource, Task<TResult>> function,
            CancellationToken cancellationToken)
        {
            return Linq.EnumerableExtensions.ForEachSafeAsync(source, function, cancellationToken);
        }

        /// <inheritdoc cref="Linq.EnumerableExtensions.ForEachSafeAsync{TSource, TResult}(IEnumerable{TSource}, Func{TSource, Task{TResult}})" />
        [Obsolete("Use \"Acolyte.Linq.EnumerableExtensions.ForEachSafeAsync\" instead. This method will be removed in next major version.")]
        public static Task<Result<TResult, Exception>[]> ForEachSafeAsync<TSource, TResult>(
            this IEnumerable<TSource> source, Func<TSource, Task<TResult>> function)
        {
            return Linq.EnumerableExtensions.ForEachSafeAsync(source, function);
        }

#if NETSTANDARD2_1

        /// <inheritdoc cref="Linq.EnumerableExtensions.ForEachAsync{TSource}(IAsyncEnumerable{TSource}, Func{TSource, Task}, CancellationToken)" />
        [Obsolete("Use \"Acolyte.Linq.EnumerableExtensions.ForEachAsync\" instead. This method will be removed in next major version.")]
        public static Task ForEachAsync<TSource>(this IAsyncEnumerable<TSource> source,
            Func<TSource, Task> action, CancellationToken cancellationToken)
        {
            return Linq.EnumerableExtensions.ForEachAsync(source, action, cancellationToken);
        }

        /// <inheritdoc cref="Linq.EnumerableExtensions.ForEachAsync{TSource}(IAsyncEnumerable{TSource}, Func{TSource, Task})" />
        [Obsolete("Use \"Acolyte.Linq.EnumerableExtensions.ForEachAsync\" instead. This method will be removed in next major version.")]
        public static Task ForEachAsync<TSource>(this IAsyncEnumerable<TSource> source,
            Func<TSource, Task> action)
        {
            return Linq.EnumerableExtensions.ForEachAsync(source, action);
        }

        /// <inheritdoc cref="Linq.EnumerableExtensions.ForEachAsync{TSource, TResult}(IAsyncEnumerable{TSource}, Func{TSource, Task{TResult}}, CancellationToken)" />
        [Obsolete("Use \"Acolyte.Linq.EnumerableExtensions.ForEachAsync\" instead. This method will be removed in next major version.")]
        public static Task<TResult[]> ForEachAsync<TSource, TResult>(
            this IAsyncEnumerable<TSource> source, Func<TSource, Task<TResult>> function,
            CancellationToken cancellationToken)
        {
            return Linq.EnumerableExtensions.ForEachAsync(source, function, cancellationToken);
        }

        /// <inheritdoc cref="Linq.EnumerableExtensions.ForEachAsync{TSource, TResult}(IAsyncEnumerable{TSource}, Func{TSource, Task{TResult}})" />
        [Obsolete("Use \"Acolyte.Linq.EnumerableExtensions.ForEachAsync\" instead. This method will be removed in next major version.")]
        public static Task<TResult[]> ForEachAsync<TSource, TResult>(
            this IAsyncEnumerable<TSource> source, Func<TSource, Task<TResult>> function)
        {
            return Linq.EnumerableExtensions.ForEachAsync(source, function);
        }

        /// <inheritdoc cref="Linq.EnumerableExtensions.ForEachSafeAsync{TSource}(IAsyncEnumerable{TSource}, Func{TSource, Task}, CancellationToken)" />
        [Obsolete("Use \"Acolyte.Linq.EnumerableExtensions.ForEachSafeAsync\" instead. This method will be removed in next major version.")]
        public static Task<Result<NoneResult, Exception>[]> ForEachSafeAsync<TSource>(
            this IAsyncEnumerable<TSource> source, Func<TSource, Task> action,
            CancellationToken cancellationToken)
        {
            return Linq.EnumerableExtensions.ForEachSafeAsync(source, action, cancellationToken);
        }

        /// <inheritdoc cref="Linq.EnumerableExtensions.ForEachSafeAsync{TSource}(IAsyncEnumerable{TSource}, Func{TSource, Task})" />
        [Obsolete("Use \"Acolyte.Linq.EnumerableExtensions.ForEachSafeAsync\" instead. This method will be removed in next major version.")]
        public static Task<Result<NoneResult, Exception>[]> ForEachSafeAsync<TSource>(
            this IAsyncEnumerable<TSource> source, Func<TSource, Task> action)
        {
            return Linq.EnumerableExtensions.ForEachSafeAsync(source, action);
        }

        /// <inheritdoc cref="Linq.EnumerableExtensions.ForEachSafeAsync{TSource, TResult}(IAsyncEnumerable{TSource}, Func{TSource, Task{TResult}}, CancellationToken)" />
        [Obsolete("Use \"Acolyte.Linq.EnumerableExtensions.ForEachSafeAsync\" instead. This method will be removed in next major version.")]
        public static Task<Result<TResult, Exception>[]> ForEachSafeAsync<TSource, TResult>(
            this IAsyncEnumerable<TSource> source, Func<TSource, Task<TResult>> function,
            CancellationToken cancellationToken)
        {
            return Linq.EnumerableExtensions.ForEachSafeAsync(source, function, cancellationToken);
        }

        /// <inheritdoc cref="Linq.EnumerableExtensions.ForEachSafeAsync{TSource, TResult}(IAsyncEnumerable{TSource}, Func{TSource, Task{TResult}})" />
        [Obsolete("Use \"Acolyte.Linq.EnumerableExtensions.ForEachSafeAsync\" instead. This method will be removed in next major version.")]
        public static Task<Result<TResult, Exception>[]> ForEachSafeAsync<TSource, TResult>(
            this IAsyncEnumerable<TSource> source, Func<TSource, Task<TResult>> function)
        {
            return Linq.EnumerableExtensions.ForEachSafeAsync(source, function);
        }

#endif

        #endregion

        #region Distinct By

        /// <inheritdoc cref="Linq.EnumerableExtensions.DistinctBy{TSource, TKey}(IEnumerable{TSource}, Func{TSource, TKey}, IEqualityComparer{TKey}?)" />
        [Obsolete("Use \"Acolyte.Linq.EnumerableExtensions.DistinctBy\" instead. This method will be removed in next major version.")]
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(
            this IEnumerable<TSource> source, Func<TSource, TKey> keySelector,
             IEqualityComparer<TKey>? keyComparer)
        {
            return Linq.EnumerableExtensions.DistinctBy(source, keySelector, keyComparer);
        }

        /// <inheritdoc cref="Linq.EnumerableExtensions.DistinctBy{TSource, TKey}(IEnumerable{TSource}, Func{TSource, TKey})" />
        [Obsolete("Use \"Acolyte.Linq.EnumerableExtensions.DistinctBy\" instead. This method will be removed in next major version.")]
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(
            this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            return Linq.EnumerableExtensions.DistinctBy(source, keySelector);
        }

        #endregion

        #region Slicing

        /// <inheritdoc cref="Linq.EnumerableExtensions.SliceIfRequired{TSource}(IEnumerable{TSource}, int?, int?)" />
        [Obsolete("Use \"Acolyte.Linq.EnumerableExtensions.SliceIfRequired\" instead. This method will be removed in next major version.")]
        public static IEnumerable<TSource> SliceIfRequired<TSource>(
            this IEnumerable<TSource> source, int? startIndex, int? count)
        {
            return Linq.EnumerableExtensions.SliceIfRequired(source, startIndex, count);
        }

        #endregion

        #region Order By Sequence

        #region Order By Sequence Overloads Without Comparer

        /// <inheritdoc cref="Linq.EnumerableExtensions.OrderBySequence{TSource}(IEnumerable{TSource}, IEnumerable{TSource})" />
        [Obsolete("Use \"Acolyte.Linq.EnumerableExtensions.OrderBySequence\" instead. This method will be removed in next major version.")]
        public static IEnumerable<TSource> OrderBySequence<TSource>(
            this IEnumerable<TSource> source, IEnumerable<TSource> order)
        {
            return Linq.EnumerableExtensions.OrderBySequence(source, order);
        }

        /// <inheritdoc cref="Linq.EnumerableExtensions.OrderBySequence{TSource, TOrder}(IEnumerable{TSource}, IEnumerable{TOrder}, Func{TSource, TOrder})" />
        [Obsolete("Use \"Acolyte.Linq.EnumerableExtensions.OrderBySequence\" instead. This method will be removed in next major version.")]
        public static IEnumerable<TSource> OrderBySequence<TSource, TOrder>(
            this IEnumerable<TSource> source, IEnumerable<TOrder> order,
            Func<TSource, TOrder> sourceKeySelector)
        {
            return Linq.EnumerableExtensions.OrderBySequence(source, order, sourceKeySelector);
        }

        /// <inheritdoc cref="Linq.EnumerableExtensions.OrderBySequence{TSource, TOrder, TKey}(IEnumerable{TSource}, IEnumerable{TOrder}, Func{TSource, TKey}, Func{TOrder, TKey})" />
        [Obsolete("Use \"Acolyte.Linq.EnumerableExtensions.OrderBySequence\" instead. This method will be removed in next major version.")]
        public static IEnumerable<TSource> OrderBySequence<TSource, TOrder, TKey>(
            this IEnumerable<TSource> source, IEnumerable<TOrder> order,
            Func<TSource, TKey> sourceKeySelector, Func<TOrder, TKey> orderKeySelector)
        {
            return Linq.EnumerableExtensions.OrderBySequence(
                source, order, sourceKeySelector, orderKeySelector
            );
        }

        /// <inheritdoc cref="Linq.EnumerableExtensions.OrderBySequence{TSource, TOrder, TKey, TResult}(IEnumerable{TSource}, IEnumerable{TOrder}, Func{TSource, TKey}, Func{TOrder, TKey}, Func{TSource, TOrder, TResult})" />
        [Obsolete("Use \"Acolyte.Linq.EnumerableExtensions.OrderBySequence\" instead. This method will be removed in next major version.")]
        public static IEnumerable<TResult> OrderBySequence<TSource, TOrder, TKey, TResult>(
            this IEnumerable<TSource> source, IEnumerable<TOrder> order,
            Func<TSource, TKey> sourceKeySelector, Func<TOrder, TKey> orderKeySelector,
            Func<TSource, TOrder, TResult> resultSelector)
        {
            return Linq.EnumerableExtensions.OrderBySequence(
                source, order, sourceKeySelector, orderKeySelector, resultSelector
            );
        }

        #endregion

        #region Order By Sequence Overloads With Comparer

        /// <inheritdoc cref="Linq.EnumerableExtensions.OrderBySequence{TSource, TOrder, TKey, TResult}(IEnumerable{TSource}, IEnumerable{TOrder}, Func{TSource, TKey}, Func{TOrder, TKey}, Func{TSource, TOrder, TResult}, IEqualityComparer{TKey}?)" />
        [Obsolete("Use \"Acolyte.Linq.EnumerableExtensions.OrderBySequence\" instead. This method will be removed in next major version.")]
        public static IEnumerable<TResult> OrderBySequence<TSource, TOrder, TKey, TResult>(
            this IEnumerable<TSource> source, IEnumerable<TOrder> order,
            Func<TSource, TKey> sourceKeySelector, Func<TOrder, TKey> orderKeySelector,
            Func<TSource, TOrder, TResult> resultSelector, IEqualityComparer<TKey>? comparer)
        {
            return Linq.EnumerableExtensions.OrderBySequence(
                source, order, sourceKeySelector, orderKeySelector, resultSelector, comparer
            );
        }

        #endregion

        #endregion

        #region Zip

        /// <inheritdoc cref="Linq.EnumerableExtensions.ZipTwo{TFirst, TSecond}(IEnumerable{TFirst}, IEnumerable{TSecond})" />
        [Obsolete("Use \"Acolyte.Linq.EnumerableExtensions.ZipTwo\" instead. This method will be removed in next major version.")]
        public static IEnumerable<(TFirst, TSecond)> ZipTwo<TFirst, TSecond>(
            this IEnumerable<TFirst> first, IEnumerable<TSecond> second)
        {
            return Linq.EnumerableExtensions.ZipTwo(first, second);
        }

        /// <inheritdoc cref="Linq.EnumerableExtensions.ZipThree{TFirst, TSecond, TThird, TResult}(IEnumerable{TFirst}, IEnumerable{TSecond}, IEnumerable{TThird}, Func{TFirst, TSecond, TThird, TResult})" />
        [Obsolete("Use \"Acolyte.Linq.EnumerableExtensions.ZipThree\" instead. This method will be removed in next major version.")]
        public static IEnumerable<TResult> ZipThree<TFirst, TSecond, TThird, TResult>(
            this IEnumerable<TFirst> first, IEnumerable<TSecond> second, IEnumerable<TThird> third,
            Func<TFirst, TSecond, TThird, TResult> resultSelector)
        {
            return Linq.EnumerableExtensions.ZipThree(first, second, third, resultSelector);
        }

        /// <inheritdoc cref="Linq.EnumerableExtensions.ZipThree{TFirst, TSecond, TThird}(IEnumerable{TFirst}, IEnumerable{TSecond}, IEnumerable{TThird})" />
        [Obsolete("Use \"Acolyte.Linq.EnumerableExtensions.ZipThree\" instead. This method will be removed in next major version.")]
        public static IEnumerable<(TFirst, TSecond, TThird)> ZipThree<TFirst, TSecond, TThird>(
            this IEnumerable<TFirst> first, IEnumerable<TSecond> second, IEnumerable<TThird> third)
        {
            return Linq.EnumerableExtensions.ZipThree(first, second, third);
        }

        #endregion
    }
}
