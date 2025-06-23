namespace App.Blog.Services;

public interface IBlogRepository
{
    Task<RecentBlogPost[]> GetRecentPostsAsync(int count = 3, CancellationToken cancellationToken = default);
}