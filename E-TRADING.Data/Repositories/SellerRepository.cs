using E_TRADING.Data.Entities;
using E_TRADING.Data.Repositories.Generic;
using System.Linq;

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

        public override void Delete(Seller entity)
        {
            entity.IsDeleted = true;
        }

        public override void Delete(string id)
        {
            var entity = _context.Sellers.FirstOrDefault(x => x.Id == id);
            entity.IsDeleted = true;
        }
    }
}
