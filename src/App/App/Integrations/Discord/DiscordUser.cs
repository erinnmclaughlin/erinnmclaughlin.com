using System.Text.Json.Serialization;

namespace App.Integrations.Discord;

public sealed record DiscordUser
{
    [JsonPropertyName("discriminator")]
    public string? Discriminator { get; init; }
    
    [JsonPropertyName("global_name")]
    public string? GlobalName { get; init; }
    
    [JsonPropertyName("username")]
    public required string Username { get; init; }
}