using INTS_API.Entities;
using Microsoft.EntityFrameworkCore;

namespace INTS_API.Persistence
{
    /// <summary>
    /// Kontekst za bazu podataka
    /// </summary>
    public class LibraryDBContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookCopy> BookCopies { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<BorrowedBook> BorrowedBooks { get; set; }
        public DbSet<UserRating> UserRatings { get; set; }

        public LibraryDBContext(DbContextOptions<LibraryDBContext> options) : base(options) { }

        /// <summary>
        /// Konfiguracija baze podataka
        /// </summary>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(LibraryDBContext).Assembly);
        }
    }
}
