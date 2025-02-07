using System;

namespace DotNetProductAPIShowCase.Domains;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAll(int page, int pageSize);

    Task<Product?> GetById(int id);

    Task<int> Insert(Product product);

    Task<int> Update(Product product);

    Task<int> Delete(Product product);
}
