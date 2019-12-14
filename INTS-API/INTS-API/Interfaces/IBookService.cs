using INTS_API.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace INTS_API.Interfaces
{
    public interface IBookService
    {
        Task<bool> AddBokk(Book book);
        Task<List<Book>> GetRandomBooks(int? number);
        Task<List<Book>> GetRandomBooksByCategory(string category, int? bookNumber);
        Task CreateBookReservation(string username, string bookName);
        Task<List<Book>> GetUserReservations(string username);
    }
}
