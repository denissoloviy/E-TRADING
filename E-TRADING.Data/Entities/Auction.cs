using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_TRADING.Data.Entities
{
    public class Auction : BaseEntity, ISoftDelete
    {
        [Required(ErrorMessage = "Поле обов'язкове для заповнення")]
        [Key, ForeignKey("Product")]
        public override string Id { get; set; }

        [Required(ErrorMessage = "Поле обов'язкове для заповнення")]
        [DisplayName("Час старту аукціону")]
        public DateTime DateStart { get; set; }

        [Required(ErrorMessage = "Поле обов'язкове для заповнення")]
        [DisplayName("Час завершення аукціону")]
        public DateTime DateEnd { get; set; }

        [Required(ErrorMessage = "Поле обов'язкове для заповнення")]
        [DisplayName("Мінімальний крок")]
        public decimal MinStep { get; set; }

        [DisplayName("Остання ставка")]
        public decimal LastBid { get; set; }

        [DisplayName("Покупець")]
        public string BuyerId { get; set; }

        [DisplayName("Покупець")]
        [ForeignKey("BuyerId")]
        public virtual Buyer LastBuyer { get; set; }

        [DisplayName("Товар")]
        public virtual Product Product { get; set; }

        public bool IsDeleted { get; set; }
    }
}
