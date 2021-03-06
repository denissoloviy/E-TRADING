﻿using AutoMapper;
using E_TRADING.Common;
using E_TRADING.Common.Extensions;
using E_TRADING.Common.Models;
using E_TRADING.Data.Entities;
using E_TRADING.Data.Managers;
using E_TRADING.Data.Repositories;
using E_TRADING.Web.Attributes;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web.Mvc;

namespace E_TRADING.Controllers
{
    public class AuctionController : Controller
    {
        IMapper _mapper;
        IProductRepository _productRepository;
        IAuctionRepository _auctionRepository;
        ISellerRepository _sellerRepository;
        ApplicationUserManager _userManager;
        public AuctionController(IMapper mapper,
            IProductRepository productRepository,
            IAuctionRepository auctionRepository,
            ISellerRepository sellerRepository,
            ApplicationUserManager userManager)
        {
            _mapper = mapper;
            _productRepository = productRepository;
            _auctionRepository = auctionRepository;
            _sellerRepository = sellerRepository;
            _userManager = userManager;
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("ru-RU");
        }

        [AuthorizeDeleted(Roles = UserRole.Seller)]
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var auctions = _auctionRepository.FindBy(item => item.Product.Seller.User.Id == userId).ToList();
            var res = auctions.Select(item => _mapper.Map<AuctionViewModel>(item)).ToList();
            var seller = _sellerRepository.FirstOrDefault(item => item.Id == userId);
            ViewBag.Helper = _mapper.Map<SellerProfileHelperViewModel>(seller);
            return View("../Seller/Auctions", res);
        }

        [AuthorizeDeleted(Roles = UserRole.Seller)]
        public ActionResult Create(string id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            var product = _productRepository.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductName = product.Name;
            var model = new Auction
            {
                Id = id,
                DateEnd = DateTime.UtcNow.ConvertToSiteZoneFromUtc(),
                DateStart = DateTime.UtcNow.ConvertToSiteZoneFromUtc()
            };
            return View(model);
        }

        [AuthorizeDeleted(Roles = UserRole.Seller)]
        [HttpPost]
        [ActionName("Create")]
        [ValidateAntiForgeryToken]
        public ActionResult CreateConfirm(Auction model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            model.DateStart = model.DateStart.ConvertToUtcFromSiteTimeZone();
            model.DateEnd = model.DateEnd.ConvertToUtcFromSiteTimeZone();
            model.LastBid = model.StartPrice;
            _auctionRepository.Add(model);
            _auctionRepository.SaveChanges();
            return RedirectToAction("Index");
        }

        [AuthorizeDeleted(Roles = UserRole.Seller)]
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            var auction = _auctionRepository.Find(id);
            if (auction == null)
            {
                return HttpNotFound();
            }
            auction.DateStart = auction.DateStart.ConvertToSiteZoneFromUtc();
            auction.DateEnd = auction.DateEnd.ConvertToSiteZoneFromUtc();
            ViewBag.ProductName = auction.Product.Name;
            return View(auction);
        }

        [AuthorizeDeleted(Roles = UserRole.Seller)]
        [HttpPost]
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditConfirm(Auction model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            model.DateStart = model.DateStart.ConvertToUtcFromSiteTimeZone();
            model.DateEnd = model.DateEnd.ConvertToUtcFromSiteTimeZone();
            model.LastBid = model.StartPrice;
            _auctionRepository.Update(model);
            _auctionRepository.SaveChanges();
            return RedirectToAction("Index");
        }
        
        [AuthorizeDeleted(Roles = UserRole.Seller)]
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            var auction = _auctionRepository.Find(id);
            if (auction == null)
            {
                return HttpNotFound();
            }
            var isStarted = auction.DateStart <= DateTime.UtcNow;
            if (isStarted)
            {
                TempData["Errors"] = "Auction has already started, delete unavailable";
                return RedirectToAction("Details", new { id = id });
            }
            var res = _mapper.Map<AuctionViewModel>(auction);
            res.IsStarted = isStarted;
            var product = _productRepository.FirstOrDefault(p => p.Id == id);
            res.Product = _mapper.Map<ProductViewModel>(product);
            foreach (var img in product.Images)
            {
                res.Product.Images.Add(@"/Content/ProductImages/" + img.Id + img.Extention);
            }
            return View(res);
        }

        [AuthorizeDeleted(Roles = UserRole.Seller)]
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirm(string id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            var auction = _auctionRepository.Find(id);
            if (auction == null)
            {
                return HttpNotFound();
            }
            var isStarted = auction.DateStart <= DateTime.UtcNow;
            if (isStarted)
            {
                TempData["Errors"] = "Auction has already started, delete unavailable";
                return RedirectToAction("Details", new { id = id });
            }
            _auctionRepository.Delete(auction);
            _auctionRepository.SaveChanges();
            return RedirectToAction("Index");
        }

        [AuthorizeDeleted(Roles = UserRole.Seller + "," + UserRole.Buyer)]
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            var auction = _auctionRepository.Find(id);
            if (auction == null)
            {
                return HttpNotFound();
            }
            if (User.Identity.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();
                ViewBag.IsSeller = _userManager.IsInRole(userId, UserRole.Seller);
            }
            var res = _mapper.Map<AuctionViewModel>(auction);
            var product = _productRepository.FirstOrDefault(p => p.Id == id);
            res.Product = _mapper.Map<ProductViewModel>(product);
            foreach (var img in product.Images)
            {
                res.Product.Images.Add(@"/Content/ProductImages/" + img.Id + img.Extention);
            }
            ViewBag.Errors = TempData["Errors"];
            return View(res);
        }

        [AuthorizeDeleted(Roles = UserRole.Buyer)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Bid(BidModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["Errors"] = "Entered data is incorrect";
                return RedirectToAction("Details", new { id = model.Id });
            }
            var auction = _auctionRepository.FirstOrDefault(item => item.Id == model.Id);
            if (auction == null)
            {
                TempData["Errors"] = "No auction found";
                return RedirectToAction("Details", new { id = model.Id });
            }
            if (auction.DateEnd < DateTime.UtcNow)
            {
                TempData["Errors"] = "Auction has ended";
                return RedirectToAction("Details", new { id = model.Id });
            }
            if (model.NewBid < auction.LastBid + auction.MinStep)
            {
                TempData["Errors"] = $"Min step is {auction.MinStep}";
                return RedirectToAction("Details", new { id = model.Id });
            }
            auction.LastBid = model.NewBid;
            auction.BuyerId = User.Identity.GetUserId();
            _auctionRepository.SaveChanges();

            return RedirectToAction("Details", new { id = model.Id });
        }
    }
}