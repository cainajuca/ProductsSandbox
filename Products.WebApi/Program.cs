using Products.WebApi.Models;
using Products.WebApi.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IProductRepository, ProductInMemoryRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Endpoints
app.MapGet("/products", async (IProductRepository productRepository) => 
{
    try
    {
        var products = await productRepository.GetProductsAsync();
        return Results.Ok(products);
    }
    catch (Exception ex)
    {
        return Results.Problem($"An error occurred while retrieving products: {ex.Message}");
    }
}).WithName("GetAllProducts");

app.MapGet("/products/{id:int:min(0)}", async (int id, IProductRepository productRepository) =>
{
    try
    { 
        var product = await productRepository.GetProductByIdAsync(id);

        if (product is null)
            return Results.NotFound();

        return Results.Ok(product);
    }
    catch (Exception ex)
    {
        return Results.Problem($"An error occurred while retrieving a product: {ex.Message}");
    }
}).WithName("GetProductById");

app.MapPost("/products", async (ProductInputDto input, IProductRepository productRepository) =>
{
    try
    {
        var createdProduct = await productRepository.AddProduct(input);
        return Results.Created($"/products/{createdProduct.Id}", createdProduct);
    }
    catch (Exception ex)
    {
        return Results.Problem($"An error occurred while adding a product: {ex.Message}");
    }
}).WithName("AddProduct");

app.MapPut("/products/{id:int:min(0)}", async (int id, ProductInputDto input, IProductRepository productRepository) =>
{
    try
    {
        var updatedProduct = await productRepository.UpdateProductById(id, input);

        if (updatedProduct is null)
            return Results.NotFound();

        return Results.Ok(updatedProduct);
    }
    catch (Exception ex)
    {
        return Results.Problem($"An error occurred while updating a product: {ex.Message}");
    }
}).WithName("UpdateProductById");

app.MapDelete("/products/{id:int:min(0)}", async (int id, IProductRepository productRepository) =>
{
    try
    {
        var deleted = await productRepository.DeleteProductById(id);

        if (!deleted)
            return Results.NotFound();

        return Results.NoContent();
    }
    catch (Exception ex)
    {
        return Results.Problem($"An error occurred while deleting a product: {ex.Message}");
    }
}).WithName("DeleteProductById");

app.Run();
