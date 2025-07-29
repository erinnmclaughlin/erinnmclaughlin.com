using System.Text.Json;
using App.Integrations.GitHub;
using App.Integrations.GitHub.Models;
using App.Integrations.NuGet;
using Microsoft.Extensions.FileProviders;

namespace App.Components.Pages.Home.Services;

public sealed class FeaturedProjectsProvider : IFeaturedProjectsProvider
{
    private readonly IFileProvider _fileProvider;
    private readonly IGitHubApiClient _githubApi;
    private readonly ILogger<FeaturedProjectsProvider> _logger;
    private readonly INuGetApiClient _nugetApi;

    public FeaturedProjectsProvider(IWebHostEnvironment env, IGitHubApiClient githubApi, ILogger<FeaturedProjectsProvider> logger, INuGetApiClient nugetApi)
    {
        _fileProvider = env.WebRootFileProvider;
        _githubApi = githubApi;
        _nugetApi = nugetApi;
        _logger = logger;
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
        var repo = await GetRepositoryAsync(repoId, cancellationToken);
        var nuget = await GetPackageDetailsAsync(nugetId, cancellationToken);
        
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
            Languages = await GetRepositoryLanguagesAsync(repoId, cancellationToken)
        };
    }

    private async Task<GetRepositoryResponse?> GetRepositoryAsync(string? repoId, CancellationToken cancellationToken)
    {
        if (repoId is null)
            return null;

        try
        {
            return await _githubApi.GetRepository(repoId, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to fetch repository details for {RepoId}", repoId);
            return null;
        }
    }
    
    private async Task<NugetPackageInfo?> GetPackageDetailsAsync(string? nugetId, CancellationToken cancellationToken)
    {
        if (nugetId is null)
            return null;

        try
        {
            return await _nugetApi.GetPackageDetail(nugetId, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to get package details for {NugetId}", nugetId);
            return null;
        }
    }
    
    private async Task<Dictionary<string, int>> GetRepositoryLanguagesAsync(string? repoId, CancellationToken cancellationToken)
    {
        if (repoId is null)
            return [];

        try
        {
            return await _githubApi.ListRepositoryLanguages(repoId, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to fetch repository languages for {RepoId}", repoId);
            return [];
        }
    }
}