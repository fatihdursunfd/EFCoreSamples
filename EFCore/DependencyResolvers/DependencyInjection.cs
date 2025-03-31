using Microsoft.OpenApi.Models;
using System.Reflection;

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

        public static void Register(this IApplicationBuilder app, IHostEnvironment env)
        {
            app.UseStaticFiles();

            app.SwaggerConfiguration(env);

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();
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
    }
}