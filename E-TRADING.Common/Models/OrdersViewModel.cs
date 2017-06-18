using E_TRADING.Common.OrderStatuses;
using System.ComponentModel;

namespace E_TRADING.Common.Models
{
    public class OrderViewModel
    {
        public string Id { get; set; }        
        public string ProductId { get; set; }

        [DisplayName("Name")]
        public string Name { get; set; }

        [DisplayName("Order status")]
        public OrderStatus Status { get; set; }

        public OrderStatusType StatusType { get; set; }

        [DisplayName("Order number")]
        public string OrderNumberString { get; set; }

        [DisplayName("Invoice number")]
        public string InvoiceNumber { get; set; }

        [DisplayName("Amount")]
        public int Amount { get; set; }

        [DisplayName("Full price")]
        public decimal FullPrice { get; set; }

        [DisplayName("Delivery addres")]
        public string ShippingAddress { get; set; }

        [DisplayName("Buyer")]
        public string Buyer { get; set; }

        [DisplayName("Seller")]
        public string Seller { get; set; }

        [DisplayName("Image")]
        public string MainImage { get; set; }
    }
}
