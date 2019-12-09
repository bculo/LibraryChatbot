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

        public Task<List<Book>> GetRandomBooksAsync(int number)
        {
            return _context.Books.OrderBy(i => Guid.NewGuid()).Take(number).ToListAsync();
        }
    }
}
