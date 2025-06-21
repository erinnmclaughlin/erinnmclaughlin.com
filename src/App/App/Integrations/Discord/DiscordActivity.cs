using System.Text.Json.Serialization;

namespace App.Integrations.Discord;

public sealed record DiscordActivity
{
    [JsonPropertyName("name")]
    public required string Name { get; init; }
    
    [JsonPropertyName("type")]
    public required DiscordActivityType Type { get; init; }
    
    [JsonPropertyName("details")]
    public string? Details { get; init; }
    
    [JsonPropertyName("state")]
    public string? State { get; init; }
    
    [JsonPropertyName("assets")]
    public Dictionary<string, string> Assets { get; init; } = [];
}