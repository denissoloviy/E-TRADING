using AutoMapper;
using E_TRADING.Common.Models;
using E_TRADING.Data;
using E_TRADING.Data.Entities;
using E_TRADING.Data.Managers;
using E_TRADING.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_TRADING.Admin.Controllers
{
    [Authorize(Roles = "SuperAdmin, Administrator")]
    public class CatalogsController : Controller
    {
        IMapper _mapper;
        ApplicationUserManager _userManager;
        ICategoryRepository _categoryRepository;
        public CatalogsController(IMapper mapper, ApplicationUserManager userManager, ICategoryRepository categoryRepository)
        {
            _mapper = mapper;
            _userManager = userManager;
            _categoryRepository = categoryRepository;
        }

        // GET: Catalogs
        public ActionResult Index(string id)
        {
            var subCategories = new List<Category>();
            if (!string.IsNullOrEmpty(id))
                subCategories = _categoryRepository.FirstOrDefault(o => o.Id == id).Categories.ToList();
            else
                subCategories = _categoryRepository.FindBy(o => o.MasterCategory == null).ToList();

            ViewBag.Id = id;
            return View("Index", subCategories);
        }

        // GET: Catalogs
        public ActionResult GoBack(string id)
        {
            var parentCategoty = _categoryRepository.FirstOrDefault(o => o.Id == id).MasterCategory;

            return Index(parentCategoty?.Id);
        }

        // POST: Catalogs
        public ActionResult Create(string name, string id)
        {
            if (!string.IsNullOrEmpty(name))
            {
                _categoryRepository.Add(new Category()
                {
                    Name = name,
                    MasterCategoryId = string.IsNullOrEmpty(id)? null : id
                });
                _categoryRepository.SaveChanges();
            }

            return Index(id);
        }

        // POST: Catalogs
        public ActionResult Delete(string id)
        {
            var parentCategoty = _categoryRepository.FirstOrDefault(o => o.Id == id).MasterCategory;

            _categoryRepository.Delete(id);
            _categoryRepository.SaveChanges();

            return Index(parentCategoty?.Id);
        }

        // GET: Catalogs
        [HttpGet]
        public ActionResult Edit(string id)
        {
            var catalog = _categoryRepository.FirstOrDefault(c => c.Id == id);
            return View(catalog);
        }

        // POST: Catalogs
        [HttpPost]
        public ActionResult Edit(Category categoryData)
        {
            var catalog = _categoryRepository.FirstOrDefault(c => c.Id == categoryData.Id);
            catalog.Name = categoryData.Name;
            _categoryRepository.SaveChanges();

            return Index(catalog?.MasterCategoryId);
        }

        // POST: Catalogs
        public ActionResult GoTo(string id)
        {
            return Index(id);
        }


    }
}