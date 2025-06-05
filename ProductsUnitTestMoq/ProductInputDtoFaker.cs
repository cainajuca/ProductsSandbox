using Bogus;
using Products.WebApi.Models;

namespace ProductsUnitTestMoq;
public class ProductInputDtoFaker : Faker<ProductInputDto>
{
    public ProductInputDtoFaker()
    {
        RuleFor(p => p.Name, f => f.Commerce.ProductName());
        RuleFor(p => p.Category, f => f.Lorem.Word());
    }
}
