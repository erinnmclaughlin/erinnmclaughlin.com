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

    public async Task<GitHubGist[]> ListGists(CancellationToken cancellationToken)
    {
        return await _httpClient.GetFromJsonAsync<GitHubGist[]>("gists", cancellationToken) ?? [];
    }
    
    public async Task<GitHubSocialAccount[]> ListSocialAccounts(CancellationToken cancellationToken)
    {
        return await _httpClient.GetFromJsonAsync<GitHubSocialAccount[]>("user/social_accounts", cancellationToken) ?? [];
    }

    public async Task<GitHubUserEvent[]> ListUserActivities(CancellationToken cancellationToken)
    {
        return await _httpClient.GetFromJsonAsync<GitHubUserEvent[]>($"users/{_options.Username}/events", cancellationToken) ?? [];
    }

    public async Task<GetRepositoryResponse?> GetRepository(string repositoryName, CancellationToken cancellationToken)
    {
        var response = await _httpClient.GetAsync($"repos/{_options.Username}/{repositoryName}", cancellationToken);

        if (response.IsSuccessStatusCode)
            return await response.Content.ReadFromJsonAsync<GetRepositoryResponse>(cancellationToken);

        return null;
    }

    public async Task<GitHubUser?> GetUserProfile(CancellationToken cancellationToken)
    {
        return await _httpClient.GetFromJsonAsync<GitHubUser>("user", cancellationToken);
    }
}