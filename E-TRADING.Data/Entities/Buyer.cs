using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_TRADING.Data.Entities
{
    public class Buyer : BaseEntity
    {
        //[DisplayName("Поштовий індекс")]
        //public string ZipCode { get; set; } ////todo Хай буде в Address в User?
        [Key, ForeignKey("User")]
        public override string Id { get; set; }

        //[Required(ErrorMessage = "Поле обов'язкове для заповнення")]
        //[DisplayName("Користувач")]
        //public string UserId { get; set; }
        //[ForeignKey("UserId")]
        [DisplayName("Користувач")]
        public virtual User User { get; set; }

        [DisplayName("Замовлення")]
        public virtual ICollection<Order> Orders { get; set; }
    }
}
