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
                return HttpNotFound("Product was not found");

            var model = new ProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                Amount = product.Amount,

                SellerId = product.SellerId,
                SellerName = product.Seller.User.UserName //??
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult BuyProduct(string id)
        {

            return View();
        }
    }
}