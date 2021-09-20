using System;
using System.Collections.Generic;

namespace Acolyte.Reflection
{
    public static class TypesForReflection
    {
        private static readonly HashSet<Type> TupleGenericDefinitionsHashSet = new()
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

        public static IEnumerable<Type> GetTupleGenericDefinitions()
        {
            return TupleGenericDefinitionsHashSet;
        }

        private static readonly HashSet<Type> ValueTupleGenericDefinitionsHashSet = new()
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

        public static IEnumerable<Type> GetValueTupleGenericDefinitions()
        {
            return ValueTupleGenericDefinitionsHashSet;
        }

        #region Obsolete

        /// <inheritdoc cref="GetTupleGenericDefinitions" />
        [Obsolete("Use \"Acolyte.Reflection.TypesForReflection.GetTupleGenericDefinitions\" instead. This method will be removed in next major version.", error: false)]
        public static IEnumerable<Type> GetTupleGenericDefenitions()
        {
            return GetTupleGenericDefinitions();
        }


        /// <inheritdoc cref="GetTupleGenericDefenitions" />
        [Obsolete("Use \"Acolyte.Reflection.TypesForReflection.GetTupleGenericDefenitions\" instead. This method will be removed in next major version.", error: false)]
        internal static HashSet<Type> GetTupleGenericDefenitionsHashSet()
        {
            return new HashSet<Type>(GetTupleGenericDefenitions());
        }

        /// <inheritdoc cref="GetValueTupleGenericDefinitions" />
        [Obsolete("Use \"Acolyte.Reflection.TypesForReflection.GetValueTupleGenericDefinitions\" instead. This method will be removed in next major version.", error: false)]
        public static IEnumerable<Type> GetValueTupleGenericDefenitions()
        {
            return GetValueTupleGenericDefinitions();
        }

        /// <inheritdoc cref="GetValueTupleGenericDefenitions" />
        [Obsolete("Use \"Acolyte.Reflection.TypesForReflection.GetValueTupleGenericDefinitionsHashSet\" instead. This method will be removed in next major version.", error: false)]
        internal static HashSet<Type> GetValueTupleGenericDefenitionsHashSet()
        {
            return new HashSet<Type>(GetValueTupleGenericDefenitions());
        }

        #endregion
    }
}
