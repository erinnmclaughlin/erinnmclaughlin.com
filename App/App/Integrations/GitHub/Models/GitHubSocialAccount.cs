using System.Text.Json.Serialization;

namespace App.Integrations.GitHub.Models;

public sealed record GitHubSocialAccount
{
    [JsonPropertyName("provider")]
    public required string Provider { get; init; }
    
    [JsonPropertyName("url")]
    public required string Url { get; init; }
}