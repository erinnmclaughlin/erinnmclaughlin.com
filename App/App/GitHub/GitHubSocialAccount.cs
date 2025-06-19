using System.Text.Json.Serialization;

namespace App.GitHub;

public sealed record GitHubSocialAccount
{
    [JsonPropertyName("provider")]
    public required string Provider { get; init; }
    
    [JsonPropertyName("url")]
    public required string Url { get; init; }
}