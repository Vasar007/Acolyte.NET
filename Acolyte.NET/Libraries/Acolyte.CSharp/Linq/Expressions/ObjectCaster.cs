using System;
using System.Linq.Expressions;
using Acolyte.Reflection;

namespace Acolyte.Linq.Expressions
{
    /// <summary>
    /// Class to cast boxed type to other boxed type.
    /// </summary>
    public static class ObjectCaster
    {
        public static object UncheckedCast(Type toType, object value)
        {
            var parameter = Expression.Parameter(TypesForReflection.Object);
            var expression = Expression.Convert(parameter, value.GetType());
            expression = Expression.Convert(expression, toType);
            expression = Expression.Convert(expression, TypesForReflection.Object);
            Func<object, object> convertor = Expression.Lambda<Func<object, object>>(expression, parameter).Compile();
            return convertor(value);
        }
    }
}
