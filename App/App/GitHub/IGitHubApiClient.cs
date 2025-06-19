namespace App.GitHub;

public interface IGitHubApiClient
{
    Task<GitHubSocialAccount[]> ListSocialAccounts(CancellationToken cancellationToken = default);
}

internal sealed class GitHubApiClient : IGitHubApiClient
{
    private readonly HttpClient _httpClient;

    public GitHubApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<GitHubSocialAccount[]> ListSocialAccounts(CancellationToken cancellationToken)
    {
        return await _httpClient.GetFromJsonAsync<GitHubSocialAccount[]>("user/social_accounts", cancellationToken) ?? [];
    }
}