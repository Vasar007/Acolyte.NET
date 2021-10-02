using System;
using System.Collections.Generic;
using System.Linq;
using Acolyte.Assertions;

namespace Acolyte.Common
{
    public static class TupleExtensions
    {

#if ITUIPLE

        public static IEnumerable<object?> EnumerateTupleValues(
            this System.Runtime.CompilerServices.ITuple tuple)
        {
            tuple.ThrowIfNull(nameof(tuple));

            int index = 0;
            while (index < tuple.Length)
            {
                yield return tuple[index++];
            }
        }

#endif

        public static IEnumerable<T> ToEnumerable<T>(this Tuple<T> tuple)
        {
            tuple.ThrowIfNull(nameof(tuple));

            yield return tuple.Item1;
        }

        public static IEnumerable<object?> ToObjectEnumerable<T1, T2>(this Tuple<T1, T2> tuple)
        {
            tuple.ThrowIfNull(nameof(tuple));

            yield return tuple.Item1;
            yield return tuple.Item2;
        }

        public static IEnumerable<T> ToEnumerable<T>(this Tuple<T, T> tuple)
        {
            return tuple.ToObjectEnumerable().Cast<T>();
        }

        public static IEnumerable<object?> ToObjectEnumerable<T1, T2, T3>(this Tuple<T1, T2, T3> tuple)
        {
            tuple.ThrowIfNull(nameof(tuple));

            yield return tuple.Item1;
            yield return tuple.Item2;
            yield return tuple.Item3;
        }

        public static IEnumerable<T> ToEnumerable<T>(this Tuple<T, T, T> tuple)
        {
            return tuple.ToObjectEnumerable().Cast<T>();
        }

        public static IEnumerable<object?> ToObjectEnumerable<T1, T2, T3, T4>(
            this Tuple<T1, T2, T3, T4> tuple)
        {
            tuple.ThrowIfNull(nameof(tuple));

            yield return tuple.Item1;
            yield return tuple.Item2;
            yield return tuple.Item3;
            yield return tuple.Item4;
        }

        public static IEnumerable<T> ToEnumerable<T>(this Tuple<T, T, T, T> tuple)
        {
            return tuple.ToObjectEnumerable().Cast<T>();
        }

        public static IEnumerable<object?> ToObjectEnumerable<T1, T2, T3, T4, T5>(
            this Tuple<T1, T2, T3, T4, T5> tuple)
        {
            tuple.ThrowIfNull(nameof(tuple));

            yield return tuple.Item1;
            yield return tuple.Item2;
            yield return tuple.Item3;
            yield return tuple.Item4;
            yield return tuple.Item5;
        }

        public static IEnumerable<T> ToEnumerable<T>(this Tuple<T, T, T, T, T> tuple)
        {
            return tuple.ToObjectEnumerable().Cast<T>();
        }

        public static IEnumerable<object?> ToObjectEnumerable<T1, T2, T3, T4, T5, T6>(
            this Tuple<T1, T2, T3, T4, T5, T6> tuple)
        {
            tuple.ThrowIfNull(nameof(tuple));

            yield return tuple.Item1;
            yield return tuple.Item2;
            yield return tuple.Item3;
            yield return tuple.Item4;
            yield return tuple.Item5;
            yield return tuple.Item6;
        }

        public static IEnumerable<T> ToEnumerable<T>(this Tuple<T, T, T, T, T, T> tuple)
        {
            return tuple.ToObjectEnumerable().Cast<T>();
        }

        public static IEnumerable<object?> ToObjectEnumerable<T1, T2, T3, T4, T5, T6, T7>(
            this Tuple<T1, T2, T3, T4, T5, T6, T7> tuple)
        {
            tuple.ThrowIfNull(nameof(tuple));

            yield return tuple.Item1;
            yield return tuple.Item2;
            yield return tuple.Item3;
            yield return tuple.Item4;
            yield return tuple.Item5;
            yield return tuple.Item6;
            yield return tuple.Item7;
        }

        public static IEnumerable<T> ToEnumerable<T>(this Tuple<T, T, T, T, T, T, T> tuple)
        {
            return tuple.ToObjectEnumerable().Cast<T>();
        }
    }
}
