using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using Acolyte.Linq;

namespace Acolyte.Enumerations
{
    /// <summary>
    /// Contains extension methods for enumeration values.
    /// </summary>
    public static class EnumExtensions
    {
        #region Check Enum Values

        public static bool IsDefined<TEnum>(this TEnum enumValue)
            where TEnum : struct, Enum
        {
            return Enum.IsDefined(typeof(TEnum), enumValue);
        }

        public static bool IsDefinedOrFlag<TEnum>(this TEnum enumValue)
          where TEnum : struct, Enum
        {
            return enumValue.IsDefined() || EnumHelper.HasFlagsAttribute<TEnum>();
        }

        public static bool HasOnlyOneFlag<TEnum>(this TEnum value)
            where TEnum : struct, Enum
        {
            IReadOnlyList<TEnum> flagValues = EnumHelper.GetUniqueFlagValues<TEnum>();
            return flagValues.Contains(value);
        }

        public static bool HasAnyFlag<TEnum>(this TEnum value, params TEnum[] flagsToCheck)
            where TEnum : struct, Enum
        {
            return flagsToCheck.Any(flag => value.HasFlag(flag));
        }

        #endregion

        #region Enum Values

        public static IReadOnlyList<TEnum> FlagsToCollection<TEnum>(this TEnum value)
          where TEnum : struct, Enum
        {
            IReadOnlyList<TEnum> flagValues = EnumHelper.GetUniqueFlagValues<TEnum>();
            return flagValues
                .Where(flagValue => value.HasFlag(flagValue))
                .ToReadOnlyList();
        }

        #endregion

        #region Enum Attributes

        public static IReadOnlyList<TAttribute> GetEnumValueAttributes<TAttribute>(
            this Enum enumValue)
             where TAttribute : Attribute
        {
            Type type = enumValue.GetType();
            IReadOnlyList<MemberInfo> members = type.GetMember(enumValue.ToString());

            if (members.Count == 0) return Array.Empty<TAttribute>();

            MemberInfo member = members[0];

            return member.GetCustomAttributes(typeof(TAttribute), inherit: false)
                .Cast<TAttribute>()
                .ToReadOnlyList();
        }

        public static string GetDescription<TEnum>(this TEnum enumValue)
            where TEnum : struct, Enum
        {
            IReadOnlyList<DescriptionAttribute> attributes =
                enumValue.GetEnumValueAttributes<DescriptionAttribute>();

            if (attributes.Count > 0) return attributes[0].Description;

            return enumValue.ToString();
        }

        #endregion

        #region Enum Conversions

        #region Convert

        public static TEnum Convert<TEnum>(this Enum enumValue)
            where TEnum : struct, Enum
        {
            return enumValue.Convert<TEnum>(ignoreCase: false);
        }

        public static TEnum Convert<TEnum>(this Enum enumValue, bool ignoreCase)
            where TEnum : struct, Enum
        {
            string enumValueStr = enumValue.ToString();

            return EnumHelper.ParseDefined<TEnum>(enumValueStr, ignoreCase);
        }

        #endregion

        #region Try Convert

        public static bool TryConvert<TEnum>(this Enum enumValue, out TEnum result)
          where TEnum : struct, Enum
        {
            return enumValue.TryConvert(ignoreCase: false, out result);
        }

        public static bool TryConvert<TEnum>(this Enum enumValue, bool ignoreCase, out TEnum result)
            where TEnum : struct, Enum
        {
            string enumValueStr = enumValue.ToString();

            return EnumHelper.TryParseDefined(enumValueStr, ignoreCase, out result);
        }

        #endregion

        #region Convert Or Default

        public static TEnum ConvertOrDefault<TEnum>(this Enum enumValue, TEnum defaultValue)
            where TEnum : struct, Enum
        {
            return enumValue.ConvertOrDefault(ignoreCase: false, defaultValue);
        }

        public static TEnum ConvertOrDefault<TEnum>(this Enum enumValue, bool ignoreCase)
            where TEnum : struct, Enum
        {
            return enumValue.ConvertOrDefault(ignoreCase, defaultValue: default(TEnum));
        }

        public static TEnum ConvertOrDefault<TEnum>(this Enum enumValue, bool ignoreCase,
            TEnum defaultValue)
            where TEnum : struct, Enum
        {
            string enumValueStr = enumValue.ToString();

            return EnumHelper.ParseDefinedOrDefault(enumValueStr, ignoreCase, defaultValue);
        }

        #endregion

        #region Try Convert Or Default

        public static bool TryConvertOrDefault<TEnum>(this Enum enumValue, TEnum defaultValue,
            out TEnum result)
            where TEnum : struct, Enum
        {
            return enumValue.TryConvertOrDefault(ignoreCase: false, defaultValue, out result);
        }

        public static bool TryConvertOrDefault<TEnum>(this Enum enumValue, bool ignoreCase,
           out TEnum result)
            where TEnum : struct, Enum
        {
            return enumValue.TryConvertOrDefault(ignoreCase, defaultValue: default, out result);
        }

        public static bool TryConvertOrDefault<TEnum>(this Enum enumValue, bool ignoreCase,
            TEnum defaultValue, out TEnum result)
            where TEnum : struct, Enum
        {
            string enumValueStr = enumValue.ToString();

            return EnumHelper.TryParseDefinedOrDefault(
                enumValueStr, ignoreCase, defaultValue, out result
            );
        }

        #endregion

        #endregion

        #region Defined Values

        public static TEnum GetDefinedValueOrDefault<TEnum>(this TEnum enumValue,
            TEnum defaultValue)
            where TEnum : struct, Enum
        {
            // Flags are treated as valid values.
            if (enumValue.IsDefinedOrFlag())
                return enumValue;

            return defaultValue;
        }

        #endregion
    }
}
