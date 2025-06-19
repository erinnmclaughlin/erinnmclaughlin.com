using App.Integrations.GitHub.Models;

namespace App.Integrations.GitHub;

public interface IGitHubApiClient
{
    Task<GitHubGist[]> ListGists(CancellationToken cancellationToken = default);
    Task<GitHubSocialAccount[]> ListSocialAccounts(CancellationToken cancellationToken = default);
    Task<GitHubUserEvent[]> ListUserActivities(string username, CancellationToken cancellationToken = default);
}