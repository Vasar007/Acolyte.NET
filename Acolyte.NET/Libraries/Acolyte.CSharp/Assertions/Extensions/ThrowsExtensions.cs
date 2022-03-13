using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Acolyte.Common;
using Acolyte.Enumerations;
using Acolyte.Exceptions;

namespace Acolyte.Assertions
{
    // TODO: split this class on smaller pieces.

    /// <summary>
    /// Contains extension methods to check certain conditions.
    /// </summary>
    public static class ThrowsExtensions
    {
        #region Null Checks For Generic Types

        /// <summary>
        /// Provides <see langword="null" /> check for every reference type value.
        /// </summary>
        /// <typeparam name="T">Type which extension method will apply to.</typeparam>
        /// <param name="obj">Instance to check.</param>
        /// <param name="paramName">
        /// Name of the parameter for error message. Use operator <see langword="nameof" /> to get
        /// proper parameter name.
        /// </param>
        /// <returns>Returns passed value.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="obj" /> is <see langword="null" />. -or-
        /// <paramref name="paramName" /> is <see langword="null" />.
        /// </exception>
        [return: NotNull]
        public static T ThrowIfNull<T>(this T? obj, string paramName)
            where T : class?
        {
            if (paramName is null)
            {
                throw new ArgumentNullException(nameof(paramName));
            }

            if (obj is null)
            {
                throw new ArgumentNullException(paramName);
            }

            return obj;
        }

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
        /// <remarks>
        /// This method does not have any generic type constraints because sometimes it can be used
        /// for nullable value types, or for generic types (including F# code, for example).
        /// In such cases, this restriction removes opportunity to use <see langword="null" /> check.
        /// </remarks>
        [return: NotNull]
        public static T ThrowIfNullValue<T>(this T? obj, string paramName,
            bool assertOnPureValueTypes)
        {
            paramName.ThrowIfNull(nameof(paramName));

            if (assertOnPureValueTypes)
            {
                if (default(T) is not null)
                {
                    throw new ArgumentException(
                        "The passed parameter is a pure value type parameter.", paramName
                    );
                }
            }

            if (obj is null)
            {
                throw new ArgumentNullException(paramName);
            }

            return obj;
        }

        /// <summary>
        /// Provides <see langword="null" /> check for every object value.
        /// </summary>
        /// <typeparam name="T">Type which extension method will apply to.</typeparam>
        /// <param name="obj">Instance to check.</param>
        /// <param name="paramName">
        /// Name of the parameter for error message. Use operator <see langword="nameof" /> to get
        /// proper parameter name.
        /// </param>
        /// <returns>Returns passed value.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="obj" /> is <see langword="null" />. -or-
        /// <paramref name="paramName" /> is <see langword="null" />.
        /// </exception>
        /// <remarks>
        /// This method calls <see cref="ThrowIfNullValue{T}(T, string, bool)" /> with parameter
        /// assertOnPureValueTypes = <see langword="false" />
        /// </remarks>
        public static T ThrowIfNullValue<T>(this T? obj, string paramName)
        {
            return obj.ThrowIfNullValue(paramName, assertOnPureValueTypes: false);
        }

        #endregion

        #region Checks For Strings

        /// <summary>
        /// Checks if the string is <see langword="null" /> or empty.
        /// </summary>
        /// <param name="str">String to check.</param>
        /// <param name="paramName">
        /// Name of the parameter for error message. Use operator <see langword="nameof" /> to get
        /// proper parameter name.
        /// </param>
        /// <returns>The original string.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="str" /> is <see langword="null" />. -or-
        /// <paramref name="paramName" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="str" /> presents empty string.
        /// </exception>
        [return: NotNull]
        public static string ThrowIfNullOrEmpty(this string? str, string paramName)
        {
            paramName.ThrowIfNull(nameof(paramName));

            if (str is null)
            {
                throw new ArgumentNullException(paramName);
            }
            if (str.Length == 0)
            {
                throw new ArgumentException($"{paramName} presents empty string.", paramName);
            }

            return str;
        }

