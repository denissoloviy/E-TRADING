using AutoMapper;
using E_TRADING.Common;
using E_TRADING.Common.Models;
using E_TRADING.Data.Entities;
using E_TRADING.Data.Repositories;
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

            return RedirectToAction("UploadImages");
        }
        
        public ActionResult Delete(string id)
        {
            _productRepository.Delete(id);
            _productRepository.SaveChanges();
            return RedirectToAction("Products", "Seller");
        }

        //[HttpPost]
        //[Authorize(Roles =UserRole.Seller)]
        //public ActionResult UploadImage(string id, HttpPostedFileBase image)
        //{
        //    var imageId = Guid.NewGuid();
        //    var path = @"~/Content/Images/" + imageId;

        //    if (image == null)
        //        ModelState.AddModelError("ImageFile", "Виберіть зображення");

        //    if (!ValidateFileExtension())
        //    {
        //        ModelState.AddModelError("ImageFile",
        //            $"Виберіть файл відповідного формату: .bmp, .png, .jpg, .jpeg");
        //    }
        //    else
        //    {
        //        if (!string.IsNullOrEmpty(model.Image))
        //        {
        //            if (_fileManager.FileExists(Path.Combine(path, model.Image), true))
        //            {
        //                _fileManager.DeleteFile(Path.Combine(path, model.Image), true);
        //            }
        //        }
        //        var fileName = news.Slug + Path.GetExtension(model.ImageFile.FileName);
        //        _fileManager.UploadFile(model.ImageFile, path, fileName, true);
        //        news.Image = fileName;
        //    }

        //    return View();
        //}

        #region Helpers

        private bool ValidateFileExtension(HttpPostedFileBase file)
        {
            var ext = Path.GetExtension(file.FileName);
            return new string[]{ ".bmp", ".png", ".jpg", ".jpeg" }.Contains(Path.GetExtension(file.FileName));
        }

        #endregion
    }
}