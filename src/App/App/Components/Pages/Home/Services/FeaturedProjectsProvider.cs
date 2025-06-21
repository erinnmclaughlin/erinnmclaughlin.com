using System.Text.Json;
using App.Integrations.GitHub;
using App.Integrations.NuGet;
using Microsoft.Extensions.FileProviders;

namespace App.Components.Pages.Home.Services;

public sealed class FeaturedProjectsProvider : IFeaturedProjectsProvider
{
    private readonly IFileProvider _fileProvider;
    private readonly IGitHubApiClient _githubApi;
    private readonly INuGetApiClient _nugetApi;

    public FeaturedProjectsProvider(IWebHostEnvironment env, IGitHubApiClient githubApi, INuGetApiClient nugetApi)
    {
        _fileProvider = env.WebRootFileProvider;
        _githubApi = githubApi;
        _nugetApi = nugetApi;
    }
    
    public FeaturedProjectDefinition[] GetFeaturedProjects()
    {
        var fileInfo = _fileProvider.GetFileInfo("featured_projects.json");
        
        if (!fileInfo.Exists)
        {
            return [];
        }

        using var stream = fileInfo.CreateReadStream();
        return JsonSerializer.Deserialize<FeaturedProjectDefinition[]>(stream) ?? [];
    }

    public async Task<FeaturedProject> GetFeaturedProjectDetailsAsync(FeaturedProjectDefinition projectDefinition, CancellationToken cancellationToken)
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