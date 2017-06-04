using E_TRADING.Common.OrderStatuses;
using E_TRADING.Data;
using E_TRADING.Data.Entities;
using E_TRADING.Hubs;
using Quartz;
using System;
using System.Linq;

namespace E_TRADING.Jobs
{
    public class AuctionUpdater : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            using (var _db = new ApplicationDbContext())
            {
                var auctions = _db.Auctions.Where(item => !item.IsFinished);
                foreach (var auction in auctions)
                {
                    if (auction.DateEnd <= DateTime.UtcNow)
                    {
                        AuctionHub.FinishAuction(auction.Id);
                        if (auction.LastBuyer != null)
                        {
                            auction.IsFinished = true;
                            _db.Orders.Add(new Order
                            {
                                Amount = auction.Product.Amount,
                                BuyerId = auction.BuyerId,
                                FullPrice = auction.Product.Amount * auction.Product.Price,
                                ProductId = auction.Id,
                                ShippingAddress = auction.LastBuyer.User.Address,
                                Status = OrderStatus.InProccess
                            });
                            var product = _db.Products.Find(auction.Id);
                            product.Amount -= product.Amount;
                        }
                        else
                        {
                            auction.IsStarted = false;
                        }
                    }
                    else if (auction.DateStart <= DateTime.UtcNow && auction.DateEnd >= DateTime.UtcNow && !auction.IsStarted)
                    {
                        auction.IsStarted = true;
                        AuctionHub.BeginAuction(auction.Id);
                    }
                    else if (auction.IsStarted)
                    {
                        AuctionHub.RefreshAuction(auction.Id, auction.DateEnd, auction.LastBid, auction.LastBuyer?.User.UserName);
                    }
                }
                _db.SaveChanges();
            }
        }
    }
}