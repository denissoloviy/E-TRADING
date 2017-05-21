using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_TRADING.Common.Models
{
    public class OrderViewModel
    {
        [DisplayName("ID")]
        public string Id { get; set; }

        [DisplayName("Ім'я")]
        public string Name { get; set; }

        [DisplayName("Статус замовлення")]
        public String Status { get; set; }

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
