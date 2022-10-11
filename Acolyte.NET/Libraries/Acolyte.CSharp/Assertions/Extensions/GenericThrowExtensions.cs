using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Acolyte.Assertions
{
    public static partial class ThrowsExtensions
    {
        /// <summary>
        /// Provides <see langword="null" /> check for every object value.
        /// </summary>
        /// <typeparam name="T">Type which extension method will apply to.</typeparam>
        /// <param name="obj">Instance to check.</param>
        /// <param name="paramName">
        /// Name of the parameter for error message. Use operator <see langword="nameof" /> to get
        /// proper parameter name.
        /// </param>
        /// <param name="assertOnPureValueTypes">
        /// Allows to throw exception if <paramref name="obj" /> value is pure value type
        /// ("pure" means that it is non-nullable type). It can be useful when you do not want to
        /// check "pure" value types. So, using this parameter you can catch assertion during debug
        /// and eliminate redundant checks (because non-nullable value types do not have null 
        /// value at all).
        /// </param>
        /// <returns>Returns passed value.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="obj" /> is <see langword="null" />. -or-
        /// <paramref name="paramName" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="assertOnPureValueTypes"/> is <see langword="true" /> and
        /// type of <paramref name="obj" /> is pure value type.
        /// </exception>
        /// <remarks>
        /// This method does not have any generic type constraints because sometimes it can be used
        /// for nullable value types, or for generic types (including F# code, for example).
        /// In such cases, this restriction removes opportunity to use <see langword="null" /> check.
        /// </remarks>
        [return: NotNull]
        public static T ThrowIfNullValue<T>(this T? obj, string paramName,
            bool assertOnPureValueTypes)
        {
            if (paramName is null)
            {
                throw new ArgumentNullException(nameof(paramName));
            }

            AssertOnPureValueTypes<T>(paramName, assertOnPureValueTypes);

            if (obj is null)
            {
                throw new ArgumentNullException(paramName);
            }

            return obj;
        }

        /// <exception cref="ArgumentNullException">
        /// <paramref name="obj" /> is <see langword="null" />. -or-
        /// <paramref name="paramName" /> is <see langword="null" />.
        /// </exception>
        /// <remarks>
        /// This method calls <see cref="ThrowIfNullValue{T}(T, string, bool)" /> with parameter
        /// assertOnPureValueTypes = <see langword="false" />.
        /// </remarks>
        /// <inheritdoc cref="ThrowIfNullValue{T}(T, string, bool)" path="//summary|//typeparam|//param|//returns" />
        [return: NotNull]
        public static T ThrowIfNullValue<T>(this T? obj, string paramName)
        {
            return obj.ThrowIfNullValue(paramName, assertOnPureValueTypes: false);
        }

        /// <summary>
        /// Provides <see langword="null" /> check for every reference type value.
        /// </summary>
        /// <inheritdoc cref="ThrowIfNullValue{T}(T, string)" path="//*[name() != 'remarks']" />
        [return: NotNull]
        public static T ThrowIfNull<T>(this T? obj, string paramName)
            where T : class?
        {
            return obj.ThrowIfNullValue(paramName);
        }

        /// <summary>
        /// Provides <see langword="null" /> check for every reference type value.
        /// This method does not return original object to avoid compiler warnings.
        /// </summary>
        /// <inheritdoc cref="ThrowIfNull{T}(T, string)" path="//typeparam|//param|//returns|//exception" />
        public static void ThrowIfNullDiscard<T>(this T? obj, string paramName)
            where T : class?
        {
            obj.ThrowIfNull(paramName);
        }

        /// <summary>
        /// Checks that value is not out of the specified range. Please, pay attention that lower
        /// and upper bound is included.
        /// </summary>
        /// <typeparam name="T">An type of value and bounds.</typeparam>
        /// <param name="value">An value to check.</param>
        /// <param name="paramName">
        /// Name of the parameter for error message. Use operator <see langword="nameof" /> to get
        /// proper parameter name.
        /// </param>
        /// <param name="includedLowerBound">An included lower bound of the range.</param>
        /// <param name="includedUpperBound">An included upper bound of the range.</param>
        /// <param name="comparer">A value comparer.</param>
        /// <returns>The original value.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="value" /> is <see langword="null" />. -or-
        /// <paramref name="paramName" /> is <see langword="null" />. -or-
        /// <paramref name="includedLowerBound" /> is <see langword="null" />. -or-
        /// <paramref name="includedUpperBound" /> is <see langword="null" />. -or-
        /// <paramref name="comparer" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="value" /> is not out of the specified range
        /// [<paramref name="includedLowerBound" />, <paramref name="includedUpperBound" />].
        /// </exception>
        /// <exception cref="ArgumentException">
        /// The type <typeparamref name="T" /> does not implement the <see cref="IComparable" /> or
        /// <see cref="IComparable{T}" /> interface.
        /// </exception>
        [return: NotNull]
        public static T ThrowIfValueIsOutOfRange<T>(
            [DisallowNull] this T value,
            [DisallowNull] string paramName,
            [DisallowNull] T includedLowerBound,
            [DisallowNull] T includedUpperBound,
            [DisallowNull] IComparer<T> comparer)
            where T : notnull
        {
            // Disallow null for nullable value types (see next comment).
            value.ThrowIfNullValue(nameof(value), assertOnPureValueTypes: false);
            paramName.ThrowIfNull(nameof(paramName));
            includedLowerBound.ThrowIfNullValue(nameof(includedLowerBound), assertOnPureValueTypes: false);
            includedUpperBound.ThrowIfNullValue(nameof(includedUpperBound), assertOnPureValueTypes: false);
            comparer.ThrowIfNull(nameof(comparer));

            // Be careful to compare nullable values because nullable comparer behaves 
            // inconsistently with operators (<, >, <=, >=):
            // https://stackoverflow.com/a/4731337/8581036
            bool isValueLessThanLowerBound = comparer.Compare(value, includedLowerBound) < 0;
            bool isValueGreaterThanUpperBound = comparer.Compare(value, includedUpperBound) > 0;

            if (isValueLessThanLowerBound || isValueGreaterThanUpperBound)
            {
                throw new ArgumentOutOfRangeException(
                    paramName,
                    value,
                    $"Value '{value.ToString()}' of type '{typeof(T).Name}' is out of " +
                    $"range [{includedLowerBound.ToString()}, {includedUpperBound.ToString()}]."
                );
            }

            return value;
        }

        /// <summary>
        /// Checks that value is not out of the specified range with default comparer of type
        /// <typeparamref name="T" />. Please, pay attention that lower and upper bound is included.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="value" /> is <see langword="null" />. -or-
        /// <paramref name="paramName" /> is <see langword="null" />. -or-
        /// <paramref name="includedLowerBound" /> is <see langword="null" />. -or-
        /// <paramref name="includedUpperBound" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="value" /> is out of the specified range
        /// [<paramref name="includedLowerBound" />, <paramref name="includedUpperBound" />].
        /// </exception>
        /// <exception cref="ArgumentException">
        /// The type <typeparamref name="T" /> does not implement the <see cref="IComparable" /> or
        /// <see cref="IComparable{T}" /> interface.
        /// </exception>
        /// <inheritdoc cref="ThrowIfValueIsOutOfRange{T}(T, string, T, T, IComparer{T})" path="//typeparam|//param|//returns" />
        [return: NotNull]
        public static T ThrowIfValueIsOutOfRange<T>(
            [DisallowNull] this T value,
            [DisallowNull] string paramName,
            [DisallowNull] T includedLowerBound,
            [DisallowNull] T includedUpperBound)
            where T : notnull
        {
            return value.ThrowIfValueIsOutOfRange(
                paramName,
                includedLowerBound,
                includedUpperBound,
                Comparer<T>.Default
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void AssertOnPureValueTypes<T>(string paramName, bool assertOnPureValueTypes)
        {
            if (assertOnPureValueTypes)
            {
                if (default(T) is not null)
                {
                    var pureValueType = typeof(T);
                    string message =
                        $"The passed parameter is a pure value type" +
                        $" [{pureValueType}] parameter.";

                    throw new ArgumentException(message, paramName);
                }
            }
        }
    }
}
