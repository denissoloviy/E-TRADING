using E_TRADING.Data.Entities;
using E_TRADING.Data.Repositories.Generic;

namespace E_TRADING.Data.Repositories
{
    public interface ISellerRepository : IGenericRepository<Seller>
    {
    }

    public class SellerRepository : GenericRepository<Seller>, ISellerRepository
    {
        private readonly ApplicationDbContext _context;
        public SellerRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
