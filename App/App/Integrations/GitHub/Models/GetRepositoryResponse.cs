using System.Text.Json.Serialization;

namespace App.Integrations.GitHub.Models;

public sealed record GetRepositoryResponse
{
    [JsonPropertyName("id")]
    public required int Id { get; init; }
    
    [JsonPropertyName("name")]
    public required string DisplayName { get; init; }
    
    [JsonPropertyName("full_name")]
    public required string FullName { get; init; }
    
    [JsonPropertyName("homepage")]
    public string? Homepage { get; init; }
    
    [JsonPropertyName("html_url")]
    public required string HtmlUrl { get; init; }
    
    [JsonPropertyName("description")]
    public required string? Description { get; init; }
    
    [JsonPropertyName("forks_count")]
    public required int ForksCount { get; init; }
    
    [JsonPropertyName("stargazers_count")]
    public required int StargazersCount { get; init; }
    
    [JsonPropertyName("open_issues_count")]
    public required int OpenIssuesCount { get; init; }
}