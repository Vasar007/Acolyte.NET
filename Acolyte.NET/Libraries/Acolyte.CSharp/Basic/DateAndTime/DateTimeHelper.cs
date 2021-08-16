using System;

namespace Acolyte.Basic.DateAndTime
{
    public static class DateTimeHelper
    {
        public static string SimpleFormat { get; } = "dd/MM/yyyy HH:mm:ss";


        public static string ToSimpleString(this DateTime dateTime)
        {
            return dateTime.ToString(SimpleFormat);
        }

        public static string ToLocalSimpleString(this DateTime dateTime)
        {
            return dateTime.ToLocalTime().ToSimpleString();
        }
    }
}
