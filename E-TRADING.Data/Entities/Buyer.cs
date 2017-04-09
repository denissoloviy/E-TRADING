using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_TRADING.Data.Entities
{
    public class Buyer : BaseEntity
    {
        // todo Хай буде в Address в User?
        //[DisplayName("Поштовий індекс")]
        //public string ZipCode { get; set; }
        [Key, ForeignKey("User")]
        public override string Id { get; set; }
        [DisplayName("Користувач")]
        public virtual User User { get; set; }

        [DisplayName("Замовлення")]
        public virtual ICollection<Order> Orders { get; set; }

        [DisplayName("Кошик")]
        public virtual ICollection<ShoppingCart> ShoppingCarts { get; set; }
    }
}
