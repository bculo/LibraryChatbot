using INTS_API.Entities;
using INTS_API.Interfaces;
using INTS_API.Persistence;
using INTS_DATASET.Model;
using INTS_DATASET.Readers;
using INTS_DATASET.Utils;
using INTS_DATASET.Writers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace INTS_API.Init
{
    public class InitDatabaseData
    {
        public DbContextOptions<LibraryDBContext> Options { get; set; }

        private readonly IRepository<Category> _categoryrepo;
        private readonly IRepository<Book> _bookrepo;
        private readonly IRepository<BookCopy> _bookcopyrepo;

        private Random Random { get; set; }

        public InitDatabaseData(DbContextOptions<LibraryDBContext> options)
        {
            Options = options;

            _categoryrepo = DBContextFactory.GetCategoryRepo(options);
            _bookrepo = DBContextFactory.GetBookRepo(options);
            _bookcopyrepo = DBContextFactory.GetBookCopyRepo(options);
        }

        public void Start()
        {
            //pripremi putanju do books.csv datoteke
            string projectPath = PathUtils.GetProjectDirectoryPath();
            string csvPath = Path.Combine(projectPath, "Datasets", "books.csv");

            //procitaj csv datoteku
            List<BookCSV> bookRecords = new BookCSVReader().GetIntances(csvPath);

            //pomocna lista
            List<BookCSV> booksForCsv = new List<BookCSV>();

            using (LibraryDBContext context = new LibraryDBContext(Options))
            {
                if (context.Books.Count() == 0)
                {
                    //kreiranje nasumicnih brojeva
                    Random = new Random();

                    //broj kategorija
                    int categoryCount = context.Categories.Count();

                    foreach (var book in bookRecords)
                    {
                        //odaberi nasumicni broj za kategoriju
                        int categoryID = Random.Next(0, categoryCount);

                        //Mapiranje u objekt tipa Book
                        var newBook = new Book()
                        {
                            AvarageRating = book.AvarageRating,
                            CategoryID = categoryID,
                            ISBN = book.ISBN,
                            ISBN13 = book.ISBN13,
                            LanguageCode = book.LanguageCode,
                            PageNumber = book.PageNumber,
                            Title = book.Title,
                            RatingsCount = book.RatingsCount,
                            Authors = book.Authors
                        };

                        //ako je dobar upis u bazu newBook ce biti razlicit od NULL i imati ce ID
                        context.Books.Add(newBook);

                        if (newBook != null)
                        {
                            //keiraj 30 kopija knjige
                            for (int i = 0; i < 20; i++)
                            {
                                //napravi kopiju knjige
                                BookCopy copy = new BookCopy()
                                {
                                    BookId = newBook.Id,
                                    Borrowed = false,
                                };

                                //dodaj kopiju knige u bazu
                                context.BookCopies.Add(copy);
                            }
                        }

                        try
                        {
                            context.SaveChanges();
                        }
                        catch { }
                    }

                    //upisi u novi CSV samo titlove i kod jezika
                    string csvPathForTitles = Path.Combine(projectPath, "Datasets", "titles.csv");
                    new BookCSVTitleWriter().WriteToCSVFile(booksForCsv, csvPathForTitles);
                }
            }
        }
    }
}
