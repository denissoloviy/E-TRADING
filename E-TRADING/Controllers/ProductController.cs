using AutoMapper;
using E_TRADING.Common;
using E_TRADING.Common.Models;
using E_TRADING.Data.Entities;
using E_TRADING.Data.Managers;
using E_TRADING.Data.Repositories;
using E_TRADING.Web.Attributes;
using Microsoft.AspNet.Identity;
using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_TRADING.Controllers
{
    public class ProductController : Controller
    {
        IMapper _mapper;
        IProductRepository _productRepository;
        ICategoryRepository _categoryRepository;
        IImageRepository _imageRepository;
        IAuctionRepository _auctionRepository;
        ApplicationUserManager _userManager;

        public ProductController(IMapper mapper,
            IProductRepository productRepository,
            ICategoryRepository categoryRepository,
            IImageRepository imageRepository,
            IAuctionRepository auctionRepository,
            ApplicationUserManager userManager)
        {
            _mapper = mapper;
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _imageRepository = imageRepository;
            _auctionRepository = auctionRepository;
            _userManager = userManager;
        }

        public ActionResult Index(string id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            if (User.Identity.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();
                ViewBag.IsSeller = _userManager.IsInRole(userId, UserRole.Seller);
            }
            var auction = _auctionRepository.Find(id);
            if (auction != null)
            {
                return RedirectToAction("Details", "Auction", new { id = id });
            }
            var product = _productRepository.Find(id);
            if (product == null)
            {
                return HttpNotFound("Product was not found"); 
            }
            var productModel = _mapper.Map<ProductViewModel>(product);
            foreach (var img in product.Images)
            {
                productModel.Images.Add(@"/Content/ProductImages/" + img.Id + img.Extention);
            }

            return View(productModel);
        }

        [HttpPost]
        public ActionResult BuyProduct(string id)
        {
            return View();
        }

        [AuthorizeDeleted(Roles = UserRole.Seller)]
        public ActionResult AddProduct()
        {
            var categoryList = new SelectList(_categoryRepository.FindBy(x => x.Categories.Count() == 0),
                dataValueField: "Id", dataTextField: "Name");

            ViewBag.Category = categoryList;
            return View();
        }

        [HttpPost]
        [AuthorizeDeleted(Roles = UserRole.Seller)]
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

            return RedirectToAction("UploadImages", new { id = product.Id });
        }

        [AuthorizeDeleted(Roles = UserRole.Seller)]
        public ActionResult Delete(string id)
        {
            _productRepository.Delete(id);
            _productRepository.SaveChanges();
            return RedirectToAction("Products", "Seller");
        }

        [HttpGet]
        [AuthorizeDeleted(Roles = UserRole.Seller)]
        public ActionResult UploadImages(string id)
        {
            var product = _productRepository.FirstOrDefault(p => p.Id == id);

            if (product.SellerId != User.Identity.GetUserId())
                return View("Error");

            ViewBag.ProductId = id;
            ViewBag.ProductName = product.Name;
            return View(product.Images.OrderBy(i => i.Index));
        }

        [HttpPost]
        [AuthorizeDeleted(Roles = UserRole.Seller)]
        public ActionResult UploadImages(string id, HttpPostedFileBase image)
        {
            var imageId = Guid.NewGuid();
            var path = @"~/Content/ProductImages";

            if (image == null)
                return RedirectToAction("UploadImages", new { id = id });

            var ext = Path.GetExtension(image.FileName);

            if (!new string[] { ".jpg", ".jpeg", ".gif", ".bmp", ".png" }.Contains(Path.GetExtension(ext)))
                return RedirectToAction("UploadImages", new { id = id });

            var product = _productRepository.FirstOrDefault(p => p.Id == id);

            if(product.Images.Count > 5)
                return RedirectToAction("UploadImages", new { id = id });

            var img = new Image()
            {
                Index = product.Images.Count > 0? product.Images.Select(i => i.Index).Max() + 1: 1,
                Extention = ext
            };

            //saving image to local folder
            image.SaveAs(Path.Combine(Server.MapPath(path), img.Id + img.Extention));

            //saving image link to db
            product.Images.Add(img);
            _productRepository.SaveChanges();

            ViewBag.ProductId = id;
            ViewBag.ProductName = product.Name;
            return View(product.Images.OrderBy(i => i.Index));
        }

        [HttpPost]
        [AuthorizeDeleted(Roles = UserRole.Seller)]
        public ActionResult EditImage(string id, string imageId, int? index)
        {
            var product = _productRepository.FirstOrDefault(p => p.Id == id);
            if(index.HasValue && product.Images.Where(i => i.Index == index).Count() == 0)
            {
                product.Images.FirstOrDefault(p => p.Id == imageId).Index = index.Value;
                _productRepository.SaveChanges();
            }

            return RedirectToAction("UploadImages", new { id = id });
        }

        [AuthorizeDeleted(Roles = UserRole.Seller)]
        public ActionResult DeleteImage(string id)
        {
            var image = _imageRepository.FirstOrDefault(i => i.Id == id);
            var productId = image.ProductId;

            if (image != null)
            {
                _imageRepository.Delete(image);
                _imageRepository.SaveChanges();
            }

            return RedirectToAction("UploadImages", new { id = productId });
        }
        

        #region Helpers

        private bool ValidateFileExtension(HttpPostedFileBase file)
        {
            var ext = Path.GetExtension(file.FileName);
            return new string[]{ ".bmp", ".png", ".jpg", ".jpeg" }.Contains(Path.GetExtension(file.FileName));
        }

        #endregion
    }
}