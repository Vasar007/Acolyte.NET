namespace Acolyte.Common
{
    public static class StringExtensions
    {
        public static string ToStringNullSafe<T>(this T? value)
            where T : class
        {
            return value.ToStringNullSafe(defaultValue: "NULL");
        }

        public static string ToStringNullSafe<T>(this T? value, string defaultValue)
            where T : class
        {
            return value == null
                ? defaultValue
                : value.ToString();
        }

        public static string ToStringNullSafe<T>(this T? value)
            where T : struct
        {
            return value.ToStringNullSafe(defaultValue: "NULL");
        }

        public static string ToStringNullSafe<T>(this T? value, string defaultValue)
            where T : struct
        {
            return value == null
                ? defaultValue
                : value.ToString();
        }
    }
}
