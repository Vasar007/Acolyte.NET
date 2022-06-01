using System;
using System.Collections.Concurrent;
using System.Linq.Expressions;
using Acolyte.Assertions;
using Acolyte.Reflection;

namespace Acolyte.Linq.Expressions
{
    /// <summary>
    /// Class to cast boxed type to other boxed type.
    /// </summary>
    public static class ObjectCaster
    {
        private static readonly ConcurrentDictionary<(Type, Type), Func<object, object>> Cache = new();

        public static object Cast(Type toType, object value)
        {
            toType.ThrowIfNull(nameof(toType));
            value.ThrowIfNull(nameof(value));

            Func<object, object> convertor = GetOrAddCaster(
                toType, value, (expression, type) => Expression.Convert(expression, type)
            );
            return convertor(value);
        }

        public static object CastChecked(Type toType, object value)
        {
            toType.ThrowIfNull(nameof(toType));
            value.ThrowIfNull(nameof(value));

            Func<object, object> convertor = GetOrAddCaster(
                toType, value, (expression, type) => Expression.ConvertChecked(expression, type)
            );
            return convertor(value);
        }

        private static Func<object, object> GetOrAddCaster(Type toType, object value,
            Func<Expression, Type, UnaryExpression> conversionBodyFactory)
        {
            var typeOfValue = value.GetType();
            var pair = (toType, typeOfValue);
            return Cache.GetOrAdd(pair, key => ConvertInternal(key, conversionBodyFactory));
        }

        private static Func<object, object> ConvertInternal((Type toType, Type typeOfValue) pair,
            Func<Expression, Type, UnaryExpression> conversionBodyFactory)
        {
            var parameter = Expression.Parameter(TypesForReflection.Object);
            var expression = conversionBodyFactory(parameter, pair.typeOfValue);
            expression = conversionBodyFactory(expression, pair.toType);
            expression = conversionBodyFactory(expression, TypesForReflection.Object);

            return Expression
                .Lambda<Func<object, object>>(expression, parameter)
                .Compile();
        }
    }
}
