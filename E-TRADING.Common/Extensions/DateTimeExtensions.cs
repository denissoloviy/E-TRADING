using System;

namespace E_TRADING.Common.Extensions
{
    public static class DateTimeExtensions
    {
        public static string DateTimeToFormatString(this DateTime sourceTime)
        {
            return sourceTime.ToString("dd.MM.yyyy HH:mm:ss");
        }
    }
}
