using INTS_API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace INTS_API.Persistence.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            //id primarni kljuc
            builder.HasKey(prop => prop.Id);
            builder.Property(prop => prop.Id)
                .ValueGeneratedOnAdd();

            //username unique
            builder.HasIndex(prop => prop.UserName)
                .IsUnique();
            builder.Property(prop => prop.UserName)
                .HasMaxLength(50)
                .IsRequired();

            //password
            builder.Property(prop => prop.HashedPassword)
                .HasMaxLength(250)
                .IsRequired();
        }
    }
}
