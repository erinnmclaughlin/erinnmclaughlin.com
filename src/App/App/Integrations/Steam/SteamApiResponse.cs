namespace App.Integrations.Steam;

public sealed record SteamApiResponse<T>
{
    public required T Response { get; init; }
}