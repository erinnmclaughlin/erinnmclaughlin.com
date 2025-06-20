using App.Integrations.GitHub.Models;

namespace App.Integrations.GitHub;

public interface IGitHubApiClient
{
    /// <summary>
    /// Lists the authenticated user's gists.
    /// </summary>
    Task<GitHubGist[]> ListGists(CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Lists the authenticated user's social accounts.
    /// </summary>
    Task<GitHubSocialAccount[]> ListSocialAccounts(CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Lists the authenticated user's recent activities.
    /// </summary>
    Task<GitHubUserEvent[]> ListUserActivities(CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Gets a repository that is owned by the authenticated user.
    /// </summary>
    Task<GetRepositoryResponse?> GetRepository(string repositoryName, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Lists languages for the specified repository. The value shown for each language is the number of bytes of code written in that language.
    /// </summary>
    Task<Dictionary<string, int>> ListRepositoryLanguages(string repositoryName, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Gets the authenticated user's profile.
    /// </summary>
    Task<GitHubUser?> GetUserProfile(CancellationToken cancellationToken = default);
}