using App.Integrations.GitHub.Models;

namespace App.Integrations.GitHub;

internal sealed class GitHubApiClient : IGitHubApiClient
{
    private readonly HttpClient _httpClient;

    public GitHubApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    /// <summary>
    /// Lists the authenticated user's gists.
    /// </summary>
    public async Task<GitHubGist[]> ListGists(CancellationToken cancellationToken)
    {
        return await _httpClient.GetFromJsonAsync<GitHubGist[]>("gists", cancellationToken) ?? [];
    }
    
    /// <summary>
    /// Lists the authenticated user's social accounts.
    /// </summary>
    public async Task<GitHubSocialAccount[]> ListSocialAccounts(CancellationToken cancellationToken)
    {
        return await _httpClient.GetFromJsonAsync<GitHubSocialAccount[]>("user/social_accounts", cancellationToken) ?? [];
    }

    public async Task<GitHubUserEvent[]> ListUserActivities(string username, CancellationToken cancellationToken)
    {
        return await _httpClient.GetFromJsonAsync<GitHubUserEvent[]>($"users/{username}/events", cancellationToken) ?? [];
    }
}