using INTS_API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace INTS_API.Persistence.Configuration
{
    public class UserRatingConfiguration : IEntityTypeConfiguration<UserRating>
    {
        public void Configure(EntityTypeBuilder<UserRating> builder)
        {
            //Primarni kljuc
            builder.HasKey(prop => new
            {
                prop.UserId,
                prop.BookId
            });

            //Rate
            builder.Property(prop => prop.Rate)
                .IsRequired();

            //Vanjski kljuc na korisnika (User)
            builder.HasOne(prop => prop.User)
                .WithMany(user => user.UserRatings)
                .HasForeignKey(prop => prop.UserId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            //Vanjski kljuc na knjigu (Book)
            builder.HasOne(prop => prop.Book)
                .WithMany(book => book.UserRatings)
                .HasForeignKey(prop => prop.BookId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();
        }
    }
}
