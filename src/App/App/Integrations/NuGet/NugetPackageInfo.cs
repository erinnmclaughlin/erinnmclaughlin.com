using System.Text.Json.Serialization;

namespace App.Integrations.NuGet;

public sealed record NugetPackageInfo
{
    [JsonPropertyName("id")]
    public required string Id { get; init; }
    
    [JsonPropertyName("title")]
    public required string Title { get; init; }
    
    [JsonPropertyName("description")]
    public string Description { get; init; } = "";
    
    [JsonPropertyName("version")]
    public required string Version { get; init; }
    
    [JsonPropertyName("tags")]
    public string[] Tags { get; init; } = [];
    
    [JsonPropertyName("totalDownloads")]
    public int TotalDownloads { get; init; }
}

public sealed record NugetPackageQueryResponse<T>
{
    [JsonPropertyName("data")]
    public T[] Data { get; init; } = [];
}