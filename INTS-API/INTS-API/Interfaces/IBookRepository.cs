using INTS_API.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace INTS_API.Interfaces
{
    public interface IBookRepository : IRepository<Book>
    {
        Task<List<Book>> GetRandomBooksAsync(int number);
        Task<List<Book>> GetRandomBooksByCategoryAsync(string category, int number);
        Task CreateBookReservation(string username, string bookName);
        Task<List<Book>> GetUserReservations(string username);
        Task<Book> GetBookByName(string name);
    }
}
