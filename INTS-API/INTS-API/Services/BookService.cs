using INTS_API.Entities;
using INTS_API.Interfaces;
using INTS_API.Services.Models;
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
        private readonly IRepository<BookCopy> _copyrepo;
        private readonly IRepository<BorrowedBook> _borrowrepo;
        private readonly IRepository<UserRating> _rating;

        public BookService(IBookRepository bookrepo,
            ICategoryRepository categoryrepo,
            IUserRepository userrepo,
            IRepository<BookCopy> copyrepo,
            IRepository<BorrowedBook> borrowrepo,
            IRepository<UserRating> rating)
        {
            _bookrepo = bookrepo;
            _categoryrepo = categoryrepo;
            _userepo = userrepo;
            _copyrepo = copyrepo;
            _borrowrepo = borrowrepo;
            _rating = rating;
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
            {
                List<BookCopy> copies = new List<BookCopy>();

                for (int i = 0; i < 20; i++)
                {
                    BookCopy copy = new BookCopy() { Borrowed = false, BookId = result.Id };
                    copies.Add(copy);
                }

                return  await _copyrepo.AddRangeAsync(copies);
            }

            return false;
        }

        public async Task<ServiceResult> CreateBookReservation(string username, string bookName)
        {
            //dovati knjigu
            var bookResult = await _bookrepo.GetBookByName(bookName);

            //dohvati korisnika
            var userResult = await _userepo.GetUserByNameAsync(username);

            ServiceResult result = new ServiceResult();
            User userFromDatabase = new User();

            if (userResult == null) //ako korisnik ne postoji u bazi
            {
                userFromDatabase.UserName = username;
                userFromDatabase.HashedPassword = "NESTONECE";

                //dodaj korisnika u bazu
                userFromDatabase = await _userepo.AddAsync(userFromDatabase);

                if(userFromDatabase == null) //pogreška
                {
                    result.SetErrorMessage("Uff, something went wrong :(");
                    return result;
                }
            }

            //knjiga nije pronadena u bazi
            if(bookResult == null)
            {
                result.SetErrorMessage("Uff, something went wrong :(");
                return result;
            }

            //korisnik je vec posudio ovu knjigu
            if (!await _bookrepo.CanUserMakeAReservation(bookName, userResult.Id))
            {
                result.SetErrorMessage("You already borrowed this book :;");
                return result;
            }

            //sve kopije knjige su posudene
            var bookForReservation = await _bookrepo.GetAvailableBookCopy(bookName);
            if (bookForReservation == null)
            {
                result.SetErrorMessage("This book isnt available :(");
                return result;
            }

            BorrowedBook borrowedBook = new BorrowedBook()
            {
                BookCopyId = bookForReservation.Id,
                EndDateTime = DateTime.Now.AddDays(30),
                StartDateTime = DateTime.Now,
                UserId = userResult.Id,
            };

            bookForReservation.Borrowed = true;

            //dodaj rezervaciju
            borrowedBook = await _borrowrepo.AddAsync(borrowedBook);
            if(borrowedBook != null && (await _copyrepo.UpdateAsync(bookForReservation) != null))
            { 
                return result;
            }

            result.SetErrorMessage("Uff, something went wrong :(");
            return result;
        }

        public async Task<List<Book>> GetUserReservations(string username)
        {
            //dohvati korisnika
            var userResult = await _userepo.GetUserByNameAsync(username);

            //ako korisnik ne postoji u bazi onda nema rezervacija
            if(userResult == null)
            {
                return new List<Book>();
            }

            return await _bookrepo.GetUserReservations(userResult.Id);
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

        public async Task<string> SetBookReservation(string username, string book, string rating)
        {
            int ratingINT = 5;
            int.TryParse(rating, out ratingINT);

            //dohvati korisnika
            var userResult = await _userepo.GetUserByNameAsync(username);

            //ako korisnik ne postoji u bazi onda nema rezervacija
            if (userResult == null)
            {
                return "Somethin went wrong :(";
            }

            //dovati knjigu
            var bookResult = await _bookrepo.GetBookByName(book);

            //ako korisnik ne postoji u bazi onda nema rezervacija
            if (bookResult == null)
            {
                return "Somethin went wrong :(";
            }

            UserRating ratingObject = new UserRating()
            {
                BookId = bookResult.Id,
                UserId = userResult.Id,
                Rate = ratingINT
            };

            userResult.UserRatings.Add(ratingObject);
            await _userepo.UpdateAsync(userResult);

            return "Book succesfully rated";
        }
    }
}
