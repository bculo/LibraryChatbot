using INTS_API.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace INTS_API.Configurations
{
    public class EndPointConfiguration : IInstaller
    {
        /// <summary>
        /// Konfiguracija API endpoint-a
        /// </summary>
        public void Configure(IServiceCollection services, IConfiguration configuration)
        {
            services.AddCors();
        }
    }
}
