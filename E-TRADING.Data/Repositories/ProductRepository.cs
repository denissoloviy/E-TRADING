using E_TRADING.Data.Entities;
using E_TRADING.Data.Repositories.Generic;
using System.Linq;

namespace E_TRADING.Data.Repositories
{
    public interface IProductRepository : IGenericRepository<Product>
    {
    }

    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public override void Delete(Product entity)
        {
            entity.IsDeleted = true;
        }

        public override void Delete(string id)
        {
            var entity = _context.Products.FirstOrDefault(x => x.Id == id);
            entity.IsDeleted = true;
        }
    }
}