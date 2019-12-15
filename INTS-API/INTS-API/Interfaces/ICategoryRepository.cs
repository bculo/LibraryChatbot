using INTS_API.Entities;
using System.Threading.Tasks;

namespace INTS_API.Interfaces
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<Category> GetCategoryByName(string name);
    }
}
