using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace INTS_API.Interfaces
{
    public interface IInstaller
    {
        void Configure(IServiceCollection services, IConfiguration configuration);
    }
}
