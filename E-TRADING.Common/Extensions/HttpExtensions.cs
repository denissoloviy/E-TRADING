using System.Web.Mvc;

namespace E_TRADING.Common.Extensions
{
    public static class HttpExtensions
    {
        public static string ActionIsActive(this ViewContext viewContext, string action,
            string cssClass = "active")
        {
            return viewContext?.RouteData?.Values["Action"]?.ToString().ToLower() == action?.ToLower() ? cssClass : "";
        }

        public static string ControllerIsActive(this ViewContext viewContext, string controller,
            string cssClass = "active")
        {
            return viewContext?.RouteData?.Values["Controller"]?.ToString().ToLower() == controller?.ToLower()
                ? cssClass
                : "";
        }

        public static string RouteIsActive(this ViewContext viewContext, string action, string controller,
            string cssClass = "active")
        {
            return viewContext?.RouteData?.Values["Action"]?.ToString().ToLower() == action?.ToLower()
                   && viewContext?.RouteData?.Values["Controller"]?.ToString().ToLower() == controller?.ToLower()
                ? cssClass
                : "";
        }
    }
}
