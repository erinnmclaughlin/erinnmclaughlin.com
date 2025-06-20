namespace App.Integrations.Discord;

public sealed record DiscordApiResponse<T>
{
    public bool Success { get; init; }
    public required T Data { get; init; }
}