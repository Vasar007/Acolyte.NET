using System;
using System.Collections.Generic;

namespace Acolyte.Reflection
{
    public static class TypesForReflection
    {
        public static IEnumerable<Type> GetTupleGenericDefenitions()
        {
            return GetTupleGenericDefenitionsHashSet();
        }

        internal static HashSet<Type> GetTupleGenericDefenitionsHashSet()
        {
            return new HashSet<Type>
            {
                typeof(Tuple<>),
                typeof(Tuple<,>),
                typeof(Tuple<,,>),
                typeof(Tuple<,,,>),
                typeof(Tuple<,,,,>),
                typeof(Tuple<,,,,,>),
                typeof(Tuple<,,,,,,>),
                typeof(Tuple<,,,,,,,>),
            };
        }

        public static IEnumerable<Type> GetValueTupleGenericDefenitions()
        {
            return GetValueTupleGenericDefenitionsHashSet();
        }

        internal static HashSet<Type> GetValueTupleGenericDefenitionsHashSet()
        {
            return new HashSet<Type>
            {
                typeof(ValueTuple<>),
                typeof(ValueTuple<,>),
                typeof(ValueTuple<,,>),
                typeof(ValueTuple<,,,>),
                typeof(ValueTuple<,,,,>),
                typeof(ValueTuple<,,,,,>),
                typeof(ValueTuple<,,,,,,>),
                typeof(ValueTuple<,,,,,,,>),
            };
        }
    }
}
