using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using Acolyte.Linq;

namespace Acolyte.Common
{
    /// <summary>
    /// Contains extension methods for enumeration values.
    /// </summary>
    public static class EnumExtensions
    {
        #region Enum Attributes

        public static IReadOnlyList<TAttribute> GetEnumValueAttributes<TAttribute>(this Enum enumValue)
             where TAttribute : Attribute
        {
            Type type = enumValue.GetType();
            IReadOnlyList<MemberInfo> members = type.GetMember(enumValue.ToString());

            if (members.Count == 0) return Array.Empty<TAttribute>();

            MemberInfo member = members.First();

            return member.GetCustomAttributes(typeof(TAttribute), inherit: false)
                .Cast<TAttribute>()
                .ToReadOnlyList();
        }

        public static string GetDescription<T>(this T enumValue)
            where T : struct, Enum
        {
            IReadOnlyList<DescriptionAttribute> attributes =
                enumValue.GetEnumValueAttributes<DescriptionAttribute>();

            if (attributes.Count > 0) return attributes.First().Description;

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

            if (Enum.TryParse(enumValueStr, ignoreCase, out TEnum result)) return result;

            throw new ArgumentOutOfRangeException(
                nameof(enumValue),
                enumValue,
                $"Cannot convert value '{enumValueStr}' to enum type '{typeof(TEnum).Name}'."
            );
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

            if (Enum.TryParse(enumValueStr, ignoreCase, out result)) return true;

            // "result" is equal to default here (because of Enum.TryParse).
            return false;
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
            if (Enum.TryParse(enumValue.ToString(), ignoreCase, out TEnum result)) return result;

            return defaultValue;
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
            if (Enum.TryParse(enumValue.ToString(), ignoreCase, out result)) return true;

            result = defaultValue;
            return false;
        }

        #endregion

        #endregion

        #region Check Enum Values

        public static bool IsDefined<TEnum>(this TEnum enumValue)
            where TEnum : struct, Enum
        {
            return Enum.IsDefined(typeof(TEnum), enumValue);
        }

        #endregion
    }
}
