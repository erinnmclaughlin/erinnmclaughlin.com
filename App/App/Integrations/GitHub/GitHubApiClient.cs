using App.Integrations.GitHub.Models;
using Microsoft.Extensions.Options;

namespace App.Integrations.GitHub;

internal sealed class GitHubApiClient : IGitHubApiClient
{
    private readonly HttpClient _httpClient;
    private readonly GitHubOptions _options;

    public GitHubApiClient(HttpClient httpClient, IOptions<GitHubOptions> options)
    {
        _httpClient = httpClient;
        _options = options.Value;
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

    /// <summary>
    /// Lists the authenticated user's recent activities.
    /// </summary>
    public async Task<GitHubUserEvent[]> ListUserActivities(CancellationToken cancellationToken)
    {
        return await _httpClient.GetFromJsonAsync<GitHubUserEvent[]>($"users/{_options.Username}/events", cancellationToken) ?? [];
    }

    /// <summary>
    /// Gets a repository that is owned by the authenticated user.
    /// </summary>
    public async Task<GetRepositoryResponse?> GetRepository(string repositoryName, CancellationToken cancellationToken)
    {
        var response = await _httpClient.GetAsync($"repos/{_options.Username}/{repositoryName}", cancellationToken);

        if (response.IsSuccessStatusCode)
            return await response.Content.ReadFromJsonAsync<GetRepositoryResponse>(cancellationToken);

        return null;
    }
}