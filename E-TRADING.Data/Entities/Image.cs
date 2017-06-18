using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_TRADING.Data.Entities
{
    public class Image : BaseEntity
    {
        [Required(ErrorMessage = "This field is necessary")]
        [DisplayName("Image")]
        public int Index{ get; set; }

        [Required(ErrorMessage = "This field is necessary")]
        [DisplayName("Format")]
        public string Extention { get; set; }

        [Required(ErrorMessage = "This field is necessary")]
        [DisplayName("Product")]
        public string ProductId { get; set; }
        [DisplayName("Product")]
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
    }
}
