namespace App.Features;

public interface IFeaturedProjectsProvider
{
    Task<FeaturedProjectDefinition[]> GetFeaturedProjectsAsync(CancellationToken cancellationToken = default);
    Task<FeaturedProject> GetFeaturedProjectDetailsAsync(FeaturedProjectDefinition projectDefinition, CancellationToken cancellationToken = default);
}