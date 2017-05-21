using E_TRADING.Data.Entities;
using E_TRADING.Data.Repositories.Generic;

namespace E_TRADING.Data.Repositories
{
    public interface IShoppingCartRepository : IGenericRepository<ShoppingCart>
    {
    }

    public class ShoppingCartRepository : GenericRepository<ShoppingCart>, IShoppingCartRepository
    {
        private readonly ApplicationDbContext _context;
        public ShoppingCartRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
