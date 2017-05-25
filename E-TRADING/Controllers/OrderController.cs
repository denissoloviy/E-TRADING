using AutoMapper;
using E_TRADING.Common;
using E_TRADING.Common.Models;
using E_TRADING.Common.OrderStatuses;
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
    public class OrderController : Controller
    {
        IMapper _mapper;
        ApplicationUserManager _userManager;
        IBuyerRepository _buyerRepository;
        ISellerRepository _sellerRepository;
        IOrderRepository _orderRepository;
        IShoppingCartRepository _shoppingCartRepository;

        public OrderController(IMapper mapper,
            ApplicationUserManager userManager,
            IBuyerRepository buyerRepository,
            ISellerRepository sellerRepository,
            IOrderRepository orderRepository,
            IShoppingCartRepository shoppingCartRepository)
        {
            _mapper = mapper;
            _userManager = userManager;
            _buyerRepository = buyerRepository;
            _sellerRepository = sellerRepository;
            _orderRepository = orderRepository;
            _shoppingCartRepository = shoppingCartRepository;
        }

        [Authorize(Roles = UserRole.Buyer + "," + UserRole.Seller)]
        public ActionResult ActiveOrders()
        {
            var userId = User.Identity.GetUserId();
            if (_userManager.IsInRole(userId, UserRole.Buyer))
            {
                var buyer = _buyerRepository.FirstOrDefault(item => item.Id == userId);
                var orders = buyer.Orders.Where(item => StatusTypes.ActiveStatuses.Contains(item.Status));
                var res = orders.Select(item => _mapper.Map<OrderViewModel>(item));
                return View("../Buyer/Orders", res);
            }
            else
            {
                var seller = _sellerRepository.FirstOrDefault(item => item.Id == userId);
                var orders = seller.Products.SelectMany(item => item.Orders)
                    .Where(item => StatusTypes.ActiveStatuses.Contains(item.Status));
                var res = orders.Select(item => _mapper.Map<OrderViewModel>(item));
                return View("../Seller/Orders", res);
            }
        }

        public ActionResult InactiveOrders()
        {
            var userId = User.Identity.GetUserId();
            if (_userManager.IsInRole(userId, UserRole.Buyer))
            {
                var buyer = _buyerRepository.FirstOrDefault(item => item.Id == userId);
                var orders = buyer.Orders.Where(item => StatusTypes.InactiveStatuses.Contains(item.Status));
                var res = orders.Select(item => _mapper.Map<OrderViewModel>(item));
                return View("../Buyer/Orders", res);
            }
            else
            {
                var seller = _sellerRepository.FirstOrDefault(item => item.Id == userId);
                var orders = seller.Products.SelectMany(item => item.Orders)
                    .Where(item => StatusTypes.InactiveStatuses.Contains(item.Status));
                var res = orders.Select(item => _mapper.Map<OrderViewModel>(item));
                return View("../Seller/Orders", res);
            }
        }

        [Authorize(Roles = UserRole.Buyer)]
        public ActionResult CreateOrder()
        {
            var userId = User.Identity.GetUserId();
            var buyer = _buyerRepository.FirstOrDefault(item => item.Id == userId);
            var orders = buyer.ShoppingCarts.Select(item =>
            new Order
            {
                Amount = item.Amount,
                BuyerId = buyer.Id,
                FullPrice = item.Amount * item.Product.Price,
                ProductId = item.ProductId,
                ShippingAddress = buyer.User.Address,
                Status = OrderStatus.InProccess
            }).ToList();
            return View(orders);
            //foreach (var item in orders)
            //{
            //    _orderRepository.Add(item);
            //}
            //_orderRepository.SaveChanges();
            //var shopCarts = buyer.ShoppingCarts.ToList();
            //foreach (var item in shopCarts)
            //{
            //    _shoppingCartRepository.Delete(item);
            //}
            //_shoppingCartRepository.SaveChanges();
            //return RedirectToAction("ActiveOrders");
        }

        [Authorize(Roles = UserRole.Buyer)]
        [HttpPost]
        public ActionResult CreateOrderConfirm(List<Order> model)
        {
            var userId = User.Identity.GetUserId();
            var buyer = _buyerRepository.FirstOrDefault(item => item.Id == userId);
            var orders = buyer.ShoppingCarts.Select(item =>
            new Order
            {
                Amount = item.Amount,
                BuyerId = buyer.Id,
                FullPrice = item.Amount * item.Product.Price,
                ProductId = item.ProductId,
                ShippingAddress = buyer.User.Address,
                Status = OrderStatus.InProccess
            });
            foreach (var item in orders)
            {
                _orderRepository.Add(item);
            }
            _orderRepository.SaveChanges();
            var shopCarts = buyer.ShoppingCarts.ToList();
            foreach (var item in shopCarts)
            {
                _shoppingCartRepository.Delete(item);
            }
            _shoppingCartRepository.SaveChanges();
            return RedirectToAction("ActiveOrders");
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