using System;
using System.Collections.Generic;

namespace Acolyte.Reflection
{
    public static class TypesForReflection
    {
        public static IEnumerable<Type> GetTupleGenericDefinitions()
        {
            return GetTupleGenericDefinitionsHashSet();
        }

        internal static HashSet<Type> GetTupleGenericDefinitionsHashSet()
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

        public static IEnumerable<Type> GetValueTupleGenericDefinitions()
        {
            return GetValueTupleGenericDefinitionsHashSet();
        }


        internal static HashSet<Type> GetValueTupleGenericDefinitionsHashSet()
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

        #region Obsolete

        /// <inheritdoc cref="GetTupleGenericDefinitions" />
        [Obsolete("Use \"Acolyte.Reflection.TypesForReflection.GetTupleGenericDefinitions\" instead. This method will be removed in next major version.", error: false)]
        public static IEnumerable<Type> GetTupleGenericDefenitions()
        {
            return GetTupleGenericDefinitions();
        }


        /// <inheritdoc cref="GetTupleGenericDefinitionsHashSet" />
        [Obsolete("Use \"Acolyte.Reflection.TypesForReflection.GetTupleGenericDefinitionsHashSet\" instead. This method will be removed in next major version.", error: false)]
        internal static HashSet<Type> GetTupleGenericDefenitionsHashSet()
        {
            return GetTupleGenericDefinitionsHashSet();
        }

        /// <inheritdoc cref="GetValueTupleGenericDefinitions" />
        [Obsolete("Use \"Acolyte.Reflection.TypesForReflection.GetValueTupleGenericDefinitions\" instead. This method will be removed in next major version.", error: false)]
        public static IEnumerable<Type> GetValueTupleGenericDefenitions()
        {
            return GetValueTupleGenericDefinitions();
        }

        /// <inheritdoc cref="GetValueTupleGenericDefinitionsHashSet" />
        [Obsolete("Use \"Acolyte.Reflection.TypesForReflection.GetValueTupleGenericDefinitionsHashSet\" instead. This method will be removed in next major version.", error: false)]
        internal static HashSet<Type> GetValueTupleGenericDefenitionsHashSet()
        {
            return GetValueTupleGenericDefinitionsHashSet();
        }

        #endregion
    }
}
