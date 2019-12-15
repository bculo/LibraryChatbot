using INTS_API.Init;
using INTS_API.Interfaces;
using INTS_API.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace INTS_API.Configurations
{
    public class DatabaseConfiguration : IInstaller
    {
        /// <summary>
        /// Pripremi bazu
        /// </summary>
        public void Configure(IServiceCollection services, IConfiguration configuration)
        {
            //configuration.GetConnectionString("DefaultConnection")
            //citanje iz datoteke appsettings.json

            //dodaj db context
            services.AddDbContext<LibraryDBContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });
        }
    }
}
