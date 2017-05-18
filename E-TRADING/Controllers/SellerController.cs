﻿using AutoMapper;
using E_TRADING.Common;
using E_TRADING.Common.Extensions;
using E_TRADING.Common.Models;
using E_TRADING.Data.Entities;
using E_TRADING.Data.Managers;
using E_TRADING.Data.Repositories;
using E_TRADING.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Mvc;

namespace E_TRADING.Controllers
{
    [Authorize(Roles = UserRole.Seller)]
    public class SellerController : Controller
    {
        IMapper _mapper;
        ApplicationUserManager _userManager;
        ISellerRepository _sellerRepository;
        public SellerController(IMapper mapper,
            ApplicationUserManager userManager,
            ISellerRepository sellerRepository)
        {
            _mapper = mapper;
            _userManager = userManager;
            _sellerRepository = sellerRepository;
        }

        public ActionResult Index()
        {
            return View(FillSellerViewEditViewModel());
        }

        public ActionResult Edit()
        {
            return View(FillSellerViewEditViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SellerViewEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var seller = GetSeller();

            seller.Alias = model.Alias;
            seller.ContactPhone = model.ContactPhone;
            seller.OfficeAddress = model.OfficeAddress;
            seller.User.Address = model.Address;
            seller.User.FirstName = model.FirstName;
            seller.User.LastName = model.LastName;
            seller.User.PhoneNumber = model.PhoneNumber;

            _sellerRepository.SaveChanges();
            return RedirectToAction("Index", "Seller");
        }

        public ActionResult Products()
        {
            var seller = GetSeller();
            var res = new ProductsViewModel
            {
                Products = seller.Products.Select(item => _mapper.Map<ProductViewModel>(item)).ToList(),
                ProductsCount = seller.Products.Sum(item => item.Amount)
            };
            return View(res);
        }

        #region Helpers

        private SellerViewEditViewModel FillSellerViewEditViewModel()
        {
            var seller = GetSeller();
            var model = new SellerViewEditViewModel
            {
                Address = seller.User.Address,
                Alias = seller.Alias,
                ContactPhone = seller.ContactPhone,
                FirstName = seller.User.FirstName,
                LastName = seller.User.LastName,
                OfficeAddress = seller.OfficeAddress,
                PhoneNumber = seller.User.PhoneNumber,
                UserName = seller.User.UserName,
                ProductsCount = seller.Products.Sum(item => item.Amount)
            };
            return _mapper.Map<SellerViewEditViewModel>(seller);
        }

        private Seller GetSeller()
        {
            var userId = User.Identity.GetUserId();
            return _sellerRepository.FirstOrDefault(item => item.Id == userId);
        }

        #endregion
    }
}