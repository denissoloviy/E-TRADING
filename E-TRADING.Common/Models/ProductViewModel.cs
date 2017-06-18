using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace E_TRADING.Common.Models
{
    public class ProductViewModel
    {
        public string Id { get; set; }        
        public string ShopCartId { get; set; }

        [Required(ErrorMessage = "This field is necessary")]
        [DisplayName("Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "This field is necessary")]
        [DisplayName("Description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "This field is necessary")]
        [DisplayName("Amount")]
        public int Amount { get; set; }

        [Required(ErrorMessage = "This field is necessary")]
        [DisplayName("Price")]
        public decimal Price { get; set; }

        [DisplayName("Date added")]
        public string AddedDate { get; set; }

        [Required(ErrorMessage = "This field is necessary")]
        [DisplayName("Category")]
        public string Category { get; set; }

        public string SellerId { get; set; }
        public string SellerName { get; set; }
        public bool IsDeleted { get; set; }

        public List<string> Images { get; set; }

        public ProductViewModel()
        {
            Images = new List<string>();
        }
    }
}
