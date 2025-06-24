using App.Integrations.GitHub;
using App.Integrations.NuGet;

namespace App.Components.Pages.Home.Components;

public interface IAppProjectCardViewModel
{
    Task<FeaturedProject> GetProjectDetailsAsync(ProjectDefinition projectDefinition, CancellationToken cancellationToken = default);
}

public sealed class AppProjectCardViewModel : IAppProjectCardViewModel
{
    private readonly IGitHubApiClient _githubApi;
    private readonly INuGetApiClient _nugetApi;

    public AppProjectCardViewModel(IGitHubApiClient githubApi, INuGetApiClient nugetApi)
    {
        _githubApi = githubApi;
        _nugetApi = nugetApi;
    }
    
    public async Task<FeaturedProject> GetProjectDetailsAsync(ProjectDefinition projectDefinition, CancellationToken cancellationToken = default)
    {
        var (repoId, nugetId) = (projectDefinition.RepoId, projectDefinition.NuGetId);
        var repo = repoId is null ? null : await _githubApi.GetRepository(repoId, cancellationToken);
        var nuget = nugetId is null ? null : await _nugetApi.GetPackageDetail(nugetId, cancellationToken);
        
        return new FeaturedProject
        {
            DisplayName = projectDefinition.DisplayName,
            Description = repo?.Description ?? nuget?.Description,
            PackageUrl = nuget is null ? null : $"https://www.nuget.org/packages/{nuget.Id}",
            RepositoryUrl = repo?.HtmlUrl,
            WebsiteUrl = projectDefinition.WebsiteUrl,
            WebsiteUrlTitle = projectDefinition.WebsiteUrlTitle,
            StarCount = repo?.StargazersCount,
            ForkCount = repo?.ForksCount,
            DownloadCount = nuget?.TotalDownloads,
            Languages = repoId is null ? [] : await _githubApi.ListRepositoryLanguages(repoId, cancellationToken)
        };
    }
}