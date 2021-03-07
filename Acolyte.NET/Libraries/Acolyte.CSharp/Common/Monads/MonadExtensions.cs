using System;
using Acolyte.Assertions;

namespace Acolyte.Common.Monads
{
    /// <summary>
    /// Provides a set of monadic functions (simplify to use functional style in C#).
    /// </summary>
    public static class MonadExtensions
    {
        // We cannot merge methods for class? and Nullable<struct> because
        // this lead to changing signature of delegates that passes in methods.
        // I.g. user call some method and specify source Nullable<struct> delegate for value type
        // value, not for Nullable<struct> and expect that delegate will be called only when
        // source has value.

        #region With

        public static TResult? With<TSource, TResult>(this TSource? source,
            Func<TSource, TResult> func)
            where TSource : class?
        {
            func.ThrowIfNull(nameof(func));

            return source is null
                ? default
                : func(source);
        }

        public static TResult? With<TSource, TResult>(this TSource? source,
            Func<TSource, TResult> func)
            where TSource : struct
        {
            func.ThrowIfNull(nameof(func));

            return !source.HasValue
                ? default
                : func(source.Value);
        }

        #endregion

        #region Do

        public static TSource? Do<TSource>(this TSource? source, Action<TSource> action)
            where TSource : class?
        {
            action.ThrowIfNull(nameof(action));

            if (source is not null)
            {
                action(source);
            }

            return source;
        }

        public static TSource? Do<TSource>(this TSource? source, Action<TSource> action)
            where TSource : struct
        {
            action.ThrowIfNull(nameof(action));

            if (source.HasValue)
            {
                action(source.Value);
            }

            return source;
        }

        #endregion

        #region ApplyIf

        public static TSource ApplyIf<TSource>(this TSource source, Func<TSource, bool> condition,
            Func<TSource, TSource> func)
        {
            return ApplyIf(source, condition, func, defaultValue: source);
        }

        public static TResult ApplyIf<TSource, TResult>(this TSource source,
            Func<TSource, bool> condition, Func<TSource, TResult> func, TResult defaultValue)
        {
            func.ThrowIfNull(nameof(func));

            return condition(source)
                ? func(source)
                : defaultValue;
        }

        #endregion

        #region To

        public static TResult? To<TResult>(this object? value)
        {
            try
            {
                return (TResult?) value;
            }
            catch (InvalidCastException ex)
            {
                string message = FormatErrorMessage<TResult>(value);

                throw new InvalidCastException(message, ex);
            }
        }

        #endregion

        #region Helpers Methods

        private static string FormatErrorMessage<T>(object? value)
        {
            return value is null
                ? InvalidNullCast<T>()
                : InvalidCast<T>(value);
        }

        private static string InvalidCast<T>(object value)
        {
            return $"Cannot cast object '{value}' of type '{value.GetType()}' to the " +
                   $"object of type '{typeof(T).Name}'.";
        }

        private static string InvalidNullCast<T>()
        {
            return $"The desired type '{typeof(T).Name}' does not support null objects.";
        }

        #endregion
    }
}
