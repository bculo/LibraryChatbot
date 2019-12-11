using INTS_API.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace INTS_API.Interfaces
{
    public interface IBookService
    {
        Task<bool> AddBokk(Book book);
        Task<List<Book>> GetRandomBooks();
    }
}
