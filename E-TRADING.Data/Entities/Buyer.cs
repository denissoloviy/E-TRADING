using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_TRADING.Data.Entities
{
    public class Buyer : BaseEntity, ISoftDelete
    {
        [Key, ForeignKey("User")]
        public override string Id { get; set; }
        [DisplayName("User")]
        public virtual User User { get; set; }

        [DisplayName("Order")]
        public virtual ICollection<Order> Orders { get; set; }

        [DisplayName("Shopping cart")]
        public virtual ICollection<ShoppingCart> ShoppingCarts { get; set; }

        public bool IsDeleted { get; set; }
    }
}
