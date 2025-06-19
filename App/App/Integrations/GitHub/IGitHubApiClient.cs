namespace App.Integrations.GitHub;

public interface IGitHubApiClient
{
    Task<GitHubGist[]> ListGists(CancellationToken cancellationToken = default);
    Task<GitHubSocialAccount[]> ListSocialAccounts(CancellationToken cancellationToken = default);
}