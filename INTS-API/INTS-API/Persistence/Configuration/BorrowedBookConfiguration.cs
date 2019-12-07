using INTS_API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace INTS_API.Persistence.Configuration
{
    public class BorrowedBookConfiguration : IEntityTypeConfiguration<BorrowedBook>
    {
        public void Configure(EntityTypeBuilder<BorrowedBook> builder)
        {
            //id primarni kljuc
            builder.HasKey(prop => prop.Id);
            builder.Property(prop => prop.Id)
                .ValueGeneratedOnAdd();

            //StartDateTime
            builder.Property(prop => prop.StartDateTime)
                .IsRequired();

            //EndDateTime
            builder.Property(prop => prop.EndDateTime)
                .IsRequired();

            //ReturnDateTime
            builder.Property(prop => prop.ReturnDateTime)
                .IsRequired(false);

            //Vanjski kljuc na korisnika (User)
            builder.HasOne(prop => prop.User)
                .WithMany(user => user.BorrowedBooks)
                .HasForeignKey(prop => prop.UserId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            //Vanjski kljuc na kopiju knjige (BookCopy)
            builder.HasOne(prop => prop.BookCopy)
                .WithMany(book => book.BorrowedBooks)
                .HasForeignKey(prop => prop.BookCopyId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();
        }
    }
}
