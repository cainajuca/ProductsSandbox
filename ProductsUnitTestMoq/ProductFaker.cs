using Bogus;
using Products.WebApi.Models;

namespace ProductsUnitTestMoq;
public class ProductFaker : Faker<Product>
{
    public ProductFaker()
    {
        RuleFor(p => p.Id, f => f.Random.Int());
        RuleFor(p => p.Name, f => f.Commerce.ProductName());
        RuleFor(p => p.Category, f => f.Lorem.Word());
        RuleFor(p => p.LastUpdatedAt, f => f.Date.Recent(1));
    }
}
