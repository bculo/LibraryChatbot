using INTS_API.Interfaces;
using INTS_API.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace INTS_API.Configurations
{
    public class OptionsConfiguration : IInstaller
    {
        public void Configure(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<SecurityOptions>(configuration.GetSection(nameof(SecurityOptions)));
            services.Configure<CategoryOptions>(configuration.GetSection(nameof(CategoryOptions)));
        }
    }
}
