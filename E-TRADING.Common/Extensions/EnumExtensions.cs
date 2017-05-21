using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace E_TRADING.Common.Extensions
{
    public static class EnumExtensions
    {
        public static string GetName(this Enum enumValue)
        {
            return enumValue.GetType()
                            .GetMember(enumValue.ToString())
                            .FirstOrDefault()?
                            .GetCustomAttribute<DisplayAttribute>()?.Name;
        }
    }
}