        /// <summary>
        /// Checks if the string is <see langword="null" />, empty or contains only whitespaces.
        /// </summary>
        /// <param name="str">String to check.</param>
        /// <param name="paramName">
        /// Name of the parameter for error message. Use operator <see langword="nameof" /> to get
        /// proper parameter name.
        /// </param>
        /// <returns>The original string.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="str" /> is <see langword="null" />. -or-
        /// <paramref name="paramName" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="str" /> presents empty string or contains only whitespaces.
        /// </exception>
        [return: NotNull]
        public static string ThrowIfNullOrWhiteSpace(this string? str, string paramName)
        {
            paramName.ThrowIfNull(nameof(paramName));

            if (str is null)
            {
                throw new ArgumentNullException(paramName);
            }
            if (str.Length == 0)
            {
                throw new ArgumentException($"{paramName} presents empty string.", paramName);
            }
            if (str.All(char.IsWhiteSpace))
            {
                throw new ArgumentException($"{paramName} contains only whitespaces.", paramName);
            }

            return str;
        }

        #endregion

        #region Checks For Collections

        /// <summary>
        /// Checks if collection is <see langword="null" /> or empty.
        /// </summary>
        /// <typeparam name="T">Internal type of <paramref name="collection" />.</typeparam>
        /// <param name="collection">Collection to check.</param>
        /// <param name="paramName">
        /// Name of the parameter for error message. Use operator <see langword="nameof" /> to get
        /// proper parameter name.
        /// </param>
        /// <returns>
        /// Returns <paramref name="collection" /> without any changes if collection is not
        /// <see langword="null" /> or empty; otherwise throws exception.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="collection" /> is <see langword="null" />. -or-
        /// <paramref name="paramName" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="collection" /> contains no elements.
        /// </exception>
        public static IEnumerable<T> ThrowIfNullOrEmpty<T>(this IEnumerable<T>? collection,
            string paramName)
        {
            paramName.ThrowIfNull(nameof(paramName));

            if (collection is null)
            {
                throw new ArgumentNullException(paramName);
            }
            if (!collection.Any())
            {
                throw new ArgumentException($"{paramName} contains no elements.", paramName);
            }

            return collection;
        }


        /// <inheritdoc cref="ThrowIfNullOrEmpty{T}(IEnumerable{T}?, string)" />
        public static IReadOnlyCollection<T> ThrowIfNullOrEmpty<T>(
            this IReadOnlyCollection<T>? collection, string paramName)
        {
            var cast = collection?.AsEnumerable();
            cast.ThrowIfNull(paramName);

            if (collection!.Count == 0)
            {
                throw new ArgumentException($"{paramName} contains no elements.", paramName);
            }

            return collection!;
        }

        /// <inheritdoc cref="ThrowIfNullOrEmpty{T}(IEnumerable{T}?, string)" />
        public static IReadOnlyList<T> ThrowIfNullOrEmpty<T>(
           this IReadOnlyList<T>? collection, string paramName)
        {
            var cast = (IReadOnlyCollection<T>?) collection;
            cast.ThrowIfNullOrEmpty(paramName);

            return collection!;
        }

        /// <inheritdoc cref="ThrowIfNullOrEmpty{T}(IEnumerable{T}?, string)" />
        public static IReadOnlyDictionary<TKey, TValue> ThrowIfNullOrEmpty<TKey, TValue>(
           this IReadOnlyDictionary<TKey, TValue>? collection, string paramName)
        {
            var cast = (IReadOnlyCollection<KeyValuePair<TKey, TValue>>?) collection;
            cast.ThrowIfNullOrEmpty(paramName);

            return collection!;
        }

        #endregion

        #region Checks For Guids

        /// <summary>
        /// Checks if the <see cref="Guid" /> is empty.
        /// </summary>
        /// <param name="guid"><see cref="Guid" /> to check.</param>
        /// <param name="paramName">
        /// Name of the parameter for error message. Use operator <see langword="nameof" /> to get
        /// proper parameter name.
        /// </param>
        /// <returns>The original guid.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="paramName" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="guid" /> presents empty guid.
        /// </exception>
        public static Guid ThrowIfEmpty(this Guid guid, string paramName)
        {
            paramName.ThrowIfNull(nameof(paramName));

            if (guid.IsEmpty())
            {
                throw new ArgumentException($"{paramName} is empty.", paramName);
            }

            return guid;
        }

        #endregion

        #region Checks For Tasks

