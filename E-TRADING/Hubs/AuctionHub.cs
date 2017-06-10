using System;
using Microsoft.AspNet.SignalR;

namespace E_TRADING.Hubs
{
    public class AuctionHub : Hub
    {
        public static void RefreshAuction(string auctionId, DateTime auctionEndDate, decimal lastBid, string userName)
        {
            var newTime = auctionEndDate - DateTime.UtcNow;
            var stringNextTime = newTime.ToString(@"hh\:mm\:ss");
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<AuctionHub>();
            hubContext.Clients.All.refreshAuction(auctionId, stringNextTime, lastBid, userName);
        }

        public static void FinishAuction(string auctionId)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<AuctionHub>();
            hubContext.Clients.All.finishAuction(auctionId);
        }

        public static void BeginAuction(string auctionId)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<AuctionHub>();
            hubContext.Clients.All.beginAuction(auctionId);
        }
    }
}