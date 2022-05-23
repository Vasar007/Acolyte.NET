using System;
using Acolyte.Enumerations;

namespace Acolyte.Assertions
{
    public static partial class ThrowsExtensions
    {
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
    }
}
