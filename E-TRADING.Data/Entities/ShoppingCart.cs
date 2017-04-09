using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_TRADING.Data.Entities
{
    public class ShoppingCart : BaseEntity
    {
        public int Amount { get; set; }

        [Required(ErrorMessage = "Поле обов'язкове для заповнення")]
        [DisplayName("Покупець")]
        public string BuyerId { get; set; }
        [DisplayName("Покупець")]
        [ForeignKey("BuyerId")]
        public virtual Buyer Buyer { get; set; }

        [Required(ErrorMessage = "Поле обов'язкове для заповнення")]
        [DisplayName("Товар")]
        public string ProductId { get; set; }
        [DisplayName("Товар")]
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
    }
}
