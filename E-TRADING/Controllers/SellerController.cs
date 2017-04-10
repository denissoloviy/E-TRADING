using E_TRADING.Data.Managers;
using E_TRADING.Data.Repositories;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_TRADING.Controllers
{
    public class SellerController : Controller
    {
        private readonly ApplicationUserManager _userManager;
        private readonly ISellerRepository _sellerRepository;
        public SellerController(ApplicationUserManager userManager,
            ISellerRepository sellerRepository)
        {
            _userManager = userManager;
            _sellerRepository = sellerRepository;
        }

        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var seller = _sellerRepository.FirstOrDefault(item => item.Id == userId);
            return View(seller);
        }

        public ActionResult Products()
        {
            return View();
        }
    }
}