namespace App.Steam;

public sealed record SteamApiResponse<T>
{
    public required T Response { get; init; }
}