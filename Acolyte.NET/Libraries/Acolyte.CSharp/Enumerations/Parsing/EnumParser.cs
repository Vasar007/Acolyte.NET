using System;

namespace Acolyte.Enumerations.Parsing
{
    public static class EnumParser
    {
        public static bool DefaultIgnoreCase { get; } = false;

        #region Parse

        public static TEnum Parse<TEnum>(string enumValue)
          where TEnum : struct, Enum
        {
            return Parse<TEnum>(enumValue, DefaultIgnoreCase);
        }

        public static TEnum Parse<TEnum>(string enumValue, bool ignoreCase)
            where TEnum : struct, Enum
        {
            if (TryParseOrDefault(enumValue, ignoreCase, out TEnum result))
            {
                return result;
            }

            throw new ArgumentOutOfRangeException(
                nameof(enumValue),
                enumValue,
                $"Cannot parse value [{enumValue}] to enum type '{typeof(TEnum).Name}'."
            );
        }

        #endregion

        #region Parse Unsafe

        public static object ParseUnsafe(Type enumType, string enumValue)
        {
            return ParseUnsafe(enumType, enumValue, DefaultIgnoreCase);
        }

        public static object ParseUnsafe(Type enumType, string enumValue, bool ignoreCase)
        {
            return Enum.Parse(enumType, enumValue, ignoreCase);
        }

        #endregion

        #region Parse Or Default

        public static TEnum ParseOrDefault<TEnum>(string enumValue, TEnum defaultValue)
           where TEnum : struct, Enum
        {
            return ParseOrDefault(enumValue, DefaultIgnoreCase, defaultValue);
        }

        public static TEnum ParseOrDefault<TEnum>(string enumValue, bool ignoreCase)
         where TEnum : struct, Enum
        {
            return ParseOrDefault(enumValue, ignoreCase, defaultValue: default(TEnum));
        }

        public static TEnum ParseOrDefault<TEnum>(string enumValue, bool ignoreCase,
            TEnum defaultValue)
            where TEnum : struct, Enum
        {
            TryParseOrDefault(enumValue, ignoreCase, defaultValue, out TEnum result);

            // "result" will be equal to default here if "enumValue" is
            // invalid enum (because of "TryParseOrDefault").
            return result;
        }

        #endregion

        #region Try Parse Or Default

        public static bool TryParseOrDefault<TEnum>(string enumValue, TEnum defaultValue,
            out TEnum result)
           where TEnum : struct, Enum
        {
            return TryParseOrDefault(enumValue, DefaultIgnoreCase, defaultValue, out result);
        }

        public static bool TryParseOrDefault<TEnum>(string enumValue, bool ignoreCase,
            out TEnum result)
         where TEnum : struct, Enum
        {
            return TryParseOrDefault(enumValue, ignoreCase, defaultValue: default, out result);
        }

        public static bool TryParseOrDefault<TEnum>(string enumValue, bool ignoreCase,
            TEnum defaultValue, out TEnum result)
            where TEnum : struct, Enum
        {
            if (Enum.TryParse(enumValue, ignoreCase, out result)) return true;

            result = defaultValue;
            return false;
        }

        #endregion

        #region Parse Defined

        public static TEnum ParseDefined<TEnum>(string enumValue)
            where TEnum : struct, Enum
        {
            return ParseDefined<TEnum>(enumValue, DefaultIgnoreCase);
        }

        public static TEnum ParseDefined<TEnum>(string enumValue, bool ignoreCase)
            where TEnum : struct, Enum
        {
            if (TryParseOrDefault(enumValue, ignoreCase, out TEnum result) &&
                result.IsDefinedOrFlag())
            {
                return result;
            }

            throw new ArgumentOutOfRangeException(
                nameof(enumValue),
                enumValue,
                $"Cannot parse value [{enumValue}] to enum type '{typeof(TEnum).Name}'."
            );
        }

        #endregion

        #region Try Parse Defined

        public static bool TryParseDefined<TEnum>(string enumValue, out TEnum result)
          where TEnum : struct, Enum
        {
            return TryParseDefined(enumValue, DefaultIgnoreCase, out result);
        }

        public static bool TryParseDefined<TEnum>(string enumValue, bool ignoreCase,
            out TEnum result)
            where TEnum : struct, Enum
        {
            return TryParseDefinedOrDefault(enumValue, ignoreCase, out result);
        }

        #endregion

        #region Parse Defined Or Default

        public static TEnum ParseDefinedOrDefault<TEnum>(string enumValue, TEnum defaultValue)
            where TEnum : struct, Enum
        {
            return ParseDefinedOrDefault(enumValue, DefaultIgnoreCase, defaultValue);
        }

        public static TEnum ParseDefinedOrDefault<TEnum>(string enumValue, bool ignoreCase)
            where TEnum : struct, Enum
        {
            return ParseDefinedOrDefault(enumValue, ignoreCase, defaultValue: default(TEnum));
        }

        public static TEnum ParseDefinedOrDefault<TEnum>(string enumValue, bool ignoreCase,
            TEnum defaultValue)
            where TEnum : struct, Enum
        {
            if (TryParseOrDefault(enumValue, ignoreCase, defaultValue, out TEnum result) &&
                result.IsDefinedOrFlag())
            {
                return result;
            }

            return defaultValue;
        }

        #endregion

        #region Try Parse Defined Or Default

        public static bool TryParseDefinedOrDefault<TEnum>(string enumValue, TEnum defaultValue,
            out TEnum result)
            where TEnum : struct, Enum
        {
            return TryParseDefinedOrDefault(enumValue, DefaultIgnoreCase, defaultValue, out result);
        }

        public static bool TryParseDefinedOrDefault<TEnum>(string enumValue, bool ignoreCase,
           out TEnum result)
            where TEnum : struct, Enum
        {
            return TryParseDefinedOrDefault(enumValue, ignoreCase, defaultValue: default, out result);
        }

        public static bool TryParseDefinedOrDefault<TEnum>(string enumValue, bool ignoreCase,
            TEnum defaultValue, out TEnum result)
            where TEnum : struct, Enum
        {
            if (TryParseOrDefault(enumValue, ignoreCase, defaultValue, out result) &&
                result.IsDefinedOrFlag())
            {
                return true;
            }

            result = defaultValue;
            return false;
        }

        #endregion
    }
}
