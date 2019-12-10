using INTS_API.Entities;
using INTS_API.Interfaces;
using INTS_API.Options;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace INTS_API.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookrepo;
        private readonly CategoryOptions _options;

        public BookService(IBookRepository bookrepo, IOptions<CategoryOptions> options)
        {
            _bookrepo = bookrepo;
            _options = options.Value;
        }

        /// <summary>
        /// Dodaj novu knjigu sa random kategorijom
        /// </summary>
        public async Task AddBokk(Book book)
        {
            Random rand = new Random();
            book.CategoryID = rand.Next(0, _options.NumOfCategories);
            await _bookrepo.AddAsync(book);
        }

        public async Task<List<Book>> GetRandomBooks()
        {
            return await _bookrepo.GetRandomBooksAsync(5);
        }
    }
}
