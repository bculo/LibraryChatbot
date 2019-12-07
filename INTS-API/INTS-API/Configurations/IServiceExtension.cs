using System;
using System.Linq;
using INTS_API.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace INTS_API.Configurations
{
    public static class IServiceExtension
    {
        /// <summary>
        /// Instalacija i konfiguracija
        /// </summary>
        public static void StartConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            //Refleksijom kreiraj sve instance IInstaller-a
            var instances = typeof(Startup).Assembly.ExportedTypes
                .Where(x => typeof(IInstaller).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                .Select(Activator.CreateInstance)
                .Cast<IInstaller>()
                .ToList();

            //Zapocni konfiguraciju
            instances.ForEach(x => x.Configure(services, configuration));
        }
    }
}
