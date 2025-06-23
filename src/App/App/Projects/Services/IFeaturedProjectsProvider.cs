namespace App.Projects.Services;

public interface IFeaturedProjectsProvider
{
    FeaturedProjectDefinition[] GetFeaturedProjects();
    Task<FeaturedProject> GetFeaturedProjectDetailsAsync(FeaturedProjectDefinition projectDefinition, CancellationToken cancellationToken = default);
}