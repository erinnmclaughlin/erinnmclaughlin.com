namespace App.Integrations.Discord;

public sealed class DiscordApiClient
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<DiscordApiClient> _logger;

    public DiscordApiClient(HttpClient httpClient, ILogger<DiscordApiClient> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<DiscordUserData?> GetUserDetail(CancellationToken cancellationToken = default)
    {
        try
        {
            var response = await _httpClient.GetFromJsonAsync<DiscordApiResponse<DiscordUserData>>("users/699320279532568687", cancellationToken);
            return response?.Data;
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "Failed to get user detail from Discord API (Lanyard).");
            return null;
        }
    }
}