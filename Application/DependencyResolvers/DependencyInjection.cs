using Application.Interfaces;
using Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Application.DependencyResolvers
{
    public static class DependencyInjection
    {
        public static void RegisterApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(typeof(DependencyInjection).Assembly);
            });

            services.AddScoped<ILocationService, LocationService>();
            services.AddScoped<ICompanyService, CompanyService>();
        }
    }
}