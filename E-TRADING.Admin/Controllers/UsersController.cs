using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using E_TRADING.Data;
using E_TRADING.Data.Repositories;
using E_TRADING.Data.Entities;
using E_TRADING.Data.Managers;
using E_TRADING.Admin.Models;
using Microsoft.AspNet.Identity;


namespace E_TRADING.Admin.Controllers
{
    public class UsersController : Controller
    {
        IBuyerRepository _buyerRepository;
        ISellerRepository _sellerRepository;
        ApplicationUserManager _userManager;
        public UsersController(  IBuyerRepository buyerRepository,ISellerRepository sellerRepository,ApplicationUserManager userManager)
        {
             _buyerRepository=buyerRepository;
             _sellerRepository=sellerRepository;
             _userManager = userManager;
        }



        #region Users

        // GET: Users
        public ActionResult Index()
        {
            return View();
        }

        #endregion

        #region AdminUsers

        // GET: Users/AdminUsers
        public ActionResult AdminUsers()
        {
            return View();
        }

        #endregion
        #region UsersList
        public ActionResult UserList()
        {
            var x = _buyerRepository.GetAll().ToList();
            var y = _sellerRepository.GetAll().ToList();
            var z = new BuyerSellersViewModel()
            {
                Buyers = x,
                Sellers = y
            };

            return View(z);
        }
        public ActionResult GetBuyerInformation(string id)
        {
            if(String.IsNullOrEmpty(id))
            {
                return UserList();
            }
            var buyer = _buyerRepository.FindBy(b => b.Id == id);
            return View(buyer.First());
        }
        public ActionResult GetSellerInformation(string id)
        {
            if (String.IsNullOrEmpty(id))
            {
                return UserList();
            }
            var seller = _sellerRepository.FindBy(s => s.Id == id);  
            return View(seller.First());
        }
        public ActionResult BuyerDelete(string id)
        {
            _buyerRepository.Delete(id);    
            _buyerRepository.SaveChanges();
            return RedirectToAction("UserList");
        }       
        public ActionResult SellerDelete(string id)
        {
            _sellerRepository.Delete(id);
            _sellerRepository.SaveChanges();
            return RedirectToAction("UserList");
        }
        public ActionResult ChangePassword(string id,string changed="false")
        {
           if(String.IsNullOrEmpty(id))
               return RedirectToAction("UserList");
           if(changed=="true")
               ViewBag.Message = "Password change succesfull";
           ViewBag.UserId = id;
           return View();
        }
        [HttpPost]
        public ActionResult ChangePassword(string id, string password,string changed="false")
        {
            _userManager.RemovePassword(id);
            _userManager.AddPassword(id, password);
            return RedirectToAction("ChangePassword", new { id = id, changed = "true" });
        }     
        #endregion
    }
}