﻿using E_TRADING.Data;
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
        public HomeController(ApplicationDbContext db)
        {
            _db = db;
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}