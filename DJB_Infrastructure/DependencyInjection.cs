using DJB_Core.Interfaces;
using DJB_Core.Options;
using DJB_Infrastructure.Data;
using DJB_Infrastructure.Repository;
using DJB_Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace DJB_Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureDI(this IServiceCollection services)
        {
            services.AddDbContext<DataBaseContext>((provider, options) => options.UseSqlServer(provider.GetRequiredService<IOptionsSnapshot<ConnectionStringsOptions>>().Value.DefaultConnection));
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IExternalVendorRepository, ExternalVendorRepository>();
            services.AddHttpClient<IPokemonHttpClient, PokemonHttpClient>((provider, options) =>
            {
                options.BaseAddress = new Uri(provider.GetRequiredService<IOptionsMonitor<ExternalApiUrlsOptions>>().CurrentValue.PokeMonUri);
            });
            services.AddHttpClient<IOfficialJokeHttpClient, OfficialJokeHttpClient>((provider, options) =>
            {
                options.BaseAddress = new Uri(provider.GetRequiredService<IOptionsMonitor<ExternalApiUrlsOptions>>().CurrentValue.JokesUri);
            });
            return services;
        }
    }
}
