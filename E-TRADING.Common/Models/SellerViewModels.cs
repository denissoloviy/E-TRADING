using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace E_TRADING.Common.Models
{
    public class SellerViewEditViewModel
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

        [Required(ErrorMessage = "Поле обов'язкове для заповнення")]
        [DisplayName("Псевдонім")]
        public string Alias { get; set; }

        [DisplayName("Адреса офісу")]
        public string OfficeAddress { get; set; }

        [Required(ErrorMessage = "Поле обов'язкове для заповнення")]
        [DisplayName("Контактний номер телефону")]
        public string ContactPhone { get; set; }

        public int ProductsCount { get; set; }
    }

    public class ProductsViewModel
    {
        public ICollection<ProductViewModel> Products { get; set; }
        public int ProductsCount { get; set; }
    }
}
