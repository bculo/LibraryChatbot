using INTS_API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

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

            //Inicijalizacija podataka za kategorije
            var predefiendCategories = new List<Category>()
            {
                new Category {Id=1, Name = "Action"},
                new Category {Id=2, Name = "Anthology"},
                new Category {Id=3, Name = "Bibliography"},
                new Category {Id=4, Name = "Comic"},
                new Category {Id=5, Name = "Crime"},
                new Category {Id=6, Name = "Drama"},
                new Category {Id=7, Name = "Fairytale"},
                new Category {Id=8, Name = "Fantasy"},
                new Category {Id=9, Name = "Graphic novel"},
                new Category {Id=10, Name = "Historical fiction"},
                new Category {Id=11, Name = "Horror"},
                new Category {Id=12, Name = "History"},
                new Category {Id=13, Name = "Mystery"},
                new Category {Id=14, Name = "Physics"},
                new Category {Id=15, Name = "Poetry"},
                new Category {Id=16, Name = "Political thriller"},
                new Category {Id=17, Name = "Romance"},
                new Category {Id=18, Name = "Satire"},
                new Category {Id=19, Name = "Science fiction"},
                new Category {Id=20, Name = "Short story"},
                new Category {Id=21, Name = "Thriller"},
            };

            builder.HasData(predefiendCategories);
        }
    }
}
