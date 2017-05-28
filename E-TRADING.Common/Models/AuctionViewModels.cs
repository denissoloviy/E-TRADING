using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace E_TRADING.Common.Models
{
    public class AuctionViewModel
    {
        public string Id { get; set; }
        public bool IsStarted { get; set; }

        [DisplayName("Час старту аукціону")]
        public string DateStart { get; set; }
        
        [DisplayName("Час завершення аукціону")]
        public string DateEnd { get; set; }
        
        [DisplayName("Мінімальний крок")]
        public decimal MinStep { get; set; }
        
        [DisplayName("Початкова ціна")]
        public decimal StartPrice { get; set; }

        [DisplayName("Остання ставка")]
        public decimal LastBid { get; set; }

        [DisplayName("Покупець")]
        public string BuyerName { get; set; }
        
        [DisplayName("Товар")]
        public string ProductName { get; set; }

        public bool IsDeleted { get; set; }

        public ProductViewModel Product { get; set; }
    }

    public class BidModel
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public decimal NewBid { get; set; }
    }
}
