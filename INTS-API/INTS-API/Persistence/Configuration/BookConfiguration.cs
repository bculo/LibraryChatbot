using INTS_API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace INTS_API.Persistence.Configuration
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            //id primarni kljuc
            builder.HasKey(prop => prop.Id);
            builder.Property(prop => prop.Id)
                .ValueGeneratedOnAdd();

            //naslov
            builder.Property(prop => prop.Title)
                .HasMaxLength(255)
                .IsRequired();

            //authors
            builder.Property(prop => prop.Authors)
                .IsRequired(false);

            //avarage rating
            builder.Property(prop => prop.AvarageRating)
                .IsRequired();

            //ISBN
            builder.Property(prop => prop.ISBN)
                .IsRequired(false);

            //ISBN13
            builder.Property(prop => prop.ISBN13)
                .IsRequired(false);

            //Language
            builder.Property(prop => prop.LanguageCode)
                .HasMaxLength(10)
                .IsRequired();

            //PageNumber
            builder.Property(prop => prop.PageNumber)
                .IsRequired();

            //RatingsCount
            builder.Property(prop => prop.RatingsCount)
                .IsRequired();

            //Vanjski kljuc na kategoriju
            builder.HasOne(prop => prop.Category)
                .WithMany(category => category.Books)
                .HasForeignKey(prop => prop.CategoryID)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();
        }
    }
}
