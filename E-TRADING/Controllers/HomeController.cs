using E_TRADING.Data;
using E_TRADING.Data.Repositories;
using E_TRADING.Models;
using System.Linq;
using System.Web.Mvc;
using System.Collections.Generic;
using System;
using E_TRADING.Data.Entities;


namespace E_TRADING.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext _db;
        ICategoryRepository _categoryRepository;
        IProductRepository _productRepository;
        IShoppingCartRepository _shoppingcartRepository;
        public HomeController(ApplicationDbContext db,
            ICategoryRepository categoryRepository,
            IProductRepository productRepository,
            IShoppingCartRepository shoppingcartRepository)
        {
            _db = db;
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
            _shoppingcartRepository = shoppingcartRepository;
        }

        public ActionResult Index()
        {
            Random x = new Random();
            var Products = _productRepository.GetAll().Where(item => !item.IsDeleted && item.Amount > 0).ToList();

            List<Product> products = new List<Product>();
            for (int i = 0; i < 4; i++)
            {
                if (Products.Count != 0)
                {
                    var y = x.Next(0, Products.Count);
                    products.Add(Products[y]);
                    Products.RemoveAt(y);
                }
            }

            var result = new CategoryProductViewModel
            {
                Categories = _categoryRepository.FindBy(item => item.MasterCategoryId == null).ToList(),
                Products = products
            };

            return View(result);
        }

        public ActionResult Search(decimal? PriceMin, decimal? PriceMax, string Category = "", string SearchString = "")
        {

            var _products = _productRepository.GetAll();
            var result = new CategoryProductViewModel
            {
                Categories = _categoryRepository.FindBy(item => item.MasterCategoryId == null).ToList(),
            };

            if (!String.IsNullOrEmpty(Category))
            {
                List<string> MainCategoryWithSubCategories = new List<string>();
                Category cat = _categoryRepository.FindBy(item => item.Id == Category).First();
                GetAllCategories(cat, ref MainCategoryWithSubCategories);

                ViewBag.SelectedCategory = Category;
                _products = _products.Where(p => MainCategoryWithSubCategories.Contains(p.CategoryId));
            }
            if (!String.IsNullOrEmpty(SearchString))
            {
                ViewBag.SearchString = SearchString;
                _products = _products.Where(p => p.Name.Contains(SearchString));
            }
            if (PriceMin != null)
            {
                ViewBag.PriceMin = PriceMin;
                _products = _products.Where(p => p.Price > PriceMin);
            }
            if (PriceMax != null)
            {
                ViewBag.PriceMax = PriceMax;
                _products = _products.Where(p => p.Price < PriceMax);
            }
            result.Products = _products.ToList();
            return View(result);
        }

        public void GetAllCategories(Category cat, ref List<string> CatWithSubCat)
        {
            if (cat.Categories.Count == 0)
                CatWithSubCat.Add(cat.Id);
            else
            {
                CatWithSubCat.Add(cat.Id);
                foreach (var x in cat.Categories)
                {
                    GetAllCategories(x, ref CatWithSubCat);
                }
            }
        }

        public ActionResult About()
        {
            return View();
        }
    }
}