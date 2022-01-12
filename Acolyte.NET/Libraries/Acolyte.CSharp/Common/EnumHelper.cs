using System;
using System.Collections.Generic;
using System.Linq;
using Acolyte.Linq;
using Acolyte.Numeric;

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

        private static TEnum[] GetUniqueFlagValuesFor<TEnum>(Func<TEnum, bool> filter)
           where TEnum : struct, Enum
        {
            IReadOnlyList<TEnum> notUniqueValues = GetValues<TEnum>();

            return notUniqueValues
                .Where(filter)
                .ToArray();
        }

        public static TEnum[] GetUniqueFlagValues<TEnum>()
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
            return ParseDefined<TEnum>(enumValue, ignoreCase: false);
        }

        public static TEnum ParseDefined<TEnum>(string enumValue, bool ignoreCase)
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

        public static bool TryParseDefined<TEnum>(string enumValue, out TEnum result)
          where TEnum : struct, Enum
        {
            return TryParseDefined(enumValue, ignoreCase: false, out result);
        }

        public static bool TryParseDefined<TEnum>(string enumValue, bool ignoreCase, out TEnum result)
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

        public static TEnum ParseDefinedOrDefault<TEnum>(string enumValue, TEnum defaultValue)
            where TEnum : struct, Enum
        {
            return ParseDefinedOrDefault(enumValue, ignoreCase: false, defaultValue);
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
            if (Enum.TryParse(enumValue, ignoreCase, out TEnum result) &&
                result.IsDefined())
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
            return TryParseDefinedOrDefault(enumValue, ignoreCase: false, defaultValue, out result);
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
            if (Enum.TryParse(enumValue, ignoreCase, out result) && result.IsDefined())
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
