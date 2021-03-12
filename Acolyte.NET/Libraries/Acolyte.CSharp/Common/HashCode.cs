using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Acolyte.Common
{
#if NETSTANDARD2_0
    /// <summary>
    /// A hash code used to help with implementing <see cref="object.GetHashCode()" />.
    /// If you can use <see href="https://docs.microsoft.com/en-us/dotnet/api/system.hashcode" />,
    /// use standard version instead!
    /// </summary>
    /// <remarks>
    /// <see href="https://stackoverflow.com/questions/263400/what-is-the-best-algorithm-for-an-overridden-system-object-gethashcode" />
    /// <see href="https://rehansaeed.com/gethashcode-made-easy/" />
    /// </remarks>
    [Obsolete("Use System.HashCode from package Microsoft.Bcl.HashCode.", error: false)]
    // TODO: replace this class with static helper for System.HashCode.
    public readonly struct HashCode : IEquatable<HashCode>
    {
        /// <summary>
        /// Default hash code value for empty collections.
        /// </summary>
        private const int EmptyCollectionPrimeNumber = 29;

        /// <summary>
        /// Field to accumulate hash code value.
        /// </summary>
        private readonly int _value;

        /// <summary>
        /// Initializes a new instance of the <see cref="HashCode" /> <see langword="struct" />.
        /// </summary>
        /// <param name="value">The value.</param>
        private HashCode(
            int value)
        {
            _value = value;
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="HashCode" /> to <see cref="int" />.
        /// </summary>
        /// <param name="hashCode">The hash code.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator int(HashCode hashCode)
        {
            return hashCode._value;
        }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(HashCode left, HashCode right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(HashCode left, HashCode right)
        {
            return !(left == right);
        }

        /// <summary>
        /// Takes the hash code of the specified item.
        /// </summary>
        /// <typeparam name="T">The type of the item.</typeparam>
        /// <param name="item">The item.</param>
        /// <returns>The new hash code.</returns>
        public static HashCode Of<T>(T item)
        {
            return new HashCode(GetHashCode(item));
        }

        /// <summary>
        /// Takes the hash code of the specified items.
        /// </summary>
        /// <typeparam name="T">The type of the items.</typeparam>
        /// <param name="items">The collection.</param>
        /// <returns>The new hash code.</returns>
        public static HashCode OfEach<T>(IEnumerable<T?>? items)
        {
            return items is null
                ? new HashCode(0)
                : new HashCode(GetHashCode(items, 0));
        }

        /// <summary>
        /// Adds the hash code of the specified item.
        /// </summary>
        /// <typeparam name="T">The type of the item.</typeparam>
        /// <param name="item">The item.</param>
        /// <returns>The new hash code.</returns>
        public HashCode And<T>(T? item)
        {
            return new HashCode(CombineHashCodes(_value, GetHashCode(item)));
        }

        /// <summary>
        /// Adds the hash code of the specified items in the collection.
        /// </summary>
        /// <typeparam name="T">The type of the items.</typeparam>
        /// <param name="items">The collection.</param>
        /// <returns>The new hash code.</returns>
        public HashCode AndEach<T>(IEnumerable<T?>? items)
        {
            if (items is null)
            {
                return new HashCode(_value);
            }

            return new HashCode(GetHashCode(items, _value));
        }

        /// <summary>
        /// Diffuses the hash code returned by the specified value.
        /// </summary>
        /// <typeparam name="T1">The type of the value to add the hash code.</typeparam>
        /// <param name="value1">The value to add to the hash code.</param>
        /// <returns>The hash code that represents the single value.</returns>
        public static HashCode Combine<T1>(T1? value1)
        {
            return Of(value1);
        }

        /// <summary>
        /// Combines two values into a hash code.
        /// </summary>
        /// <typeparam name="T1">
        /// The type of the first value to combine into the hash code.
        /// </typeparam>
        /// <typeparam name="T2">
        /// The type of the second value to combine into the hash code.
        /// </typeparam>
        /// <param name="value1">The first value to combine into the hash code.</param>
        /// <param name="value2">The second value to combine into the hash code.</param>
        /// <returns>The hash code that represents the two values.</returns>
        public static HashCode Combine<T1, T2>(T1? value1, T2? value2)
        {
            return Of(value1)
                .And(value2);
        }

        /// <summary>
        /// Combines three values into a hash code.
        /// </summary>
        /// <typeparam name="T1">
        /// The type of the first value to combine into the hash code.
        /// </typeparam>
        /// <typeparam name="T2">
        /// The type of the second value to combine into the hash code.
        /// </typeparam>
        /// <typeparam name="T3">
        /// The type of the third value to combine into the hash code.
        /// </typeparam>
        /// <param name="value1">The first value to combine into the hash code.</param>
        /// <param name="value2">The second value to combine into the hash code.</param>
        /// <param name="value3">The third value to combine into the hash code.</param>
        /// <returns>The hash code that represents the three values.</returns>
        public static HashCode Combine<T1, T2, T3>(T1? value1, T2? value2, T3? value3)
        {
            return Of(value1)
                .And(value2)
                .And(value3);
        }

        /// <summary>
        /// Combines four values into a hash code.
        /// </summary>
        /// <typeparam name="T1">
        /// The type of the first value to combine into the hash code.
        /// </typeparam>
        /// <typeparam name="T2">
        /// The type of the second value to combine into the hash code.
        /// </typeparam>
        /// <typeparam name="T3">
        /// The type of the third value to combine into the hash code.
        /// </typeparam>
        /// <typeparam name="T4">
        /// The type of the fourth value to combine into the hash code.
        /// </typeparam>
        /// <param name="value1">The first value to combine into the hash code.</param>
        /// <param name="value2">The second value to combine into the hash code.</param>
        /// <param name="value3">The third value to combine into the hash code.</param>
        /// <param name="value4">The fourth value to combine into the hash code.</param>
        /// <returns>The hash code that represents the four values.</returns>
        public static HashCode Combine<T1, T2, T3, T4>(T1? value1, T2? value2, T3? value3,
            T4? value4)
        {
            return Of(value1)
                 .And(value2)
                 .And(value3)
                 .And(value4);
        }

        /// <summary>
        /// Combines five values into a hash code.
        /// </summary>
        /// <typeparam name="T1">
        /// The type of the first value to combine into the hash code.
        /// </typeparam>
        /// <typeparam name="T2">
        /// The type of the second value to combine into the hash code.
        /// </typeparam>
        /// <typeparam name="T3">
        /// The type of the third value to combine into the hash code.
        /// </typeparam>
        /// <typeparam name="T4">
        /// The type of the fourth value to combine into the hash code.
        /// </typeparam>
        /// <typeparam name="T5">
        /// The type of the fifth value to combine into the hash code.
        /// </typeparam>
        /// <param name="value1">The first value to combine into the hash code.</param>
        /// <param name="value2">The second value to combine into the hash code.</param>
        /// <param name="value3">The third value to combine into the hash code.</param>
        /// <param name="value4">The fourth value to combine into the hash code.</param>
        /// <param name="value5">The fifth value to combine into the hash code.</param>
        /// <returns>The hash code that represents the five values.</returns>
        public static HashCode Combine<T1, T2, T3, T4, T5>(T1? value1, T2? value2, T3? value3,
            T4? value4, T5? value5)
        {
            return Of(value1)
                     .And(value2)
                     .And(value3)
                     .And(value4)
                     .And(value5);
        }

        /// <summary>
        /// Combines six values into a hash code.
        /// </summary>
        /// <typeparam name="T1">
        /// The type of the first value to combine into the hash code.
        /// </typeparam>
        /// <typeparam name="T2">
        /// The type of the second value to combine into the hash code.
        /// </typeparam>
        /// <typeparam name="T3">
        /// The type of the third value to combine into the hash code.
        /// </typeparam>
        /// <typeparam name="T4">
        /// The type of the fourth value to combine into the hash code.
        /// </typeparam>
        /// <typeparam name="T5">
        /// The type of the fifth value to combine into the hash code.
        /// </typeparam>
        /// <typeparam name="T6">
        /// The type of the sixth value to combine into the hash code.
        /// </typeparam>
        /// <param name="value1">The first value to combine into the hash code.</param>
        /// <param name="value2">The second value to combine into the hash code.</param>
        /// <param name="value3">The third value to combine into the hash code.</param>
        /// <param name="value4">The fourth value to combine into the hash code.</param>
        /// <param name="value5">The fifth value to combine into the hash code.</param>
        /// <param name="value6">The sixth value to combine into the hash code.</param>
        /// <returns>The hash code that represents the six values.</returns>
        public static HashCode Combine<T1, T2, T3, T4, T5, T6>(T1? value1, T2? value2,
            T3? value3, T4? value4, T5? value5, T6? value6)
        {
            return Of(value1)
                  .And(value2)
                  .And(value3)
                  .And(value4)
                  .And(value5)
                  .And(value6);
        }

        /// <summary>
        /// Combines seven values into a hash code.
        /// </summary>
        /// <typeparam name="T1">
        /// The type of the first value to combine into the hash code.
        /// </typeparam>
        /// <typeparam name="T2">
        /// The type of the second value to combine into the hash code.
        /// </typeparam>
        /// <typeparam name="T3">
        /// The type of the third value to combine into the hash code.
        /// </typeparam>
        /// <typeparam name="T4">
        /// The type of the fourth value to combine into the hash code.
        /// </typeparam>
        /// <typeparam name="T5">
        /// The type of the fifth value to combine into the hash code.
        /// </typeparam>
        /// <typeparam name="T6">
        /// The type of the sixth value to combine into the hash code.
        /// </typeparam>
        /// <typeparam name="T7">
        /// The type of the seventh value to combine into the hash code.
        /// </typeparam>
        /// <param name="value1">The first value to combine into the hash code.</param>
        /// <param name="value2">The second value to combine into the hash code.</param>
        /// <param name="value3">The third value to combine into the hash code.</param>
        /// <param name="value4">The fourth value to combine into the hash code.</param>
        /// <param name="value5">The fifth value to combine into the hash code.</param>
        /// <param name="value6">The sixth value to combine into the hash code.</param>
        /// <param name="value7">The seventh value to combine into the hash code.</param>
        /// <returns>The hash code that represents the seven values.</returns>
        public static HashCode Combine<T1, T2, T3, T4, T5, T6, T7>(T1? value1, T2? value2,
            T3? value3, T4? value4, T5? value5, T6? value6, T7? value7)
        {
            return Of(value1)
                .And(value2)
                .And(value3)
                .And(value4)
                .And(value5)
                .And(value6)
                .And(value7);
        }

        /// <summary>
        /// Combines eight values into a hash code.
        /// </summary>
        /// <typeparam name="T1">
        /// The type of the first value to combine into the hash code.
        /// </typeparam>
        /// <typeparam name="T2">
        /// The type of the second value to combine into the hash code.
        /// </typeparam>
        /// <typeparam name="T3">
        /// The type of the third value to combine into the hash code.
        /// </typeparam>
        /// <typeparam name="T4">
        /// The type of the fourth value to combine into the hash code.
        /// </typeparam>
        /// <typeparam name="T5">
        /// The type of the fifth value to combine into the hash code.
        /// </typeparam>
        /// <typeparam name="T6">
        /// The type of the sixth value to combine into the hash code.
        /// </typeparam>
        /// <typeparam name="T7">
        /// The type of the seventh value to combine into the hash code.
        /// </typeparam>
        /// <typeparam name="T8">
        /// The type of the eighth value to combine into the hash code.
        /// </typeparam>
        /// <param name="value1">The first value to combine into the hash code.</param>
        /// <param name="value2">The second value to combine into the hash code.</param>
        /// <param name="value3">The third value to combine into the hash code.</param>
        /// <param name="value4">The fourth value to combine into the hash code.</param>
        /// <param name="value5">The fifth value to combine into the hash code.</param>
        /// <param name="value6">The sixth value to combine into the hash code.</param>
        /// <param name="value7">The seventh value to combine into the hash code.</param>
        /// <param name="value8">The eighth value to combine into the hash code.</param>
        /// <returns>The hash code that represents the eight values.</returns>
        public static HashCode Combine<T1, T2, T3, T4, T5, T6, T7, T8>(T1? value1, T2? value2,
            T3? value3, T4? value4, T5? value5, T6? value6, T7? value7, T8? value8)
        {
            return Of(value1)
                .And(value2)
                .And(value3)
                .And(value4)
                .And(value5)
                .And(value6)
                .And(value7)
                .And(value8);
        }

        #region IEquatable<BasicInfo> Implementation

        /// <inheritdoc />
        public bool Equals(HashCode other)
        {
            return _value.Equals(other._value);
        }

        #endregion

        #region Object Overridden Methods

        /// <inheritdoc />
        public override bool Equals(object? obj)
        {
            if (obj is HashCode code)
            {
                return Equals(code);
            }

            return Equals(obj);
        }

        /// <summary>
        /// Throws <see cref="NotSupportedException" />.
        /// </summary>
        /// <returns>Does not return.</returns>
        /// <exception cref="NotSupportedException">Implicitly convert this value type to an
        /// <see cref="int" /> to get the hash code.</exception>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override int GetHashCode()
        {
            throw new NotSupportedException(
                "Implicitly convert this value type to an int to get the hash code."
            );
        }

        #endregion

        /// <summary>
        /// Combines two hash code values.
        /// </summary>
        /// <param name="h1">The first hash code value.</param>
        /// <param name="h2">The second hash code value.</param>
        /// <returns>Combined result of specified hash code values.</returns>
        private static int CombineHashCodes(int h1, int h2)
        {
            unchecked
            {
                // Code copied from System.Tuple so it must be the best way to combine hash codes
                // or at least a good one.
                return ((h1 << 5) + h1) ^ h2;
            }
        }

        /// <summary>
        /// Null-safe invocation of GetHashCode method for <typeparamref name="T" />.
        /// </summary>
        /// <typeparam name="T">The type of object to get hash code from.</typeparam>
        /// <param name="item">The value to get hash code from.</param>
        /// <returns>
        /// Returns result of item.GetHashCode method if item is not <c>null</c> or 0 otherwise.
        /// </returns>
        private static int GetHashCode<T>(T item)
        {
            return item?.GetHashCode() ?? 0;
        }

        /// <summary>
        /// Gets hash code for items in specified collection with start hash code value.
        /// </summary>
        /// <typeparam name="T">The type of items in collection.</typeparam>
        /// <param name="items">The collection to get hash code from.</param>
        /// <param name="startHashCode">An initial value for combining hash code.</param>
        /// <returns>
        /// Combined result of all items in collection. If collection is empty, default prime value
        /// would be combined with specified <paramref name="startHashCode" />.
        /// </returns>
        private static int GetHashCode<T>(IEnumerable<T> items, int startHashCode)
        {
            int temp = startHashCode;

            IEnumerator<T> enumerator = items.GetEnumerator();
            if (enumerator.MoveNext())
            {
                temp = CombineHashCodes(temp, GetHashCode(enumerator.Current));

                while (enumerator.MoveNext())
                {
                    temp = CombineHashCodes(temp, GetHashCode(enumerator.Current));
                }
            }
            else
            {
                temp = CombineHashCodes(temp, EmptyCollectionPrimeNumber);
            }

            return temp;
        }
    }
#endif
}
