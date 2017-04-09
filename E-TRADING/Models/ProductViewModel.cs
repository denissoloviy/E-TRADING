using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_TRADING.Models
{
    public class ProductViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Amount { get; set; }
        public decimal Price { get; set; }
        public string Status { get; set; }

        public string SellerId { get; set; }
        public string SellerName { get; set; }
    }
}