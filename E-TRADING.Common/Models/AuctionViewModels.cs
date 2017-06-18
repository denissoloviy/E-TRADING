using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace E_TRADING.Common.Models
{
    public class AuctionViewModel
    {
        public string Id { get; set; }
        public bool IsStarted { get; set; }
        public bool IsFinished { get; set; }

        public string TimeLeft { get; set; }

        [DisplayName("Auction starting time")]
        public string DateStart { get; set; }
        
        [DisplayName("Actuion end time")]
        public string DateEnd { get; set; }
        
        [DisplayName("Min step")]
        public decimal MinStep { get; set; }
        
        [DisplayName("Starting price")]
        public decimal StartPrice { get; set; }

        [DisplayName("Last bid")]
        public decimal LastBid { get; set; }

        [DisplayName("Buyer")]
        public string BuyerName { get; set; }
        
        [DisplayName("Product")]
        public string ProductName { get; set; }

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
