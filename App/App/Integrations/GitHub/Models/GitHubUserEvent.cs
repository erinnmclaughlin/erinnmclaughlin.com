using System.Text.Json;
using System.Text.Json.Serialization;

namespace App.Integrations.GitHub.Models;

public record GitHubUserEvent
{
    [JsonPropertyName("id")]
    public required string Id { get; init; }
    
    [JsonPropertyName("type")]
    public required string Type { get; init; }

    [JsonPropertyName("repo")]
    public RepositoryReference? Repository { get; init; }
    
    [JsonPropertyName("public")]
    public required bool Public { get; init; }
    
    [JsonPropertyName("payload")]
    public JsonElement? Payload { get; init; }
    
    [JsonPropertyName("created_at")]
    public DateTimeOffset CreatedAt { get; init; }
    
    public sealed record RepositoryReference
    {
        [JsonPropertyName("id")]
        public required int Id { get; init; }
        
        [JsonPropertyName("name")]
        public required string Name { get; init; }
        
        [JsonPropertyName("url")]
        public required string Url { get; init; }

        public string GetHtmlUrl()
        {
            return $"https://github.com/{Name}";
        }
    }
}
