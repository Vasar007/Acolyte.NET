using System;
using Acolyte.Assertions;

namespace Acolyte.Common.Extensions
{
    public static class ExtensionMethodsConvertible
    {
        public static bool HasFlag<TConvertible>(this TConvertible value, TConvertible flag)
           where TConvertible : IConvertible
        {
            value.ThrowIfNullValue(nameof(value), assertOnPureValueTypes: false);
            flag.ThrowIfNullValue(nameof(flag), assertOnPureValueTypes: false);

            // Get the type code of the input value.
            TypeCode typeCode = value.GetTypeCode();

            // If the underlying type of the flag is signed.
            if (typeCode == TypeCode.SByte || typeCode == TypeCode.Int16 || typeCode == TypeCode.Int32 || typeCode == TypeCode.Int64)
                return (Convert.ToInt64(value) & Convert.ToInt64(flag)) != 0;

            // If the underlying type of the flag is unsigned.
            if (typeCode == TypeCode.Byte || typeCode == TypeCode.UInt16 || typeCode == TypeCode.UInt32 || typeCode == TypeCode.UInt64)
                return (Convert.ToUInt64(value) & Convert.ToUInt64(flag)) != 0;

            // Unsupported flag type.
            throw new NotSupportedException($"The comparison of the type {flag.GetType().Name} is not implemented.");
        }
    }
}
