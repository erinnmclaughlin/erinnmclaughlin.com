namespace App.Features;

public interface IFeaturedProjectsProvider
{
    Task<FeaturedProject[]> GetFeaturedProjectsAsync(CancellationToken cancellationToken = default);
}