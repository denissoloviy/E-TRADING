using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;

namespace E_TRADING.Common.Extensions
{
    public static class StringExtensions
    {
        public static string GetDisplayName<TSource, TProperty>(Expression<Func<TSource, TProperty>> expression)
        {
            var attribute = Attribute.GetCustomAttribute(((MemberExpression)expression.Body).Member, typeof(DisplayNameAttribute)) as DisplayNameAttribute;
            if (attribute == null)
            {
                var propInfo = ((MemberExpression)expression.Body).Member as PropertyInfo;
                return propInfo.Name;
            }
            return attribute.DisplayName;
        }
    }
}
