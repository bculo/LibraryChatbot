using INTS_API.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace INTS_API.Interfaces
{
    public interface IBookRepository : IRepository<Book>
    {
        Task<List<Book>> GetRandomBooksAsync(int number);
        Task<BookCopy> GetAvailableBookCopy(string bookName);
        Task<List<Book>> GetRandomBooksByCategoryAsync(string category, int number);

        Task<bool> CanUserMakeAReservation(string bookName, Guid id);
        Task<List<Book>> GetUserReservations(Guid id);
        Task<Book> GetBookByName(string name);
    }
}
