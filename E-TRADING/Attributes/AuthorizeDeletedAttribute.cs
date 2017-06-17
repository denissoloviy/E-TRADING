using E_TRADING.Common;
using E_TRADING.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_TRADING.Web.Attributes
{
    public class AuthorizeDeletedAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (!base.AuthorizeCore(httpContext))
            {
                return false;
            }
            var db = new ApplicationDbContext();
            var userName = httpContext.User.Identity.Name;
            if (httpContext.User.IsInRole(UserRole.Buyer))
            {
                var user = db.Buyers.FirstOrDefault(x => x.User.UserName == userName);
                if (user == null || user.IsDeleted)
                {
                    return false;
                }
            }
            else if (httpContext.User.IsInRole(UserRole.Seller))
            {
                var user = db.Sellers.FirstOrDefault(x => x.User.UserName == userName);
                if (user == null || user.IsDeleted)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
            return true;
        }
    }
}
