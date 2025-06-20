namespace App.Features;

public sealed record FeaturedProject
{
    public required string DisplayName { get; init; }
    public required string? Description { get; init; }
    public required string? PackageUrl { get; init; }
    public required string? RepositoryUrl { get; init; }
    public required string? WebsiteUrl { get; init; }
    public required int? StarCount { get; init; }
    public required int? ForkCount { get; init; }
    public required int? DownloadCount { get; init; }
    public string? WebsiteUrlTitle { get; init; }
}