using Dapper;
using Npgsql;

namespace App.Blog.Services;

public sealed class BlogRepository : IBlogRepository
{
    private readonly NpgsqlDataSource _dataSource;

    public BlogRepository(NpgsqlDataSource dataSource)
    {
        _dataSource = dataSource;
    }
    
    public async Task<RecentBlogPost[]> GetRecentPostsAsync(int count, CancellationToken cancellationToken = default)
    {
        await using var connection = await _dataSource.OpenConnectionAsync(cancellationToken);
        var posts = await connection.QueryAsync<RecentBlogPost>(
                """
                SELECT id, slug, title, content_preview as "ContentPreview", created_at as "CreatedAt"
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
}
