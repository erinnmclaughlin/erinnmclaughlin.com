namespace App.Components.Pages.Home.Components;

public sealed record RecentBlogPost
{
    public required string Slug { get; init; }
    public required string Title { get; init; }
    public required string ContentPreview { get; init; }
}