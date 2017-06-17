﻿using System;
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
        [DisplayName("Псевдонім")]
        public string Alias { get; set; }

        [DisplayName("Адреса офісу")]
        public string OfficeAddress { get; set; }

        [DisplayName("Номер паспорту")]
        public string Passport { get; set; }

        [DisplayName("Контактний номер телефону")]
        public string ContactPhone { get; set; }

        [DisplayName("Користувач підтверджений")]
        public bool IsConfirmed { get; set; }

        [DisplayName("Текст помилки")]
        public string ErrorText { get; set; }

        [DisplayName("Користувач")]
        public virtual User User { get; set; }

        [DisplayName("Товари")]
        public virtual ICollection<Product> Products { get; set; }

        public bool IsDeleted { get; set; }
    }
}
