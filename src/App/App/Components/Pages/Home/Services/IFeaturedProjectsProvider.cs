namespace App.Components.Pages.Home.Services;

public interface IFeaturedProjectsProvider
{
    FeaturedProjectDefinition[] GetFeaturedProjects();
    Task<FeaturedProject> GetFeaturedProjectDetailsAsync(FeaturedProjectDefinition projectDefinition, CancellationToken cancellationToken = default);
}