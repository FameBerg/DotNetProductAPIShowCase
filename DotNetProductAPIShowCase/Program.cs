using DotNetProductAPIShowCase.Domains;
using DotNetProductAPIShowCase.Infrastructure;
using DotNetProductAPIShowCase.Services;
using DotNetProductAPIShowCase.Presentations.validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using DotNetProductAPIShowCase.Infrastructure.DatabaseContexts;
using Microsoft.EntityFrameworkCore;
using DotNetProductAPIShowCase.Presentations.Filters;
using DotNetProductAPIShowCase.Applications.Configurations;
using DotNetProductAPIShowCase.Presentations.Middlewares;
[assembly: ApiController]

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

//Cors origin Configuration
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:3000");
                      });
});

//ENV Configuration
//Load user secrets in DEV ENV from dotnet user-secrets
if (builder.Environment.IsDevelopment())
{
    Console.WriteLine("DEV MODE");
    builder.Configuration.AddUserSecrets<Program>();
}

//Bind ConnectionStringOptions to IOptions
builder.Services.Configure<ConnectionStringOptions>(
    builder.Configuration.GetSection("ConnectionStrings"));

IConfiguration config = builder.Configuration;
ConnectionStringOptions? connectionString = config.GetSection("ConnectionStrings").Get<ConnectionStringOptions>();

//Database Configuration
builder.Services.AddDbContext<EFCoreContext>(options =>
    options.UseSqlServer(connectionString.DefaultConnection));

//Exception Handler Configuration
builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add<ExceptionFilter>();
}).ConfigureApiBehaviorOptions(options =>
{
    options.SuppressModelStateInvalidFilter = true; // Disabled auto return 400 and manual validate to use custom exception filter.
});

//Fluent Validator Configuration
builder.Services.AddValidatorsFromAssemblyContaining<ProductPageValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<ProductValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateProductPriceValidator>();

//Dependency Configuration
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

var app = builder.Build();

app.UseCors(MyAllowSpecificOrigins);

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<CustomLoggingMiddleware>();

app.MapControllers();

app.Run();