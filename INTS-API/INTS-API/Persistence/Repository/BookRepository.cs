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
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<Book>> GetRandomBooksByCategoryAsync(string category, int number)
        {
            return await _context.Books
                .Include(i => i.Category)
                .Where(i => i.Category.Name == category)
                .OrderBy(i => Guid.NewGuid())
                .Take(number)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<Book>> GetUserReservations(Guid id)
        {
            //dohvati posudene knjige od korisnika
            var result = await _context.BorrowedBooks
                .Include(i => i.BookCopy)
                .ThenInclude(i => i.Book)
                .Where(i => i.UserId == id && i.ReturnDateTime == null)
                .Select(i => new Book
                {
                    Title = i.BookCopy.Book.Title
                })
                .AsNoTracking()
                .ToListAsync();

            return result;
        }

        public async Task<Book> GetBookByName(string name)
        {
            return await _context.Books.AsNoTracking().FirstOrDefaultAsync(i => i.Title.ToLower() == name.ToLower());
        }

        public async Task<BookCopy> GetAvailableBookCopy(string bookName)
        {
            //dohvati kopije knjige
            var result = await _context.Books
                .Include(i => i.BookCopies)
                .AsNoTracking()
                .FirstOrDefaultAsync(i => i.Title.ToLower() == bookName.ToLower());

            //vrati prvu slobodnu kopiju
            return result.BookCopies.FirstOrDefault(i => !i.Borrowed);
        }

        public async Task<bool> CanUserMakeAReservation(string bookName, Guid id)
        {
            var result = await _context.BorrowedBooks
                .Include(i => i.BookCopy)
                .ThenInclude(i => i.Book)
                .Where(i => i.UserId == id && i.ReturnDateTime == null)
                .Select(i => new Book
                {
                    Title = i.BookCopy.Book.Title
                })
                .Where(i => i.Title.ToLower() == bookName.ToLower())
                .AsNoTracking()
                .ToListAsync();

            if (result.Count > 0)
                return false;
            return true;
        }
    }
}
