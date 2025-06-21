using System.Text.Json.Serialization;

namespace App.Integrations.GitHub.Models;

public sealed record GitHubGist
{
    [JsonPropertyName("id")]
    public required string Id { get; init; }
    
    [JsonPropertyName("description")]
    public required string Description { get; init; }
    
    [JsonPropertyName("public")]
    public required bool Public { get; init; }
    
    [JsonPropertyName("html_url")]
    public required string Url { get; init; }
    
    [JsonPropertyName("created_at")]
    public required DateTimeOffset CreatedAt { get; init; }
    
    [JsonPropertyName("updated_at")]
    public required DateTimeOffset UpdatedAt { get; init; }
}
