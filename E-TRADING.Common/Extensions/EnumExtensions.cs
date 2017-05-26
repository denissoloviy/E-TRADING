using E_TRADING.Common.OrderStatuses;
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
                            .FirstOrDefault()
                            .GetCustomAttribute<DisplayAttribute>().Name;
        }

        public static OrderStatusType GetStatusType(this OrderStatus enumValue)
        {
            if (StatusTypes.SuccessedStatuses.Contains(enumValue))
            {
                return OrderStatusType.Successed;
            }
            else if (StatusTypes.WaitingStatuses.Contains(enumValue))
            {
                return OrderStatusType.Waiting;
            }
            return OrderStatusType.Failed;
        }

        public static string GetCssClassName(this OrderStatusType enumValue)
        {
            switch (enumValue)
            {
                case OrderStatusType.Waiting:
                    return "bg-warning";
                case OrderStatusType.Successed:
                    return "bg-success";
                case OrderStatusType.Failed:
                default:
                    return "bg-danger";
            }
        }
    }
}
