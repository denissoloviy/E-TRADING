using E_TRADING.Data.Entities;
using E_TRADING.Data.Repositories.Generic;

namespace E_TRADING.Data.Repositories
{
    public interface IBuyerRepository: IGenericRepository<Buyer>
    {
    }

    public class BuyerRepository : GenericRepository<Buyer>, IBuyerRepository
    {
        private readonly ApplicationDbContext _context;
        public BuyerRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
