using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Products.WebApi.Models;
using Xunit;

namespace Products.WebApi.Tests;

public class ProgramEndpointsTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public ProgramEndpointsTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetAllProducts_ReturnsOkAndProductList()
    {
        var response = await _client.GetAsync("/products");
        response.EnsureSuccessStatusCode();

        var products = await response.Content.ReadFromJsonAsync<List<Product>>();
        Assert.NotNull(products);
        Assert.NotEmpty(products);
    }

    [Fact]
    public async Task GetProductById_ExistingId_ReturnsOkAndProduct()
    {
        var response = await _client.GetAsync("/products/1");
        response.EnsureSuccessStatusCode();

        var product = await response.Content.ReadFromJsonAsync<Product>();
        Assert.NotNull(product);
        Assert.Equal(1, product.Id);
    }

    [Fact]
    public async Task GetProductById_NonExistingId_ReturnsNotFound()
    {
        var response = await _client.GetAsync("/products/9999");
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task AddProduct_ValidInput_ReturnsCreatedAndProduct()
    {
        var input = new ProductInputDto { Name = "Test Product", Category = "Test Category" };
        var response = await _client.PostAsJsonAsync("/products", input);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        var product = await response.Content.ReadFromJsonAsync<Product>();
        Assert.NotNull(product);
        Assert.Equal(input.Name, product.Name);
        Assert.Equal(input.Category, product.Category);
    }

    [Fact]
    public async Task UpdateProductById_ExistingId_ReturnsOkAndUpdatedProduct()
    {
        var input = new ProductInputDto { Name = "Updated Name", Category = "Updated Category" };
        var response = await _client.PutAsJsonAsync("/products/1", input);

        response.EnsureSuccessStatusCode();

        var product = await response.Content.ReadFromJsonAsync<Product>();
        Assert.NotNull(product);
        Assert.Equal(1, product.Id);
        Assert.Equal(input.Name, product.Name);
        Assert.Equal(input.Category, product.Category);
    }

    [Fact]
    public async Task UpdateProductById_NonExistingId_ReturnsNotFound()
    {
        var input = new ProductInputDto { Name = "Doesn't Matter", Category = "None" };
        var response = await _client.PutAsJsonAsync("/products/9999", input);

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task DeleteProductById_ExistingId_ReturnsNoContent()
    {
        // Add a product to ensure it exists
        var input = new ProductInputDto { Name = "ToDelete", Category = "Temp" };
        var postResponse = await _client.PostAsJsonAsync("/products", input);
        var product = await postResponse.Content.ReadFromJsonAsync<Product>();

        var response = await _client.DeleteAsync($"/products/{product.Id}");
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }

    [Fact]
    public async Task DeleteProductById_NonExistingId_ReturnsNotFound()
    {
        var response = await _client.DeleteAsync("/products/9999");
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}