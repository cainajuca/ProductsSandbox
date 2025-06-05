using System.Text.Json.Serialization;

namespace Products.WebApi.Models;

public class Product
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("category")]
    public string Category { get; set; } = string.Empty;

    [JsonPropertyName("last-updated-at")]
    public DateTime LastUpdatedAt { get; set; }
}
