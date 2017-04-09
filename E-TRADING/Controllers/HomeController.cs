using E_TRADING.Data;
using E_TRADING.Data.Repositories;
using E_TRADING.Models;
using System.Linq;
using System.Web.Mvc;

namespace E_TRADING.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext _db;
        ICategoryRepository _categoryRepository;
        IProductRepository _productRepository;
        public HomeController(ApplicationDbContext db,
            ICategoryRepository categoryRepository,
            IProductRepository productRepository)
        {
            _db = db;
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
        }
        public ActionResult Index()
        {
            var result = new CategoryProductViewModel
            {
                Categories = _categoryRepository.FindBy(item => item.MasterCategoryId == null).ToList(),
                Products = _productRepository.GetAll().Take(10).ToList()
            };

            //_productRepository.Add(new Data.Entities.Product
            //{
                
            //});
            //_productRepository.SaveChanges();

            return View(result);
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