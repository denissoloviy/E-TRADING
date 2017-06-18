using AutoMapper;
using E_TRADING.Common;
using E_TRADING.Common.Models;
using E_TRADING.Common.OrderStatuses;
using E_TRADING.Data.Entities;
using E_TRADING.Data.Managers;
using E_TRADING.Data.Repositories;
using E_TRADING.Web.Attributes;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
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
        IProductRepository _productRepository;

        public OrderController(IMapper mapper,
            ApplicationUserManager userManager,
            IBuyerRepository buyerRepository,
            ISellerRepository sellerRepository,
            IOrderRepository orderRepository,
            IShoppingCartRepository shoppingCartRepository,
            IProductRepository productRepository)
        {
            _mapper = mapper;
            _userManager = userManager;
            _buyerRepository = buyerRepository;
            _sellerRepository = sellerRepository;
            _orderRepository = orderRepository;
            _shoppingCartRepository = shoppingCartRepository;
            _productRepository = productRepository;
        }

        [AuthorizeDeleted(Roles = UserRole.Buyer + "," + UserRole.Seller)]
        public ActionResult ActiveOrders()
        {
            ViewBag.Title = "Active orders";
            var userId = User.Identity.GetUserId();
            if (_userManager.IsInRole(userId, UserRole.Buyer))
            {
                var buyer = _buyerRepository.FirstOrDefault(item => item.Id == userId);
                var orders = buyer.Orders.Where(item => StatusTypes.ActiveStatuses.Contains(item.Status));
                var res = new List<OrderViewModel>();
                foreach(var o in orders)
                {
                    var oModel = _mapper.Map<OrderViewModel>(o);
                    var image = o.Product.Images.Count > 0 ?
                        o.Product.Images.FirstOrDefault(i => i.Index == o.Product.Images.Select(s => s.Index).Min()) : null;
                    oModel.MainImage = image != null ? "/Content/ProductImages/" + image.Id + image.Extention : "/Content/no_available_image.png";
                    res.Add(oModel);
                }

                ViewBag.Helper = _mapper.Map<BuyerProfileHelperViewModel>(buyer);
                return View("../Buyer/Orders", res);
            }
            else
            {
                var seller = _sellerRepository.FirstOrDefault(item => item.Id == userId);
                var orders = seller.Products.SelectMany(item => item.Orders)
                    .Where(item => StatusTypes.ActiveStatuses.Contains(item.Status));
                var res = new List<OrderViewModel>();
                foreach (var o in orders)
                {
                    var oModel = _mapper.Map<OrderViewModel>(o);
                    var image = o.Product.Images.Count > 0 ?
                        o.Product.Images.FirstOrDefault(i => i.Index == o.Product.Images.Select(s => s.Index).Min()) : null;
                    oModel.MainImage = image != null ? "/Content/ProductImages/" + image.Id + image.Extention : "/Content/no_available_image.png";
                    res.Add(oModel);
                }
                ViewBag.Helper = _mapper.Map<SellerProfileHelperViewModel>(seller);
                return View("../Seller/Orders", res);
            }
        }

        [AuthorizeDeleted(Roles = UserRole.Buyer + "," + UserRole.Seller)]
        public ActionResult InactiveOrders()
        {
            ViewBag.Title = "Orders Archive";
            var userId = User.Identity.GetUserId();
            if (_userManager.IsInRole(userId, UserRole.Buyer))
            {
                var buyer = _buyerRepository.FirstOrDefault(item => item.Id == userId);
                var orders = buyer.Orders.Where(item => StatusTypes.InactiveStatuses.Contains(item.Status));
                var res = new List<OrderViewModel>();
                foreach (var o in orders)
                {
                    var oModel = _mapper.Map<OrderViewModel>(o);
                    var image = o.Product.Images.Count > 0 ?
                        o.Product.Images.FirstOrDefault(i => i.Index == o.Product.Images.Select(s => s.Index).Min()) : null;
                    oModel.MainImage = image != null ? "/Content/ProductImages/" + image.Id + image.Extention : "/Content/no_available_image.png";
                    res.Add(oModel);
                }
                ViewBag.Helper = _mapper.Map<BuyerProfileHelperViewModel>(buyer);
                return View("../Buyer/Orders", res);
            }
            else
            {
                var seller = _sellerRepository.FirstOrDefault(item => item.Id == userId);
                var orders = seller.Products.SelectMany(item => item.Orders)
                    .Where(item => StatusTypes.InactiveStatuses.Contains(item.Status));
                var res = new List<OrderViewModel>();
                foreach (var o in orders)
                {
                    var oModel = _mapper.Map<OrderViewModel>(o);
                    var image = o.Product.Images.Count > 0 ?
                        o.Product.Images.FirstOrDefault(i => i.Index == o.Product.Images.Select(s => s.Index).Min()) : null;
                    oModel.MainImage = image != null ? "/Content/ProductImages/" + image.Id + image.Extention : "/Content/no_available_image.png";
                    res.Add(oModel);
                }
                ViewBag.Helper = _mapper.Map<SellerProfileHelperViewModel>(seller);
                return View("../Seller/Orders", res);
            }
        }

        [AuthorizeDeleted(Roles = UserRole.Buyer)]
        public ActionResult Create()
        {
            var userId = User.Identity.GetUserId();
            var buyer = _buyerRepository.FirstOrDefault(item => item.Id == userId);
            foreach (var item in buyer.ShoppingCarts)
            {
                if (item.Product.Amount < item.Amount)
                {
                    TempData["Error"] = $"Not enough : {item.Product.Name} to create an order";
                }
            }
            var orders = buyer.ShoppingCarts.Select(item =>
            new Order
            {
                Amount = item.Amount,
                BuyerId = buyer.Id,
                Buyer = buyer,
                FullPrice = item.Amount * item.Product.Price,
                ProductId = item.ProductId,
                Product = item.Product,
                ShippingAddress = buyer.User.Address,
                Status = OrderStatus.InProccess
            }).ToList();
            ViewBag.Helper = _mapper.Map<BuyerProfileHelperViewModel>(buyer);
            return View("../Buyer/CreateOrder", orders);
        }

        [AuthorizeDeleted(Roles = UserRole.Buyer)]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult CreateConfirm(List<Order> order)
        {
            var userId = User.Identity.GetUserId();
            var buyer = _buyerRepository.FirstOrDefault(item => item.Id == userId);
            var orders = new List<Order>();
            foreach (var item in buyer.ShoppingCarts)
            {
                var ord = order.FirstOrDefault(m => m.ProductId == item.ProductId);
                orders.Add(new Order
                {
                    Amount = item.Amount,
                    BuyerId = buyer.Id,
                    FullPrice = item.Amount * item.Product.Price,
                    ProductId = item.ProductId,
                    ShippingAddress = ord.ShippingAddress,
                    Status = OrderStatus.InProccess
                });
                var product = _productRepository.Find(item.ProductId);
                product.Amount -= item.Amount;
            }
            _productRepository.SaveChanges();
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

        [AuthorizeDeleted(Roles = UserRole.Seller)]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Confirm(string id)
        {
            var order = _orderRepository.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            order.Status = OrderStatus.NeedToPay;
            _orderRepository.SaveChanges();
            return RedirectToAction("ActiveOrders");
        }

        [AuthorizeDeleted(Roles = UserRole.Buyer)]
        public ActionResult Pay(string id)
        {
            var order = _orderRepository.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return RedirectToAction("PayConfirm", new { id = id });
        }

        [AuthorizeDeleted(Roles = UserRole.Buyer)]
        public ActionResult PayConfirm(string id)
        {
            var order = _orderRepository.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            order.Status = OrderStatus.Paid;
            _orderRepository.SaveChanges();
            return RedirectToAction("ActiveOrders");
        }

        [AuthorizeDeleted(Roles = UserRole.Seller)]
        public ActionResult Send(string id)
        {
            var order = _orderRepository.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            var model = _mapper.Map<OrderViewModel>(order);
            return View(model);
        }

        [AuthorizeDeleted(Roles = UserRole.Seller)]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult SendConfirm([System.Web.Http.FromBody]string id, [System.Web.Http.FromBody]string invoiceNumber)
        {
            var order = _orderRepository.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            order.Status = OrderStatus.InShipping;
            order.InvoiceNumber = invoiceNumber;
            _orderRepository.SaveChanges();
            return RedirectToAction("ActiveOrders");
        }

        [AuthorizeDeleted(Roles = UserRole.Buyer)]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult DeliveryConfirm(string id)
        {
            var order = _orderRepository.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            order.Status = OrderStatus.Successful;
            _orderRepository.SaveChanges();
            return RedirectToAction("InactiveOrders");
        }

        [AuthorizeDeleted(Roles = UserRole.Buyer + "," + UserRole.Seller)]
        public ActionResult Cancel(string id)
        {
            var order = _orderRepository.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            var model = _mapper.Map<OrderViewModel>(order);
            return View(model);
        }

        [AuthorizeDeleted(Roles = UserRole.Buyer + "," + UserRole.Seller)]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult CancelConfirm(string id)
        {
            var order = _orderRepository.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            var userId = User.Identity.GetUserId();
            if (_userManager.IsInRole(userId, UserRole.Buyer))
            {
                order.Status = OrderStatus.CanceledByBuyer;
            }
            else
            {
                order.Status = OrderStatus.CanceledBySeller;
            }
            _orderRepository.SaveChanges();
            var product = _productRepository.Find(order.ProductId);
            product.Amount += order.Amount;
            _productRepository.SaveChanges();

            return RedirectToAction("InactiveOrders");
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