using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_TRADING.Data.Entities
{
    public class Seller : BaseEntity, ISoftDelete
    {
        [Key, ForeignKey("User")]
        public override string Id { get; set; }
        [DisplayName("Alias")]
        public string Alias { get; set; }

        [DisplayName("Office addres")]
        public string OfficeAddress { get; set; }

        [DisplayName("Passport number")]
        public string Passport { get; set; }

        [DisplayName("Contact number")]
        public string ContactPhone { get; set; }

        [DisplayName("User confirmed")]
        public bool IsConfirmed { get; set; }

        [DisplayName("Error message")]
        public string ErrorText { get; set; }

        [DisplayName("User")]
        public virtual User User { get; set; }

        [DisplayName("Products")]
        public virtual ICollection<Product> Products { get; set; }

        public bool IsDeleted { get; set; }
    }
}
