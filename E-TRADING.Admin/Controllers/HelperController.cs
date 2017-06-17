using E_TRADING.Common;
using E_TRADING.Data.Managers;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;

namespace E_TRADING.Admin.Controllers
{
    public class HelperController : Controller
    {
        ApplicationUserManager _userManager;
        public HelperController(ApplicationUserManager userManager)
        {
            _userManager = userManager;
        }

        // GET: Helper
        public ActionResult SuperAdminPage()
        {
            var userId = User.Identity.GetUserId();
            return PartialView("_Admins", _userManager.IsInRole(userId, UserRole.SuperAdmin));
        }
    }
}