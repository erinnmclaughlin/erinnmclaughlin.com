using Dapper;
using Microsoft.FeatureManagement;
using Npgsql;

namespace App.Components.Pages.Home.Components;

public interface IAppBlogSectionViewModel
{
    Task<RecentBlogPost[]> GetRecentPostsAsync(int count = 3, CancellationToken cancellationToken = default);
}

public sealed class AppBlogSectionViewModel : IAppBlogSectionViewModel
{
    private readonly NpgsqlDataSource _dataSource;
    private readonly IFeatureManager _featureManager;
    private readonly ILogger<AppBlogSectionViewModel> _logger;

    public AppBlogSectionViewModel(NpgsqlDataSource dataSource, IFeatureManager featureManager, ILogger<AppBlogSectionViewModel> logger)
    {
        _dataSource = dataSource;
        _featureManager = featureManager;
        _logger = logger;
    }
    
    public async Task<RecentBlogPost[]> GetRecentPostsAsync(int count, CancellationToken cancellationToken = default)
    {
        if (!await _featureManager.IsEnabledAsync(FeatureFlags.Blog))
        {
            return [];
        }

        try
        {
            await using var connection = await _dataSource.OpenConnectionAsync(cancellationToken);
            var posts = await connection.QueryAsync<RecentBlogPost>(
                """
                SELECT slug, title, content_preview as "ContentPreview"
                FROM posts
                ORDER BY created_at DESC
                LIMIT @count;
                """,
                new
                {
                    count
                });
        
            return posts.ToArray();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to retrieve recent blog posts.");
            return [];
        }
    }
}
