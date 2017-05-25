using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_TRADING.Data.Entities
{
    public class Image : BaseEntity
    {
        [Required(ErrorMessage = "Поле обов'язкове для заповнення")]
        [DisplayName("Зображення")]
        public int Index{ get; set; }

        [Required(ErrorMessage = "Поле обов'язкове для заповнення")]
        [DisplayName("Формат")]
        public string Extention { get; set; }

        [Required(ErrorMessage = "Поле обов'язкове для заповнення")]
        [DisplayName("Товар")]
        public string ProductId { get; set; }
        [DisplayName("Товар")]
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
    }
}
