using System.Web.Mvc;

namespace E_TRADING.Common.Extensions
{
    public static class HttpExtensions
    {
        public static string ActionIsActive(this ViewContext viewContext, string action)
        {
            return viewContext?.RouteData?.Values["Action"]?.ToString().ToLower() == action?.ToLower() ? "active" : "";
        }
    }
}
