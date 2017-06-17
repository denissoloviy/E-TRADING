using System;
using System.Linq;
using System.Web.Mvc;
using E_TRADING.Data.Repositories;
using E_TRADING.Data.Entities;
using E_TRADING.Data.Managers;
using E_TRADING.Admin.Models;
using E_TRADING.Common;
using Microsoft.AspNet.Identity;

namespace E_TRADING.Admin.Controllers
{
    [Authorize(Roles = UserRole.SuperAdmin + ", " + UserRole.Administrator)]
    public class UsersController : Controller
    {
        IBuyerRepository _buyerRepository;
        ISellerRepository _sellerRepository;
        ApplicationUserManager _userManager;
        IUserRepository _userRepository;
        public UsersController(IBuyerRepository buyerRepository,
            ISellerRepository sellerRepository,
            ApplicationUserManager userManager,
            IUserRepository userRepository)
        {
            _buyerRepository = buyerRepository;
            _sellerRepository = sellerRepository;
            _userManager = userManager;
            _userRepository = userRepository;
        }

        #region AdminUsers

        // GET: Users/AdminUsers
        [Authorize(Roles = UserRole.SuperAdmin)]
        public ActionResult AdminUsers()
        {
            var users = _userRepository.GetApplicationUsersInRole(UserRole.Administrator).ToList();
            return View(users);
        }

        [Authorize(Roles = UserRole.SuperAdmin)]
        public ActionResult AddAdmin()
        {
            return View();
        }

        [Authorize(Roles = UserRole.SuperAdmin)]
        [HttpPost]
        public ActionResult AddAdmin(string email, string pass)
        {
            var user = new User { UserName = email, Email = email };
            var result = _userManager.Create(user, pass);
            if (result.Succeeded)
            {
                _userManager.AddToRole(user.Id, UserRole.Administrator);
                ViewBag.Succes = "Додано нового адміна!";
            }
            else
            {
                ViewBag.Error = result.Errors.First();
            }
            return View();
        }

        [Authorize(Roles = UserRole.SuperAdmin)]
        public ActionResult DeleteAdmin(string id)
        {
            _userManager.Delete(_userManager.FindById(id));
            return RedirectToAction("AdminUsers");
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
            if (string.IsNullOrEmpty(id))
            {
                return UserList();
            }
            var buyer = _buyerRepository.Find(id);
            return View(buyer);
        }

        public ActionResult GetSellerInformation(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return UserList();
            }
            var seller = _sellerRepository.Find(id);
            return View(seller);
        }

        public ActionResult BuyerDelete(string id)
        {
            _buyerRepository.Delete(id);
            _buyerRepository.SaveChanges();
            return RedirectToAction("UserList");
        }

        public ActionResult BuyerUnDelete(string id)
        {
            _buyerRepository.UnDelete(id);
            _buyerRepository.SaveChanges();
            return RedirectToAction("UserList");
        }

        public ActionResult SellerDelete(string id)
        {
            _sellerRepository.Delete(id);
            _sellerRepository.SaveChanges();
            return RedirectToAction("UserList");
        }
        
        public ActionResult SellerUnDelete(string id)
        {
            _sellerRepository.UnDelete(id);
            _sellerRepository.SaveChanges();
            return RedirectToAction("UserList");
        }

        public ActionResult ChangePassword(string id, string changed = "false")
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("UserList");
            }
            if (changed == "true")
            {
                ViewBag.Message = "Password change succesfull";
            }
            ViewBag.UserId = id;
            return View();
        }

        [HttpPost]
        public ActionResult ChangePassword(string id, string password, string changed = "false")
        {
            _userManager.RemovePassword(id);
            _userManager.AddPassword(id, password);
            return RedirectToAction("ChangePassword", new { id = id, changed = "true" });
        }

        public ActionResult Confirm(string id)
        {
            var seller = _sellerRepository.Find(id);
            return View(seller);
        }

        [HttpPost]
        public ActionResult Confirm(string id, string errorText, bool isConfirmed = false)
        {
            var seller = _sellerRepository.Find(id);
            seller.IsConfirmed = isConfirmed;
            seller.ErrorText = errorText;
            _sellerRepository.SaveChanges();
            return RedirectToAction("GetSellerInformation", new { id = id });
        }

        #endregion
    }
}