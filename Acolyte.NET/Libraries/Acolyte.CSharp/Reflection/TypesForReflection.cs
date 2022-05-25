using System;
using System.Collections.Generic;

namespace Acolyte.Reflection
{
    public static class TypesForReflection
    {
        public static Type Object { get; } = typeof(object);

        public static Type ObjectRef { get; } = Object.MakeByRefType();

        public static Type ValueType { get; } = typeof(ValueType);

        public static Type Nullable { get; } = typeof(Nullable<>);

        public static Type List { get; } = typeof(List<>);

        public static Type IList { get; } = typeof(IList<>);

        public static Type IReadOnlyCollection { get; } = typeof(IReadOnlyCollection<>);

        public static Type IReadOnlyList { get; } = typeof(IReadOnlyList<>);

        public static Type Dictionary { get; } = typeof(Dictionary<,>);

        public static Type IDictionary { get; } = typeof(IDictionary<,>);

        public static Type IReadOnlyDictionary { get; } = typeof(IReadOnlyDictionary<,>);

        public static Type Int32 { get; } = typeof(int);


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
