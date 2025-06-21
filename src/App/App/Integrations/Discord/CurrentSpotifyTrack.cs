using System.Text.Json.Serialization;

namespace App.Integrations.Discord;

public sealed record CurrentSpotifyTrack
{
    [JsonPropertyName("album")]
    public string? Album { get; init; }
    
    [JsonPropertyName("album_art_url")]
    public string? AlbumImageUrl { get; init; }
    
    [JsonPropertyName("artist")]
    public required string Artist { get; init; }
    
    [JsonPropertyName("song")]
    public required string Song { get; init; }
    
    [JsonPropertyName("track_id")]
    public required string TrackId { get; init; }
}