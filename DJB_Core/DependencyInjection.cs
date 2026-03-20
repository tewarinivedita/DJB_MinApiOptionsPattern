using DJB_Core.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DJB_Core
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCoreDI(this IServiceCollection services, IConfiguration configuration) 
        {
            var ds = configuration.GetSection("ConnectionStrings");
            var ds2 = configuration.GetSection("ExternalApiUrls");

            services.Configure<ConnectionStringsOptions>(configuration.GetSection("ConnectionStrings"));
            services.Configure<ExternalApiUrlsOptions>(configuration.GetSection("ExternalApiUrls"));

            return services;
        }
    }
}
