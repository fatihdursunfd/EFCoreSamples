using API.DependencyResolvers;
using Infrastructure.DependencyResolvers;
using Application.DependencyResolvers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Register(builder.Configuration);
builder.Services.RegisterApplication();
builder.Services.RegisterInfrastructure(builder.Configuration);

builder.WebHost.ConfigureKestrel(options => options.Limits.MaxRequestBodySize = 55 * 1024 * 1024);

var app = builder.Build();

await app.Register(builder.Environment);

app.MapControllers();

app.Run();
