using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_TRADING.Data.Entities
{
    public class ShoppingCart : BaseEntity
    {
        public int Amount { get; set; }

        [Required(ErrorMessage = "This field is necessary")]
        [DisplayName("Buyer")]
        public string BuyerId { get; set; }
        [DisplayName("Buyer")]
        [ForeignKey("BuyerId")]
        public virtual Buyer Buyer { get; set; }

        [Required(ErrorMessage = "This field is necessary")]
        [DisplayName("Product")]
        public string ProductId { get; set; }
        [DisplayName("Product")]
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
    }
}
