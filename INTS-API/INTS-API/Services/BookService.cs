using INTS_API.Entities;
using INTS_API.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace INTS_API.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookrepo;
        private readonly IRepository<Category> _categoryrepo;

        public BookService(IBookRepository bookrepo, IRepository<Category> categoryrepo)
        {
            _bookrepo = bookrepo;
            _categoryrepo = categoryrepo;
        }

        /// <summary>
        /// Dodaj novu knjigu sa random kategorijom
        /// </summary>
        public async Task AddBokk(Book book)
        {
            int count = await _categoryrepo.CountAsync();

            Random rand = new Random();
            book.CategoryID = rand.Next(0, count);

            await _bookrepo.AddAsync(book);
        }

        public async Task<List<Book>> GetRandomBooks()
        {
            return await _bookrepo.GetRandomBooksAsync(5);
        }
    }
}
