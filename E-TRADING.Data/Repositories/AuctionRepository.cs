using E_TRADING.Data.Entities;
using E_TRADING.Data.Repositories.Generic;
using System.Linq;

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

        public override void Delete(Auction entity)
        {
            entity.IsDeleted = true;
        }

        public override void Delete(string id)
        {
            var entity = _context.Auctions.FirstOrDefault(x => x.Id == id);
            entity.IsDeleted = true;
        }
    }
}
