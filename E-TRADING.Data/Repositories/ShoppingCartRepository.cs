using E_TRADING.Data.Entities;
using E_TRADING.Data.Repositories.Generic;

namespace E_TRADING.Data.Repositories
{
    public interface IShoppingCartRepository : IGenericRepository<ShoppingCart>
    {
        bool AddProduct(Product entity);
    }

    public class ShoppingCartRepository : GenericRepository<ShoppingCart>, IShoppingCartRepository
    {
        private readonly ApplicationDbContext _context;
        public ShoppingCartRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public override void Add(ShoppingCart entity)
        {
            if (!AddProduct(entity.Product))
            {
                base.Add(entity);
            }
        }

        public bool AddProduct(Product entity)
        {
            var shoppingCart = FirstOrDefault(item => item.ProductId == entity.Id);
            if (shoppingCart != null)
            {
                shoppingCart.Amount++;
                return true;
            }
            return false;
        }
    }
}
