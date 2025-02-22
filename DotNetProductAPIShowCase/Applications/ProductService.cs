using System;
using System.Threading.Tasks;
using DotNetProductAPIShowCase.Applications.DTOS;
using DotNetProductAPIShowCase.Applications.Exceptions;
using DotNetProductAPIShowCase.Domains;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DotNetProductAPIShowCase.Services;

public class ProductService
{
    private IProductRepository _repository;

    public ProductService(IProductRepository productRepository)
    {
        this._repository = productRepository;
    }

    private async Task<Product> ApplyProductUpdates(Product productToUpdate)
    {
        await this._repository.Update(productToUpdate);
        return new Product { Id = productToUpdate.Id, Name = productToUpdate.Name, Price = productToUpdate.Price, Description = productToUpdate.Description };
    }

    public async Task<IEnumerable<Product>> GetAllProducts(ProductPageDTO productPage)
    {
        return await this._repository.GetAll(productPage.Page, productPage.PageSize);
    }

    public async Task<Product> GetProductById(int id)
    {
        Product? product = await this._repository.GetById(id);

        if (product == null)
        {
            throw new NotFoundException($"We Cannot find product by id {id}");
        }

        return product;
    }

    public async Task<int> AddProduct(ProductDTO product)
    {
        Product productToInsert = new Product { Name = product.Name, Price = product.Price, Description = product.Description };
        return await this._repository.Insert(productToInsert);
    }

    public async Task<Product> UpdateProduct(int id, ProductDTO product)
    {
        Product productToUpdate = await this.GetProductById(id);

        productToUpdate.Name = product.Name;
        productToUpdate.Price = product.Price;
        productToUpdate.Description = product.Description;

        return await this.ApplyProductUpdates(productToUpdate);
    }

    public async Task<Product> UpdateProductPrice(int id, decimal price)
    {
        Product productToUpdate = await this.GetProductById(id);

        productToUpdate.Price = price;

        return await this.ApplyProductUpdates(productToUpdate);
    }

    public async Task DeleteProduct(int id)
    {
        Product productToDelete = await this.GetProductById(id);

        await this._repository.Delete(productToDelete);
    }
}
