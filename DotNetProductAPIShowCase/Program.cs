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
[assembly: ApiController]

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add<ExceptionFilter>();
}).ConfigureApiBehaviorOptions(options =>
{
    options.SuppressModelStateInvalidFilter = true; // Disabled auto return 400 and manual validate to use custom exception filter.
});

builder.Services.AddValidatorsFromAssemblyContaining<ProductPageValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<ProductValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateProductPriceValidator>();

builder.Services.AddScoped<ProductService>();

builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddDbContext<EFCoreContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();