using E_TRADING.Common.OrderStatuses;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_TRADING.Data.Entities
{
    public class Order : BaseEntity
    {
        [DisplayName("Order status")]
        public OrderStatus Status { get; set; }

        [DisplayName("Amount")]
        public int Amount { get; set; }

        [Required(ErrorMessage = "This field is necessary")]
        [DisplayName("Full price")]
        public decimal FullPrice { get; set; }

        [Required(ErrorMessage = "This field is necessary")]
        [DisplayName("Delivery addres")]
        public string ShippingAddress { get; set; }

        [DisplayName("Order number")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderNumber { get; set; }

        [NotMapped]
        [DisplayName("Order number")]
        public string OrderNumberString { get { return OrderNumber.ToString("D8"); } }
        
        [DisplayName("Invoice number")]
        public string InvoiceNumber { get; set; }

        [Required(ErrorMessage = "This field is necessary")]
        [DisplayName("Product")]
        public string ProductId { get; set; }
        [DisplayName("Product")]
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }

        [Required(ErrorMessage = "This field is necessary")]
        [DisplayName("Buyer")]
        public string BuyerId { get; set; }
        [DisplayName("Buyer")]
        [ForeignKey("BuyerId")]
        public virtual Buyer Buyer { get; set; }
    }
}
