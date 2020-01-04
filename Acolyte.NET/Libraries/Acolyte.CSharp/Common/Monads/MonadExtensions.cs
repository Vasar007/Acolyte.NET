using System;
using System.Diagnostics.CodeAnalysis;
using Acolyte.Assertions;

namespace Acolyte.Common.Monads
{
    public static class MonadExtensions
    {
        public static TResult Maybe<TMaybe, TResult>(this TMaybe maybe, Func<TMaybe, TResult> just)
            where TMaybe : class
        {
            just.ThrowIfNull(nameof(just));

            return maybe is null
                ? default
                : just(maybe);
        }

        [return: MaybeNull]
        public static TResult With<TSource, TResult>(this TSource source,
            Func<TSource, TResult> func)
            where TSource : class?
        {
            return source is null
                ? default
                : func(source);
        }

        [return: MaybeNull]
        public static TSource Do<TSource>(this TSource source, Action<TSource> action)
            where TSource : class?
        {
            if (!(source is null))
                action(source);

            return source;
        }

        public static TSource? Do<TSource>(this TSource? source, Action<TSource> action)
            where TSource : struct
        {
            if (source.HasValue)
                action(source.Value);

            return source;
        }

        [return: MaybeNull]
        public static TResult Return<TSource, TResult>(this TSource source,
            Func<TSource, TResult> func)
            where TSource : class?
        {
            return Return(source, func, default);
        }

        [return: MaybeNull]
        public static TResult Return<TSource, TResult>(this TSource source,
            Func<TSource, TResult> func, [AllowNull] TResult defaultValue)
            where TSource : class?
        {
            return source is null
                ? defaultValue
                : func(source);
        }

        public static TResult To<TResult>(this object value)
        {
            try
            {
                return (TResult) value;
            }
            catch (InvalidCastException ex)
            {
                string message = FormatErrorMessage<TResult>(value);

                throw new InvalidCastException(message, ex);
            }
        }

        private static string FormatErrorMessage<T>(object value)
        {
            return value == null ? InvalidNullCast<T>() : InvalidCast<T>(value);
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
    }
}
