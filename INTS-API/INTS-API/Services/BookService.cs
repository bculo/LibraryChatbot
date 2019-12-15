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
        private readonly ICategoryRepository _categoryrepo;
        private readonly IUserRepository _userepo;

        public BookService(IBookRepository bookrepo, ICategoryRepository categoryrepo, IUserRepository userrepo)
        {
            _bookrepo = bookrepo;
            _categoryrepo = categoryrepo;
            _userepo = userrepo;
        }

        /// <summary>
        /// Dodaj novu knjigu sa random kategorijom
        /// </summary>
        public async Task<bool> AddBokk(Book book)
        {
            int count = await _categoryrepo.CountAsync();

            Random rand = new Random();
            book.CategoryID = rand.Next(0, count);

            Book result = await _bookrepo.AddAsync(book);

            if (result != null)
                return true;

            return false;
        }

        public async Task CreateBookReservation(string username, string bookName)
        {
            var bookResult = _bookrepo.GetBookByName(bookName);
            var userResult = _userepo.GetUserByNameAsync(username);

            await Task.WhenAll(bookResult, userResult);

            if(userResult == null)
            {
                var userFromDatabase = new User
                {
                    UserName = username
                };

                userFromDatabase = await _userepo.AddAsync(userFromDatabase);

                if(userFromDatabase != null)
                {

                }
            }
        }

        public async Task<List<Book>> GetUserReservations(string username)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Book>> GetRandomBooks(int? number)
        {
            if(number.HasValue)
                return await _bookrepo.GetRandomBooksAsync(number.Value);
            else
                return await _bookrepo.GetRandomBooksAsync(5);
        }

        public async Task<List<Book>> GetRandomBooksByCategory(string category, int? bookNumber)
        {
            string finalCateogry = string.Empty;

            if (category == null)
                return null;

            if (category.Length > 1)
            {
                string firstLetter = category[0].ToString().ToUpper();
                string restOfTheString = category.Substring(1);
                finalCateogry = firstLetter + restOfTheString;
            }
            else
            {
                finalCateogry = category.ToUpper();
            }

            int numberOfRecords = (bookNumber.HasValue) ? bookNumber.Value : 5;

            return await _bookrepo.GetRandomBooksByCategoryAsync(finalCateogry, numberOfRecords);
        }
    }
}
