namespace App.Integrations.Steam;

public interface ISteamApiClient
{
    Task<RecentlyPlayedGame[]> GetRecentlyPlayedGames(CancellationToken cancellationToken = default);
}