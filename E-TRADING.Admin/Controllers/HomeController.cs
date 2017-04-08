using E_TRADING.Data;
using E_TRADING.Data.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_TRADING.Admin.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext _db;
        ApplicationUserManager _userManager;
        public HomeController(ApplicationDbContext db,
            ApplicationUserManager userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}