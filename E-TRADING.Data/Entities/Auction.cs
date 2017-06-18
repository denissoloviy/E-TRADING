using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_TRADING.Data.Entities
{
    public class Auction : BaseEntity
    {
        [Required(ErrorMessage = "This field is necessary")]
        [Key, ForeignKey("Product")]
        public override string Id { get; set; }

        [Required(ErrorMessage = "This field is necessary")]
        [DisplayName("Auction start time")]
        [DataType(DataType.DateTime)]
        public DateTime DateStart { get; set; }

        [Required(ErrorMessage = "This field is necessary")]
        [DisplayName("Auction end time")]
        [DataType(DataType.DateTime)]
        public DateTime DateEnd { get; set; }

        [Required(ErrorMessage = "This field is necessary")]
        [DisplayName("Min step")]
        public decimal MinStep { get; set; }

        [Required(ErrorMessage = "This field is necessary")]
        [DisplayName("Starting price")]
        public decimal StartPrice { get; set; }

        [DisplayName("Last bid")]
        public decimal LastBid { get; set; }

        [DisplayName("Buyer")]
        public string BuyerId { get; set; }

        [DisplayName("Buyer")]
        [ForeignKey("BuyerId")]
        public virtual Buyer LastBuyer { get; set; }

        [DisplayName("Product")]
        public virtual Product Product { get; set; }

        public bool IsStarted { get; set; }
        public bool IsFinished { get; set; }
    }
}
