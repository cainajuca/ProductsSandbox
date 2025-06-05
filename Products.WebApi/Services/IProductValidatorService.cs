using Products.WebApi.Models;

namespace Products.WebApi.Services;
public interface IProductValidatorService
{
    bool IsValid(ProductInputDto productInput);
}