using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_TRADING.Data.Entities
{
    public class Category : BaseEntity, ISoftDelete
    {
        [Required(ErrorMessage = "This field is necessary")]
        [DisplayName("Name")]
        public string Name { get; set; }

        [DisplayName("Main category")]
        public string MasterCategoryId { get; set; }
        [DisplayName("Main category")]
        [ForeignKey("MasterCategoryId")]
        public virtual Category MasterCategory { get; set; }

        [DisplayName("Sub category")]
        public virtual ICollection<Category> Categories { get; set; }

        public bool IsDeleted { get; set; }
    }
}
