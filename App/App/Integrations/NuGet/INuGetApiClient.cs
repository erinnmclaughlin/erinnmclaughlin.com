using System.Text.Json;

namespace App.Integrations.NuGet;

public interface INuGetApiClient
{
    Task<NugetPackageInfo?> GetPackageDetail(string packageId, CancellationToken cancellationToken = default);
}

public sealed class NuGetApiClient : INuGetApiClient
{
    public async Task<NugetPackageInfo?> GetPackageDetail(string packageId, CancellationToken cancellationToken = default)
    {
        using var httpClient = new HttpClient();
        var json = await httpClient.GetFromJsonAsync<JsonElement>("https://api.nuget.org/v3/index.json", cancellationToken);
        var resource = json.GetProperty("resources").EnumerateArray().AsEnumerable().First(x => x.GetProperty("@type").GetString() == "SearchQueryService");
        var url = resource.GetProperty("@id").GetString() + $"?q=packageid:{packageId}";
        var info = await httpClient.GetFromJsonAsync<NugetPackageQueryResponse<NugetPackageInfo>>(url, cancellationToken);
        return info?.Data.FirstOrDefault();
    }
}