        /// <summary>
        /// Checks task state and throws exception if task is in the
        /// <see cref="TaskStatus.Faulted" /> state.
        /// </summary>
        /// <param name="task">Task to check.</param>
        /// <returns>The original task.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="task" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="task" /> is not completed yet (that is, the task is in one of the three
        /// final states: <see cref="TaskStatus.RanToCompletion" />,
        /// <see cref="TaskStatus.Faulted" />, or <see cref="TaskStatus.Canceled" />).
        /// </exception>
        /// <exception cref="TaskFaultedException">
        /// <paramref name="task" /> is faulted (that is, the task is in the 
        /// <see cref="TaskStatus.Faulted" /> state).
        /// </exception>
        [return: NotNull]
        public static Task ThrowIfFaulted(this Task task)
        {
            _ = task.ThrowIfNull(nameof(task));

            if (!task.IsCompleted)
            {
                throw new ArgumentException("Task is not completed yet.", nameof(task));
            }

            if (task.IsFaulted)
            {
                throw new TaskFaultedException("Task is faulted.", task.Exception);
            }

            return task;
        }

        #endregion

        #region Checks For Enums

        /// <summary>
        /// Checks that enumeration value is defined.
        /// </summary>
        /// <typeparam name="TEnum">An enumeration type which provides values for check.</typeparam>
        /// <param name="enumValue">An enumeration value to check.</param>
        /// <param name="paramName">
        /// Name of the parameter for error message. Use operator <see langword="nameof" /> to get
        /// proper parameter name.
        /// </param>
        /// <param name="assertOnFlags">
        /// If this parameter is equal to <see langword="true" />, check will ignore
        /// <see cref="FlagsAttribute" />.
        /// </param>
        /// <returns>The original enumeration value.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="paramName" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="enumValue" /> is not defined for the <typeparamref name="TEnum" /> type.
        /// </exception>
        public static TEnum ThrowIfEnumValueIsUndefined<TEnum>(this TEnum enumValue,
            string paramName, bool assertOnFlags)
            where TEnum : struct, Enum
        {
            paramName.ThrowIfNull(nameof(paramName));

            // Assert on enumeration with FlagsAttribute could be dangerous
            // because such enumeration can have undefined but valid values.
            if (!enumValue.IsDefined() && (assertOnFlags || !EnumHelper.HasFlagsAttribute<TEnum>()))
            {
                throw new ArgumentOutOfRangeException(
                    paramName,
                    enumValue,
                    $"Enum value '{enumValue.ToString()}' of type " +
                    $"'{typeof(TEnum).Name}' is undefined."
                );
            }

            return enumValue;
        }

        /// <summary>
        /// Checks that enumeration value is defined. If enumeration has
        /// <see cref="FlagsAttribute" />, check will be skipped.
        /// </summary>
        /// <typeparam name="TEnum">An enumeration type which provides values for check.</typeparam>
        /// <param name="enumValue">An enumeration value to check.</param>
        /// <param name="paramName">
        /// Name of the parameter for error message. Use operator <see langword="nameof" /> to get
        /// proper parameter name.
        /// </param>
        /// <returns>The original enumeration value.</returns>
        /// <inheritdoc cref="ThrowIfEnumValueIsUndefined{TEnum}(TEnum, string, bool)" />
        public static TEnum ThrowIfEnumValueIsUndefined<TEnum>(this TEnum enumValue,
            string paramName)
            where TEnum : struct, Enum
        {
            return enumValue.ThrowIfEnumValueIsUndefined(paramName, assertOnFlags: false);
        }

        #endregion

        #region Out Of Range Checks For Generic Types

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
        /// <typeparam name="T">An type of value and bounds.</typeparam>
        /// <param name="value">An value to check.</param>
        /// <param name="paramName">
        /// Name of the parameter for error message. Use operator <see langword="nameof" /> to get
        /// proper parameter name.
        /// </param>
        /// <param name="includedLowerBound">An included lower bound of the range.</param>
        /// <param name="includedUpperBound">An included upper bound of the range.</param>
        /// <returns>The original value.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="value" /> is <see langword="null" />. -or-
        /// <paramref name="paramName" /> is <see langword="null" />. -or-
        /// <paramref name="includedLowerBound" /> is <see langword="null" />. -or-
        /// <paramref name="includedUpperBound" /> is <see langword="null" />..
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="value" /> is out of the specified range
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

        #endregion
    }
}
