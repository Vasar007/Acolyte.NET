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
    }
}
