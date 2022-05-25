using System;
using System.Linq.Expressions;
using Acolyte.Assertions;

namespace Acolyte.Linq.Expressions
{
    /// <summary>
    /// Class to cast to type <typeparamref name="TTarget"/>.
    /// </summary>
    /// <typeparam name="TTarget">The target type.</typeparam>
    public static class CastTo<TTarget>
    {
        private static class Cache<TSourceInternal>
        {
            public static readonly Func<TSourceInternal, TTarget> UncheckedCaster = ConvertUnchecked();
            public static readonly Func<TSourceInternal, TTarget> CheckedCaster = ConvertChecked();

            private static Func<TSourceInternal, TTarget> ConvertUnchecked()
            {
                return ConvertInternal(Expression.Convert);
            }

            private static Func<TSourceInternal, TTarget> ConvertChecked()
            {
                return ConvertInternal(Expression.ConvertChecked);
            }

            private static Func<TSourceInternal, TTarget> ConvertInternal(
                Func<ParameterExpression, Type, UnaryExpression> conversionBodyFactory)
            {
                var parameter = Expression.Parameter(typeof(TSourceInternal));
                var conversionBody = conversionBodyFactory(parameter, typeof(TTarget));

                return Expression
                    .Lambda<Func<TSourceInternal, TTarget>>(conversionBody, parameter)
                    .Compile();
            }
        }

        /// <summary>
        /// Casts <typeparamref name="TSource"/> to <typeparamref name="TTarget"/> applying
        /// conversion operation without overflow check. This does not cause boxing for value types.
        /// Useful in generic methods.
        /// </summary>
        /// <typeparam name="TSource">
        /// The source type to cast from. Usually a generic type.
        /// </typeparam>
        /// <returns>Casted value.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// No conversion operator is defined between <typeparamref name="TSource" /> and
        /// <typeparamref name="TTarget" />.
        /// </exception>
        public static TTarget From<TSource>(TSource source)
        {
            source.ThrowIfNullValue(nameof(source), assertOnPureValueTypes: false);

            return Cache<TSource>.UncheckedCaster(source);
        }

        /// <summary>
        /// Casts <typeparamref name="TSource"/> to <typeparamref name="TTarget"/> applying
        /// conversion operation that throws an exception if the target type is overflowed.
        /// This does not cause boxing for value types.
        /// Useful in generic methods.
        /// </summary>
        /// <typeparam name="TSource">
        /// The source type to cast from. Usually a generic type.
        /// </typeparam>
        /// <returns>Casted value.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// No conversion operator is defined between <typeparamref name="TSource" /> and
        /// <typeparamref name="TTarget" />.
        /// </exception>
        /// <exception cref="OverflowException">
        /// Conversion operation resulted in an overflow.
        /// </exception>
        public static TTarget FromChecked<TSource>(TSource source)
        {
            source.ThrowIfNullValue(nameof(source), assertOnPureValueTypes: false);

            return Cache<TSource>.CheckedCaster(source);
        }
    }
}
