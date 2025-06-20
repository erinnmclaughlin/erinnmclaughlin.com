using System.Text.Json;
using App.Integrations.GitHub;
using App.Integrations.NuGet;
using Microsoft.Extensions.FileProviders;

namespace App.Features;

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
    
    public async Task<FeaturedProject[]> GetFeaturedProjectsAsync(CancellationToken cancellationToken)
    {
        var fileInfo = _fileProvider.GetFileInfo("featured_projects.json");
        
        if (!fileInfo.Exists)
        {
            return [];
        }

        await using var stream = fileInfo.CreateReadStream();
        var projects = JsonSerializer.Deserialize<FeaturedProjectDefinition[]>(stream) ?? [];
        
        var featuredProjects = new List<FeaturedProject>();

        foreach (var project in projects)
        {
            var repo = project.RepoId is null ? null : await _githubApi.GetRepository(project.RepoId, cancellationToken);
            var nuget = project.NuGetId is null ? null : await _nugetApi.GetPackageDetail(project.NuGetId, cancellationToken);
            
            featuredProjects.Add(new FeaturedProject
            {
                DisplayName = project.DisplayName,
                Description = repo?.Description ?? nuget?.Description,
                PackageUrl = nuget is null ? null : $"https://www.nuget.org/packages/{nuget.Id}",
                RepositoryUrl = repo?.HtmlUrl,
                WebsiteUrl = project.WebsiteUrl,
                WebsiteUrlTitle = project.WebsiteUrlTitle,
                StarCount = repo?.StargazersCount,
                ForkCount = repo?.ForksCount,
                DownloadCount = nuget?.TotalDownloads
            });
        }
        
        return featuredProjects.ToArray();
    }
}