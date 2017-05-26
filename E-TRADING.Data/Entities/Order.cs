using E_TRADING.Common.OrderStatuses;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_TRADING.Data.Entities
{
    public class Order : BaseEntity
    {
        [DisplayName("Статус замовлення")]
        public OrderStatus Status { get; set; }

        [DisplayName("Кількість")]
        public int Amount { get; set; }

        [Required(ErrorMessage = "Поле обов'язкове для заповнення")]
        [DisplayName("Загальна ціна")]
        public decimal FullPrice { get; set; }

        [Required(ErrorMessage = "Поле обов'язкове для заповнення")]
        [DisplayName("Адреса доставки")]
        public string ShippingAddress { get; set; }

        [DisplayName("Номер замовлення")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderNumber { get; set; }

        [NotMapped]
        [DisplayName("Номер замовлення")]
        public string OrderNumberString { get { return OrderNumber.ToString("D8"); } }
        
        [DisplayName("Номер накладної")]
        public string InvoiceNumber { get; set; }

        [Required(ErrorMessage = "Поле обов'язкове для заповнення")]
        [DisplayName("Товар")]
        public string ProductId { get; set; }
        [DisplayName("Товар")]
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }

        [Required(ErrorMessage = "Поле обов'язкове для заповнення")]
        [DisplayName("Покупець")]
        public string BuyerId { get; set; }
        [DisplayName("Покупець")]
        [ForeignKey("BuyerId")]
        public virtual Buyer Buyer { get; set; }
    }
}
