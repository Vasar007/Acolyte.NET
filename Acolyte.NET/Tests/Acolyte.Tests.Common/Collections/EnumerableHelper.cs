using System;
using System.Collections.Generic;
using System.Linq;

namespace Acolyte.Tests.Collections
{
    public static class EnumerableHelper
    {
        public static IEnumerable<TResult> Empty<TResult>()
        {
            return Enumerable.Empty<TResult>();
        }

        public static IEnumerable<TResult> Repeat<TResult>(TResult element, int count)
        {
            return Enumerable.Repeat(element, count);
        }

        public static IEnumerable<TSource> Reverse<TSource>(this IEnumerable<TSource> source)
        {
            return Enumerable.Reverse(source);
        }

        #region Max

        public static int Max(this IEnumerable<int> source)
        {
            return Enumerable.Max(source);
        }

        public static uint Max(this IEnumerable<uint> source)
        {
            return Enumerable.Max(source);
        }

        public static long Max(this IEnumerable<long> source)
        {
            return Enumerable.Max(source);
        }

        public static ulong Max(this IEnumerable<ulong> source)
        {
            return Enumerable.Max(source);
        }

        public static float Max(this IEnumerable<float> source)
        {
            return Enumerable.Max(source);
        }

        public static double Max(this IEnumerable<double> source)
        {
            return Enumerable.Max(source);
        }

        public static decimal Max(this IEnumerable<decimal> source)
        {
            return Enumerable.Max(source);
        }

        public static TSource? Max<TSource>(this IEnumerable<TSource> source)
        {
            return Enumerable.Max(source);
        }

        public static int Max<TSource>(this IEnumerable<TSource> source, Func<TSource, int> selector)
        {
            return Enumerable.Max(source, selector);
        }

        public static uint Max<TSource>(this IEnumerable<TSource> source, Func<TSource, uint> selector)
        {
            return Enumerable.Max(source, selector);
        }

        public static long Max<TSource>(this IEnumerable<TSource> source, Func<TSource, long> selector)
        {
            return Enumerable.Max(source, selector);
        }

        public static ulong Max<TSource>(this IEnumerable<TSource> source, Func<TSource, ulong> selector)
        {
            return Enumerable.Max(source, selector);
        }

        public static float Max<TSource>(this IEnumerable<TSource> source, Func<TSource, float> selector)
        {
            return Enumerable.Max(source, selector);
        }

        public static double Max<TSource>(this IEnumerable<TSource> source, Func<TSource, double> selector)
        {
            return Enumerable.Max(source, selector);
        }

        public static decimal Max<TSource>(this IEnumerable<TSource> source, Func<TSource, decimal> selector)
        {
            return Enumerable.Max(source, selector);
        }

        public static TResult? Max<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
        {
            return Enumerable.Max(source, selector);
        }

        #endregion

        #region Min

        public static int Min(this IEnumerable<int> source)
        {
            return Enumerable.Min(source);
        }

        public static uint Min(this IEnumerable<uint> source)
        {
            return Enumerable.Min(source);
        }

        public static long Min(this IEnumerable<long> source)
        {
            return Enumerable.Min(source);
        }

        public static ulong Min(this IEnumerable<ulong> source)
        {
            return Enumerable.Min(source);
        }

        public static float Min(this IEnumerable<float> source)
        {
            return Enumerable.Min(source);
        }

        public static double Min(this IEnumerable<double> source)
        {
            return Enumerable.Min(source);
        }

        public static decimal Min(this IEnumerable<decimal> source)
        {
            return Enumerable.Min(source);
        }

        public static TSource? Min<TSource>(this IEnumerable<TSource> source)
        {
            return Enumerable.Min(source);
        }

        public static int Min<TSource>(this IEnumerable<TSource> source, Func<TSource, int> selector)
        {
            return Enumerable.Min(source, selector);
        }

        public static uint Min<TSource>(this IEnumerable<TSource> source, Func<TSource, uint> selector)
        {
            return Enumerable.Min(source, selector);
        }

        public static long Min<TSource>(this IEnumerable<TSource> source, Func<TSource, long> selector)
        {
            return Enumerable.Min(source, selector);
        }

        public static ulong Min<TSource>(this IEnumerable<TSource> source, Func<TSource, ulong> selector)
        {
            return Enumerable.Min(source, selector);
        }

        public static float Min<TSource>(this IEnumerable<TSource> source, Func<TSource, float> selector)
        {
            return Enumerable.Min(source, selector);
        }

        public static double Min<TSource>(this IEnumerable<TSource> source, Func<TSource, double> selector)
        {
            return Enumerable.Min(source, selector);
        }

        public static decimal Min<TSource>(this IEnumerable<TSource> source, Func<TSource, decimal> selector)
        {
            return Enumerable.Min(source, selector);
        }

        public static TResult? Min<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
        {
            return Enumerable.Min(source, selector);
        }

        #endregion
    }
}
