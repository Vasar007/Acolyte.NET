// Copyright (c) Veeam Software Group GmbH

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Acolyte.Common.Monads;

namespace Acolyte.Collections
{
    public class TypedEnumDictionary : Dictionary<Type, Enum>
    {
        #region Constructors

        public TypedEnumDictionary()
        {
        }

        public TypedEnumDictionary(
            IDictionary<Type, Enum> dictionary)
            : base(dictionary)
        {
        }

        public TypedEnumDictionary(
            IEqualityComparer<Type> comparer)
           : base(comparer)
        {
        }

        public TypedEnumDictionary(
            int capacity)
            : base(capacity)
        {
        }

        public TypedEnumDictionary(
            IDictionary<Type, Enum> dictionary,
            IEqualityComparer<Type> comparer)
            : base(dictionary, comparer)
        {
        }

        public TypedEnumDictionary(
            int capacity,
            IEqualityComparer<Type> comparer)
            : base(capacity, comparer)
        {
        }

#if NETSTANDARD2_1

        public TypedEnumDictionary(
            IEnumerable<KeyValuePair<Type, Enum>> collection)
           : base(collection)
        {
        }

        public TypedEnumDictionary(
            IEnumerable<KeyValuePair<Type, Enum>> collection,
            IEqualityComparer<Type> comparer)
            : base(collection, comparer)
        {
        }

#endif

        protected TypedEnumDictionary(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context)
        {
        }

        #endregion

        public void Add<TEnum>(TEnum enumValue)
            where TEnum : struct, Enum
        {
            Add(typeof(TEnum), enumValue);
        }

        public TEnum Get<TEnum>()
            where TEnum : struct, Enum
        {
            Enum value = this[typeof(TEnum)];
            return value.To<TEnum>();
        }

        public TEnum GetOrDefault<TEnum>()
            where TEnum : struct, Enum
        {
            // "result" will contains found value or default.
            TryGetValue(out TEnum result);
            return result;
        }

        public bool TryGetValue<TEnum>(out TEnum value)
            where TEnum : struct, Enum
        {
            if (TryGetValue(typeof(TEnum), out Enum result))
            {
                value = result.To<TEnum>();
                return true;
            }

            value = default;
            return false;
        }
    }
}
