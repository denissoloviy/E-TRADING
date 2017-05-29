using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_TRADING.Data.Entities
{
    public class Auction : BaseEntity
    {
        [Required(ErrorMessage = "Поле обов'язкове для заповнення")]
        [Key, ForeignKey("Product")]
        public override string Id { get; set; }

        [Required(ErrorMessage = "Поле обов'язкове для заповнення")]
        [DisplayName("Час старту аукціону")]
        [DataType(DataType.DateTime)]
        public DateTime DateStart { get; set; }

        [Required(ErrorMessage = "Поле обов'язкове для заповнення")]
        [DisplayName("Час завершення аукціону")]
        [DataType(DataType.DateTime)]
        public DateTime DateEnd { get; set; }

        [Required(ErrorMessage = "Поле обов'язкове для заповнення")]
        [DisplayName("Мінімальний крок")]
        public decimal MinStep { get; set; }

        [Required(ErrorMessage = "Поле обов'язкове для заповнення")]
        [DisplayName("Початкова ціна")]
        public decimal StartPrice { get; set; }

        [DisplayName("Остання ставка")]
        public decimal LastBid { get; set; }

        [DisplayName("Покупець")]
        public string BuyerId { get; set; }

        [DisplayName("Покупець")]
        [ForeignKey("BuyerId")]
        public virtual Buyer LastBuyer { get; set; }

        [DisplayName("Товар")]
        public virtual Product Product { get; set; }
        
        public bool IsFinished { get; set; }
    }
}
