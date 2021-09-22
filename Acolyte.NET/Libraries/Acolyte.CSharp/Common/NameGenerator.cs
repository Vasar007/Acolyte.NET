using System.Globalization;
using Acolyte.Assertions;

namespace Acolyte.Common
{
    public static class NameGenerator
    {
        public static string MakeUniqueFromHash(string? prefix, object obj)
        {
            obj.ThrowIfNull(nameof(obj));

            int hashCode = obj.GetHashCode();
            string hashCodeAsString = hashCode.ToString(
                NumberFormatInfo.InvariantInfo
            );

            return $"{prefix}{hashCodeAsString}";
        }
    }
}
