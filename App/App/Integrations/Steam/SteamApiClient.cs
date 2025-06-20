using Microsoft.Extensions.Options;

namespace App.Integrations.Steam;

public sealed class SteamApiClient : ISteamApiClient
{
    private readonly HttpClient _httpClient;
    private readonly SteamOptions _options;

    public SteamApiClient(HttpClient httpClient, IOptions<SteamOptions> options)
    {
        _httpClient = httpClient;
        _options = options.Value;
    }

    public async Task<RecentlyPlayedGame[]> GetRecentlyPlayedGames(CancellationToken cancellationToken)
    {
        var url = $"IPlayerService/GetRecentlyPlayedGames/v0001/?key={_options.ApiKey}&steamid={_options.SteamId}&format=json";
        var response = await _httpClient.GetFromJsonAsync<SteamApiResponse<GetRecentlyPlayedGamesResponse>>(url, cancellationToken);
        return response?.Response.Games ?? [];
    }
}