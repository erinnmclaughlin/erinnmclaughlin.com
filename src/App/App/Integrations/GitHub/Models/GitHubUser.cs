using System.Text.Json.Serialization;

namespace App.Integrations.GitHub.Models;

public sealed record GitHubUser
{
    [JsonPropertyName("id")]
    public required int Id { get; init; }
    
    [JsonPropertyName("avatar_url")]
    public required string AvatarUrl { get; init; }
    
    [JsonPropertyName("url")]
    public required string Url { get; init; }
    
    [JsonPropertyName("name")]
    public required string Name { get; init; }
    
    [JsonPropertyName("company")]
    public string? Company { get; init; }
    
    [JsonPropertyName("blog")]
    public string? Blog { get; init; }
    
    [JsonPropertyName("location")]
    public string? Location { get; init; }
    
    [JsonPropertyName("bio")]
    public string? Bio { get; init; }
    
    [JsonPropertyName("twitter_username")]
    public string? TwitterUserName { get; init; }
    
    [JsonPropertyName("created_at")]
    public DateTimeOffset CreatedAt { get; init; }
    
    [JsonPropertyName("hirable")]
    public bool Hirable { get; init; }
    
    [JsonPropertyName("public_repos")]
    public int PublicRepoCount { get; init; }
    
    [JsonPropertyName("total_private_repos")]
    public int PrivateRepoCount { get; init; }
}