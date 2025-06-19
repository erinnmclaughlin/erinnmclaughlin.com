namespace App.Steam;

public interface ISteamApiClient
{
    Task<GetRecentlyPlayedGamesResponse> GetRecentlyPlayedGames(CancellationToken cancellationToken = default);
}

public sealed class SteamApiClient : ISteamApiClient
{
    private readonly IConfiguration _configuration;
    private readonly HttpClient _httpClient;

    public SteamApiClient(IConfiguration configuration, HttpClient httpClient)
    {
        _configuration = configuration;
        _httpClient = httpClient;
    }

    public async Task<GetRecentlyPlayedGamesResponse> GetRecentlyPlayedGames(CancellationToken cancellationToken)
    {
        var apiKey = _configuration["Steam:ApiKey"];
        var steamId = _configuration["Steam:SteamId"];
        var url = $"IPlayerService/GetRecentlyPlayedGames/v0001/?key={apiKey}&steamid={steamId}&format=json";

        var response = await _httpClient.GetFromJsonAsync<SteamApiResponse<GetRecentlyPlayedGamesResponse>>(url, cancellationToken);

        return response?.Response ?? new GetRecentlyPlayedGamesResponse
        {
            TotalCount = 0,
            Games = []
        };
    }
}