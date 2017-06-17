using E_TRADING.Data.Entities;
using System.Linq;

namespace E_TRADING.Data.Repositories
{
    public interface IUserRepository
    {
        IQueryable<User> GetApplicationUsersInRole(string roleName);
    }

    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<User> GetApplicationUsersInRole(string roleName)
        {
            return from role in _context.Roles
                   where role.Name == roleName
                   from userRoles in role.Users
                   join user in _context.Users
                       on userRoles.UserId equals user.Id
                   select user;
        }
    }
}
