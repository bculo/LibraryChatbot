using INTS_API.Entities;
using System.Threading.Tasks;

namespace INTS_API.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetUserByNameAsync(string username);
    }
}
