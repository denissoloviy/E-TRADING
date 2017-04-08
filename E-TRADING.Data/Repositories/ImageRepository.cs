using E_TRADING.Data.Entities;
using E_TRADING.Data.Repositories.Generic;

namespace E_TRADING.Data.Repositories
{
    public interface IImageRepository : IGenericRepository<Image>
    {
    }

    public class ImageRepository : GenericRepository<Image>, IImageRepository
    {
        private readonly ApplicationDbContext _context;
        public ImageRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}