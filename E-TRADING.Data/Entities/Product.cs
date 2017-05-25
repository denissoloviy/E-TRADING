using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_TRADING.Data.Entities
{
    public class Product : BaseEntity
    {
        [Required(ErrorMessage = "Поле обов'язкове для заповнення")]
        [DisplayName("Назва")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Поле обов'язкове для заповнення")]
        [DisplayName("Опис")]
        public string Description { get; set; }

        [DisplayName("Код товару")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductCode { get; set; }

        [NotMapped]
        [DisplayName("Код товару")]
        public string ProductCodeString { get { return ProductCode.ToString("D8"); } }

        [DisplayName("Доступна к-сть")]
        public int Amount { get; set; }

        [Required(ErrorMessage = "Поле обов'язкове для заповнення")]
        [DisplayName("Ціна")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Поле обов'язкове для заповнення")]
        [DisplayName("Продавець")]
        public string SellerId { get; set; }
        [DisplayName("Продавець")]
        [ForeignKey("SellerId")]
        public virtual Seller Seller { get; set; }

        [Required(ErrorMessage = "Поле обов'язкове для заповнення")]
        [DisplayName("Категорія")]
        public string CategoryId { get; set; }
        [DisplayName("Категорія")]
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        [DisplayName("Замовлення")]
        public virtual ICollection<Order> Orders { get; set; }
        [DisplayName("Зображення")]
        public  virtual ICollection<Image> Images { get; set; }
    }
}
