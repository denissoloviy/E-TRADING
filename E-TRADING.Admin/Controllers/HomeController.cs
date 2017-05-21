using AutoMapper;
using E_TRADING.Common.Models;
using E_TRADING.Data;
using E_TRADING.Data.Managers;
using E_TRADING.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_TRADING.Admin.Controllers
{
    public class HomeController : Controller
    {
        IMapper _mapper;
        ApplicationUserManager _userManager;
        IOrderRepository _orderRepository;
        public HomeController(IMapper mapper, ApplicationUserManager userManager, IOrderRepository orderRepository)
        {
            _mapper = mapper;
            _userManager = userManager;
            _orderRepository = orderRepository;
        }

        [Authorize(Roles = "SuperAdmin, Administrator")]
        public ActionResult Index()
        {
            var products = _orderRepository.GetAll().ToList().Select(item => _mapper.Map<OrderViewModel>(item));
            return View(products);
        }
    }
}