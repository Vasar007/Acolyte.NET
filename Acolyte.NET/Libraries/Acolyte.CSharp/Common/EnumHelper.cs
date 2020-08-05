using System;
using System.Collections.Generic;
using System.Linq;

namespace Acolyte.Common
{
    /// <summary>
    /// Contains common logic to work with enumeration values.
    /// </summary>
    public static class EnumHelper
    {
        #region String Parsing

        #region Parse Or Default

        public static TEnum ParseOrDefault<TEnum>(string stringValue, TEnum defaultValue)
           where TEnum : struct, Enum
        {
            return ParseOrDefault(stringValue, ignoreCase: false, defaultValue);
        }

        public static TEnum ParseOrDefault<TEnum>(string stringValue, bool ignoreCase)
         where TEnum : struct, Enum
        {
            return ParseOrDefault(stringValue, ignoreCase, defaultValue: default(TEnum));
        }

        public static TEnum ParseOrDefault<TEnum>(string stringValue, bool ignoreCase,
            TEnum defaultValue)
            where TEnum : struct, Enum
        {
            if (Enum.TryParse(stringValue, ignoreCase, out TEnum result)) return result;

            return defaultValue;
        }

        #endregion

        #region Try Parse Or Default

        public static bool TryParseOrDefault<TEnum>(string stringValue, TEnum defaultValue,
            out TEnum result)
           where TEnum : struct, Enum
        {
            return TryParseOrDefault(stringValue, ignoreCase: false, defaultValue, out result);
        }

        public static bool TryParseOrDefault<TEnum>(string stringValue, bool ignoreCase,
            out TEnum result)
         where TEnum : struct, Enum
        {
            return TryParseOrDefault(stringValue, ignoreCase, defaultValue: default, out result);
        }

        public static bool TryParseOrDefault<TEnum>(string stringValue, bool ignoreCase,
            TEnum defaultValue, out TEnum result)
            where TEnum : struct, Enum
        {
            if (Enum.TryParse(stringValue, ignoreCase, out result)) return true;

            result = defaultValue;
            return false;
        }

        #endregion

        #endregion

        #region To Object

        public static TEnum ToObject<TEnum>(int value)
            where TEnum : struct, Enum
        {
            return (TEnum) Enum.ToObject(typeof(TEnum), value);
        }

        #endregion

        #region Enum Values

        public static IReadOnlyList<TEnum> GetValues<TEnum>()
            where TEnum : struct, Enum
        {
            return Enum.GetValues(typeof(TEnum))
                .Cast<TEnum>()
                .ToList();
        }

        public static TEnum GetMaxValue<TEnum>()
            where TEnum : struct, Enum
        {
            return GetValues<TEnum>().Max();
        }

        public static TEnum GetMinValue<TEnum>()
            where TEnum : struct, Enum
        {
            return GetValues<TEnum>().Min();
        }

        #endregion

        #region Enum Conversions

        #region Parse Defined

        public static TEnum ParseDefined<TEnum>(String enumValue)
            where TEnum : struct, Enum
        {
            return ParseDefined<TEnum>(enumValue, ignoreCase: false);
        }

        public static TEnum ParseDefined<TEnum>(String enumValue, bool ignoreCase)
            where TEnum : struct, Enum
        {
            if (Enum.TryParse(enumValue, ignoreCase, out TEnum result) && result.IsDefined())
            {
                return result;
            }

            throw new ArgumentOutOfRangeException(
                nameof(enumValue),
                enumValue,
                $"Cannot parse value '{enumValue}' to enum type '{typeof(TEnum).Name}'."
            );
        }

        #endregion

        #region Try Parse Defined

        public static bool TryParseDefined<TEnum>(String enumValue, out TEnum result)
          where TEnum : struct, Enum
        {
            return TryParseDefined(enumValue, ignoreCase: false, out result);
        }

        public static bool TryParseDefined<TEnum>(String enumValue, bool ignoreCase, out TEnum result)
            where TEnum : struct, Enum
        {
            if (Enum.TryParse(enumValue, ignoreCase, out result) && result.IsDefined())
            {
                return true;
            }

            // "result" is equal to default here (because of Enum.TryParse).
            return false;
        }

        #endregion

        #region Parse Defined Or Default

        public static TEnum ParseDefinedOrDefault<TEnum>(String enumValue, TEnum defaultValue)
            where TEnum : struct, Enum
        {
            return ParseDefinedOrDefault(enumValue, ignoreCase: false, defaultValue);
        }

        public static TEnum ParseDefinedOrDefault<TEnum>(String enumValue, bool ignoreCase)
            where TEnum : struct, Enum
        {
            return ParseDefinedOrDefault(enumValue, ignoreCase, defaultValue: default(TEnum));
        }

        public static TEnum ParseDefinedOrDefault<TEnum>(String enumValue, bool ignoreCase,
            TEnum defaultValue)
            where TEnum : struct, Enum
        {
            if (Enum.TryParse(enumValue, ignoreCase, out TEnum result) &&
                result.IsDefined())
            {
                return result;
            }

            return defaultValue;
        }

        #endregion

        #region Try Parse Defined Or Default

        public static bool TryParseDefinedOrDefault<TEnum>(String enumValue, TEnum defaultValue,
            out TEnum result)
            where TEnum : struct, Enum
        {
            return TryParseDefinedOrDefault(enumValue, ignoreCase: false, defaultValue, out result);
        }

        public static bool TryParseDefinedOrDefault<TEnum>(String enumValue, bool ignoreCase,
           out TEnum result)
            where TEnum : struct, Enum
        {
            return TryParseDefinedOrDefault(enumValue, ignoreCase, defaultValue: default, out result);
        }

        public static bool TryParseDefinedOrDefault<TEnum>(String enumValue, bool ignoreCase,
            TEnum defaultValue, out TEnum result)
            where TEnum : struct, Enum
        {
            if (Enum.TryParse(enumValue, ignoreCase, out result) && result.IsDefined())
            {
                return true;
            }

            result = defaultValue;
            return false;
        }

        #endregion

        #endregion
    }
}
