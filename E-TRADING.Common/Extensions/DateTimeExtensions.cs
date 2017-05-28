using System;
using System.Configuration;

namespace E_TRADING.Common.Extensions
{
    public static class DateTimeExtensions
    {
        public static string DateTimeToFormatString(this DateTime sourceTime)
        {
            return sourceTime.ToString("dd.MM.yyyy HH:mm:ss");
        }

        public static DateTime ConvertToSiteZoneFromUtc(this DateTime sourceTime)
        {
            var timeZone = ConfigurationManager.AppSettings["SiteTimeZone"];
            return TimeZoneInfo.ConvertTimeFromUtc(sourceTime,
                TimeZoneInfo.FindSystemTimeZoneById(timeZone));
        }

        public static DateTime ConvertToUtcFromSiteTimeZone(this DateTime sourceTime)
        {
            var timeZone = ConfigurationManager.AppSettings["SiteTimeZone"];
            return TimeZoneInfo.ConvertTimeToUtc(sourceTime, TimeZoneInfo.FindSystemTimeZoneById(timeZone));
        }
    }
}
