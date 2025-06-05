using Moq;
using Products.WebApi.Models;
using Products.WebApi.Repositories;
using Products.WebApi.Services;

namespace ProductsUnitTestMoq;

public class RepositoryUnitTests
{
    public readonly Mock<IDateTimeProvider> _mockDateTimeProvider;
    public readonly Mock<IProductValidatorService> _productValidatorService;
    public readonly IProductRepository _sut;

    public RepositoryUnitTests()
    {
        _mockDateTimeProvider = new Mock<IDateTimeProvider>();
        _productValidatorService = new Mock<IProductValidatorService>();

        _sut = new ProductInMemoryRepository(_mockDateTimeProvider.Object, _productValidatorService.Object);
    }

    [Fact]
    public async Task AddProduct_ShouldReturnError_WhenProductValidationFails()
    {
        // Arrange
        _mockDateTimeProvider.Setup(p => p.DateTimeNow).Returns(new DateTime(2025, 6, 2));
        _productValidatorService.Setup(p => p.IsValid(It.IsAny<ProductInputDto>())).Returns(false);

        var productInput = new ProductInputDtoFaker().UseSeed(1243).Generate();

        var mockValidator = new Mock<IProductValidatorService>();
        mockValidator.Setup(v => v.IsValid(It.IsAny<ProductInputDto>())).Returns(true);

        // Act
        var ex = await Assert.ThrowsAsync<ArgumentException>(() => _sut.AddProduct(productInput));
        Assert.Equal("Invalid product input data.", ex.Message);
    }

    [Fact]
    public async Task AddProduct_ShouldCreateProduct_WhenAllParametersAreValid()
    {
        // Arrange
        _mockDateTimeProvider.Setup(p => p.DateTimeNow).Returns(new DateTime(2025, 6, 2));
        _productValidatorService.Setup(p => p.IsValid(It.IsAny<ProductInputDto>())).Returns(true);

        var productInput = new ProductInputDtoFaker().UseSeed(1243).Generate();

        var mockValidator = new Mock<IProductValidatorService>();
        mockValidator.Setup(v => v.IsValid(It.IsAny<ProductInputDto>())).Returns(true);

        // Act
        var createdProduct = await _sut.AddProduct(productInput);

        // Assert
        Assert.NotNull(createdProduct);
        Assert.Equal(productInput.Name, createdProduct.Name);
        Assert.Equal(productInput.Category, createdProduct.Category);
        Assert.Equal(new DateTime(2025, 6, 2), createdProduct.LastUpdatedAt);
    }
}
