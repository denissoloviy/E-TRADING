using E_TRADING.Data.Entities;
using E_TRADING.Data.Repositories.Generic;
using System.Linq;

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

        public override void Delete(Buyer entity)
        {
            entity.IsDeleted = true;
        }

        public override void Delete(string id)
        {
            var entity = _context.Buyers.FirstOrDefault(x => x.Id == id);
            entity.IsDeleted = true;
        }
    }
}
