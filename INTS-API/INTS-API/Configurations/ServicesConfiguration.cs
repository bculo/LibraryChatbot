using INTS_API.Interfaces;
using INTS_API.Persistence.Repository;
using INTS_API.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace INTS_API.Configurations
{
    public class ServicesConfiguration : IInstaller
    {
        /// <summary>
        /// DI konfiguracija
        /// </summary>
        public void Configure(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IHasher, PasswordHasher>();
            services.AddScoped<ITokenManager, TokenManager>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped(typeof(IRepository<>), typeof(AsyncRepository<>));
        }
    }
}
