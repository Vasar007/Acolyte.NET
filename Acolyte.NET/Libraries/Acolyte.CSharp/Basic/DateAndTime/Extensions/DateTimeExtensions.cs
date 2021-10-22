using System;

namespace Acolyte.Basic.DateAndTime
{
    public static class DateTimeExtensions
    {
        public static string ToSimpleString(this DateTime dateTime)
        {
            return dateTime.ToString(DateTimeHelper.SimpleFormat);
        }

        public static string ToLocalSimpleString(this DateTime dateTime)
        {
            return dateTime.ToLocalTime().ToSimpleString();
        }
    }
}
