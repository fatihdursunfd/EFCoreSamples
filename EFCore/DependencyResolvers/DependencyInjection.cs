using Infrastructure.Data.Seed;
using Infrastructure.Data;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace API.DependencyResolvers
{
    public static class DependencyInjection
    {
        public static void Register(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();

            services.AddEndpointsApiExplorer();

            services.SwaggerConfiguration(configuration);
        }

        public static async Task Register(this IApplicationBuilder app, IHostEnvironment env)
        {
            app.UseStaticFiles();

            app.SwaggerConfiguration(env);

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();

            await app.Seed();
        }


        private static void SwaggerConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            string? version = configuration.GetSection("Swagger:Version").Value;
            string? title = configuration.GetSection("Swagger:Title").Value;
            string? description = configuration.GetSection("Swagger:Description").Value;
            string? contactName = configuration.GetSection("Swagger:Contact:Name").Value;
            string? contactEmail = configuration.GetSection("Swagger:Contact:Email").Value;
            string? contactUrl = configuration.GetSection("Swagger:Contact:Url").Value;

            services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc(version, new OpenApiInfo()
                {
                    Title = title,
                    Description = description,
                    Version = version,
                });

                //string xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                //opt.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

                opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = JwtBearerDefaults.AuthenticationScheme,
                    BearerFormat = "JWT"
                });

                opt.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                   {
                       new OpenApiSecurityScheme
                       {
                           Reference = new OpenApiReference
                           {
                               Type = ReferenceType.SecurityScheme,
                               Id = "Bearer"
                           }
                       },
                       new string[]{ }
                   }
                });

                opt.UseInlineDefinitionsForEnums();
            });

        }
        private static void SwaggerConfiguration(this IApplicationBuilder app, IHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(opt =>
                {
                    opt.InjectStylesheet("/assets/css/swagger.css");
                });
            }
        }

        private static async Task Seed(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var services = scope.ServiceProvider;
                var _context = services.GetService<AppDbContext>();

                CancellationTokenSource cts = new();

                await DataSeeder.Seed(_context, cts.Token);
            }
        }
    }
}