using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_TRADING.Data.Entities
{
    public class Product : BaseEntity, ISoftDelete
    {
        [Required(ErrorMessage = "This field is necessary")]
        [DisplayName("Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "This field is necessary")]
        [DisplayName("Description")]
        public string Description { get; set; }

        [DisplayName("Product code")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductCode { get; set; }

        [NotMapped]
        [DisplayName("Product code")]
        public string ProductCodeString { get { return ProductCode.ToString("D8"); } }

        [DisplayName("Amoun available")]
        public int Amount { get; set; }

        [Required(ErrorMessage = "This field is necessary")]
        [DisplayName("Price")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "This field is necessary")]
        [DisplayName("Seller")]
        public string SellerId { get; set; }
        [DisplayName("Seller")]
        [ForeignKey("SellerId")]
        public virtual Seller Seller { get; set; }

        [Required(ErrorMessage = "This field is necessary")]
        [DisplayName("Category")]
        public string CategoryId { get; set; }
        [DisplayName("Category")]
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        [DisplayName("Order")]
        public virtual ICollection<Order> Orders { get; set; }
        [DisplayName("Image")]
        public virtual ICollection<Image> Images { get; set; }

        public bool IsDeleted { get; set; }
    }
}
