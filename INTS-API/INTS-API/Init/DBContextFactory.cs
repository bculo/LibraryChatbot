using INTS_API.Entities;
using INTS_API.Interfaces;
using INTS_API.Persistence;
using INTS_API.Persistence.Repository;
using Microsoft.EntityFrameworkCore;

namespace INTS_API.Init
{
    public static class DBContextFactory
    {
        /// <summary>
        /// Repository za kategorije
        /// </summary>
        public static IRepository<Category> GetCategoryRepo(DbContextOptions<LibraryDBContext> options)
        {
            return new AsyncRepository<Category>(new LibraryDBContext(options));
        }

        /// <summary>
        /// Repository za knjige
        /// </summary>
        public static IRepository<Book> GetBookRepo(DbContextOptions<LibraryDBContext> options)
        {
            return new AsyncRepository<Book>(new LibraryDBContext(options));
        }


        /// <summary>
        /// Repository za kopije knjige
        /// </summary>
        public static IRepository<BookCopy> GetBookCopyRepo(DbContextOptions<LibraryDBContext> options)
        {
            return new AsyncRepository<BookCopy>(new LibraryDBContext(options));
        }
    }
}
