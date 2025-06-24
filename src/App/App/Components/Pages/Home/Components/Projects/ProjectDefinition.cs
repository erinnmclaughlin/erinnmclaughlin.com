using System.Text.Json.Serialization;

namespace App.Components.Pages.Home.Components;

public sealed record ProjectDefinition
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