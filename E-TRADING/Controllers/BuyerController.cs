using AutoMapper;
using E_TRADING.Common;
using E_TRADING.Common.Models;
using E_TRADING.Data.Entities;
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
    [Authorize(Roles = UserRole.Buyer)]
    public class BuyerController : Controller
    {
        IMapper _mapper;
        ApplicationUserManager _userManager;
        IBuyerRepository _buyerRepository;
        public BuyerController(IMapper mapper,
            ApplicationUserManager userManager,
            IBuyerRepository buyerRepository)
        {
            _mapper = mapper;
            _userManager = userManager;
            _buyerRepository = buyerRepository;
        }

        public ActionResult Index()
        {
            return View(FillBuyerViewEditViewModel());
        }

        public ActionResult Edit()
        {
            return View(FillBuyerViewEditViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BuyerViewEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var buyer = GetBuyer();
            
            buyer.User.Address = model.Address;
            buyer.User.FirstName = model.FirstName;
            buyer.User.LastName = model.LastName;
            buyer.User.PhoneNumber = model.PhoneNumber;

            _buyerRepository.SaveChanges();
            return RedirectToAction("Index", "Buyer");
        }

        public ActionResult ShoppingCart()
        {
            var buyer = GetBuyer();
            var res = new ShoppingCartViewModel
            {
                Products = buyer.ShoppingCarts.Select(item => _mapper.Map<ProductViewModel>(item)).ToList(),
                Helper = new BuyerProfileHelperViewModel
                {

                }
            };
            return View(res);
        }

        #region Helpers

        private BuyerViewEditViewModel FillBuyerViewEditViewModel()
        {
            var buyer = GetBuyer();
            return _mapper.Map<BuyerViewEditViewModel>(buyer);
        }

        private Buyer GetBuyer()
        {
            var userId = User.Identity.GetUserId();
            return _buyerRepository.FirstOrDefault(item => item.Id == userId);
        }

        #endregion
    }
}