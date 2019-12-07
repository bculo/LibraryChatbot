using INTS_API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace INTS_API.Persistence.Configuration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            //id primarni kljuc
            builder.HasKey(prop => prop.Id);
            builder.Property(prop => prop.Id)
                .ValueGeneratedOnAdd();

            //name unique
            builder.HasIndex(prop => prop.Name)
                .IsUnique();
            builder.Property(prop => prop.Name)
                .HasMaxLength(50)
                .IsRequired();
        }
    }
}
