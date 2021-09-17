using System;
using System.Collections.Generic;
using System.Linq;

namespace Acolyte.Common
{
    public static class ValueTupleExtensions
    {
        public static IEnumerable<T> ToEnumerable<T>(this ValueTuple<T> tuple)
        {
            yield return tuple.Item1;
        }

        public static IEnumerable<object?> ToObjectEnumerable<T1, T2>(this ValueTuple<T1, T2> tuple)
        {
            yield return tuple.Item1;
            yield return tuple.Item2;
        }

        public static IEnumerable<T> ToEnumerable<T>(this ValueTuple<T, T> tuple)
        {
            return tuple.ToObjectEnumerable().Cast<T>();
        }

        public static IEnumerable<object?> ToObjectEnumerable<T1, T2, T3>(
            this ValueTuple<T1, T2, T3> tuple)
        {

            yield return tuple.Item1;
            yield return tuple.Item2;
            yield return tuple.Item3;
        }

        public static IEnumerable<T> ToEnumerable<T>(this ValueTuple<T, T, T> tuple)
        {
            return tuple.ToObjectEnumerable().Cast<T>();
        }

        public static IEnumerable<object?> ToObjectEnumerable<T1, T2, T3, T4>(
            this ValueTuple<T1, T2, T3, T4> tuple)
        {
            yield return tuple.Item1;
            yield return tuple.Item2;
            yield return tuple.Item3;
            yield return tuple.Item4;
        }

        public static IEnumerable<T> ToEnumerable<T>(this ValueTuple<T, T, T, T> tuple)
        {
            return tuple.ToObjectEnumerable().Cast<T>();
        }

        public static IEnumerable<object?> ToObjectEnumerable<T1, T2, T3, T4, T5>(
            this ValueTuple<T1, T2, T3, T4, T5> tuple)
        {
            yield return tuple.Item1;
            yield return tuple.Item2;
            yield return tuple.Item3;
            yield return tuple.Item4;
            yield return tuple.Item5;
        }

        public static IEnumerable<T> ToEnumerable<T>(this ValueTuple<T, T, T, T, T> tuple)
        {
            return tuple.ToObjectEnumerable().Cast<T>();
        }

        public static IEnumerable<object?> ToObjectEnumerable<T1, T2, T3, T4, T5, T6>(
            this ValueTuple<T1, T2, T3, T4, T5, T6> tuple)
        {
            yield return tuple.Item1;
            yield return tuple.Item2;
            yield return tuple.Item3;
            yield return tuple.Item4;
            yield return tuple.Item5;
            yield return tuple.Item6;
        }

        public static IEnumerable<T> ToEnumerable<T>(this ValueTuple<T, T, T, T, T, T> tuple)
        {
            return tuple.ToObjectEnumerable().Cast<T>();
        }

        public static IEnumerable<object?> ToObjectEnumerable<T1, T2, T3, T4, T5, T6, T7>(
            this ValueTuple<T1, T2, T3, T4, T5, T6, T7> tuple)
        {
            yield return tuple.Item1;
            yield return tuple.Item2;
            yield return tuple.Item3;
            yield return tuple.Item4;
            yield return tuple.Item5;
            yield return tuple.Item6;
            yield return tuple.Item7;
        }

        public static IEnumerable<T> ToEnumerable<T>(this ValueTuple<T, T, T, T, T, T, T> tuple)
        {
            return tuple.ToObjectEnumerable().Cast<T>();
        }
    }
}
