using INTS_API.Entities;
using INTS_API.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace INTS_API.Persistence.Repository
{
    public class UserRepository : AsyncRepository<User>, IUserRepository
    {
        public UserRepository(LibraryDBContext dBContext) : base(dBContext) { }

        public async Task<User> GetUserByNameAsync(string username)
        {
            return await _context.Users
                .Include(i => i.UserRatings)
                .AsNoTracking()
                .FirstOrDefaultAsync(i => i.UserName.ToLower() == username.ToLower().Trim());
        }
    }
}
