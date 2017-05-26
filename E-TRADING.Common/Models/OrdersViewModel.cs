using E_TRADING.Common.OrderStatuses;
using System.ComponentModel;

namespace E_TRADING.Common.Models
{
    public class OrderViewModel
    {
        public string Id { get; set; }        
        public string ProductId { get; set; }

        [DisplayName("Ім'я")]
        public string Name { get; set; }

        [DisplayName("Статус замовлення")]
        public string Status { get; set; }

        public OrderStatusType StatusType { get; set; }

        [DisplayName("Кількість")]
        public int Amount { get; set; }

        [DisplayName("Загальна ціна")]
        public decimal FullPrice { get; set; }

        [DisplayName("Адреса доставки")]
        public string ShippingAddress { get; set; }

        [DisplayName("Покупець")]
        public string Buyer { get; set; }

        [DisplayName("Продавець")]
        public string Seller { get; set; }
    }
}
