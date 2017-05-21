using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace E_TRADING.Common.Models
{
    public class BuyerViewEditViewModel : BuyerProfileHelperViewModel
    {
        [DisplayName("Ім'я користувача")]
        public string UserName { get; set; }

        [DisplayName("Email")]
        public string Email { get; set; }

        [DisplayName("Номер телефону")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Поле обов'язкове для заповнення")]
        [DisplayName("Ім'я")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Поле обов'язкове для заповнення")]
        [DisplayName("Прізвище")]
        public string LastName { get; set; }

        [DisplayName("Адреса")]
        public string Address { get; set; }

        public BuyerProfileHelperViewModel Helper { get; set; }
    }
    
    public class ShoppingCartViewModel
    {
        public ICollection<ProductViewModel> Products { get; set; }
        public BuyerProfileHelperViewModel Helper { get; set; }
    }

    public class BuyerProfileHelperViewModel
    {
        public int ActiveOrdersCount { get; set; }
        public int InactiveOrdersCount { get; set; }
        public int ShoppingCartCount { get; set; }
    }
}
