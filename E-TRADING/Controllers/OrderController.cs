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
    [Authorize]
    public class OrderController : Controller
    {
        IMapper _mapper;
        ApplicationUserManager _userManager;
        IBuyerRepository _buyerRepository;
        ISellerRepository _sellerRepository;
        public OrderController(IMapper mapper,
            ApplicationUserManager userManager,
            IBuyerRepository buyerRepository,
            ISellerRepository sellerRepository)
        {
            _mapper = mapper;
            _userManager = userManager;
            _buyerRepository = buyerRepository;
            _sellerRepository = sellerRepository;
        }

        public ActionResult Index()
        {
            return View();
        }


        #region Helpers
        
        private Buyer GetBuyer()
        {
            var userId = User.Identity.GetUserId();
            return _buyerRepository.FirstOrDefault(item => item.Id == userId);
        }

        private Seller GetSeller()
        {
            var userId = User.Identity.GetUserId();
            return _sellerRepository.FirstOrDefault(item => item.Id == userId);
        }

        #endregion
    }
}