namespace App.Blog;

public sealed record RecentBlogPost
{
    public required Guid Id { get; init; }
    public required string Slug { get; init; }
    public required string Title { get; init; }
    public required string ContentPreview { get; init; }
    public required DateTimeOffset CreatedAt { get; init; }
}