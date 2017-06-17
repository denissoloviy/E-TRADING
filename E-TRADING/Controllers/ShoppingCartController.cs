using AutoMapper;
using E_TRADING.Common;
using E_TRADING.Common.Models;
using E_TRADING.Common.OrderStatuses;
using E_TRADING.Data.Entities;
using E_TRADING.Data.Repositories;
using E_TRADING.Web.Attributes;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Mvc;

namespace E_TRADING.Controllers
{
    [AuthorizeDeleted(Roles = UserRole.Buyer)]
    public class ShoppingCartController : Controller
    {
        IMapper _mapper;
        IBuyerRepository _buyerRepository;
        IShoppingCartRepository _shoppingCartRepository;
        IProductRepository _productRepository;
        public ShoppingCartController(IMapper mapper,
            IBuyerRepository buyerRepository,
            IShoppingCartRepository shoppingCartRepository,
            IProductRepository productRepository)
        {
            _mapper = mapper;
            _buyerRepository = buyerRepository;
            _shoppingCartRepository = shoppingCartRepository;
            _productRepository = productRepository;
        }

        public ActionResult Index()
        {
            ViewBag.Error = TempData["Error"];
            var buyer = GetBuyer();
            var res = new ShoppingCartViewModel
            {
                Products = buyer.ShoppingCarts.Select(item => _mapper.Map<ProductViewModel>(item)).ToList(),
                Helper = new BuyerProfileHelperViewModel
                {
                    ActiveOrdersCount = buyer.Orders.Where(item => StatusTypes.ActiveStatuses.Contains(item.Status)).Count(),
                    InactiveOrdersCount = buyer.Orders.Where(item => StatusTypes.InactiveStatuses.Contains(item.Status)).Count(),
                    ShoppingCartCount = buyer.ShoppingCarts.Sum(item => item.Amount)
                }
            };
            return View("../Buyer/ShoppingCart", res);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(string id, int? amount)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            var buyer = GetBuyer();
            amount = amount ?? 1;
            var product = _productRepository.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            if (amount > product.Amount)
            {
                amount = product.Amount;
            }
            var shopCart = buyer.ShoppingCarts.FirstOrDefault(item => item.ProductId == id);
            if (shopCart != null)
            {
                if (shopCart.Amount + amount > product.Amount)
                {
                    shopCart.Amount = product.Amount;
                }
                else
                {
                    shopCart.Amount = shopCart.Amount + amount.Value;
                }
                _shoppingCartRepository.SaveChanges();
            }
            else
            {
                shopCart = new ShoppingCart
                {
                    BuyerId = buyer.Id,
                    Amount = amount.Value,
                    ProductId = product.Id
                };
                _shoppingCartRepository.Add(shopCart);
                _shoppingCartRepository.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            var buyer = GetBuyer();
            var shopCart = buyer.ShoppingCarts.FirstOrDefault(item => item.Id == id);
            if (shopCart == null)
            {
                return HttpNotFound();
            }
            _shoppingCartRepository.Delete(shopCart);
            _shoppingCartRepository.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Clear()
        {
            var buyer = GetBuyer();
            var shopCarts = buyer.ShoppingCarts.ToList();
            foreach (var item in shopCarts)
            {
                _shoppingCartRepository.Delete(item);
            }
            _shoppingCartRepository.SaveChanges();
            return RedirectToAction("Index");
        }

        #region Helpers

        private Buyer GetBuyer()
        {
            var userId = User.Identity.GetUserId();
            return _buyerRepository.FirstOrDefault(item => item.Id == userId);
        }

        #endregion
    }
}