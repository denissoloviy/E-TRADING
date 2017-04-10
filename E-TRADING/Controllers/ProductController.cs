using E_TRADING.Common;
using E_TRADING.Common.Extensions;
using E_TRADING.Data.Entities;
using E_TRADING.Data.Repositories;
using E_TRADING.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_TRADING.Controllers
{
    public class ProductController : Controller
    {
        IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        // GET: Product
        public ActionResult Index(string id)
        {
            var product = _productRepository.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return HttpNotFound("Product was not found");
            }
            return View(FillProductViewModel(product));
        }

        [HttpPost]
        public ActionResult BuyProduct(string id)
        {
            return View();
        }

        [Authorize(Roles = UserRole.Seller)]
        public ActionResult AddProduct()
        {
            return View();
        }

        #region Helpers

        private ProductViewModel FillProductViewModel(Product product)
        {
            var model = new ProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                Amount = product.Amount,
                AddedDate = product.AddedDate.DateTimeToFormatString(),

                SellerId = product.SellerId,
                SellerName = product.Seller.User.UserName // product.Seller.Alias
            };
            return model;
        }

        #endregion
    }
}