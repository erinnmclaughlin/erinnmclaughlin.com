using System.Text.Json.Serialization;

namespace App.Integrations.Discord;

public sealed record DiscordUserData
{
    [JsonPropertyName("discord_user")]
    public required DiscordUser DiscordUser { get; init; }
    
    [JsonPropertyName("activities")]
    public DiscordActivity[] Activities { get; init; } = [];
    
    [JsonPropertyName("spotify")]
    public CurrentSpotifyTrack? CurrentSpotifyTrack { get; init; }
}