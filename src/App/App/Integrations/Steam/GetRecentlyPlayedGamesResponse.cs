using System.Text.Json.Serialization;

namespace App.Integrations.Steam;

public sealed class GetRecentlyPlayedGamesResponse
{
    [JsonPropertyName("total_count")]
    public int TotalCount { get; init; }

    [JsonPropertyName("games")]
    public RecentlyPlayedGame[] Games { get; init; } = [];
}

public sealed record RecentlyPlayedGame
{
    [JsonPropertyName("appid")]
    public required int AppId { get; init; }
        
    [JsonPropertyName("name")]
    public required string Name { get; init; }
        
    [JsonPropertyName("playtime_forever")]
    public required double Playtime { get; init; }
        
    [JsonPropertyName("img_icon_url")]
    public string? IconUrlHash { get; init; }
        
    [JsonPropertyName("img_logo_url")]
    public string? LogoUrlHash { get; init; }

    public string? GetIconUrl()
    {
        if (IconUrlHash is null)
            return null;

        return $"http://media.steampowered.com/steamcommunity/public/images/apps/{AppId}/{IconUrlHash}.jpg";
    }
        
    public string? GetLogoUrl()
    {
        if (LogoUrlHash is null)
            return null;

        return $"http://media.steampowered.com/steamcommunity/public/images/apps/{AppId}/{LogoUrlHash}.jpg";
    }
}