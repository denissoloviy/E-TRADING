using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_TRADING.Admin.Controllers
{
    public class UsersController : Controller
    {
        public UsersController()
        {

        }

        #region Users

        // GET: Users
        public ActionResult Index()
        {
            return View();
        }

        #endregion

        #region AdminUsers

        // GET: Users/AdminUsers
        public ActionResult AdminUsers()
        {
            return View();
        }

        #endregion
    }
}