using Products.WebApi.Models;

namespace Products.WebApi.Repositories;

public interface IProductRepository
{
    Task<List<Product>> GetProductsAsync();
    Task<Product?> GetProductByIdAsync(int id);
    Task<Product> AddProduct(ProductInputDto product);
    Task<Product?> UpdateProductById(int id, ProductInputDto product);
    Task<bool> DeleteProductById(int id);
}
