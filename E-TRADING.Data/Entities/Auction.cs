using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_TRADING.Data.Entities
{
    public class Auction : BaseEntity
    {
        [Key, ForeignKey("Product")]
        public override string Id { get; set; }

        [DisplayName("Час старту аукціону")]
        public DateTime DateStart { get; set; }

        [DisplayName("Час завершення аукціону")]
        public DateTime DateEnd { get; set; }

        [DisplayName("Мінімальний крок")]
        public decimal MinStep { get; set; }

        [DisplayName("Остання ставка")]
        public decimal LastBid { get; set; }

        [Required(ErrorMessage = "Поле обов'язкове для заповнення")]
        [DisplayName("Покупець")]
        public string BuyerId { get; set; }
        [DisplayName("Покупець")]
        [ForeignKey("BuyerId")]
        public virtual Buyer LastBuyer { get; set; }

        [DisplayName("Товар")]
        public virtual Product Product { get; set; }
    }
}
