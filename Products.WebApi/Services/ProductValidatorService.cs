using Products.WebApi.Models;

namespace Products.WebApi.Services;
public class ProductValidatorService : IProductValidatorService
{
    public bool IsValid(ProductInputDto productInput) => true;
}