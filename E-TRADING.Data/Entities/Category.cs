using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_TRADING.Data.Entities
{
    public class Category : BaseEntity
    {
        [Required(ErrorMessage = "Поле обов'язкове для заповнення")]
        [DisplayName("Назва")]
        public string Name { get; set; }

        [DisplayName("Надкатегорія")]
        public string MasterCategoryId { get; set; }
        [DisplayName("Надкатегорія")]
        [ForeignKey("MasterCategoryId")]
        public virtual Category MasterCategory { get; set; }

        [DisplayName("Підкатегорії")]
        public virtual ICollection<Category> Categories { get; set; }
    }
}
