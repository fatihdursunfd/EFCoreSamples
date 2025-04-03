using Domain.Entities.Identity;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Application.Common.Helpers;
using Application.Interfaces.Data;
using Infrastructure.Utilities;
using Application.Common.Validators;

namespace Infrastructure.DependencyResolvers
{
    public static class DependencyInjection
    {
        public static void RegisterInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var identityBuilder = services
                .AddDbContext<AppDbContext>(opt =>
                {
                    opt.UseSqlServer(configuration.GetConnectionString("SQLServerConnString"), x =>
                    {
                        x.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName);
                        x.MaxBatchSize(1000);
                    });
                })
                .AddIdentity<ApplicationUser, ApplicationRole>(opt =>
                {
                    opt.User.RequireUniqueEmail = false;
                    opt.Password.RequiredLength = 8;
                    opt.Password.RequireLowercase = true;
                    opt.Password.RequireUppercase = true;
                    opt.Password.RequireNonAlphanumeric = true;
                    opt.Password.RequireDigit = true;
                    opt.SignIn.RequireConfirmedEmail = true;
                })
                .AddEntityFrameworkStores<AppDbContext>()
                .AddRoles<ApplicationRole>()
                .AddRoleValidator<ApplicationRoleValidator>();


            services
                .AddAuthentication(opt =>
                {
                    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(opt =>
                {
                    opt.Audience = configuration.GetSection("TokenOptions:Audience").Value;
                    opt.ClaimsIssuer = configuration.GetSection("TokenOptions:Issuer").Value;
                    opt.SaveToken = true;
                    opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidIssuer = configuration.GetSection("TokenOptions:Issuer").Value,
                        ValidAudience = configuration.GetSection("TokenOptions:Audience").Value,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = SecurityHelper.CreateSecurityKey(configuration.GetSection("TokenOptions:SecurityKey").Value ?? string.Empty),
                        ClockSkew = TimeSpan.FromSeconds(0)
                    };
                });

            var userType = identityBuilder.UserType;
            var provider = typeof(RefreshTokenProvider<>).MakeGenericType(userType);

            identityBuilder.AddTokenProvider(nameof(RefreshTokenProvider<ApplicationUser>), provider);

            services.Configure<RefreshTokenProviderOptions>(opt =>
            {
                opt.TokenLifespan = TimeSpan.FromHours(Convert.ToInt32(configuration.GetSection("TokenOptions:RefreshTokenExpiration").Value));
            });

            services.AddScoped<IAppDbContext, AppDbContext>();
            services.AddScoped<ITokenHelper, TokenHelper>();

            services.AddSingleton<ICurrentUser, CurrentUser>();
        }
    }
}