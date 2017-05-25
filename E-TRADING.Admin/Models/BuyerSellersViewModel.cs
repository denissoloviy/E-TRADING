using System.Collections.Generic;
using E_TRADING.Data.Entities;

namespace E_TRADING.Admin.Models
{
    public class BuyerSellersViewModel
    {
        public List<Buyer> Buyers { get; set; }
        public List<Seller> Sellers { get; set; }
    }
}