using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace E_TRADING.Common.Models
{
    public class BuyerViewEditViewModel : BuyerProfileHelperViewModel
    {
        [DisplayName("User name")]
        public string UserName { get; set; }

        [DisplayName("Email")]
        public string Email { get; set; }

        [DisplayName("Phone number")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "This field is necessary")]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "This field is necessary")]
        [DisplayName("Last name")]
        public string LastName { get; set; }

        [DisplayName("Addres")]
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
        public int ShoppingCartCount { get; set; }
        public int ActiveOrdersCount { get; set; }
        public int InactiveOrdersCount { get; set; }
    }
}
