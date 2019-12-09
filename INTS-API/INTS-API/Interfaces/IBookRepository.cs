using INTS_API.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace INTS_API.Interfaces
{
    public interface IBookRepository : IRepository<Book>
    {
        Task<List<Book>> GetRandomBooksAsync(int number);
    }
}
