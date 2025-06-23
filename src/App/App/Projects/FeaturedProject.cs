namespace App.Projects;

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
    public required IReadOnlyDictionary<string, int> Languages { get; init; }

    public double GetLanguagePercentage(string language)
    {
        if (!Languages.TryGetValue(language, out var languageBytes))
            return 0;
        
        return languageBytes / (double)Languages.Values.Sum();
    }
}