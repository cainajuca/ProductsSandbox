using Products.WebApi.Models;

namespace Products.WebApi.Repositories;

public class ProductInMemoryRepository : IProductRepository
{
    private readonly List<Product> _products =
    [
        new Product { Id = 1, Name = "Product 1", Category = "Carnes" },
        new Product { Id = 2, Name = "Product 2", Category = "Congelados" },
        new Product { Id = 3, Name = "Product 3", Category = "Massas" },
    ];

    private int _nextProductId = 3;

    public Task<List<Product>> GetProductsAsync()
    {
        return Task.FromResult(_products);
    }

    public Task<Product?> GetProductByIdAsync(int id)
    {
        var product = _products.FirstOrDefault(x => x.Id == id);

        return Task.FromResult(product);
    }

    public Task<Product> AddProduct(ProductInputDto product)
    {
        var newProduct = new Product
        {
            Id = ++_nextProductId,
            Name = product.Name,
            Category = product.Category
        };

        _products.Add(newProduct);

        return Task.FromResult(newProduct);
    }

    public Task<Product?> UpdateProductById(int id, ProductInputDto product)
    {
        var existingProduct = _products.FirstOrDefault(x => x.Id == id);

        if (existingProduct != null)
        {
            existingProduct.Name = product.Name;
            existingProduct.Category = product.Category;
        }

        return Task.FromResult(existingProduct);
    }

    public Task<bool> DeleteProductById(int id)
    {
        var existingProduct = _products.FirstOrDefault(x => x.Id == id);

        if (existingProduct == null)
            return Task.FromResult(false);

        _products.Remove(existingProduct);

        return Task.FromResult(true);
    }
}
