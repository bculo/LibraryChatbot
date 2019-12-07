using INTS_API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace INTS_API.Persistence.Configuration
{
    public class BookCopyConfiguration : IEntityTypeConfiguration<BookCopy>
    {
        public void Configure(EntityTypeBuilder<BookCopy> builder)
        {
            //id primarni kljuc
            builder.HasKey(prop => prop.Id);
            builder.Property(prop => prop.Id)
                .ValueGeneratedOnAdd();

            //Borrowed
            builder.Property(prop => prop.Borrowed)
                .IsRequired();

            //Vanjski kljuc na knjigu (Book)
            builder.HasOne(prop => prop.Book)
                .WithMany(book => book.BookCopies)
                .HasForeignKey(prop => prop.BookId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();
        }
    }
}
