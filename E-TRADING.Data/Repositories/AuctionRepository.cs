using E_TRADING.Data.Entities;
using E_TRADING.Data.Repositories.Generic;

namespace E_TRADING.Data.Repositories
{
    public interface IAuctionRepository : IGenericRepository<Auction>
    {
    }

    public class AuctionRepository : GenericRepository<Auction>, IAuctionRepository
    {
        private readonly ApplicationDbContext _context;
        public AuctionRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
