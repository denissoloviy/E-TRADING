using E_TRADING.Data.Entities;
using E_TRADING.Data.Repositories.Generic;
using System.Linq;

namespace E_TRADING.Data.Repositories
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
    }

    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _context;
        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        
        public override void Delete(Category entity)
        {
            entity.IsDeleted = true;
        }

        public override void Delete(string id)
        {
            var entity = _context.Categories.FirstOrDefault(x => x.Id == id);
            entity.IsDeleted = true;
        }
    }
}
