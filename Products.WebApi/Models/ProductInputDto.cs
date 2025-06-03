using System.Text.Json.Serialization;

namespace Products.WebApi.Models;

public class ProductInputDto
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("category")]
    public string Category { get; set; }
}
