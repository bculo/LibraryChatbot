using System.Collections.Generic;
using System.Threading.Tasks;

namespace INTS_API.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(object instanceId);
        Task<T> AddAsync(T instance);
        Task<T> UpdateAsync(T instance);
        Task<bool> DeleteAsync(T instance);
        Task<int> CountAsync();
    }
    
}
