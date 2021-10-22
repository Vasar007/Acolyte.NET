using System;
using System.Globalization;

namespace Acolyte.Basic.DateAndTime
{
    public static class DateTimeExtensions
    {
        private const double MsSqlTimeResolutionMs = 4.0;

        public static string ToSimpleString(this DateTime dateTime)
        {
            return dateTime.ToString(DateTimeHelper.SimpleFormat);
        }

        public static string ToLocalSimpleString(this DateTime dateTime)
        {
            return dateTime.ToLocalTime().ToSimpleString();
        }

        public static DateTime TrimMilliseconds(this DateTime dt)
        {
            long ticks = dt.Ticks - (dt.Ticks % TimeSpan.TicksPerSecond);
            return new DateTime(ticks, dt.Kind);
        }

        /// <summary>
        /// Converts UTC time obtained by remoting to UTC time on the local
        /// machine. It is necessary if the time was not specified
        /// <see cref="DateTimeKind.Utc" /> during the transfer.
        /// </summary>
        /// <param name="dateTime">"Local time" obtained by remoting.</param>
        /// <param name="localUtcOffset">
        /// UTC shift on the machine that gets the value by remoting.
        /// </param>
        /// <param name="remoteUtcOffset">
        /// UTC shift on the machine to which the value is transmitted by
        /// remoting.
        /// </param>
        /// <returns>Converted UTC time.</returns>
        public static DateTime TranslateRemotingFakeLocalTimeToUtc(
            this DateTime dateTime, TimeSpan localUtcOffset,
            TimeSpan remoteUtcOffset)
        {
            TimeSpan utcOffset = remoteUtcOffset - localUtcOffset;
            DateTime translatedTime = dateTime.Add(utcOffset);
            return new DateTime(translatedTime.Ticks, DateTimeKind.Utc);
        }

        public static TimeSpan Duration(this DateTime self, DateTime other)
        {
            return (self - other).Duration();
        }

        public static DateTime RoundUp(this DateTime dt, TimeSpan d)
        {
            var delta = (d.Ticks - (dt.Ticks % d.Ticks)) % d.Ticks;
            return new DateTime(dt.Ticks + delta, dt.Kind);
        }

        public static DateTime RoundDown(this DateTime dt, TimeSpan d)
        {
            var delta = dt.Ticks % d.Ticks;
            return new DateTime(dt.Ticks - delta, dt.Kind);
        }

        public static DateTime Max(DateTime dateTime1, DateTime dateTime2)
        {
            return new DateTime(Math.Max(dateTime1.Ticks, dateTime2.Ticks));
        }

        public static DateTime? Max(DateTime? dateTime1, DateTime? dateTime2)
        {
            if (dateTime1 == null && dateTime2 == null)
            {
                return null;
            }

            if (dateTime1 == null)
            {
                return dateTime2;
            }

            if (dateTime2 == null)
            {
                return dateTime1;
            }

            return Max((DateTime) dateTime1, (DateTime) dateTime2);
        }

        public static bool IsTheSameDay(DateTime dateTime1, DateTime dateTime2)
        {
            return dateTime1.Year == dateTime2.Year &&
                   dateTime1.Month == dateTime2.Month &&
                   dateTime1.Day == dateTime2.Day;
        }

        public static bool IsTheSameTime(DateTime dateTime1, DateTime dateTime2)
        {
            // MSSQL DateTime has only a 3.3mS resolution.
            double msDelta = (dateTime1 - dateTime2).TotalMilliseconds;
            return Math.Abs(msDelta) < MsSqlTimeResolutionMs;
        }

        public static bool IsTheSameTimeExcludeMs(DateTime dateTime1, DateTime dateTime2)
        {
            return dateTime1.Date == dateTime2.Date &&
                   dateTime1.Hour == dateTime2.Hour &&
                   dateTime1.Minute == dateTime2.Minute &&
                   dateTime1.Second == dateTime2.Second;
        }

        public static bool IsTheSameOrGreaterTime(DateTime dateTime1, DateTime dateTime2)
        {
            if (dateTime1 > dateTime2)
            {
                return true;
            }

            return IsTheSameTime(dateTime1, dateTime2);
        }

        public static bool IsTheSameTimeExcludeMilliseconds(DateTime dateTime1, DateTime dateTime2)
        {
            double deltaSeconds = (dateTime1 - dateTime2).TotalSeconds;
            return Math.Abs(deltaSeconds) < 1.0;
        }

        public static DateTime GetDateTimeWithoutMilliseconds(DateTime time)
        {
            return new DateTime(time.Year, time.Month, time.Day, time.Hour, time.Minute, time.Second, 0, time.Kind);
        }

        public static bool IsDayTime(DateTime dateTime, TimeSpan time)
        {
            return dateTime.Hour == time.Hours && dateTime.Minute == time.Minutes;
        }

        public static string ToInvariantCultureString(this DateTime value)
        {
            return value.ToString(CultureInfo.InvariantCulture);
        }

        public static string ToInvariantShortDateString(this DateTime value)
        {
            return value.ToString("d", CultureInfo.InvariantCulture);
        }

        public static DateTime GetWithoutMilliseconds(DateTime dateTime)
        {
            return dateTime.AddMilliseconds(-dateTime.Millisecond);
        }
    }
}
