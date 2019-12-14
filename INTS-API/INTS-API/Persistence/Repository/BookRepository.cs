using INTS_API.Entities;
using INTS_API.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INTS_API.Persistence.Repository
{
    public class BookRepository : AsyncRepository<Book>, IBookRepository
    {
        public BookRepository(LibraryDBContext context) : base(context) { }

        public async Task<List<Book>> GetRandomBooksAsync(int number)
        {
            return await _context.Books
                .OrderBy(i => Guid.NewGuid())
                .Take(number)
                .ToListAsync();
        }

        public async Task<List<Book>> GetRandomBooksByCategoryAsync(string category, int number)
        {
            return await _context.Books
                .Include(i => i.Category)
                .Where(i => i.Category.Name == category)
                .OrderBy(i => Guid.NewGuid())
                .Take(number)
                .ToListAsync();
        }

        public async Task CreateBookReservation(string username, string bookName)
        {
            
        }

        public Task<List<Book>> GetUserReservations(string username)
        {
            throw new NotImplementedException();
        }

        public async Task<Book> GetBookByName(string name)
        {
            return await _context.Books.FirstOrDefaultAsync(i => i.Title.ToLower() == name.ToLower());
        }
    }
}
