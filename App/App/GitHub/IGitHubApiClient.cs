namespace App.GitHub;

public interface IGitHubApiClient
{
    Task<GitHubGist[]> ListGists(CancellationToken cancellationToken = default);
    Task<GitHubSocialAccount[]> ListSocialAccounts(CancellationToken cancellationToken = default);
}

internal sealed class GitHubApiClient : IGitHubApiClient
{
    private readonly HttpClient _httpClient;

    public GitHubApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<GitHubGist[]> ListGists(CancellationToken cancellationToken = default)
    {
        return await _httpClient.GetFromJsonAsync<GitHubGist[]>("gists", cancellationToken) ?? [];
    }
    
    public async Task<GitHubSocialAccount[]> ListSocialAccounts(CancellationToken cancellationToken)
    {
        return await _httpClient.GetFromJsonAsync<GitHubSocialAccount[]>("user/social_accounts", cancellationToken) ?? [];
    }
}