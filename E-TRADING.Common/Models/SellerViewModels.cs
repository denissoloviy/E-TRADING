using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace E_TRADING.Common.Models
{
    public class SellerViewEditViewModel
    {
        [DisplayName("User name")]
        public string UserName { get; set; }

        [DisplayName("Email")]
        public string Email { get; set; }

        [DisplayName("Phone number")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "This field is necessary")]
        [DisplayName("First name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "This field is necessary")]
        [DisplayName("Last name")]
        public string LastName { get; set; }

        [DisplayName("Addres")]
        public string Address { get; set; }

        [Required(ErrorMessage = "This field is necessary")]
        [DisplayName("Alias")]
        public string Alias { get; set; }

        [DisplayName("Office addres")]
        public string OfficeAddress { get; set; }

        [Required(ErrorMessage = "This field is necessary")]
        [DisplayName("Contact number")]
        public string ContactPhone { get; set; }

        public SellerProfileHelperViewModel Helper { get; set; }
    }

    public class SellerProductsViewModel
    {
        public ICollection<ProductViewModel> Products { get; set; }

        public SellerProfileHelperViewModel Helper { get; set; }
    }

    public class SellerProfileHelperViewModel
    {
        public int ProductsCount { get; set; }
        public int ActiveAuctionsCount { get; set; }
        public int ActiveOrdersCount { get; set; }
        public int InactiveOrdersCount { get; set; }
        public int ArchiveProductsCount { get; set; }
    }
}
