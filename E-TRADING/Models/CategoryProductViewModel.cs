using E_TRADING.Data.Entities;
using System.Collections.Generic;

namespace E_TRADING.Models
{
    public class CategoryProductViewModel
    {
        public List<Category> Categories { get; set; }
        public List<Product> Products { get; set; }
    }
}