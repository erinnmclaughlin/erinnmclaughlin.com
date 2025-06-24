using System.Text.Json;
using Microsoft.Extensions.FileProviders;

namespace App.Components.Pages.Home.Components;

public interface IAppProjectsSectionViewModel
{
    ProjectDefinition[] GetFeaturedProjects();
}

public sealed class AppProjectsSectionViewModel : IAppProjectsSectionViewModel
{
    private readonly IFileProvider _fileProvider;

    public AppProjectsSectionViewModel(IWebHostEnvironment env)
    {
        _fileProvider = env.WebRootFileProvider;
    }
    
    public ProjectDefinition[] GetFeaturedProjects()
    {
        var fileInfo = _fileProvider.GetFileInfo("featured_projects.json");
        
        if (!fileInfo.Exists)
        {
            return [];
        }

        using var stream = fileInfo.CreateReadStream();
        return JsonSerializer.Deserialize<ProjectDefinition[]>(stream) ?? [];
    }
}