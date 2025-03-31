using Application.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.DependencyResolvers
{
    public static class DependencyInjection
    {
        public static void RegisterInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {

            services
                .AddDbContext<AppDbContext>(opt =>
                {
                    opt.UseSqlServer(configuration.GetConnectionString("SQLServerConnString"), x =>
                    {
                        x.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName);
                        x.MaxBatchSize(1000);
                    });
                });

            services.AddScoped<IAppDbContext, AppDbContext>();

        }

    }
}