using AutoMapper;
using E_TRADING.Common;
using E_TRADING.Common.Models;
using E_TRADING.Data.Entities;
using E_TRADING.Data.Repositories;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Mvc;

namespace E_TRADING.Controllers
{
    public class ProductController : Controller
    {
        IMapper _mapper;
        IProductRepository _productRepository;
        ICategoryRepository _categoryRepository;

        public ProductController(IMapper mapper,
            IProductRepository productRepository,
            ICategoryRepository categoryRepository)
        {
            _mapper = mapper;
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }
        // GET: Product
        public ActionResult Index(string id)
        {
            var product = _productRepository.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return HttpNotFound("Product was not found");
            }

            return View(_mapper.Map<ProductViewModel>(product));
        }

        [HttpPost]
        public ActionResult BuyProduct(string id)
        {
            return View();
        }

        [Authorize(Roles = UserRole.Seller)]
        public ActionResult AddProduct()
        {
            var categoryList = new SelectList(_categoryRepository.FindBy(x => x.Categories.Count() == 0),
                dataValueField: "Id", dataTextField: "Name");

            ViewBag.Category = categoryList;
            return View();
        }

        [HttpPost]
        [Authorize(Roles = UserRole.Seller)]
        public ActionResult AddProduct(ProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var categoryList = new SelectList(_categoryRepository.FindBy(x => x.Categories.Count() == 0),
                    dataValueField: "Id", dataTextField: "Name");

                ViewBag.Category = categoryList;
                return View(model);
            }

            var product = new Product()
            {
                Name = model.Name,
                Description = model.Description,
                Amount = model.Amount,
                Price = model.Price,
                SellerId = User.Identity.GetUserId(),
                CategoryId = model.Category
            };

            _productRepository.Add(product);
            _productRepository.SaveChanges();

            return RedirectToAction("Index", "Seller");
        }

        #region Helpers

        #endregion
    }
}