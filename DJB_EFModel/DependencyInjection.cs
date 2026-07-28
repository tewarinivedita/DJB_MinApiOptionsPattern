using DJB_Application;
using DJB_Application.Interface;
using DJB_Core;
using DJB_Infrastructure;
using DJB_Infrastructure.AI;

namespace DJB_Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAppDI(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddApplicationDI()
                .AddInfrastructureDI()
                .AddCoreDI(configuration);
            return services;
        }
    }
}
