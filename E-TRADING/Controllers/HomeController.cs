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
            var Products = _productRepository.GetAll().ToList();

            List<Product> products = new List<Product>();
            for (int i = 0; i < 4;i++)
            {
                var y = x.Next(0, Products.Count);
                products.Add(Products[y]);
                Products.RemoveAt(y);
            }
         
            var result = new CategoryProductViewModel
            {
                Categories = _categoryRepository.FindBy(item => item.MasterCategoryId == null).ToList(),
                Products = products
            };

            //_productRepository.Add(new Data.Entities.Product
            //{
                
            //});
            //_productRepository.SaveChanges();

            return View(result);
        }
        public ActionResult Search(string Category="")
        {
            var Categories = _categoryRepository.FindBy(item => item.MasterCategoryId == null).ToList();
            return View(Categories);
        }
        public ActionResult ShoppingCart(string UserId="")
        {
            Random x = new Random();
            var Products = _productRepository.GetAll().ToList();

            List<Product> products = new List<Product>();
            for (int i = 0; i < 4;i++)
            {
                var y = x.Next(0, Products.Count);
                products.Add(Products[y]);
                Products.RemoveAt(y);
            }
            return View(products);
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();  
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}