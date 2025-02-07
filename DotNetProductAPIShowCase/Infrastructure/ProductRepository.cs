using System;
using DotNetProductAPIShowCase.Domains;
using DotNetProductAPIShowCase.Infrastructure.DatabaseContexts;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace DotNetProductAPIShowCase.Infrastructure;

public class ProductRepository : IProductRepository
{
    private readonly EFCoreContext _context;

    public ProductRepository(EFCoreContext context)
    {
        this._context = context;
    }

    public async Task<IEnumerable<Product>> GetAll(int page, int pageSize)
    {

        List<Product> products = await this._context.Products.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

        return products;
    }

    public async Task<Product?> GetById(int id)
    {
        return await this._context.Products.SingleOrDefaultAsync(product => product.Id == id);
    }

    public async Task<int> Insert(Product product)
    {
        this._context.Products.Add(product);
        await this._context.SaveChangesAsync();
        return product.Id;
    }

    public async Task<int> Update(Product product)
    {
        this._context.Products.Update(product);
        return await this._context.SaveChangesAsync();
    }

    public async Task<int> Delete(Product product)
    {
        this._context.Products.Remove(product);
        return await this._context.SaveChangesAsync();
    }

}
