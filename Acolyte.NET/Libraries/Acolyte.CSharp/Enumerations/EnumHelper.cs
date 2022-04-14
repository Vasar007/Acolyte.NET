using System;
using System.Collections.Generic;
using System.Linq;
using Acolyte.Linq;
using Acolyte.Numeric;

namespace Acolyte.Enumerations
{
    /// <summary>
    /// Contains common logic to work with enumeration values.
    /// </summary>
    public static class EnumHelper
    {
        public static bool DefaultIgnoreCase { get; } = false;

        #region String Parsing

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

        public static object Parse(Type enumType, string enumValue)
        {
            return Parse(enumType, enumValue, DefaultIgnoreCase);
        }

        public static object Parse(Type enumType, string enumValue, bool ignoreCase)
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

        #endregion

        #region To Object

        public static TEnum ToObject<TEnum>(object value)
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
                .ToReadOnlyList();
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

        public static TEnum GetAllFlagValues<TEnum>()
            where TEnum : struct, Enum
        {
            var uniqueFlags = GetUniqueFlagValues<TEnum>();
            if (uniqueFlags.IsNullOrEmpty())
                throw new InvalidOperationException($"Failed to process enum of type [{typeof(TEnum).Name}]: it is enum type without {nameof(FlagsAttribute)}.");

            Type underlyingType = Enum.GetUnderlyingType(typeof(TEnum));
            TypeCode typeCode = Type.GetTypeCode(underlyingType);

            object allValue;
            unchecked
            {
                allValue = typeCode switch
                {
                    TypeCode.SByte => uniqueFlags.Select(e => Convert.ToSByte(e)).Aggregate((seed, flag) => (sbyte) (seed | flag)),
                    TypeCode.Int16 => uniqueFlags.Select(e => Convert.ToInt16(e)).Aggregate((seed, flag) => (short) (seed | flag)),
                    TypeCode.Int32 => uniqueFlags.Select(e => Convert.ToInt32(e)).Aggregate((seed, flag) => seed | flag),
                    TypeCode.Int64 => uniqueFlags.Select(e => Convert.ToInt64(e)).Aggregate((seed, flag) => seed | flag),

                    TypeCode.Byte => uniqueFlags.Select(e => Convert.ToByte(e)).Aggregate((seed, flag) => (byte) (seed | flag)),
                    TypeCode.UInt16 => uniqueFlags.Select(e => Convert.ToUInt16(e)).Aggregate((seed, flag) => (ushort) (seed | flag)),
                    TypeCode.UInt32 => uniqueFlags.Select(e => Convert.ToUInt32(e)).Aggregate((seed, flag) => seed | flag),
                    TypeCode.UInt64 => uniqueFlags.Select(e => Convert.ToUInt64(e)).Aggregate((seed, flag) => seed | flag),

                    _ => throw new NotSupportedException($"Failed to process enum of type [{typeof(TEnum).Name}]. Backing type [{typeCode.ToString()}] is not supported.")
                };
            }

            return ToObject<TEnum>(allValue);
        }

        private static IReadOnlyList<TEnum> GetUniqueFlagValuesFor<TEnum>(Func<TEnum, bool> filter)
           where TEnum : struct, Enum
        {
            IReadOnlyList<TEnum> notUniqueValues = GetValues<TEnum>();

            return notUniqueValues
                .Where(filter)
                .ToReadOnlyList();
        }

        public static IReadOnlyList<TEnum> GetUniqueFlagValues<TEnum>()
            where TEnum : struct, Enum
        {
            Type underlyingType = Enum.GetUnderlyingType(typeof(TEnum));
            TypeCode typeCode = Type.GetTypeCode(underlyingType);

            return typeCode switch
            {
                TypeCode.SByte => GetUniqueFlagValuesFor<TEnum>(value => MathHelper.IsPowerOf2(Convert.ToSByte(value))),
                TypeCode.Int16 => GetUniqueFlagValuesFor<TEnum>(value => MathHelper.IsPowerOf2(Convert.ToInt16(value))),
                TypeCode.Int32 => GetUniqueFlagValuesFor<TEnum>(value => MathHelper.IsPowerOf2(Convert.ToInt32(value))),
                TypeCode.Int64 => GetUniqueFlagValuesFor<TEnum>(value => MathHelper.IsPowerOf2(Convert.ToInt64(value))),

                TypeCode.Byte => GetUniqueFlagValuesFor<TEnum>(value => MathHelper.IsPowerOf2(Convert.ToByte(value))),
                TypeCode.UInt16 => GetUniqueFlagValuesFor<TEnum>(value => MathHelper.IsPowerOf2(Convert.ToUInt16(value))),
                TypeCode.UInt32 => GetUniqueFlagValuesFor<TEnum>(value => MathHelper.IsPowerOf2(Convert.ToUInt32(value))),
                TypeCode.UInt64 => GetUniqueFlagValuesFor<TEnum>(value => MathHelper.IsPowerOf2(Convert.ToUInt64(value))),

                _ => throw new NotSupportedException($"Failed to process enum of type [{typeof(TEnum).Name}]. Backing type [{typeCode.ToString()}] is not supported.")
            };
        }

        #endregion

        #region Enum Conversions

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

        #endregion

        #region Attributes

        public static bool HasFlagsAttribute<TEnum>()
            where TEnum : struct, Enum
        {
            Type enumType = typeof(TEnum);
            return enumType.IsDefined(typeof(FlagsAttribute), inherit: false);
        }

        #endregion
    }
}
