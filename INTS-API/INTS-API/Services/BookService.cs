using INTS_API.Entities;
using INTS_API.Interfaces;
using System;
using System.Threading.Tasks;

namespace INTS_API.Services
{
    public class BookService : IBookService
    {
        private readonly IRepository<Book> _bookrepo;
        private readonly IRepository<Category> _categoryrepo;

        public BookService(IRepository<Book> bookrepo, IRepository<Category> categoryrepo)
        {
            _bookrepo = bookrepo;
            _categoryrepo = categoryrepo;
        }

        public async Task AddBokk(Book book)
        {
            int numberOfCategories = await _categoryrepo.CountAsync();

            Random rand = new Random();
            int categoryId = rand.Next(0, numberOfCategories);

            book.CategoryID = categoryId;

            await _bookrepo.AddAsync(book);
        }
    }
}
