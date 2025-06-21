using System.Text.Json.Serialization;

namespace App.Features;

public sealed record FeaturedProjectDefinition
{
    [JsonPropertyName("displayName")]
    public required string DisplayName { get; init; }
    
    [JsonPropertyName("repoId")]
    public string? RepoId { get; init; }
    
    [JsonPropertyName("nugetId")]
    public string? NuGetId { get; init; }
    
    [JsonPropertyName("website")]
    public string? WebsiteUrl { get; init; }
    
    [JsonPropertyName("websiteUrlTitle")]
    public string? WebsiteUrlTitle { get; init; }
}