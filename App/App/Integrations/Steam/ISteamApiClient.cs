namespace App.Integrations.Steam;

public interface ISteamApiClient
{
    Task<GetRecentlyPlayedGamesResponse> GetRecentlyPlayedGames(CancellationToken cancellationToken = default);
}